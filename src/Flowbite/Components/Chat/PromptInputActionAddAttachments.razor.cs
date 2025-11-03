using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Action menu item that triggers the file picker for attachments.
/// </summary>
public partial class PromptInputActionAddAttachments : Flowbite.Base.FlowbiteComponentBase
{
    [CascadingParameter] private PromptInputContext PromptContext { get; set; } = default!;

    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "flex w-full items-center gap-3 rounded-lg px-3 py-2 text-sm text-gray-700 transition hover:bg-gray-100 " +
        "focus:outline-none focus:ring-2 focus:ring-primary-500 dark:text-gray-200 dark:hover:bg-gray-700";

    private async Task HandleClickAsync()
    {
        await PromptContext.TriggerFilePickerAsync();
        MenuContext.Close();
    }
}
