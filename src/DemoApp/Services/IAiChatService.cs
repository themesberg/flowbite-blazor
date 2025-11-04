namespace DemoApp.Services;

/// <summary>
/// Service for AI chat operations using LlmTornado.
/// </summary>
public interface IAiChatService
{
    /// <summary>
    /// Sends a message to the AI provider and returns the response.
    /// </summary>
    /// <param name="providerKey">Provider key (e.g., "openai", "anthropic", "google", "openrouter")</param>
    /// <param name="apiKey">API key for the provider</param>
    /// <param name="modelName">Model name to use</param>
    /// <param name="userMessage">User's message</param>
    /// <param name="conversationHistory">Previous messages in the conversation</param>
    /// <param name="enableWebSearch">Whether to enable web search capabilities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>AI response with content and metadata</returns>
    Task<AiChatResponse> SendMessageAsync(
        string providerKey,
        string apiKey,
        string modelName,
        string userMessage,
        IReadOnlyList<AiChatMessage> conversationHistory,
        bool enableWebSearch = false,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents a message in the chat conversation.
/// </summary>
/// <param name="Role">Role of the message sender</param>
/// <param name="Content">Content of the message</param>
public sealed record AiChatMessage(string Role, string Content);

/// <summary>
/// Response from the AI chat service.
/// </summary>
/// <param name="Content">Main response content</param>
/// <param name="Reasoning">Optional reasoning/thinking process</param>
/// <param name="Sources">Optional sources/citations</param>
/// <param name="DurationMilliseconds">Response generation time in milliseconds</param>
/// <param name="IsSuccess">Whether the request was successful</param>
/// <param name="ErrorMessage">Error message if request failed</param>
public sealed record AiChatResponse(
    string Content,
    string? Reasoning = null,
    IReadOnlyList<AiChatSource>? Sources = null,
    long? DurationMilliseconds = null,
    bool IsSuccess = true,
    string? ErrorMessage = null);

/// <summary>
/// Represents a source/citation in the AI response.
/// </summary>
/// <param name="Title">Title of the source</param>
/// <param name="Url">URL of the source</param>
public sealed record AiChatSource(string Title, string Url);
