using Microsoft.JSInterop;

namespace Flowbite.Services;

/// <summary>
/// Service for managing floating element positioning using Floating UI.
/// </summary>
/// <remarks>
/// This service wraps @floating-ui/dom to provide viewport-aware positioning
/// for dropdowns, tooltips, and popovers. It handles automatic flip and shift
/// behavior when elements would otherwise overflow the viewport.
/// </remarks>
public class FloatingService : IFloatingService
{
    private readonly IJSRuntime _js;
    private readonly HashSet<string> _activeIds = [];
    private bool _disposed;

    /// <summary>
    /// Creates a new instance of the FloatingService.
    /// </summary>
    /// <param name="js">The JavaScript runtime for interop calls.</param>
    public FloatingService(IJSRuntime js)
    {
        _js = js;
    }

    /// <inheritdoc />
    public async Task<string?> InitializeAsync(string id, FloatingOptions? options = null)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FloatingService));
        }

        options ??= new FloatingOptions();

        try
        {
            var result = await _js.InvokeAsync<FloatingInitResult?>(
                "FlowbiteFloating.initialize",
                id,
                new
                {
                    placement = options.Placement,
                    offset = options.Offset,
                    enableFlip = options.EnableFlip,
                    enableShift = options.EnableShift,
                    shiftPadding = options.ShiftPadding
                });

            if (result != null)
            {
                _activeIds.Add(id);
                return result.Placement;
            }

            return null;
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FloatingService] Failed to initialize floating element {id}: {ex.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task UpdatePositionAsync(string id)
    {
        if (_disposed) return;
        if (!_activeIds.Contains(id)) return;

        try
        {
            await _js.InvokeVoidAsync("FlowbiteFloating.updatePosition", id);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FloatingService] Failed to update position for {id}: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task DestroyAsync(string id)
    {
        if (_disposed) return;
        if (!_activeIds.Remove(id)) return;

        try
        {
            await _js.InvokeVoidAsync("FlowbiteFloating.destroy", id);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FloatingService] Failed to destroy floating element {id}: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string id)
    {
        if (_disposed) return false;

        try
        {
            return await _js.InvokeAsync<bool>("FlowbiteFloating.exists", id);
        }
        catch (JSException)
        {
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<string?> GetPlacementAsync(string id)
    {
        if (_disposed) return null;

        try
        {
            return await _js.InvokeAsync<string?>("FlowbiteFloating.getPlacement", id);
        }
        catch (JSException)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        // Clean up all active floating instances
        foreach (var id in _activeIds.ToList())
        {
            try
            {
                await _js.InvokeVoidAsync("FlowbiteFloating.destroy", id);
            }
            catch (JSException)
            {
                // Ignore errors during cleanup - the page may be unloading
            }
        }

        _activeIds.Clear();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Result from the floating UI initialization.
    /// </summary>
    private class FloatingInitResult
    {
        public string? Placement { get; set; }
    }
}
