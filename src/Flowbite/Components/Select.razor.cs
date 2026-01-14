using Microsoft.AspNetCore.Components;
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
    private string BaseClasses => "block w-full text-sm border rounded-lg disabled:cursor-not-allowed disabled:opacity-50";

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

    private string ThemeClass => "relative";

    private string SelectClass => MergeClasses(
        BaseClasses,
        GetSizeClasses(),
        GetColorClasses(),
        Shadow ? "shadow-sm" : null,
        Icon != null ? "pl-10" : null,
        Addon != null ? "rounded-l-none" : null
    );

    private string GetSizeClasses() => Size switch
    {
        TextInputSize.Small => "p-2 text-xs",
        TextInputSize.Large => "p-4 text-base",
        _ => "p-2.5 text-sm"
    };

    private string GetColorClasses() => EffectiveColor switch
    {
        SelectColor.Info => "bg-blue-50 border-blue-500 text-blue-900 focus:ring-blue-500 focus:border-blue-500 dark:bg-blue-100 dark:border-blue-400",
        SelectColor.Success => "bg-green-50 border-green-500 text-green-900 focus:ring-green-500 focus:border-green-500 dark:bg-green-100 dark:border-green-400",
        SelectColor.Warning => "bg-yellow-50 border-yellow-500 text-yellow-900 focus:ring-yellow-500 focus:border-yellow-500 dark:bg-yellow-100 dark:border-yellow-400",
        SelectColor.Failure => "bg-red-50 border-red-500 text-red-900 focus:ring-red-500 focus:border-red-500 dark:bg-red-100 dark:border-red-400",
        _ => "bg-gray-50 border-gray-300 text-gray-900 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:text-white"
    };

    private string GetHelperTextColorClass() => EffectiveColor switch
    {
        SelectColor.Info => "text-blue-600 dark:text-blue-500",
        SelectColor.Success => "text-green-600 dark:text-green-500",
        SelectColor.Warning => "text-yellow-600 dark:text-yellow-500",
        SelectColor.Failure => "text-red-600 dark:text-red-500",
        _ => "text-gray-600 dark:text-gray-400"
    };

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
}
