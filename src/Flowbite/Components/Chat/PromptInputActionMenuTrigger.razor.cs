using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Button that toggles the prompt input action menu.
/// </summary>
public partial class PromptInputActionMenuTrigger : Flowbite.Base.FlowbiteComponentBase
{
    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex h-9 w-9 items-center justify-center rounded-full border border-transparent text-gray-500 " +
        "transition hover:bg-gray-100 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-primary-500 " +
        "dark:text-gray-400 dark:hover:bg-gray-800 dark:hover:text-white";

    private Task HandleClickAsync()
    {
        MenuContext.Toggle();
        return Task.CompletedTask;
    }
}
