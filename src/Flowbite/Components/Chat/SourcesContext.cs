using System;

namespace Flowbite.Components.Chat;

/// <summary>
/// Context shared between sources components to track open state and source count.
/// </summary>
internal sealed class SourcesContext
{
    public event System.Action? StateChanged;

    public bool IsOpen { get; private set; }

    public int Count { get; private set; }

    public string ContentId { get; } = $"sources-{Guid.NewGuid():N}";

    public void SetCount(int count)
    {
        if (Count == count)
        {
            return;
        }

        Count = count;
        StateChanged?.Invoke();
    }

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
