namespace DemoApp.Services;

using System.Diagnostics;
using LlmTornado;
using LlmTornado.Chat;
using LlmTornado.Chat.Models;
using LlmTornado.Code;

/// <summary>
/// Implementation of AI chat service using LlmTornado.
/// </summary>
public class AiChatService : IAiChatService
{
    /// <inheritdoc />
    public async Task<AiChatResponse> SendMessageAsync(
        string providerKey,
        string apiKey,
        string modelName,
        string userMessage,
        IReadOnlyList<AiChatMessage> conversationHistory,
        bool enableWebSearch = false,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[AiChatService] SendMessageAsync called");
        Console.WriteLine($"[AiChatService] Provider: {providerKey}");
        Console.WriteLine($"[AiChatService] Model: {modelName}");
        Console.WriteLine($"[AiChatService] User message length: {userMessage?.Length ?? 0}");
        Console.WriteLine($"[AiChatService] Conversation history count: {conversationHistory?.Count ?? 0}");
        Console.WriteLine($"[AiChatService] API Key present: {!string.IsNullOrEmpty(apiKey)}");
        Console.WriteLine($"[AiChatService] API Key length: {apiKey?.Length ?? 0}");

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "API key is required.");
        }

        if (string.IsNullOrEmpty(userMessage))
        {
            throw new ArgumentNullException(nameof(userMessage), "User message is required.");
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            Console.WriteLine($"[AiChatService] Mapping provider key: {providerKey}");
            
            // Map provider key to LLmProviders enum
            var provider = MapProviderKey(providerKey);
            Console.WriteLine($"[AiChatService] Mapped to provider: {provider}");
            
            if (provider == LLmProviders.Unknown)
            {
                Console.WriteLine($"[AiChatService] ERROR: Unknown provider: {providerKey}");
                return new AiChatResponse(
                    Content: string.Empty,
                    IsSuccess: false,
                    ErrorMessage: $"Unknown provider: {providerKey}. Supported providers: openai, anthropic, google, openrouter.");
            }

            Console.WriteLine($"[AiChatService] Initializing TornadoApi with provider: {provider}");
            
            // Initialize TornadoApi with the provider and API key
            var api = new TornadoApi(provider, apiKey);
            Console.WriteLine($"[AiChatService] TornadoApi initialized successfully");

            Console.WriteLine($"[AiChatService] Creating ChatModel with model: {modelName}, provider: {provider}");
            // Create chat model
            var model = new ChatModel(modelName, provider);
            Console.WriteLine($"[AiChatService] ChatModel created successfully");

            // Build chat request with conversation history
            var messages = new List<ChatMessage>();

            // Add system message
            messages.Add(new ChatMessage(ChatMessageRoles.System, "You are a helpful AI assistant."));
            Console.WriteLine($"[AiChatService] Added system message");

            // Add conversation history
            Console.WriteLine($"[AiChatService] Adding {conversationHistory?.Count ?? 0} history messages");
            if (conversationHistory != null)
            {
                foreach (var msg in conversationHistory)
                {
                    var role = msg.Role.ToLowerInvariant() switch
                    {
                        "user" => ChatMessageRoles.User,
                        "assistant" => ChatMessageRoles.Assistant,
                        "system" => ChatMessageRoles.System,
                        _ => ChatMessageRoles.User
                    };
                    messages.Add(new ChatMessage(role, msg.Content ?? string.Empty));
                }
            }

            // Add current user message
            messages.Add(new ChatMessage(ChatMessageRoles.User, userMessage));
            Console.WriteLine($"[AiChatService] Total messages in request: {messages.Count}");

            // Create chat request
            var chatRequest = new ChatRequest
            {
                Model = model,
                Messages = messages,
                Temperature = 0.7
            };
            Console.WriteLine($"[AiChatService] ChatRequest created, about to call API...");

            // Execute the request
            Console.WriteLine($"[AiChatService] Calling api.Chat.CreateChatCompletion...");
            var result = await api.Chat.CreateChatCompletion(chatRequest);
            Console.WriteLine($"[AiChatService] API call completed");

            stopwatch.Stop();

            if (result?.Choices == null || result.Choices.Count == 0)
            {
                return new AiChatResponse(
                    Content: string.Empty,
                    IsSuccess: false,
                    ErrorMessage: "No response received from the AI provider.",
                    DurationMilliseconds: stopwatch.ElapsedMilliseconds);
            }

            var choice = result.Choices[0];
            var content = choice.Message?.Content ?? string.Empty;

            // Extract reasoning if available (for reasoning models)
            string? reasoning = null;
            if (choice.Message?.Reasoning != null)
            {
                reasoning = choice.Message.Reasoning;
            }

            // Note: Web search sources/citations would be extracted here if available in the response
            // The exact property name would depend on the provider's response format

            return new AiChatResponse(
                Content: content,
                Reasoning: reasoning,
                Sources: null,
                DurationMilliseconds: stopwatch.ElapsedMilliseconds,
                IsSuccess: true);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Console.WriteLine($"[AiChatService] EXCEPTION CAUGHT!");
            Console.WriteLine($"[AiChatService] Exception Type: {ex.GetType().FullName}");
            Console.WriteLine($"[AiChatService] Exception Message: {ex.Message}");
            Console.WriteLine($"[AiChatService] Stack Trace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                Console.WriteLine($"[AiChatService] Inner Exception Type: {ex.InnerException.GetType().FullName}");
                Console.WriteLine($"[AiChatService] Inner Exception Message: {ex.InnerException.Message}");
                Console.WriteLine($"[AiChatService] Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
            }

            var errorMessage = new System.Text.StringBuilder();
            errorMessage.AppendLine($"Error Type: {ex.GetType().Name}");
            errorMessage.AppendLine($"Message: {ex.Message}");
            
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine($"Inner Error: {ex.InnerException.Message}");
            }
            
            // Check for common CORS/network issues
            if (ex.Message.Contains("fetch", StringComparison.OrdinalIgnoreCase) || 
                ex.Message.Contains("CORS", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("NetworkError", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage.AppendLine();
                errorMessage.AppendLine("⚠️ This appears to be a CORS/network issue.");
                errorMessage.AppendLine("Blazor WebAssembly cannot make direct API calls to third-party services.");
                errorMessage.AppendLine("You need a server-side backend to proxy these requests.");
            }

            return new AiChatResponse(
                Content: string.Empty,
                IsSuccess: false,
                ErrorMessage: errorMessage.ToString(),
                DurationMilliseconds: stopwatch.ElapsedMilliseconds);
        }
    }

    /// <summary>
    /// Maps provider key to LLmProviders enum.
    /// </summary>
    private static LLmProviders MapProviderKey(string providerKey)
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
}
