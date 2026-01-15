using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Services;

/// <summary>
/// Service for DOM element operations using lazy-loaded JavaScript module.
/// </summary>
/// <remarks>
/// This service provides element measurement and manipulation functionality with on-demand module loading.
/// The JavaScript module is only fetched when the first operation is performed,
/// reducing initial page load time.
/// </remarks>
public interface IElementService : IAsyncDisposable
{
    /// <summary>
    /// Gets the scroll height of an element for collapse/expand animations.
    /// </summary>
    /// <param name="element">The element reference to measure.</param>
    /// <returns>The scroll height in pixels, or 0 if the element is null.</returns>
    Task<int> GetScrollHeightAsync(ElementReference element);

    /// <summary>
    /// Gets the client height of an element.
    /// </summary>
    /// <param name="element">The element reference to measure.</param>
    /// <returns>The client height in pixels, or 0 if the element is null.</returns>
    Task<int> GetClientHeightAsync(ElementReference element);

    /// <summary>
    /// Sets focus on an element.
    /// </summary>
    /// <param name="element">The element reference to focus.</param>
    /// <param name="preventScroll">Whether to prevent scrolling when focusing.</param>
    Task SetFocusAsync(ElementReference element, bool preventScroll = false);

    /// <summary>
    /// Scrolls an element into view.
    /// </summary>
    /// <param name="element">The element reference to scroll into view.</param>
    /// <param name="behavior">The scroll behavior ("auto", "smooth", or "instant").</param>
    /// <param name="block">The vertical alignment ("start", "center", "end", or "nearest").</param>
    Task ScrollIntoViewAsync(ElementReference element, string behavior = "smooth", string block = "nearest");
}

/// <summary>
/// Implementation of <see cref="IElementService"/> with lazy-loaded JavaScript module.
/// </summary>
public class ElementService : IElementService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private bool _disposed;

    /// <summary>
    /// Creates a new instance of the ElementService.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime for interop calls.</param>
    public ElementService(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Flowbite/js/element.module.js").AsTask());
    }

    /// <inheritdoc />
    public async Task<int> GetScrollHeightAsync(ElementReference element)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ElementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<int>("getScrollHeight", element);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[ElementService] Failed to get scroll height: {ex.Message}");
            return 0;
        }
    }

    /// <inheritdoc />
    public async Task<int> GetClientHeightAsync(ElementReference element)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ElementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<int>("getClientHeight", element);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[ElementService] Failed to get client height: {ex.Message}");
            return 0;
        }
    }

    /// <inheritdoc />
    public async Task SetFocusAsync(ElementReference element, bool preventScroll = false)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ElementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setFocus", element, preventScroll);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[ElementService] Failed to set focus: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task ScrollIntoViewAsync(ElementReference element, string behavior = "smooth", string block = "nearest")
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ElementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("scrollIntoView", element, new { behavior, block });
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[ElementService] Failed to scroll into view: {ex.Message}");
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
