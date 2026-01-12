using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Container for prompt input utility buttons (model picker, attachments, etc.).
/// </summary>
public partial class PromptInputTools : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Tool content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "flex flex-wrap items-center gap-2 text-sm text-gray-600 dark:text-gray-300";
}
