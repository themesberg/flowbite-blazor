using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Main body area of the prompt input surface.
/// </summary>
public partial class PromptInputBody : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Body content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "flex flex-col gap-3 px-1";
}
