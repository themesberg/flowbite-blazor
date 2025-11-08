using System;

namespace Flowbite.Components.Chat;

/// <summary>
/// Internal context for the prompt input action menu components.
/// </summary>
internal sealed class PromptInputActionMenuContext
{
    public PromptInputActionMenuContext()
    {
    }

    public event System.Action? StateChanged;

    public bool IsOpen { get; private set; }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        StateChanged?.Invoke();
    }

    public void Close()
    {
        if (IsOpen)
        {
            IsOpen = false;
            StateChanged?.Invoke();
        }
    }
}
