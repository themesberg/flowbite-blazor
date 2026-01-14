using System.Diagnostics.CodeAnalysis;

namespace Flowbite.Components;

/// <summary>
/// Textarea component for multi-line text input.
/// </summary>
public partial class Textarea
{
    private const string BaseTextareaClasses = "block w-full rounded-lg border p-2.5 text-sm focus:outline-none focus:ring-1 disabled:cursor-not-allowed disabled:opacity-50";
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

    private string GetColorClasses() => EffectiveColor switch
    {
        TextInputColor.Gray => "border-gray-300 bg-gray-50 text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500",
        TextInputColor.Info => "border-cyan-500 bg-cyan-50 text-cyan-900 placeholder-cyan-700 focus:border-cyan-500 focus:ring-cyan-500 dark:border-cyan-400 dark:bg-cyan-100 dark:focus:border-cyan-500 dark:focus:ring-cyan-500",
        TextInputColor.Failure => "border-red-500 bg-red-50 text-red-900 placeholder-red-700 focus:border-red-500 focus:ring-red-500 dark:border-red-400 dark:bg-red-100 dark:focus:border-red-500 dark:focus:ring-red-500",
        TextInputColor.Warning => "border-yellow-500 bg-yellow-50 text-yellow-900 placeholder-yellow-700 focus:border-yellow-500 focus:ring-yellow-500 dark:border-yellow-400 dark:bg-yellow-100 dark:focus:border-yellow-500 dark:focus:ring-yellow-500",
        TextInputColor.Success => "border-green-500 bg-green-50 text-green-900 placeholder-green-700 focus:border-green-500 focus:ring-green-500 dark:border-green-400 dark:bg-green-100 dark:focus:border-green-500 dark:focus:ring-green-500",
        _ => throw new ArgumentOutOfRangeException(nameof(EffectiveColor), EffectiveColor, "Invalid TextInputColor value")
    };

    private string GetTextareaClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add(BaseTextareaClasses)
                .Add(GetColorClasses())
                .Add("shadow-sm dark:shadow-sm-light", when: Shadow)
                .Add(Class)
                .Add(CssClass)
        );
    }

    private string GetHelperTextColorClasses() => EffectiveColor switch
    {
        TextInputColor.Gray => "text-gray-500 dark:text-gray-400",
        TextInputColor.Info => "text-cyan-600 dark:text-cyan-500",
        TextInputColor.Failure => "text-red-600 dark:text-red-500",
        TextInputColor.Warning => "text-yellow-600 dark:text-yellow-500",
        TextInputColor.Success => "text-green-600 dark:text-green-500",
        _ => throw new ArgumentOutOfRangeException(nameof(EffectiveColor), EffectiveColor, "Invalid TextInputColor value")
    };

    private string GetHelperTextClasses()
    {
        return CombineClasses(
            ElementClass.Empty()
                .Add(BaseHelperTextClasses)
                .Add(GetHelperTextColorClasses())
        );
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
