using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class Activity : FlowbiteComponentBase
{
    private string ComponentClasses => CombineClasses("relative ml-3 border-s border-gray-200 dark:border-gray-700");

    /// <summary>
    /// Activity items rendered within the component.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

}
