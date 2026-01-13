namespace Flowbite.Common;

/// <summary>
/// Slot configuration for the AccordionItem component, enabling per-element class customization.
/// </summary>
/// <remarks>
/// Use this class to override default styling for specific parts of the AccordionItem component
/// while preserving the Flowbite design system.
/// </remarks>
/// <example>
/// <code>
/// &lt;Accordion&gt;
///     &lt;AccordionItem Slots="@(new AccordionItemSlots {
///         Heading = "bg-gray-50",
///         Trigger = "hover:bg-gray-100",
///         Title = "text-lg font-bold",
///         Content = "p-6",
///         Indicator = "text-blue-500"
///     })"&gt;
///         &lt;Header&gt;Section Title&lt;/Header&gt;
///         &lt;ChildContent&gt;Section content&lt;/ChildContent&gt;
///     &lt;/AccordionItem&gt;
/// &lt;/Accordion&gt;
/// </code>
/// </example>
public class AccordionItemSlots : SlotBase
{
    /// <summary>
    /// CSS classes applied to the accordion item heading container.
    /// </summary>
    public string? Heading { get; set; }

    /// <summary>
    /// CSS classes applied to the clickable trigger button.
    /// </summary>
    public string? Trigger { get; set; }

    /// <summary>
    /// CSS classes applied to the title text element.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// CSS classes applied to the collapsible content container.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// CSS classes applied to the expand/collapse indicator icon.
    /// </summary>
    public string? Indicator { get; set; }
}
