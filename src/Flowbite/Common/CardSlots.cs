namespace Flowbite.Common;

/// <summary>
/// Slot configuration for the Card component, enabling per-element class customization.
/// </summary>
/// <remarks>
/// Use this class to override default styling for specific parts of the Card component
/// while preserving the Flowbite design system.
/// </remarks>
/// <example>
/// <code>
/// &lt;Card Slots="@(new CardSlots {
///     Base = "shadow-lg",
///     Image = "rounded-xl",
///     Body = "p-8"
/// })"&gt;
///     Card content
/// &lt;/Card&gt;
/// </code>
/// </example>
public class CardSlots : SlotBase
{
    /// <summary>
    /// CSS classes applied to the card's image element.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// CSS classes applied to the card's body/content container.
    /// </summary>
    /// <remarks>
    /// The body wraps the ChildContent of the card.
    /// </remarks>
    public string? Body { get; set; }
}
