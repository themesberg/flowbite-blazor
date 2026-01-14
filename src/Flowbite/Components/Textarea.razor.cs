using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Flowbite.Components;

/// <summary>
/// Textarea component for multi-line text input.
/// </summary>
public partial class Textarea
{
    private const string BaseTextareaClasses = "block w-full rounded-lg border disabled:cursor-not-allowed disabled:opacity-50 bg-gray-50 border-gray-300 text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500 p-2.5 text-sm";
    private const string BaseHelperTextClasses = "mt-2 text-sm";
    private string _elementId = Guid.NewGuid().ToString("N");

    /// <summary>
    /// Gets or sets the color variant of the textarea.
    /// When not explicitly set, the color will automatically change to Failure when validation errors occur.
    /// </summary>
    [Parameter] public TextInputColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter] public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea is required.
    /// </summary>
    [Parameter] public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the textarea has a shadow effect.
    /// </summary>
    [Parameter] public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets the helper text displayed below the textarea.
    /// </summary>
    [Parameter] public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets the number of visible text lines for the textarea.
    /// </summary>
    [Parameter] public int Rows { get; set; } = 4;

    /// <summary>
    /// Gets the effective color for the textarea, considering validation state.
    /// </summary>
    private TextInputColor EffectiveColor =>
        Color ?? (HasValidationErrors ? TextInputColor.Failure : TextInputColor.Gray);

    private string GetTextareaClasses()
    {
        var classes = new List<string> { BaseTextareaClasses };

        // Add color classes based on effective color (includes automatic validation state)
        var colorClasses = EffectiveColor switch
        {
            TextInputColor.Success => "border-green-500 bg-green-50 text-green-900 placeholder-green-700 focus:border-green-500 focus:ring-green-500 dark:border-green-400 dark:bg-green-100 dark:focus:border-green-500 dark:focus:ring-green-500",
            TextInputColor.Failure => "border-red-500 bg-red-50 text-red-900 placeholder-red-700 focus:border-red-500 focus:ring-red-500 dark:border-red-400 dark:bg-red-100 dark:focus:border-red-500 dark:focus:ring-red-500",
            TextInputColor.Warning => "border-yellow-500 bg-yellow-50 text-yellow-900 placeholder-yellow-700 focus:border-yellow-500 focus:ring-yellow-500 dark:border-yellow-400 dark:bg-yellow-100 dark:focus:border-yellow-500 dark:focus:ring-yellow-500",
            TextInputColor.Info => "border-primary-500 bg-primary-50 text-primary-900 placeholder-primary-700 focus:border-primary-500 focus:ring-primary-500 dark:border-primary-400 dark:bg-primary-100 dark:focus:border-primary-500 dark:focus:ring-primary-500",
            _ => string.Empty // Gray (default) uses base classes
        };
        if (!string.IsNullOrEmpty(colorClasses))
        {
            classes.Add(colorClasses);
        }

        // Add shadow
        if (Shadow)
        {
            classes.Add("shadow-sm dark:shadow-sm-light");
        }

        return MergeClasses(classes.ToArray());
    }

    private string GetHelperTextClasses()
    {
        var classes = new List<string> { BaseHelperTextClasses };

        var colorClasses = EffectiveColor switch
        {
            TextInputColor.Success => "text-green-500 dark:text-green-400",
            TextInputColor.Failure => "text-red-500 dark:text-red-400",
            TextInputColor.Warning => "text-yellow-500 dark:text-yellow-400",
            TextInputColor.Info => "text-primary-500 dark:text-primary-400",
            _ => "text-gray-500 dark:text-gray-400"
        };

        classes.Add(colorClasses);

        return MergeClasses(classes.ToArray());
    }

    /// <summary>
    /// Attempts to parse the provided value string.
    /// For textarea, the value is always a string, so parsing always succeeds.
    /// </summary>
    protected override bool TryParseValueFromString(
        string? value,
        out string? result,
        [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null;
        return true;
    }
}
