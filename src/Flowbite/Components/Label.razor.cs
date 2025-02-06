using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Label component for form fields and other UI elements.
/// </summary>
public partial class Label
{
    /// <summary>
    /// Gets or sets the text content of the label.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Label Value="Username" /&gt;
    /// </code>
    /// </example>
    [Parameter] public string? Value { get; set; }

    /// <summary>
    /// Gets or sets whether the label is disabled.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Label Disabled="true" Value="Inactive Field" /&gt;
    /// </code>
    /// </example>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the child content of the label.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Label&gt;Custom Content&lt;/Label&gt;
    /// </code>
    /// </example>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the label element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetClasses()
    {
        var classes = new List<string>
        {
            "text-sm font-medium text-gray-900 dark:text-gray-300", // Base classes
        };

        // Add disabled state
        if (Disabled)
        {
            classes.Add("cursor-not-allowed opacity-50");
        }

        return string.Join(" ", classes);
    }
}
