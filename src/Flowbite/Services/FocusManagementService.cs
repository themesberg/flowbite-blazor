using Microsoft.JSInterop;

namespace Flowbite.Services;

/// <summary>
/// Service for focus management operations using lazy-loaded JavaScript module.
/// </summary>
/// <remarks>
/// This service provides focus trap and body scroll management for modal/drawer components
/// with on-demand module loading. The JavaScript module is only fetched when the first
/// operation is performed, reducing initial page load time.
/// </remarks>
public interface IFocusManagementService : IAsyncDisposable
{
    /// <summary>
    /// Traps focus within the specified element.
    /// </summary>
    /// <param name="elementId">The ID of the element to trap focus within.</param>
    /// <returns>True if the focus trap was set up successfully.</returns>
    Task<bool> TrapFocusAsync(string elementId);

    /// <summary>
    /// Restores focus to the previously focused element.
    /// </summary>
    /// <returns>True if focus was restored successfully.</returns>
    Task<bool> RestoreFocusAsync();

    /// <summary>
    /// Cleans up focus trap for the specified element.
    /// </summary>
    /// <param name="elementId">The ID of the element to clean up.</param>
    /// <returns>True if cleanup was successful.</returns>
    Task<bool> CleanupFocusTrapAsync(string elementId);

    /// <summary>
    /// Disables body scrolling (for modal/drawer components).
    /// </summary>
    Task DisableBodyScrollAsync();

    /// <summary>
    /// Enables body scrolling.
    /// </summary>
    Task EnableBodyScrollAsync();

    /// <summary>
    /// Initializes a drawer component with escape key handling.
    /// </summary>
    /// <param name="elementId">The ID of the drawer element.</param>
    /// <param name="dotNetReference">The .NET object reference for callbacks.</param>
    /// <returns>True if initialization was successful.</returns>
    Task<bool> InitializeDrawerAsync(string elementId, DotNetObjectReference<object>? dotNetReference = null);

    /// <summary>
    /// Cleans up event listeners for an element.
    /// </summary>
    /// <param name="elementId">The ID of the element to clean up.</param>
    /// <returns>True if cleanup was successful.</returns>
    Task<bool> CleanupElementAsync(string elementId);

    /// <summary>
    /// Initializes a toast component with auto-dismiss.
    /// </summary>
    /// <param name="elementId">The ID of the toast element.</param>
    /// <param name="duration">Duration in milliseconds before auto-hide (0 for no auto-hide).</param>
    /// <param name="dotNetReference">The .NET object reference for callbacks.</param>
    /// <returns>The timeout ID if auto-dismiss was set up, null otherwise.</returns>
    Task<int?> InitializeToastAsync(string elementId, int duration, DotNetObjectReference<object>? dotNetReference = null);

    /// <summary>
    /// Cancels a toast auto-dismiss timeout.
    /// </summary>
    /// <param name="timeoutId">The timeout ID to cancel.</param>
    Task CancelToastTimeoutAsync(int timeoutId);
}

/// <summary>
/// Implementation of <see cref="IFocusManagementService"/> with lazy-loaded JavaScript module.
/// </summary>
public class FocusManagementService : IFocusManagementService
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private bool _disposed;

    /// <summary>
    /// Creates a new instance of the FocusManagementService.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime for interop calls.</param>
    public FocusManagementService(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Flowbite/js/focus-management.module.js").AsTask());
    }

    /// <inheritdoc />
    public async Task<bool> TrapFocusAsync(string elementId)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("trapFocus", elementId);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to trap focus: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> RestoreFocusAsync()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("restoreFocus");
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to restore focus: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> CleanupFocusTrapAsync(string elementId)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("cleanupFocusTrap", elementId);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to cleanup focus trap: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task DisableBodyScrollAsync()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disableBodyScroll");
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to disable body scroll: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task EnableBodyScrollAsync()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("enableBodyScroll");
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to enable body scroll: {ex.Message}");
        }
    }

    /// <inheritdoc />
    public async Task<bool> InitializeDrawerAsync(string elementId, DotNetObjectReference<object>? dotNetReference = null)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("initializeDrawer", elementId, dotNetReference);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to initialize drawer: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<bool> CleanupElementAsync(string elementId)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("cleanupElement", elementId);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to cleanup element: {ex.Message}");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task<int?> InitializeToastAsync(string elementId, int duration, DotNetObjectReference<object>? dotNetReference = null)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<int?>("initializeToast", elementId, duration, dotNetReference);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to initialize toast: {ex.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task CancelToastTimeoutAsync(int timeoutId)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FocusManagementService));
        }

        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("cancelToastTimeout", timeoutId);
        }
        catch (JSException ex)
        {
            Console.Error.WriteLine($"[FocusManagementService] Failed to cancel toast timeout: {ex.Message}");
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
