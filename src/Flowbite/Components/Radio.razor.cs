using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Radio button component for selecting a single option from a group.
/// </summary>
/// <remarks>
/// Provides a standard radio button input with consistent styling and behavior.
/// </remarks>
public partial class Radio
{
    private string BaseClasses => "h-4 w-4 border-gray-300 bg-gray-100 text-primary-600 focus:ring-2 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:ring-offset-gray-800 dark:focus:ring-primary-600 disabled:cursor-not-allowed disabled:opacity-50";

    /// <summary>
    /// Gets or sets whether the radio button is checked.
    /// </summary>
    [Parameter]
    public bool Value { get; set; }

    /// <summary>
    /// Event callback for when the radio button's checked state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the radio button is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the ID of the radio button.
    /// Used for label association.
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the radio button group.
    /// Radio buttons with the same name work as a group where only one can be selected.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    private string RadioClass => string.Join(" ", new[]
    {
        BaseClasses,
        AdditionalAttributes?.ContainsKey("class") == true ? AdditionalAttributes["class"]?.ToString() : null
    }.Where(c => !string.IsNullOrEmpty(c)));
}
