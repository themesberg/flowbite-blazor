using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Flowbite.Base;

/// <summary>
/// Base class for Flowbite form input components that integrate with Blazor's EditForm validation system.
/// Inherits from <see cref="InputBase{TValue}"/> to provide automatic validation integration, EditContext support, and field identification.
/// </summary>
/// <typeparam name="TValue">The type of value the input component handles</typeparam>
public abstract class FlowbiteInputBase<TValue> : InputBase<TValue>, IDisposable
{
    private EventHandler<ValidationStateChangedEventArgs>? _validationStateChangedHandler;

    /// <summary>
    /// Additional CSS classes to apply to the component.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Inline styles to apply to the component.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Combines multiple CSS class strings with additional user-provided classes.
    /// </summary>
    /// <param name="classes">The CSS classes to combine</param>
    /// <returns>A combined string of CSS classes</returns>
    protected string CombineClasses(params string?[] classes)
    {
        var allClasses = classes.Concat(new[] { Class }).Where(c => !string.IsNullOrWhiteSpace(c));
        return string.Join(" ", allClasses).Trim();
    }

    /// <summary>
    /// Checks if the current field has validation errors.
    /// </summary>
    protected bool HasValidationErrors =>
        EditContext?.GetValidationMessages(FieldIdentifier).Any() ?? false;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (EditContext != null)
        {
            _validationStateChangedHandler = (sender, args) => StateHasChanged();
            EditContext.OnValidationStateChanged += _validationStateChangedHandler;
        }
    }

    /// <summary>
    /// Disposes the component and unsubscribes from validation state changes.
    /// </summary>
    public void Dispose()
    {
        if (EditContext != null && _validationStateChangedHandler != null)
        {
            EditContext.OnValidationStateChanged -= _validationStateChangedHandler;
        }
    }
}
