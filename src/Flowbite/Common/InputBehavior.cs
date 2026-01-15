namespace Flowbite.Common;

/// <summary>
/// Specifies when input components fire their ValueChanged event.
/// </summary>
public enum InputBehavior
{
    /// <summary>
    /// Fire only on blur or when Enter is pressed.
    /// This is the default behavior, matching standard HTML input behavior.
    /// </summary>
    OnChange,

    /// <summary>
    /// Fire on every keystroke (subject to DebounceDelay if configured).
    /// Use this for search-as-you-type scenarios.
    /// </summary>
    OnInput
}
