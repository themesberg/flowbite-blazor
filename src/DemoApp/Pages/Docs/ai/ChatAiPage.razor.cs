namespace DemoApp.Pages.Docs.ai;

using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using DemoApp.Services;
using Flowbite.Components.Chat;
using LlmTornado;
using LlmTornado.Code;
using LlmTornado.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

/// <summary>
/// AI Chat playground page demonstrating Flowbite chat components with LlmTornado integration.
/// </summary>
public partial class ChatAiPage : ComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    private IAiChatService AiChatService { get; set; } = default!;

    private const string ProviderStorageKey = "flowbite.aiChat.providers";
    private const string SelectionStorageKey = "flowbite.aiChat.selection";
    private List<ChatAiMessage> Messages { get; set; } = new();
    private string? InputText { get; set; }
    private Dictionary<string, string> ProviderApiKeys { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    private string? SelectedProviderKey { get; set; }
    private string? SelectedModelId { get; set; }
    private string? PreviousModelId { get; set; }
    private List<RetrievedModel>? AvailableModels { get; set; }
    private bool IsLoadingModels { get; set; }
    private string? ModelLoadError { get; set; }
    private string? CurrentApiKey => SelectedProviderKey is null ? null : GetApiKeyForProvider(SelectedProviderKey);
    private bool HasConfiguredProviders => ProviderApiKeys.Any(kvp => !string.IsNullOrWhiteSpace(kvp.Value));
    private bool HasSelectedProviderConfigured =>
        !string.IsNullOrEmpty(SelectedProviderKey) &&
        !string.IsNullOrWhiteSpace(CurrentApiKey);
    private bool IsSettingsModalOpen { get; set; }
    private string? CredentialsValidationMessage { get; set; }
    private string? AttachmentValidationMessage { get; set; }
    private bool HasUnsupportedAttachments { get; set; }
    private bool _suppressSelectionPersistence;
    private const long AttachmentSizeLimitBytes = 5 * 1024 * 1024;
    private const int TextPreviewMaxCharacters = 160;
    private static readonly string[] PlainTextExtensions = [".txt", ".md", ".markdown", ".log"];

    private string ProviderModelLabel
    {
        get
        {
            if (string.IsNullOrEmpty(SelectedProviderKey))
            {
                return "Open settings to choose a provider and model";
            }

            var providerName = GetProviderDisplayName(SelectedProviderKey);

            if (string.IsNullOrEmpty(SelectedModelId))
            {
                return $"{providerName} / Select model";
            }

            var modelName = GetModelDisplayName(SelectedModelId);
            return $"{providerName} / {modelName}";
        }
    }

    private string ModelSelectPlaceholder =>
        IsLoadingModels ? "Loading models..." :
        !HasSelectedProviderConfigured ? "Add API key to load models" :
        (AvailableModels == null || AvailableModels.Count == 0) ? "No models available" :
        "Select a model";
    private bool WebSearchEnabled { get; set; }
    private PromptSubmissionStatus SubmissionStatus { get; set; } = PromptSubmissionStatus.Idle;
    private bool IsBusy => SubmissionStatus is PromptSubmissionStatus.Submitting or PromptSubmissionStatus.Streaming;
    private string BusyLabel => SubmissionStatus == PromptSubmissionStatus.Submitting ? "Connecting..." : "Generating response...";

    private bool IsSubmitDisabled =>
        IsBusy ||
        HasUnsupportedAttachments ||
        string.IsNullOrWhiteSpace(InputText) ||
        !HasSelectedProviderConfigured ||
        string.IsNullOrWhiteSpace(SelectedModelId) ||
        SelectedModelId == ModelSelectPlaceholder;
    private string? ProviderValidationMessage { get; set; }
    private string? ModelValidationMessage { get; set; }
    private bool IsProviderSelectionValid =>
        !string.IsNullOrWhiteSpace(SelectedProviderKey) &&
        IsProviderConfigured(SelectedProviderKey);
    private bool IsModelSelectionValid =>
        IsProviderSelectionValid &&
        !string.IsNullOrWhiteSpace(SelectedModelId);
    private bool IsSettingsFormValid => IsProviderSelectionValid && IsModelSelectionValid;
    private bool _settingsModalRequestQueued;

    // Model cache: key = "{providerKey}_{apiKeyHash}"
    private static readonly Dictionary<string, List<RetrievedModel>> ModelCache = new();

    private List<AiProviderConfig> Providers { get; } = new()
    {
        new AiProviderConfig("openrouter", "OpenRouter", ""),
        new AiProviderConfig("requesty", "Requesty", ""),
        new AiProviderConfig("openai", "OpenAI", ""),
        new AiProviderConfig("anthropic", "Anthropic", ""),
        new AiProviderConfig("google", "Google", "")
    };

    private Task HandlePromptTextChanged(string value)
    {
        InputText = value;
        return Task.CompletedTask;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        await LoadProviderCredentialsAsync();
        await LoadSelectionStateAsync();
    }

    private Task ToggleWebSearchAsync()
    {
        WebSearchEnabled = !WebSearchEnabled;
        UpdateSettingsValidation();
        return Task.CompletedTask;
    }

    private void UpdateSettingsValidation()
    {
        if (string.IsNullOrWhiteSpace(SelectedProviderKey))
        {
            ProviderValidationMessage = "Select a provider.";
        }
        else if (!IsProviderConfigured(SelectedProviderKey))
        {
            ProviderValidationMessage = $"Enter an API key for {GetProviderDisplayName(SelectedProviderKey)}.";
        }
        else
        {
            ProviderValidationMessage = null;
        }

        if (!IsProviderSelectionValid)
        {
            ModelValidationMessage = "Select and configure a provider before choosing a model.";
        }
        else if (string.IsNullOrWhiteSpace(SelectedModelId))
        {
            ModelValidationMessage = "Select a model.";
        }
        else
        {
            ModelValidationMessage = null;
        }

        var needsSettingsModal =
            !string.IsNullOrEmpty(CredentialsValidationMessage) ||
            !HasConfiguredProviders ||
            !HasSelectedProviderConfigured;

        if (needsSettingsModal && !IsSettingsModalOpen && !_settingsModalRequestQueued)
        {
            _settingsModalRequestQueued = true;
            _ = InvokeAsync(async () =>
            {
                await OpenSettingsModalAsync();
                _settingsModalRequestQueued = false;
            });
        }
        else if (!needsSettingsModal)
        {
            _settingsModalRequestQueued = false;
        }

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task HandleProviderChangedAsync(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            SelectedProviderKey = null;
            SelectedModelId = null;
            ModelLoadError = null;
            await PersistSelectionAsync();
            UpdateSettingsValidation();
            return;
        }

        SelectedProviderKey = value;
        CredentialsValidationMessage = null;
        
        // Clear current models when provider changes
        AvailableModels = null;
        SelectedModelId = null;
        ModelLoadError = null;
        
        if (IsProviderConfigured(value))
        {
            await LoadModelsAsync();
        }
        else
        {
            CredentialsValidationMessage = $"Add an API key for {GetProviderDisplayName(value)} in settings.";
        }

        await PersistSelectionAsync();
        UpdateSettingsValidation();
    }

    private async Task HandleModelChangedAsync(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        // Check if model is changing mid-conversation
        if (!string.IsNullOrEmpty(PreviousModelId) && 
            PreviousModelId != value && 
            Messages.Count > 0)
        {
            // Warn user that changing models will start a new conversation
            var confirmed = await JSRuntime.InvokeAsync<bool>(
                "confirm", 
                "Changing models will start a new conversation. Continue?");
            
            if (!confirmed)
            {
                // User cancelled, revert to previous model
                SelectedModelId = PreviousModelId;
                return;
            }
            
            // User confirmed, clear conversation
            Messages.Clear();
        }

        SelectedModelId = value;
        PreviousModelId = value;
        await PersistSelectionAsync();
        UpdateSettingsValidation();
    }

    private async Task OpenSettingsModalAsync()
    {
        IsSettingsModalOpen = true;
        UpdateSettingsValidation();

        if (!string.IsNullOrEmpty(SelectedProviderKey) &&
            IsProviderConfigured(SelectedProviderKey) &&
            (AvailableModels == null || AvailableModels.Count == 0) &&
            !IsLoadingModels)
        {
            await LoadModelsAsync();
        }
    }

    private void CloseSettingsModal()
    {
        IsSettingsModalOpen = false;
    }

    private async Task HandleModalProviderChangedAsync(string? value)
    {
        await HandleProviderChangedAsync(value);
    }

    private async Task HandleModalModelChangedAsync(string? value)
    {
        await HandleModelChangedAsync(value);
    }

    private async Task HandleProviderApiKeyChangedAsync(string providerKey, string? value)
    {
        if (string.IsNullOrWhiteSpace(providerKey))
        {
            return;
        }

        var sanitizedValue = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        var previousSelection = SelectedProviderKey;

        if (string.IsNullOrEmpty(sanitizedValue))
        {
            ProviderApiKeys.Remove(providerKey);
        }
        else
        {
            ProviderApiKeys[providerKey] = sanitizedValue;
        }

        await PersistProviderKeysAsync();

        EnsureDefaultProviderSelection();
        var activeProvider = SelectedProviderKey;

        if (activeProvider == providerKey)
        {
            AvailableModels = null;
            SelectedModelId = null;
            ModelLoadError = null;

            if (sanitizedValue != null)
            {
                await LoadModelsAsync();
            }
        }
        else if (previousSelection != activeProvider &&
                 !string.IsNullOrEmpty(activeProvider) &&
                 IsProviderConfigured(activeProvider))
        {
            AvailableModels = null;
            SelectedModelId = null;
            ModelLoadError = null;
            await LoadModelsAsync();
        }

        await PersistSelectionAsync();
        UpdateSettingsValidation();
        await InvokeAsync(StateHasChanged);
    }

    private async Task PersistProviderKeysAsync()
    {
        try
        {
            if (!HasConfiguredProviders)
            {
                await JSRuntime.InvokeVoidAsync("localStorage.removeItem", ProviderStorageKey);
                return;
            }

            var payload = JsonSerializer.Serialize(ProviderApiKeys);
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", ProviderStorageKey, payload);
        }
        catch
        {
            // Swallow storage errors to avoid breaking the UX.
        }
    }

    private async Task LoadProviderCredentialsAsync()
    {
        try
        {
            var stored = await JSRuntime.InvokeAsync<string?>("localStorage.getItem", ProviderStorageKey);
            if (!string.IsNullOrWhiteSpace(stored))
            {
                var parsed = JsonSerializer.Deserialize<Dictionary<string, string>>(stored);
                if (parsed != null)
                {
                    ProviderApiKeys = parsed
                        .Where(kvp => !string.IsNullOrWhiteSpace(kvp.Key) && !string.IsNullOrWhiteSpace(kvp.Value))
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value, StringComparer.OrdinalIgnoreCase);
                }
            }
        }
        catch
        {
            // Ignore storage read errors; the UI will fall back to manual entry.
        }

        EnsureDefaultProviderSelection();

        if (HasSelectedProviderConfigured)
        {
            await LoadModelsAsync();
        }

        await InvokeAsync(StateHasChanged);
        UpdateSettingsValidation();
    }

    private async Task LoadSelectionStateAsync()
    {
        string? stored = null;

        try
        {
            stored = await JSRuntime.InvokeAsync<string?>("localStorage.getItem", SelectionStorageKey);
        }
        catch
        {
            // Ignore storage read failures.
        }

        if (string.IsNullOrWhiteSpace(stored))
        {
            return;
        }

        SelectionState? selection = null;
        try
        {
            selection = JsonSerializer.Deserialize<SelectionState>(stored);
        }
        catch
        {
            return;
        }

        if (selection == null || string.IsNullOrWhiteSpace(selection.ProviderKey))
        {
            return;
        }

        _suppressSelectionPersistence = true;
        try
        {
            await HandleProviderChangedAsync(selection.ProviderKey);

            if (!string.IsNullOrWhiteSpace(selection.ModelId))
            {
                await HandleModelChangedAsync(selection.ModelId);
            }
        }
        finally
        {
            _suppressSelectionPersistence = false;
        }
        
        await InvokeAsync(StateHasChanged);
        UpdateSettingsValidation();
    }

    private async Task PersistSelectionAsync()
    {
        if (_suppressSelectionPersistence)
        {
            return;
        }

        try
        {
            if (string.IsNullOrWhiteSpace(SelectedProviderKey))
            {
                await JSRuntime.InvokeVoidAsync("localStorage.removeItem", SelectionStorageKey);
                return;
            }

            var payload = JsonSerializer.Serialize(new SelectionState(SelectedProviderKey, SelectedModelId));
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", SelectionStorageKey, payload);
        }
        catch
        {
            // Ignore persistence errors.
        }
    }

    private void EnsureDefaultProviderSelection()
    {
        if (!HasConfiguredProviders)
        {
            if (!string.IsNullOrEmpty(SelectedProviderKey))
            {
                SelectedProviderKey = null;
                AvailableModels = null;
                SelectedModelId = null;
                ModelLoadError = null;
            }
            UpdateSettingsValidation();
            return;
        }

        if (!string.IsNullOrEmpty(SelectedProviderKey) && IsProviderConfigured(SelectedProviderKey))
        {
            CredentialsValidationMessage = null;
            UpdateSettingsValidation();
            return;
        }

        var defaultProvider = Providers.FirstOrDefault(p => IsProviderConfigured(p.Key));
        SelectedProviderKey = defaultProvider?.Key;
        if (!string.IsNullOrEmpty(SelectedProviderKey))
        {
            CredentialsValidationMessage = null;
        }

        UpdateSettingsValidation();
    }

    private string? GetApiKeyForProvider(string providerKey)
    {
        if (string.IsNullOrWhiteSpace(providerKey))
        {
            return null;
        }

        if (ProviderApiKeys.TryGetValue(providerKey, out var value) && !string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        return null;
    }

    private bool IsProviderConfigured(string providerKey)
    {
        return !string.IsNullOrWhiteSpace(GetApiKeyForProvider(providerKey));
    }

    private string GetProviderDisplayName(string providerKey)
    {
        return Providers.FirstOrDefault(p => p.Key == providerKey)?.DisplayName ?? providerKey;
    }

    private string GetModelDisplayName(string? modelId)
    {
        if (string.IsNullOrWhiteSpace(modelId))
        {
            return "Unknown model";
        }

        var match = AvailableModels?.FirstOrDefault(m => string.Equals(m.Id, modelId, StringComparison.OrdinalIgnoreCase));
        if (match != null && !string.IsNullOrWhiteSpace(match.Name))
        {
            return match.Name;
        }

        return modelId;
    }

    private async Task LoadModelsAsync()
    {
        var providerKey = SelectedProviderKey;
        var apiKey = CurrentApiKey;

        if (string.IsNullOrWhiteSpace(providerKey) || 
            string.IsNullOrWhiteSpace(apiKey))
        {
            UpdateSettingsValidation();
            return;
        }

        if (!IsProviderConfigured(providerKey))
        {
            ModelLoadError = $"Add an API key for {GetProviderDisplayName(providerKey)} in settings.";
            await InvokeAsync(StateHasChanged);
            UpdateSettingsValidation();
            return;
        }

        // Check cache first
        var cacheKey = GetCacheKey(providerKey, apiKey);
        if (ModelCache.TryGetValue(cacheKey, out var cachedModels))
        {
            AvailableModels = cachedModels;
            SelectDefaultModel();
            UpdateSettingsValidation();
            return;
        }

        IsLoadingModels = true;
        ModelLoadError = null;
        await InvokeAsync(StateHasChanged);

        try
        {
            // Map provider key to enum
            var providerEnum = MapProviderKeyToEnum(providerKey);
            
            if (providerEnum == LLmProviders.Unknown)
            {
                ModelLoadError = $"Unknown provider: {providerKey}";
                UpdateSettingsValidation();
                return;
            }

            // Create API instance
            var api = new TornadoApi(providerEnum, apiKey);

            // Enable direct browser access for web-based providers (availble in v3.8.13)
            // api.DirectBrowserAccess = true;
            
            // Fetch models
            var models = await api.Models.GetModels(providerEnum);
            if (models != null)
            {
                models = models
                    .OrderBy(m => string.IsNullOrWhiteSpace(m.Name) ? m.Id : m.Name, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(m => m.Id, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }

            if (models == null || models.Count == 0)
            {
                ModelLoadError = "No models available for this provider";
                // Fallback to default model
                var defaultModel = Providers
                    .FirstOrDefault(p => p.Key == providerKey)
                    ?.DefaultModel;
                if (!string.IsNullOrEmpty(defaultModel))
                {
                    SelectedModelId = defaultModel;
                }
                UpdateSettingsValidation();
                return;
            }

            // Cache the models
            AvailableModels = models;
            ModelCache[cacheKey] = models;
            
            // Auto-select default or first model
            SelectDefaultModel();
        }
        catch (Exception ex)
        {
            ModelLoadError = $"Failed to load models: {ex.Message}";
            
            // Fallback to hardcoded default
            var defaultModel = Providers
                .FirstOrDefault(p => p.Key == providerKey)
                ?.DefaultModel;
            if (!string.IsNullOrEmpty(defaultModel))
            {
                SelectedModelId = defaultModel;
            }
        }
        finally
        {
            IsLoadingModels = false;
            await InvokeAsync(StateHasChanged);
            UpdateSettingsValidation();
        }
    }

    private void SelectDefaultModel()
    {
        if (AvailableModels == null || AvailableModels.Count == 0)
        {
            return;
        }

        // Try to find the configured default model
        var defaultModel = Providers
            .FirstOrDefault(p => p.Key == SelectedProviderKey)
            ?.DefaultModel;

        if (!string.IsNullOrEmpty(defaultModel))
        {
            var matchingModel = AvailableModels
                .FirstOrDefault(m => m.Id == defaultModel);
            if (matchingModel != null)
            {
                SelectedModelId = matchingModel.Id;
                PreviousModelId = matchingModel.Id;
                return;
            }
        }

        // Fall back to first model
        SelectedModelId = AvailableModels[0].Id;
        PreviousModelId = AvailableModels[0].Id;
    }

    private static string GetCacheKey(string providerKey, string apiKey)
    {
        // Create a simple hash of the API key for cache key
        var hash = apiKey.GetHashCode();
        return $"{providerKey}_{hash}";
    }

    private static LLmProviders MapProviderKeyToEnum(string providerKey)
    {
        return providerKey.ToLowerInvariant() switch
        {
            "openai" => LLmProviders.OpenAi,
            "anthropic" => LLmProviders.Anthropic,
            "google" => LLmProviders.Google,
            "openrouter" => LLmProviders.OpenRouter,
            "groq" => LLmProviders.Groq,
            "deepseek" => LLmProviders.DeepSeek,
            "mistral" => LLmProviders.Mistral,
            "xai" => LLmProviders.XAi,
            "perplexity" => LLmProviders.Perplexity,
            "cohere" => LLmProviders.Cohere,
            "requesty" => LLmProviders.Requesty,
            _ => LLmProviders.Unknown
        };
    }

    private async Task HandleSubmitAsync(PromptInputMessage prompt)
    {
        var text = prompt.Text?.Trim() ?? string.Empty;

        if (HasUnsupportedAttachments)
        {
            AttachmentValidationMessage = "PDF attachments are not supported. Remove the file to continue.";
            await InvokeAsync(StateHasChanged);
            return;
        }

        IReadOnlyList<AiChatAttachment> attachments;
        try
        {
            attachments = await CreateAttachmentPayloadsAsync(prompt.Files);
        }
        catch (InvalidOperationException ex)
        {
            Messages.Add(new ChatAiMessage(
                Id: Guid.NewGuid(),
                Role: ChatMessageRole.Assistant,
                Text: $"Attachment error: {ex.Message}"));
            await InvokeAsync(StateHasChanged);
            return;
        }

        if (attachments.Count > 0)
        {
            Console.WriteLine($"[ChatAiPage] Prepared {attachments.Count} attachment(s): {string.Join(", ", attachments.Select(a => a.FileName))}");
        }

        if (string.IsNullOrWhiteSpace(text) && attachments.Count == 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(SelectedProviderKey))
        {
            // Console.WriteLine($"[ChatAiPage] Returning early: no provider selected");
            CredentialsValidationMessage = "Please select a provider.";
            return;
        }

        if (!HasSelectedProviderConfigured)
        {
            CredentialsValidationMessage = $"Add an API key for {GetProviderDisplayName(SelectedProviderKey)} in settings.";
            return;
        }

        // Console.WriteLine($"[ChatAiPage] Validation passed, proceeding with API call");
        // Console.WriteLine($"[ChatAiPage] Provider: {SelectedProviderKey}");
        // Console.WriteLine($"[ChatAiPage] API Key length: {CurrentApiKey?.Length ?? 0}");
        
        CredentialsValidationMessage = null;

        var userMessage = new ChatAiMessage(
            Id: Guid.NewGuid(),
            Role: ChatMessageRole.User,
            Text: string.IsNullOrWhiteSpace(text) ? "Sent files for review." : text,
            Attachments: attachments);

        Messages.Add(userMessage);
        InputText = string.Empty;
        SubmissionStatus = PromptSubmissionStatus.Submitting;
        await InvokeAsync(StateHasChanged);

        try
        {
            // Console.WriteLine($"[ChatAiPage] Getting selected model");
            // Use the selected model ID
            var modelName = SelectedModelId ?? "gpt-4o";
            // Console.WriteLine($"[ChatAiPage] Model name: {modelName}");

            // Console.WriteLine($"[ChatAiPage] Converting message history");
            // Convert message history to service format
            var history = Messages
                .Where(m => m.Role != ChatMessageRole.System)
                .Select(m => new Services.AiChatMessage(
                    Role: m.Role.ToString(),
                    Content: m.Text,
                    Attachments: m.Attachments))
                .ToList();
            // Console.WriteLine($"[ChatAiPage] History messages: {history.Count}");

            // Call the AI service
            // Console.WriteLine($"[ChatAiPage] Setting status to Streaming");
            SubmissionStatus = PromptSubmissionStatus.Streaming;
            await InvokeAsync(StateHasChanged);

            // Console.WriteLine($"[ChatAiPage] About to call AiChatService.SendMessageAsync");
            var response = await AiChatService.SendMessageAsync(
                providerKey: SelectedProviderKey!,
                apiKey: CurrentApiKey!,
                modelName: modelName,
                conversationHistory: history,
                enableWebSearch: WebSearchEnabled);
            // Console.WriteLine($"[ChatAiPage] Service call returned");
            // Console.WriteLine($"[ChatAiPage] Response.IsSuccess: {response.IsSuccess}");

            if (response.IsSuccess)
            {
                // Console.WriteLine($"[ChatAiPage] Success! Content length: {response.Content?.Length ?? 0}");
                var assistantMessage = new ChatAiMessage(
                    Id: Guid.NewGuid(),
                    Role: ChatMessageRole.Assistant,
                    Text: response.Content ?? string.Empty,
                    Reasoning: response.Reasoning,
                    DurationSeconds: response.DurationMilliseconds.HasValue 
                        ? (int)(response.DurationMilliseconds.Value / 1000) 
                        : null,
                    Sources: response.Sources?.Select(s => 
                        new ChatAiSource(s.Title, s.Url)).ToList());

                Messages.Add(assistantMessage);
            }
            else
            {
                var errorMessage = new ChatAiMessage(
                    Id: Guid.NewGuid(),
                    Role: ChatMessageRole.Assistant,
                    Text: $"Error: {response.ErrorMessage}");

                Messages.Add(errorMessage);
            }
        }
        catch (Exception ex)
        {
            // Console.WriteLine($"[ChatAiPage] EXCEPTION in HandleSubmitAsync!");
            // Console.WriteLine($"[ChatAiPage] Exception Type: {ex.GetType().FullName}");
            // Console.WriteLine($"[ChatAiPage] Exception Message: {ex.Message}");
            // Console.WriteLine($"[ChatAiPage] Stack Trace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                // Console.WriteLine($"[ChatAiPage] Inner Exception Type: {ex.InnerException.GetType().FullName}");
                // Console.WriteLine($"[ChatAiPage] Inner Exception Message: {ex.InnerException.Message}");
            }

            var errorMessage = new ChatAiMessage(
                Id: Guid.NewGuid(),
                Role: ChatMessageRole.Assistant,
                Text: $"Error: {ex.Message}. Please check your API key and provider configuration.");

            Messages.Add(errorMessage);
        }
        finally
        {
            SubmissionStatus = PromptSubmissionStatus.Idle;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task<IReadOnlyList<AiChatAttachment>> CreateAttachmentPayloadsAsync(IReadOnlyList<IBrowserFile> files)
    {
        if (files is null || files.Count == 0)
        {
            return Array.Empty<AiChatAttachment>();
        }

        var payloads = new List<AiChatAttachment>(files.Count);

        foreach (var file in files)
        {
            if (file.Size > AttachmentSizeLimitBytes)
            {
                throw new InvalidOperationException(
                    $"\"{file.Name}\" ({FormatFileSize(file.Size)}) exceeds the {FormatFileSize(AttachmentSizeLimitBytes)} attachment limit.");
            }

            var contentType = string.IsNullOrWhiteSpace(file.ContentType)
                ? "application/octet-stream"
                : file.ContentType;

            if (IsPdfAttachment(contentType, file.Name))
            {
                throw new InvalidOperationException("PDF attachments are not supported.");
            }

            var bytes = await ReadFileBytesAsync(file).ConfigureAwait(false);
            var base64Data = Convert.ToBase64String(bytes);
            var isPlainText = IsPlainTextAttachment(contentType, file.Name);
            string? textContent = null;
            string? preview = null;

            if (isPlainText)
            {
                textContent = DecodeText(bytes);
                if (!string.IsNullOrWhiteSpace(textContent))
                {
                    preview = CreateTextPreview(textContent);
                }
            }

            payloads.Add(new AiChatAttachment(
                FileName: file.Name,
                ContentType: contentType,
                Size: file.Size,
                Base64Data: base64Data,
                IsImage: contentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase),
                IsPlainText: isPlainText,
                TextContent: textContent,
                TextPreview: preview));
        }

        return payloads;
    }

    private Task HandleAttachmentsChangedAsync(IReadOnlyList<PromptAttachment> attachments)
    {
        var hasPdf = attachments?.Any(att => IsPdfAttachment(att.ContentType, att.Name)) ?? false;
        HasUnsupportedAttachments = hasPdf;
        AttachmentValidationMessage = hasPdf
            ? "PDF attachments are not supported yet. Remove the file to continue."
            : null;
        return InvokeAsync(StateHasChanged);
    }

    private static async Task<byte[]> ReadFileBytesAsync(IBrowserFile file, CancellationToken cancellationToken = default)
    {
        await using var stream = file.OpenReadStream(AttachmentSizeLimitBytes, cancellationToken);
        using var buffer = new MemoryStream();
        await stream.CopyToAsync(buffer, cancellationToken).ConfigureAwait(false);
        return buffer.ToArray();
    }

    private static string? GetAttachmentPreviewSource(AiChatAttachment attachment)
    {
        if (!attachment.IsImage)
        {
            return null;
        }

        var contentType = string.IsNullOrWhiteSpace(attachment.ContentType)
            ? "image/*"
            : attachment.ContentType;

        return $"data:{contentType};base64,{attachment.Base64Data}";
    }

    private static bool IsPdfAttachment(string? contentType, string fileName)
    {
        if (!string.IsNullOrWhiteSpace(contentType) &&
            contentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        var extension = Path.GetExtension(fileName);
        return string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsPlainTextAttachment(string contentType, string fileName)
    {
        if (!string.IsNullOrWhiteSpace(contentType) &&
            contentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        var extension = Path.GetExtension(fileName);
        return !string.IsNullOrWhiteSpace(extension) &&
               PlainTextExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
    }

    private static string? DecodeText(byte[] bytes)
    {
        if (bytes.Length == 0)
        {
            return null;
        }

        try
        {
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return null;
        }
    }

    private static string? CreateTextPreview(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        var normalized = content.ReplaceLineEndings(" ").Trim();
        if (normalized.Length <= TextPreviewMaxCharacters)
        {
            return normalized;
        }

        return normalized[..TextPreviewMaxCharacters] + "…";
    }

    private static string FormatFileSize(long bytes)
    {
        var units = new[] { "B", "KB", "MB", "GB", "TB" };
        double size = bytes;
        var unitIndex = 0;

        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024d;
            unitIndex++;
        }

        return unitIndex == 0
            ? $"{bytes} {units[unitIndex]}"
            : $"{size:0.##} {units[unitIndex]}";
    }

    private async Task CopyAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        try
        {
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch
        {
            // Clipboard may not be available in all contexts
        }
    }

    private sealed record AiProviderConfig(string Key, string DisplayName, string DefaultModel);

    private sealed record SelectionState(string? ProviderKey, string? ModelId);

    private sealed record ChatAiSource(string Title, string Href);

    private sealed record ChatAiMessage(
        Guid Id,
        ChatMessageRole Role,
        string Text,
        bool IsStreaming = false,
        int? DurationSeconds = null,
        string? Reasoning = null,
        IReadOnlyList<ChatAiSource>? Sources = null,
        IReadOnlyList<AiChatAttachment>? Attachments = null);
}
