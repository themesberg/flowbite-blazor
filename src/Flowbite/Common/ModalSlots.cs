namespace Flowbite.Common;

/// <summary>
/// Slot configuration for the Modal component, enabling per-element class customization.
/// </summary>
/// <remarks>
/// Use this class to override default styling for specific parts of the Modal component
/// while preserving the Flowbite design system.
/// </remarks>
/// <example>
/// <code>
/// &lt;Modal Show="@showModal" Slots="@(new ModalSlots {
///     Backdrop = "bg-gray-900/70",
///     Content = "rounded-xl shadow-2xl",
///     Header = "border-b-2",
///     Body = "p-8",
///     Footer = "bg-gray-50"
/// })"&gt;
///     &lt;ModalHeader&gt;Title&lt;/ModalHeader&gt;
///     &lt;ModalBody&gt;Content&lt;/ModalBody&gt;
///     &lt;ModalFooter&gt;Footer&lt;/ModalFooter&gt;
/// &lt;/Modal&gt;
/// </code>
/// </example>
public class ModalSlots : SlotBase
{
    /// <summary>
    /// CSS classes applied to the modal backdrop overlay.
    /// </summary>
    public string? Backdrop { get; set; }

    /// <summary>
    /// CSS classes applied to the modal content container.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// CSS classes applied to the modal header section.
    /// </summary>
    /// <remarks>
    /// These classes are passed down to ModalHeader components via cascading value.
    /// </remarks>
    public string? Header { get; set; }

    /// <summary>
    /// CSS classes applied to the modal body section.
    /// </summary>
    /// <remarks>
    /// These classes are passed down to ModalBody components via cascading value.
    /// </remarks>
    public string? Body { get; set; }

    /// <summary>
    /// CSS classes applied to the modal footer section.
    /// </summary>
    /// <remarks>
    /// These classes are passed down to ModalFooter components via cascading value.
    /// </remarks>
    public string? Footer { get; set; }
}
