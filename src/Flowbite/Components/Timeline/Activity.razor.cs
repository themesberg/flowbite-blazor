using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class Activity : FlowbiteComponentBase
{
    private string ComponentClasses => CombineClasses("relative border-s border-gray-200 dark:border-gray-700");

    /// <summary>
    /// Activity items rendered within the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes to apply to the ordered list element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
