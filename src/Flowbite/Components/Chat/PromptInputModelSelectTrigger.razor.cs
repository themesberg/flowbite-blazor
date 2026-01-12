using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Toggle button for the model select dropdown.
/// </summary>
public partial class PromptInputModelSelectTrigger : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Trigger content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex h-10 items-center gap-2 rounded-full border border-gray-200/80 bg-white/95 px-3 text-sm font-medium text-gray-600 " +
        "transition-colors hover:border-gray-300 hover:bg-white focus:outline-none focus:ring-2 focus:ring-primary-400/60 " +
        "dark:border-white/10 dark:bg-slate-950/50 dark:text-gray-200 dark:hover:text-white";

    private Task HandleClick()
    {
        ModelContext.Toggle();
        return Task.CompletedTask;
    }
}
