using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Toggle button for the action menu dropdown.
/// </summary>
public partial class PromptInputActionMenuTrigger : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Optional custom trigger content; defaults to a plus icon.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the trigger button.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex h-10 w-10 items-center justify-center rounded-full border border-gray-200/80 bg-white/95 text-gray-500 " +
        "transition hover:border-gray-300 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-primary-400/60 " +
        "dark:border-white/10 dark:bg-slate-950/60 dark:text-gray-300 dark:hover:text-white";

    private Task HandleClick()
    {
        MenuContext.Toggle();
        return Task.CompletedTask;
    }
}
