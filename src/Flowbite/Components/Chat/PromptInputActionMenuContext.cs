using System;
using System.Threading.Tasks;

namespace Flowbite.Components.Chat;

/// <summary>
/// Context used by the prompt input action menu components to share open state.
/// </summary>
internal sealed class PromptInputActionMenuContext
{
    private Func<Task>? _closeAsync;
    private bool _isOpen;

    public event System.Action? OpenChanged;

    /// <summary>
    /// Indicates whether the menu is open.
    /// </summary>
    public bool IsOpen => _isOpen;

    /// <summary>
    /// Registers callbacks used to manage the underlying overlay.
    /// </summary>
    /// <param name="closeAsync">Delegate that closes the menu.</param>
    public void Configure(Func<Task> closeAsync)
    {
        _closeAsync = closeAsync;
    }

    /// <summary>
    /// Updates the tracked open state and notifies listeners.
    /// </summary>
    /// <param name="value">True when the menu is open.</param>
    public void UpdateIsOpen(bool value)
    {
        if (_isOpen == value)
        {
            return;
        }

        _isOpen = value;
        OpenChanged?.Invoke();
    }

    /// <summary>
    /// Attempts to close the menu.
    /// </summary>
    public Task CloseAsync() => _closeAsync?.Invoke() ?? Task.CompletedTask;
}
