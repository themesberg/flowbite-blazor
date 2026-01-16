using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using TailwindMerge;

namespace Flowbite.Base;

/// <summary>
/// Base class for Flowbite Blazor components, providing common functionality and styling.
/// </summary>
public abstract class FlowbiteComponentBase : ComponentBase
{
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
    /// Inline CSS styles to apply to the component's root element.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>
    /// Additional HTML attributes to apply to the component's root element.
    /// Component-set attributes take precedence over these values.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

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
    /// Later classes override earlier ones when conflicts occur (e.g., px-4 vs px-6).
    /// </summary>
    /// <param name="classes">The CSS classes to merge</param>
    /// <returns>A merged string of CSS classes with conflicts resolved</returns>
    protected string MergeClasses(params string?[] classes)
    {
        var combined = string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
        return TwMerge.Merge(combined) ?? string.Empty;
    }
}
