using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Flowbite.Base;
using System.Collections.Generic;

namespace Flowbite.Components;

public partial class FileInput : FlowbiteComponentBase
{
    /// <summary>
    /// Gets or sets the ID of the file input element.
    /// </summary>
    [Parameter] public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the label text for the file input.
    /// </summary>
    [Parameter] public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the CSS classes for the wrapper div.
    /// </summary>
    [Parameter] public string WrapperClasses { get; set; } = "max-w-md";

    /// <summary>
    /// Gets or sets the color of the file input, used primarily for validation states.
    /// </summary>
    [Parameter] public TextInputColor Color { get; set; } = TextInputColor.Gray;

    /// <summary>
    /// Gets or sets the helper text displayed below the file input.
    /// </summary>
    [Parameter] public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets whether the file input has a shadow effect.
    /// </summary>
    [Parameter] public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets the callback for when files are selected.
    /// </summary>
    [Parameter] public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }

    /// <summary>
    /// Gets or sets additional attributes that will be applied to the input element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "block w-full overflow-hidden rounded-lg border disabled:cursor-not-allowed disabled:opacity-50 border-gray-300 bg-gray-50 text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500 text-sm";

    private string RootClasses => "block w-full";

    private string FieldClasses => "mb-2 block";

    private string InputClasses => string.Join(" ", new[]
    {
        BaseClasses,
        GetColorClasses(),
        Shadow ? "shadow-sm dark:shadow-sm-light" : null,
        AdditionalAttributes?.ContainsKey("class") == true ? AdditionalAttributes["class"]?.ToString() : null
    }.Where(c => !string.IsNullOrEmpty(c)));

    private string GetColorClasses() => Color switch
    {
        TextInputColor.Info => "border-primary-500 file:bg-primary-100 file:text-primary-700 dark:border-primary-500 dark:file:bg-primary-900 dark:file:text-primary-300",
        TextInputColor.Success => "border-green-500 file:bg-green-100 file:text-green-700 dark:border-green-500 dark:file:bg-green-900 dark:file:text-green-300",
        TextInputColor.Warning => "border-yellow-500 file:bg-yellow-100 file:text-yellow-700 dark:border-yellow-500 dark:file:bg-yellow-900 dark:file:text-yellow-300",
        TextInputColor.Failure => "border-red-500 file:bg-red-100 file:text-red-700 dark:border-red-500 dark:file:bg-red-900 dark:file:text-red-300",
        _ => ""
    };

    private string GetHelperTextColorClass() => Color switch
    {
        TextInputColor.Info => "text-primary-600 dark:text-primary-500",
        TextInputColor.Success => "text-green-600 dark:text-green-500",
        TextInputColor.Warning => "text-yellow-600 dark:text-yellow-500",
        TextInputColor.Failure => "text-red-600 dark:text-red-500",
        _ => "text-gray-600 dark:text-gray-400"
    };

    private string HelperTextClasses => string.Join(" ", new[]
    {
        "mt-2 text-sm",
        GetHelperTextColorClass()
    }.Where(c => !string.IsNullOrEmpty(c)));
}
