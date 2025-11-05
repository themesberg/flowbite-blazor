namespace DemoApp.Pages.Docs.ai;

using DemoApp.Services;
using Flowbite.Components.Chat;
using LlmTornado;
using LlmTornado.Code;
using LlmTornado.Models;
using Microsoft.AspNetCore.Components;
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

    private List<ChatAiMessage> Messages { get; set; } = new();
    private string? InputText { get; set; }
    private string? ApiKey { get; set; }
    private string? PreviousApiKey { get; set; }
    private string? ApiKeyValidationMessage { get; set; }
    private bool ShowApiKey { get; set; }
    private string ApiKeyInputType => ShowApiKey ? "text" : "password";
    private string? SelectedProviderKey { get; set; }
    private string? SelectedModelId { get; set; }
    private string? PreviousModelId { get; set; }
    private List<RetrievedModel>? AvailableModels { get; set; }
    private string? ModelSearchText { get; set; }
    private bool IsLoadingModels { get; set; }
    private string? ModelLoadError { get; set; }

    private List<RetrievedModel>? FilteredModels
    {
        get
        {
            if (AvailableModels == null || AvailableModels.Count == 0)
                return AvailableModels;

            if (string.IsNullOrWhiteSpace(ModelSearchText))
                return AvailableModels;

            var searchLower = ModelSearchText.ToLowerInvariant();
            return AvailableModels
                .Where(m =>
                    (m.Id?.ToLowerInvariant().Contains(searchLower) ?? false) ||
                    (m.Name?.ToLowerInvariant().Contains(searchLower) ?? false))
                .ToList();
        }
    }
    private bool WebSearchEnabled { get; set; }
    private bool MicrophoneEnabled { get; set; }
    private PromptSubmissionStatus SubmissionStatus { get; set; } = PromptSubmissionStatus.Idle;
    private bool IsBusy => SubmissionStatus is PromptSubmissionStatus.Submitting or PromptSubmissionStatus.Streaming;
    private string BusyLabel => SubmissionStatus == PromptSubmissionStatus.Submitting ? "Connecting..." : "Generating response...";

    private bool IsSubmitDisabled =>
        IsBusy ||
        string.IsNullOrWhiteSpace(InputText) ||
        string.IsNullOrWhiteSpace(SelectedProviderKey) ||
        string.IsNullOrWhiteSpace(ApiKey) ||
        string.IsNullOrWhiteSpace(SelectedModelId);

    // Model cache: key = "{providerKey}_{apiKeyHash}"
    private static readonly Dictionary<string, List<RetrievedModel>> ModelCache = new();

    private List<AiProviderConfig> Providers { get; } = new()
    {
        new AiProviderConfig("openrouter", "OpenRouter", "minimax/minimax-m2:free"),
        new AiProviderConfig("openai", "OpenAI", "gpt-4o"),
        new AiProviderConfig("anthropic", "Anthropic", "claude-sonnet-4-5-20250929"),
        new AiProviderConfig("google", "Google", "gemini-1.5-pro")
    };

    protected override void OnInitialized()
    {
        SelectedProviderKey = Providers[0].Key;

        // Messages.Add(new ChatAiMessage(
        //     Id: Guid.NewGuid(),
        //     Role: ChatMessageRole.Assistant,
        //     Text: "Welcome to the Flowbite AI Chat playground! Select a provider, paste your API key, and start chatting to see Flowbite chat components in action with LlmTornado.",
        //     Reasoning: "Greet the user and explain the purpose of this demo page.",
        //     DurationSeconds: 1,
        //     Sources: new List<ChatAiSource>
        //     {
        //         new("Flowbite Chatbot Docs", "https://flowbite.com/docs/components/chatbot/"),
        //         new("LlmTornado GitHub", "https://github.com/lofcz/LlmTornado")
        //     }));
    }

    private Task HandlePromptTextChanged(string value)
    {
        InputText = value;
        return Task.CompletedTask;
    }

    private Task ToggleWebSearchAsync()
    {
        WebSearchEnabled = !WebSearchEnabled;
        return Task.CompletedTask;
    }

    private Task ToggleMicrophoneAsync()
    {
        MicrophoneEnabled = !MicrophoneEnabled;
        return Task.CompletedTask;
    }

    private async Task HandleProviderChangedAsync(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            SelectedProviderKey = value;
            ApiKeyValidationMessage = null;
            
            // Clear current models when provider changes
            AvailableModels = null;
            SelectedModelId = null;
            ModelLoadError = null;
            ModelSearchText = null;
            
            // Try to load models if we have an API key
            if (!string.IsNullOrWhiteSpace(ApiKey))
            {
                await LoadModelsAsync();
            }
        }
    }

    private Task HandleModelSearchChanged(ChangeEventArgs e)
    {
        ModelSearchText = e.Value?.ToString();
        StateHasChanged();
        return Task.CompletedTask;
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
    }

    private async Task OnApiKeyChangedAsync(ChangeEventArgs e)
    {
        var newApiKey = e.Value?.ToString();
        ApiKey = newApiKey;
        
        // Check if API key has changed and is valid length
        if (!string.IsNullOrWhiteSpace(newApiKey) && 
            newApiKey != PreviousApiKey && 
            newApiKey.Length > 10 &&
            !string.IsNullOrWhiteSpace(SelectedProviderKey))
        {
            PreviousApiKey = newApiKey;
            await LoadModelsAsync();
        }
    }

    private async Task LoadModelsAsync()
    {
        if (string.IsNullOrWhiteSpace(SelectedProviderKey) || 
            string.IsNullOrWhiteSpace(ApiKey))
        {
            return;
        }

        // Check cache first
        var cacheKey = GetCacheKey(SelectedProviderKey, ApiKey);
        if (ModelCache.TryGetValue(cacheKey, out var cachedModels))
        {
            AvailableModels = cachedModels;
            SelectDefaultModel();
            return;
        }

        IsLoadingModels = true;
        ModelLoadError = null;
        await InvokeAsync(StateHasChanged);

        try
        {
            // Map provider key to enum
            var providerEnum = MapProviderKeyToEnum(SelectedProviderKey);
            
            if (providerEnum == LLmProviders.Unknown)
            {
                ModelLoadError = $"Unknown provider: {SelectedProviderKey}";
                return;
            }

            // Create API instance
            var api = new TornadoApi(providerEnum, ApiKey);
            
            // Fetch models
            var models = await api.Models.GetModels(providerEnum);
            
            if (models == null || models.Count == 0)
            {
                ModelLoadError = "No models available for this provider";
                // Fallback to default model
                var defaultModel = Providers
                    .FirstOrDefault(p => p.Key == SelectedProviderKey)
                    ?.DefaultModel;
                if (!string.IsNullOrEmpty(defaultModel))
                {
                    SelectedModelId = defaultModel;
                }
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
                .FirstOrDefault(p => p.Key == SelectedProviderKey)
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
            _ => LLmProviders.Unknown
        };
    }

    private void ToggleApiKeyVisibility()
    {
        ShowApiKey = !ShowApiKey;
    }

    private async Task HandleSubmitAsync(PromptInputMessage prompt)
    {
        // Console.WriteLine($"[ChatAiPage] HandleSubmitAsync called");
        
        var text = prompt.Text?.Trim() ?? string.Empty;
        var attachments = prompt.Files.Select(f => f.Name).ToList();
        
        // Console.WriteLine($"[ChatAiPage] Text: {text}");
        // Console.WriteLine($"[ChatAiPage] Attachments count: {attachments.Count}");

        if (string.IsNullOrWhiteSpace(text) && attachments.Count == 0)
        {
            // Console.WriteLine($"[ChatAiPage] Returning early: no text or attachments");
            return;
        }

        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            // Console.WriteLine($"[ChatAiPage] Returning early: no API key");
            ApiKeyValidationMessage = "Please provide an API key for the selected provider.";
            return;
        }

        if (string.IsNullOrWhiteSpace(SelectedProviderKey))
        {
            // Console.WriteLine($"[ChatAiPage] Returning early: no provider selected");
            ApiKeyValidationMessage = "Please select a provider.";
            return;
        }

        // Console.WriteLine($"[ChatAiPage] Validation passed, proceeding with API call");
        // Console.WriteLine($"[ChatAiPage] Provider: {SelectedProviderKey}");
        // Console.WriteLine($"[ChatAiPage] API Key length: {ApiKey?.Length ?? 0}");
        
        ApiKeyValidationMessage = null;

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
                    Content: m.Text))
                .ToList();
            // Console.WriteLine($"[ChatAiPage] History messages: {history.Count}");

            // Call the AI service
            // Console.WriteLine($"[ChatAiPage] Setting status to Streaming");
            SubmissionStatus = PromptSubmissionStatus.Streaming;
            await InvokeAsync(StateHasChanged);

            // Console.WriteLine($"[ChatAiPage] About to call AiChatService.SendMessageAsync");
            var response = await AiChatService.SendMessageAsync(
                providerKey: SelectedProviderKey!,
                apiKey: ApiKey!,
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

    private sealed record ChatAiSource(string Title, string Href);

    private sealed record ChatAiMessage(
        Guid Id,
        ChatMessageRole Role,
        string Text,
        bool IsStreaming = false,
        int? DurationSeconds = null,
        string? Reasoning = null,
        IReadOnlyList<ChatAiSource>? Sources = null,
        IReadOnlyList<string>? Attachments = null);
}
