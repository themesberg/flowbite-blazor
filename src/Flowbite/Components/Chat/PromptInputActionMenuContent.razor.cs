using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Dropdown menu container for action menu.
/// </summary>
public partial class PromptInputActionMenuContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Menu content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the content container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "absolute left-0 bottom-full mb-2 z-[70] flex w-48 flex-col gap-1 rounded-2xl border border-gray-200/80 " +
        "bg-white/98 p-2 shadow-[0_24px_45px_-28px_rgba(15,23,42,0.55)] backdrop-blur-sm focus:outline-none " +
        "dark:border-white/10 dark:bg-slate-950/90";
}
