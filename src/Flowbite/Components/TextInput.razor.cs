using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Flowbite.Common;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components;

/// <summary>
/// TextInput component for forms and user input.
/// </summary>
public partial class TextInput<TValue>
{
    private const string BaseWrapperClasses = "flex";
    private const string BaseFieldClasses = "relative w-full";
    private const string BaseInputClasses = "block w-full border focus:outline-none focus:ring-1 disabled:cursor-not-allowed disabled:opacity-50";
    private const string BaseAddonClasses = "inline-flex items-center border border-gray-300 bg-gray-200 px-3 text-sm text-gray-900 dark:border-gray-600 dark:bg-gray-600 dark:text-gray-400";
    private const string BaseIconClasses = "pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3";
    private const string BaseRightIconClasses = "pointer-events-none absolute inset-y-0 right-0 flex items-center pr-3";
    private const string BaseHelperTextClasses = "mt-1 text-sm";

    /// <summary>
    /// Gets or sets the color variant of the input.
    /// When not explicitly set, the color will automatically change to Failure when validation errors occur.
    /// </summary>
    [Parameter] public TextInputColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the size variant of the input.
    /// </summary>
    [Parameter] public TextInputSize Size { get; set; } = TextInputSize.Medium;

    /// <summary>
    /// Gets or sets the type of the input (e.g., "text", "password", "email").
    /// </summary>
    [Parameter] public string Type { get; set; } = "text";

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter] public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    [Parameter] public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input has a shadow effect.
    /// </summary>
    [Parameter] public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets the helper text displayed below the input.
    /// </summary>
    [Parameter] public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets the regular expression pattern used for client-side validation.
    /// </summary>
    [Parameter] public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets the HTML title attribute.
    /// </summary>
    [Parameter] public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the HTML inputmode attribute for improved mobile keyboard hints.
    /// </summary>
    [Parameter] public string? InputMode { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed on the left side of the input.
    /// </summary>
    [Parameter] public IconBase? Icon { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed on the right side of the input.
    /// </summary>
    [Parameter] public IconBase? RightIcon { get; set; }

    /// <summary>
    /// Gets or sets when the ValueChanged event fires.
    /// Default is OnChange (fires on blur or Enter).
    /// Set to OnInput for real-time updates (useful for search-as-you-type).
    /// </summary>
    [Parameter] public InputBehavior Behavior { get; set; } = InputBehavior.OnChange;

    /// <summary>
    /// Gets or sets the debounce delay in milliseconds.
    /// Only applies when Behavior is OnInput. Set to 0 to disable debouncing.
    /// Useful for reducing API calls in search scenarios.
    /// </summary>
    [Parameter] public int DebounceDelay { get; set; }

    /// <summary>
    /// Gets or sets the text to display before the input.
    /// </summary>
    [Parameter] public string? AddonLeft { get; set; }

    /// <summary>
    /// Gets or sets the text to display after the input.
    /// </summary>
    [Parameter] public string? AddonRight { get; set; }

    private readonly Debouncer _debouncer = new();

    // Track the internal display value separately for OnInput mode
    private string? _internalValue;

    /// <summary>
    /// Gets the effective color for the input, considering validation state.
    /// </summary>
    private TextInputColor EffectiveColor =>
        Color ?? (HasValidationErrors ? TextInputColor.Failure : TextInputColor.Gray);

    private string GetWrapperClasses() => BaseWrapperClasses;

    private string GetFieldClasses() => BaseFieldClasses;

    private string GetAddonClasses(bool isLeft = true)
    {
        return ElementClass.Empty()
            .Add(BaseAddonClasses)
            .Add("rounded-l-md border-r-0", when: isLeft)
            .Add("rounded-r-md border-l-0", when: !isLeft)
            .ToString();
    }

    private string GetIconClasses() => BaseIconClasses;

    private string GetRightIconClasses() => BaseRightIconClasses;

    private string GetSizeClasses() => Size switch
    {
        TextInputSize.Small => "p-2 sm:text-xs",
        TextInputSize.Medium => "p-2.5 text-sm",
        TextInputSize.Large => "p-4 sm:text-base",
        _ => throw new ArgumentOutOfRangeException(nameof(Size), Size, "Invalid TextInputSize value")
    };

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // Sync internal value with external Value when it changes from outside
        // but only if we're not in the middle of debouncing user input
        if (Behavior == InputBehavior.OnInput)
        {
            var externalValue = CurrentValueAsString;
            if (_internalValue != externalValue)
            {
                _internalValue = externalValue;
            }
        }
    }

    private string GetColorClasses() => EffectiveColor switch
    {
        TextInputColor.Gray => "border-gray-300 bg-gray-50 text-gray-900 placeholder-gray-500 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500",
        TextInputColor.Info => "border-cyan-500 bg-cyan-50 text-cyan-900 placeholder-cyan-700 focus:border-cyan-500 focus:ring-cyan-500 dark:border-cyan-400 dark:bg-cyan-100 dark:focus:border-cyan-500 dark:focus:ring-cyan-500",
        TextInputColor.Failure => "border-red-500 bg-red-50 text-red-900 placeholder-red-700 focus:border-red-500 focus:ring-red-500 dark:border-red-400 dark:bg-red-100 dark:focus:border-red-500 dark:focus:ring-red-500",
        TextInputColor.Warning => "border-yellow-500 bg-yellow-50 text-yellow-900 placeholder-yellow-700 focus:border-yellow-500 focus:ring-yellow-500 dark:border-yellow-400 dark:bg-yellow-100 dark:focus:border-yellow-500 dark:focus:ring-yellow-500",
        TextInputColor.Success => "border-green-500 bg-green-50 text-green-900 placeholder-green-700 focus:border-green-500 focus:ring-green-500 dark:border-green-400 dark:bg-green-100 dark:focus:border-green-500 dark:focus:ring-green-500",
        _ => throw new ArgumentOutOfRangeException(nameof(EffectiveColor), EffectiveColor, "Invalid TextInputColor value")
    };

    private string GetInputClasses()
    {
        var hasLeftAddon = !string.IsNullOrEmpty(AddonLeft);
        var hasRightAddon = !string.IsNullOrEmpty(AddonRight);
        var hasBothAddons = hasLeftAddon && hasRightAddon;

        return MergeClasses(
            ElementClass.Empty()
                .Add(BaseInputClasses)
                .Add(GetSizeClasses())
                .Add(GetColorClasses())
                .Add("pl-10", when: Icon != null)
                .Add("pr-10", when: RightIcon != null)
                .Add("border-l-0 border-r-0", when: hasBothAddons)
                .Add("rounded-r-lg border-l-0", when: hasLeftAddon && !hasRightAddon)
                .Add("rounded-l-lg border-r-0", when: hasRightAddon && !hasLeftAddon)
                .Add("rounded-lg", when: !hasLeftAddon && !hasRightAddon)
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
    /// Attempts to parse the provided value string into the component's value type.
    /// </summary>
    protected override bool TryParseValueFromString(
        string? value,
        [MaybeNullWhen(false)] out TValue result,
        [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result))
        {
            validationErrorMessage = null;
            return true;
        }
        else
        {
            validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
            return false;
        }
    }

    /// <summary>
    /// Gets the display value for the input element.
    /// In OnInput mode, uses internal value to avoid cursor jumping.
    /// </summary>
    private string? GetDisplayValue()
    {
        // In OnInput mode with debouncing, use the internal value
        // to prevent cursor position issues during typing
        if (Behavior == InputBehavior.OnInput && _internalValue != null)
        {
            return _internalValue;
        }

        return CurrentValueAsString;
    }

    /// <summary>
    /// Called when input event fires (on every keystroke).
    /// Only used in OnInput behavior mode.
    /// </summary>
    private async Task HandleInputAsync(ChangeEventArgs e)
    {
        var value = e.Value?.ToString();
        _internalValue = value;

        if (DebounceDelay > 0)
        {
            await _debouncer.DebounceAsync(
                () =>
                {
                    CurrentValueAsString = value;
                    return Task.CompletedTask;
                },
                DebounceDelay);
        }
        else
        {
            CurrentValueAsString = value;
        }
    }

    /// <summary>
    /// Called when change event fires (on blur or Enter).
    /// Used in OnChange behavior mode.
    /// </summary>
    private Task HandleChangeAsync(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _debouncer.Dispose();
        base.Dispose();
    }
}
