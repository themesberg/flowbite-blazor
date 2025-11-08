using Microsoft.AspNetCore.Components.Forms;

namespace Flowbite.Components.Chat;

/// <summary>
/// Represents the payload submitted from a <see cref="PromptInput"/>.
/// </summary>
public sealed class PromptInputMessage
{
    /// <summary>
    /// The text portion of the prompt.
    /// </summary>
    public string? Text { get; init; }

    /// <summary>
    /// The set of attached files included with the prompt.
    /// </summary>
    public IReadOnlyList<IBrowserFile> Files { get; init; } = Array.Empty<IBrowserFile>();
}
