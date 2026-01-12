using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Button that scrolls the conversation content to the latest message.
/// </summary>
public partial class ConversationScrollButton : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    /// <summary>
    /// Indicates whether the scroll button is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    [CascadingParameter] private ConversationContext ConversationContext { get; set; } = default!;

    private string BaseClasses =>
        "sticky left-1/2 bottom-6 z-30 inline-flex h-10 w-10 -translate-x-1/2 items-center justify-center rounded-full " +
        "border border-gray-200 bg-white shadow-lg transition hover:bg-gray-50 focus:outline-none " +
        "focus:ring-2 focus:ring-primary-500 disabled:pointer-events-none disabled:opacity-50 " +
        "dark:border-gray-700 dark:bg-gray-800 dark:hover:bg-gray-700";

    private bool ShouldRenderButton => !ConversationContext.IsAtBottom && !Disabled;

    protected override void OnInitialized()
    {
        ConversationContext.AtBottomChanged += HandleAtBottomChanged;
    }

    private void HandleAtBottomChanged(bool _)
    {
        InvokeAsync(StateHasChanged);
    }

    private async Task HandleScrollClick()
    {
        if (Disabled || ConversationContext.ScrollToBottom is null)
        {
            return;
        }

        await ConversationContext.ScrollToBottom.Invoke();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        ConversationContext.AtBottomChanged -= HandleAtBottomChanged;
    }
}
