using System.Diagnostics.CodeAnalysis;

namespace Flowbite.Components;

/// <summary>
/// Select component for creating dropdown selection inputs with various styles and states.
/// </summary>
/// <remarks>
/// Provides a customizable select element with support for different sizes, colors, and states.
/// Includes features like icons, addons, and helper text.
/// </remarks>
public partial class Select
{
    private const string BaseWrapperClasses = "flex";
    private const string BaseFieldClasses = "relative w-full";
    private const string BaseSelectClasses = "block w-full appearance-none border bg-arrow-down-icon bg-[length:0.75em_0.75em] bg-[position:right_12px_center] bg-no-repeat pr-10 focus:outline-none focus:ring-1 disabled:cursor-not-allowed disabled:opacity-50";
    private const string BaseAddonClasses = "inline-flex items-center rounded-l-md border border-r-0 border-gray-300 bg-gray-200 px-3 text-sm text-gray-900 dark:border-gray-600 dark:bg-gray-600 dark:text-gray-400";
    private const string BaseIconClasses = "pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3";
    private const string BaseHelperTextClasses = "mt-2 text-sm";

    /// <summary>
    /// Gets or sets the color variant of the select.
    /// When not explicitly set, the color will automatically change to Failure when validation errors occur.
    /// </summary>
    [Parameter]
    public SelectColor? Color { get; set; }

    /// <summary>
    /// Gets or sets the size variant of the select.
    /// </summary>
    [Parameter]
    public TextInputSize Size { get; set; } = TextInputSize.Medium;

    /// <summary>
    /// Gets or sets the icon to display in the select.
    /// </summary>
    [Parameter]
    public Type? Icon { get; set; }

    /// <summary>
    /// Gets or sets the addon content.
    /// </summary>
    [Parameter]
    public RenderFragment? Addon { get; set; }

    /// <summary>
    /// Gets or sets the helper text displayed below the select.
    /// </summary>
    [Parameter]
    public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets whether the select has a shadow effect.
    /// </summary>
    [Parameter]
    public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets whether the select is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the child content (options) of the select.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the ID of the select element.
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets the effective color for the select, considering validation state.
    /// </summary>
    private SelectColor EffectiveColor =>
        Color ?? (HasValidationErrors ? SelectColor.Failure : SelectColor.Gray);

    private string GetWrapperClasses() => BaseWrapperClasses;

    private string GetFieldClasses() => BaseFieldClasses;

    private string GetAddonClasses() => BaseAddonClasses;

    private string GetIconClasses() => BaseIconClasses;

    private string GetSizeClasses() => Size switch
    {
        TextInputSize.Small => "p-2 sm:text-xs",
        TextInputSize.Medium => "p-2.5 text-sm",
        TextInputSize.Large => "p-4 sm:text-base",
        _ => throw new ArgumentOutOfRangeException(nameof(Size), Size, "Invalid TextInputSize value")
    };

    private string GetColorClasses() => EffectiveColor switch
    {
        SelectColor.Gray => "border-gray-300 bg-gray-50 text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:focus:border-primary-500 dark:focus:ring-primary-500",
        SelectColor.Info => "border-cyan-500 bg-cyan-50 text-cyan-900 focus:border-cyan-500 focus:ring-cyan-500 dark:border-cyan-400 dark:bg-cyan-100 dark:focus:border-cyan-500 dark:focus:ring-cyan-500",
        SelectColor.Failure => "border-red-500 bg-red-50 text-red-900 focus:border-red-500 focus:ring-red-500 dark:border-red-400 dark:bg-red-100 dark:focus:border-red-500 dark:focus:ring-red-500",
        SelectColor.Warning => "border-yellow-500 bg-yellow-50 text-yellow-900 focus:border-yellow-500 focus:ring-yellow-500 dark:border-yellow-400 dark:bg-yellow-100 dark:focus:border-yellow-500 dark:focus:ring-yellow-500",
        SelectColor.Success => "border-green-500 bg-green-50 text-green-900 focus:border-green-500 focus:ring-green-500 dark:border-green-400 dark:bg-green-100 dark:focus:border-green-500 dark:focus:ring-green-500",
        _ => throw new ArgumentOutOfRangeException(nameof(EffectiveColor), EffectiveColor, "Invalid SelectColor value")
    };

    private string GetSelectClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add(BaseSelectClasses)
                .Add(GetSizeClasses())
                .Add(GetColorClasses())
                .Add("pl-10", when: Icon != null)
                .Add("rounded-r-lg", when: Addon != null)
                .Add("rounded-lg", when: Addon == null)
                .Add("shadow-sm dark:shadow-sm-light", when: Shadow)
                .Add(Class)
                .Add(CssClass)
        );
    }

    private string GetHelperTextColorClasses() => EffectiveColor switch
    {
        SelectColor.Gray => "text-gray-600 dark:text-gray-400",
        SelectColor.Info => "text-cyan-600 dark:text-cyan-500",
        SelectColor.Failure => "text-red-600 dark:text-red-500",
        SelectColor.Warning => "text-yellow-600 dark:text-yellow-500",
        SelectColor.Success => "text-green-600 dark:text-green-500",
        _ => throw new ArgumentOutOfRangeException(nameof(EffectiveColor), EffectiveColor, "Invalid SelectColor value")
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
    /// For select, the value is always a string, so parsing always succeeds.
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

    /// <summary>
    /// Disposes the component and unsubscribes from validation state changes.
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
    }
}
