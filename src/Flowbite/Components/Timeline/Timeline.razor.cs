using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class Timeline : FlowbiteComponentBase
{
    private string ComponentClasses => MergeClasses(
        ElementClass.Empty()
            .Add(GetBaseClasses())
            .Add(Class));

    /// <summary>
    /// Determines the visual layout of the timeline.
    /// </summary>
    [Parameter]
    public TimelineOrder Order { get; set; } = TimelineOrder.Default;

    /// <summary>
    /// Content rendered inside the timeline.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetBaseClasses() => Order switch
    {
        TimelineOrder.Default => "relative border-s border-gray-200 dark:border-gray-700",
        TimelineOrder.Vertical => "relative",
        TimelineOrder.Horizontal => "sm:flex",
        TimelineOrder.Activity => "relative",
        _ => "relative border-s border-gray-200 dark:border-gray-700"
    };
}
