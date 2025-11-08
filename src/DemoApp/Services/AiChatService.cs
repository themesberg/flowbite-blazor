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
        IReadOnlyList<AiChatMessage> conversationHistory,
        bool enableWebSearch = false,
        CancellationToken cancellationToken = default)
    {
                                                        
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey), "API key is required.");
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
                        
            // Map provider key to LLmProviders enum
            var provider = MapProviderKey(providerKey);
                        
            if (provider == LLmProviders.Unknown)
            {
                                return new AiChatResponse(
                    Content: string.Empty,
                    IsSuccess: false,
                    ErrorMessage: $"Unknown provider: {providerKey}. Supported providers: openai, anthropic, google, openrouter.");
            }


            // Initialize TornadoApi with the provider and API key
            var api = new TornadoApi(provider, apiKey);

            // Enable direct browser access for web-based providers (availble in v3.8.13)
            // api.DirectBrowserAccess = true;
            
                        // Create chat model
            var model = new ChatModel(modelName, provider);
            
            // Build chat request with conversation history
            var messages = new List<ChatMessage>();

            // Add system message
            messages.Add(new ChatMessage(ChatMessageRoles.System, "You are a helpful AI assistant."));
            
            // Add conversation history
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

            // Create chat request
            var chatRequest = new ChatRequest
            {
                Model = model,
                Messages = messages,
                Temperature = 0.7
            };
            
            // Execute the request
            var result = await api.Chat.CreateChatCompletion(chatRequest);
            
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
                                                            
            if (ex.InnerException != null)
            {
                                                            }

            var errorMessage = new System.Text.StringBuilder();
            errorMessage.AppendLine($"Error Type: {ex.GetType().Name}");
            errorMessage.AppendLine($"Message: {ex.Message}");
            
            if (ex.InnerException != null)
            {
                errorMessage.AppendLine($"Inner Error: {ex.InnerException.Message}");
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
