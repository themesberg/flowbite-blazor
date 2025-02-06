using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components;

/// <summary>
/// Checkbox component for forms and input selection.
/// </summary>
public partial class Checkbox
{
    private const string BaseClasses = "w-4 h-4 rounded focus:ring-2 focus:ring-offset-2";

    /// <summary>
    /// Gets or sets the color variant of the checkbox.
    /// </summary>
    [Parameter] public CheckboxColor Color { get; set; } = CheckboxColor.Default;

    /// <summary>
    /// Gets or sets whether the checkbox is checked.
    /// </summary>
    [Parameter] public bool Checked { get; set; }

    /// <summary>
    /// Event callback for when the checkbox value changes.
    /// </summary>
    [Parameter] public EventCallback<bool> CheckedChanged { get; set; }

    /// <summary>
    /// Gets or sets the ID of the checkbox input element.
    /// </summary>
    [Parameter] public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the checkbox input element.
    /// </summary>
    [Parameter] public string? Name { get; set; }

    /// <summary>
    /// Gets or sets whether the checkbox is required.
    /// </summary>
    [Parameter] public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the checkbox is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the checkbox input element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetClasses()
    {
        var classes = new List<string> { BaseClasses };

        // Add color classes
        var colorClasses = Color switch
        {
            CheckboxColor.Primary => "text-primary-600 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-primary-600 dark:focus:ring-primary-600",
            CheckboxColor.Success => "text-green-600 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-green-600 dark:focus:ring-green-600",
            CheckboxColor.Warning => "text-yellow-400 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-yellow-400 dark:focus:ring-yellow-400",
            CheckboxColor.Failure => "text-red-600 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-red-600 dark:focus:ring-red-600",
            CheckboxColor.Info => "text-cyan-600 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-cyan-600 dark:focus:ring-cyan-600",
            _ => "text-primary-600 bg-gray-100 border-gray-300 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 focus:ring-primary-600 dark:focus:ring-primary-600" // default
        };
        classes.Add(colorClasses);

        // Add disabled state
        if (Disabled)
        {
            classes.Add("cursor-not-allowed opacity-50");
        }

        return string.Join(" ", classes);
    }

    private async Task OnCheckboxChanged(ChangeEventArgs e)
    {
        if (e.Value is bool newValue)
        {
            Checked = newValue;
            await CheckedChanged.InvokeAsync(Checked);
        }
    }
}
