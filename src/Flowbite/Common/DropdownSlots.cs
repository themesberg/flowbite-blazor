namespace Flowbite.Common;

/// <summary>
/// Slot configuration for the Dropdown component, enabling per-element class customization.
/// </summary>
/// <remarks>
/// Use this class to override default styling for specific parts of the Dropdown component
/// while preserving the Flowbite design system.
/// </remarks>
/// <example>
/// <code>
/// &lt;Dropdown Slots="@(new DropdownSlots {
///     Trigger = "bg-blue-600 hover:bg-blue-700",
///     Menu = "w-64 shadow-xl",
///     Item = "hover:bg-blue-100"
/// })"&gt;
///     &lt;Label&gt;Options&lt;/Label&gt;
///     &lt;ChildContent&gt;
///         &lt;DropdownItem&gt;Item 1&lt;/DropdownItem&gt;
///     &lt;/ChildContent&gt;
/// &lt;/Dropdown&gt;
/// </code>
/// </example>
public class DropdownSlots : SlotBase
{
    /// <summary>
    /// CSS classes applied to the dropdown trigger button.
    /// </summary>
    public string? Trigger { get; set; }

    /// <summary>
    /// CSS classes applied to the dropdown menu container.
    /// </summary>
    public string? Menu { get; set; }

    /// <summary>
    /// CSS classes applied to dropdown items.
    /// </summary>
    /// <remarks>
    /// These classes are passed down to DropdownItem components via cascading value.
    /// </remarks>
    public string? Item { get; set; }
}
