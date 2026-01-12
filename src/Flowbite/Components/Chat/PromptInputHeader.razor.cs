using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Optional header slot of the prompt input surface, typically used for attachments.
/// </summary>
public partial class PromptInputHeader : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Content rendered within the header.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "flex flex-col gap-2 overflow-hidden px-1";
}
