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
    /// Gets or sets the color variant of the label.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Label Color="LabelColor.Success" Value="Status" /&gt;
    /// </code>
    /// </example>
    [Parameter] public LabelColor Color { get; set; } = LabelColor.Default;

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
            "text-sm font-medium", // Base classes
        };

        // Add color classes
        var colorClasses = Color switch
        {
            LabelColor.Info => "text-cyan-500 dark:text-cyan-600",
            LabelColor.Success => "text-green-500 dark:text-green-600",
            LabelColor.Failure => "text-red-500 dark:text-red-600",
            LabelColor.Warning => "text-yellow-500 dark:text-yellow-600",
            _ => "text-gray-900 dark:text-gray-300" // default
        };
        classes.Add(colorClasses);

        // Add disabled state
        if (Disabled)
        {
            classes.Add("cursor-not-allowed opacity-50");
        }

        return string.Join(" ", classes);
    }
}
