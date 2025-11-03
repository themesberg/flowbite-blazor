using Microsoft.AspNetCore.Components.Forms;

namespace Flowbite.Components.Chat;

/// <summary>
/// Represents a file attachment within the prompt input.
/// </summary>
public sealed class PromptAttachment
{
    /// <summary>
    /// Creates a new <see cref="PromptAttachment"/> instance.
    /// </summary>
    /// <param name="file">The uploaded browser file.</param>
    public PromptAttachment(IBrowserFile file)
    {
        File = file;
        Id = Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// Unique identifier for the attachment.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets the underlying browser file.
    /// </summary>
    public IBrowserFile File { get; }

    /// <summary>
    /// Friendly file name for display.
    /// </summary>
    public string Name => File.Name;

    /// <summary>
    /// File size in bytes.
    /// </summary>
    public long Size => File.Size;
}
