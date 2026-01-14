namespace Flowbite.Utilities;

/// <summary>
/// Provides debouncing functionality for async operations.
/// Ensures only the last call within a delay period executes.
/// </summary>
/// <remarks>
/// This class is useful for scenarios like search-as-you-type where you want
/// to wait for the user to stop typing before executing an action.
/// </remarks>
public sealed class Debouncer : IDisposable
{
    private CancellationTokenSource? _cts;
    private bool _disposed;

    /// <summary>
    /// Debounces an async action by the specified delay.
    /// If called again before the delay expires, the previous call is cancelled.
    /// </summary>
    /// <param name="action">The async action to execute after the delay.</param>
    /// <param name="delayMs">The delay in milliseconds before executing the action.</param>
    /// <returns>A task that completes when the debounced action executes or is cancelled.</returns>
    public async Task DebounceAsync(Func<Task> action, int delayMs)
    {
        if (_disposed) return;

        // Cancel any pending operation
        _cts?.Cancel();
        _cts?.Dispose();

        var cts = _cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(delayMs, cts.Token);

            if (!cts.Token.IsCancellationRequested)
            {
                await action();
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when a new call cancels the previous one
        }
    }

    /// <summary>
    /// Cancels any pending debounced operation.
    /// </summary>
    public void Cancel()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Disposes the debouncer and cancels any pending operation.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}
