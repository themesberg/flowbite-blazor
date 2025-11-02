using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class Group : FlowbiteComponentBase
{
    /// <summary>
    /// Heading associated with the grouped entries.
    /// </summary>
    [Parameter]
    public string? Date { get; set; }

    /// <summary>
    /// Additional classes applied to the outer container.
    /// </summary>
    [Parameter]
    public string? ContainerClass { get; set; }

    /// <summary>
    /// Additional classes applied to the time element.
    /// </summary>
    [Parameter]
    public string? TimeClass { get; set; }

    /// <summary>
    /// Additional classes applied to the inner ordered list.
    /// </summary>
    [Parameter]
    public string? ListClass { get; set; }

    /// <summary>
    /// Grouped timeline items.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes to apply to the inner ordered list.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ContainerClasses => CombineClasses("p-5 mb-4 bg-gray-50 rounded-lg border border-gray-100 dark:bg-gray-800 dark:border-gray-700", ContainerClass);
    private string TimeClasses => JoinClasses("text-lg font-semibold text-gray-900 dark:text-white", TimeClass);
    private string ListClasses => JoinClasses("mt-3 divide-y divide-gray-200 dark:divide-gray-700", ListClass);

    private static string JoinClasses(params string?[] classes)
    {
        return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}
