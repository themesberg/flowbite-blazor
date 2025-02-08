using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components;

public partial class ToggleSwitch
{
/// <summary>
/// Reference to the button element.
/// </summary>
private ElementReference _buttonRef;

private readonly string Id = Guid.NewGuid().ToString();

/// <summary>
/// Gets or sets whether the toggle switch is checked.
/// </summary>
[Parameter] public bool Checked { get; set; }

/// <summary>
/// Event callback for when the checked state changes.
/// </summary>
[Parameter] public EventCallback<bool> CheckedChanged { get; set; }

/// <summary>
/// Gets or sets the label text for the toggle switch.
/// </summary>
[Parameter] public string? Label { get; set; }

/// <summary>
/// Gets or sets whether the toggle switch is disabled.
/// </summary>
[Parameter] public bool Disabled { get; set; }

/// <summary>
/// Gets or sets the name attribute for the hidden checkbox input.
/// </summary>
[Parameter] public string? Name { get; set; }

/// <summary>
/// Gets or sets additional attributes for the toggle switch.
/// </summary>
[Parameter(CaptureUnmatchedValues = true)]
public Dictionary<string, object>? AdditionalAttributes { get; set; }

private async Task HandleClick()
{
    if (Disabled)
        return;

    await CheckedChanged.InvokeAsync(!Checked);
}

private void HandleKeyDown(KeyboardEventArgs e)
{
    if (e.Code == "Enter")
    {
        // Enter key is handled by the button element's default behavior
        return;
    }
}

    private string GetRootClasses()
    {
        var classes = new List<string>
        {
            "group flex rounded-lg focus:outline-none"
        };

        if (!Disabled)
        {
            classes.Add("cursor-pointer");
        }

        return string.Join(" ", classes);
    }

    private string GetToggleClasses()
    {
        var classes = new List<string>
        {
            "relative h-6 w-11 min-w-11 rounded-full border",
            "after:absolute after:left-px after:top-px after:h-5 after:w-5 after:rounded-full after:bg-white after:transition-all rtl:after:right-px",
            "group-focus:ring-4 group-focus:ring-cyan-500/25",
            "border-gray-200 bg-gray-200 dark:border-gray-600 dark:bg-gray-700"
        };

        if (Checked)
        {
            classes.Add("bg-primary-700 dark:bg-primary-600");
            classes.Add("after:translate-x-full after:border-white dark:after:border-gray-300 dark:after:bg-gray-200");
        }

        if (Disabled)
        {
            classes.Add("cursor-not-allowed opacity-50");
        }

        return string.Join(" ", classes);
    }
}
