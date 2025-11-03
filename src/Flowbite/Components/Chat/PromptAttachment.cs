using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Flowbite.Components.Chat;

/// <summary>
/// Represents a file attachment within the prompt input.
/// </summary>
public sealed class PromptAttachment
{
    private const long MaxPreviewBytes = 5 * 1024 * 1024;
    private readonly IAsyncDisposable? _disposableResource;
    private readonly SemaphoreSlim _previewLock = new(1, 1);
    private string? _previewDataUrl;

    /// <summary>
    /// Creates a new <see cref="PromptAttachment"/> instance.
    /// </summary>
    /// <param name="file">The uploaded browser file.</param>
    /// <param name="disposableResource">Optional resource to dispose when the attachment is cleared.</param>
    public PromptAttachment(IBrowserFile file, IAsyncDisposable? disposableResource = null)
    {
        File = file;
        _disposableResource = disposableResource;
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

    /// <summary>
    /// MIME content type for the file.
    /// </summary>
    public string ContentType => File.ContentType;

    /// <summary>
    /// Indicates whether a preview can be generated for this attachment.
    /// </summary>
    public bool SupportsPreview =>
        !string.IsNullOrWhiteSpace(ContentType) &&
        ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase) &&
        Size <= MaxPreviewBytes;

    internal IAsyncDisposable? DisposableResource => _disposableResource;

    /// <summary>
    /// Lazily generates an inline data URL suitable for previewing the attachment.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the preview generation.</param>
    public async Task<string?> GetPreviewDataUrlAsync(CancellationToken cancellationToken = default)
    {
        if (!SupportsPreview)
        {
            return null;
        }

        if (_previewDataUrl is not null)
        {
            return _previewDataUrl;
        }

        await _previewLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_previewDataUrl is not null)
            {
                return _previewDataUrl;
            }

            await using var stream = File.OpenReadStream(MaxPreviewBytes, cancellationToken);
            using var buffer = new MemoryStream();
            await stream.CopyToAsync(buffer, cancellationToken).ConfigureAwait(false);
            var contentType = string.IsNullOrWhiteSpace(ContentType) ? "application/octet-stream" : ContentType;
            _previewDataUrl = $"data:{contentType};base64,{Convert.ToBase64String(buffer.ToArray())}";
            return _previewDataUrl;
        }
        catch when (cancellationToken.IsCancellationRequested)
        {
            return null;
        }
        catch
        {
            return null;
        }
        finally
        {
            _previewLock.Release();
        }
    }
}
