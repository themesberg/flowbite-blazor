using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Footer row of the prompt input surface hosting actions and submit button.
/// </summary>
public partial class PromptInputFooter : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Footer content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "flex flex-col gap-3 px-1 md:flex-row md:items-center md:justify-between";
}
