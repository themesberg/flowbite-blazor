using System;
using System.Threading.Tasks;

namespace Flowbite.Components.Chat;

/// <summary>
/// Context shared by reasoning components.
/// </summary>
internal sealed class ReasoningContext
{
    public event System.Action? StateChanged;

    public bool IsOpen { get; set; }

    public bool IsStreaming { get; set; }

    public int? DurationSeconds { get; set; }

    public Func<Task>? ToggleAsync { get; set; }

    public void NotifyChanged() => StateChanged?.Invoke();
}
