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

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex items-center gap-2 rounded-full border border-gray-200 bg-white px-3 py-2 text-sm text-gray-700 " +
        "transition hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-primary-500 dark:border-gray-700 " +
        "dark:bg-gray-800 dark:text-gray-100";

    private Task HandleClick()
    {
        ModelContext.Toggle();
        return Task.CompletedTask;
    }
}
