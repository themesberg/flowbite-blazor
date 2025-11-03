using System;

namespace Flowbite.Components.Chat;

/// <summary>
/// Context used by the prompt input action menu components to share open state.
/// </summary>
internal sealed class PromptInputActionMenuContext
{
    private bool _isOpen;

    public event System.Action? OpenChanged;

    /// <summary>
    /// Indicates whether the menu is open.
    /// </summary>
    public bool IsOpen
    {
        get => _isOpen;
        private set
        {
            if (_isOpen == value)
            {
                return;
            }

            _isOpen = value;
            OpenChanged?.Invoke();
        }
    }

    /// <summary>
    /// Toggles the menu open state.
    /// </summary>
    public void Toggle() => IsOpen = !IsOpen;

    /// <summary>
    /// Opens the menu.
    /// </summary>
    public void Open() => IsOpen = true;

    /// <summary>
    /// Closes the menu.
    /// </summary>
    public void Close() => IsOpen = false;
}
