namespace Flowbite.Components.Chat;

/// <summary>
/// Submission status used by <see cref="PromptInputSubmit"/>.
/// </summary>
public enum PromptSubmissionStatus
{
    /// <summary>
    /// No action in flight.
    /// </summary>
    Idle,

    /// <summary>
    /// Prompt is currently being submitted to the server.
    /// </summary>
    Submitting,

    /// <summary>
    /// Prompt is streaming a response.
    /// </summary>
    Streaming,

    /// <summary>
    /// An error occurred when submitting the prompt.
    /// </summary>
    Error
}
