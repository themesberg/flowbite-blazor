using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Dropdown list container for model selection.
/// </summary>
public partial class PromptInputModelSelectContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// List items content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string BaseClasses =>
        "absolute left-0 bottom-full mb-2 z-[70] mt-2 w-60 rounded-2xl border border-gray-200/80 bg-white/98 p-2 shadow-[0_24px_45px_-24px_rgba(15,23,42,0.45)] " +
        "backdrop-blur-sm dark:border-white/10 dark:bg-slate-950/90 max-h-80 overflow-y-auto";
}
