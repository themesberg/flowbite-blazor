namespace Flowbite.Common;

/// <summary>
/// Base class for slot configurations that enable per-element class customization.
/// </summary>
/// <remarks>
/// Slots allow fine-grained control over CSS classes applied to different parts of a component.
/// Each slot class inherits from this base and defines additional properties for specific elements.
/// </remarks>
/// <example>
/// <code>
/// // Usage with a component
/// &lt;Card Slots="@(new CardSlots { Base = "shadow-lg", Body = "p-8" })"&gt;
///     Card content
/// &lt;/Card&gt;
/// </code>
/// </example>
public abstract class SlotBase
{
    /// <summary>
    /// CSS classes applied to the component's root element.
    /// </summary>
    /// <remarks>
    /// These classes are merged with the component's default base classes using TailwindMerge.
    /// User-provided classes take precedence over defaults when conflicts occur.
    /// </remarks>
    public string? Base { get; set; }
}
