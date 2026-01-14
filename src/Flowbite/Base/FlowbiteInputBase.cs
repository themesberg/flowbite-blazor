using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TailwindMerge;

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
    /// TailwindMerge service for intelligent CSS class conflict resolution.
    /// </summary>
    [Inject]
    internal TwMerge TwMerge { get; set; } = default!;

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
    /// Merges multiple CSS class strings using TailwindMerge for intelligent conflict resolution.
    /// Later classes override earlier ones when conflicts occur (e.g., bg-gray-50 vs bg-red-50).
    /// </summary>
    /// <param name="classes">The CSS classes to merge</param>
    /// <returns>A merged string of CSS classes with conflicts resolved</returns>
    protected string MergeClasses(params string?[] classes)
    {
        var combined = string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
        return TwMerge.Merge(combined) ?? string.Empty;
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
