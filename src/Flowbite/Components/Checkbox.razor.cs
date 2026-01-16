using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components;

/// <summary>
/// Checkbox component for forms and input selection.
/// </summary>
public partial class Checkbox
{
    private const string BaseClasses = "w-4 h-4 text-primary-600 bg-gray-100 border-gray-300 rounded focus:ring-2 focus:ring-primary-600 focus:ring-offset-2 dark:bg-gray-700 dark:border-gray-600 dark:ring-offset-gray-800 dark:focus:ring-primary-600";

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

    private string GetClasses()
    {
        var classes = new List<string> { BaseClasses };

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
