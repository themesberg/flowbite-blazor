using Microsoft.AspNetCore.Components;
using Flowbite.Utilities;

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

    private string ContainerClasses => MergeClasses("p-5 mb-4 bg-gray-50 rounded-lg border border-gray-100 dark:bg-gray-800 dark:border-gray-700", ContainerClass);
    private string TimeClasses => MergeClasses("text-lg font-semibold text-gray-900 dark:text-white", TimeClass);
    private string ListClasses => MergeClasses("mt-3 divide-y divide-gray-200 dark:divide-gray-700", ListClass);
}
