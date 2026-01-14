using Microsoft.JSInterop;

namespace Flowbite.Services;

/// <summary>
/// Service for clipboard operations using lazy-loaded JavaScript module.
/// </summary>
/// <remarks>
/// This service provides clipboard functionality with on-demand module loading.
/// The JavaScript module is only fetched when the first clipboard operation is performed,
/// reducing initial page load time.
/// </remarks>
public interface IClipboardService : IAsyncDisposable
{
    /// <summary>
    /// Copies the specified content to the system clipboard.
    /// </summary>
    /// <param name="content">The text content to copy.</param>
    /// <returns>True if the copy operation was successful, false otherwise.</returns>
    Task<bool> CopyToClipboardAsync(string content);
}

/// <summary>
/// Implementation of <see cref="IClipboardService"/> with lazy-loaded JavaScript module.
/// </summary>
public class ClipboardService : IClipboardService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private bool _disposed;

    /// <summary>
    /// Creates a new instance of the ClipboardService.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime for interop calls.</param>
    public ClipboardService(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Flowbite/js/clipboard.js").AsTask());
    }

    /// <inheritdoc />
    public async Task<bool> CopyToClipboardAsync(string content)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ClipboardService));
        }

        if (string.IsNullOrEmpty(content))
        {
            return false;
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("copyToClipboard", content);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[ClipboardService] Failed to copy to clipboard: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (_moduleTask.IsValueCreated)
        {
            try
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected, module already cleaned up
            }
            catch (JSException)
            {
                // Ignore JS errors during cleanup
            }
        }

        GC.SuppressFinalize(this);
    }
}
