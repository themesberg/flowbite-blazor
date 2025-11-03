using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Scrollable container that holds the conversation message stream.
/// </summary>
public partial class ConversationContent : Flowbite.Base.FlowbiteComponentBase
{
    private ElementReference _contentElement;
    private bool _contentRegistered;

    /// <summary>
    /// Content to render inside the scrollable conversation container.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Optional attributes applied to the outer container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Automatically scrolls to the bottom after each render. Enabled by default.
    /// </summary>
    [Parameter]
    public bool AutoScroll { get; set; } = true;

    [CascadingParameter] private ConversationContext ConversationContext { get; set; } = default!;

    private string BaseClasses =>
        "relative flex-1 overflow-y-auto rounded-2xl border border-gray-200 bg-white p-6 shadow-sm " +
        "dark:border-gray-700 dark:bg-gray-900";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!_contentRegistered)
        {
            ConversationContext.RegisterContent(_contentElement);
            _contentRegistered = true;
        }

        if (AutoScroll && ConversationContext.ScrollToBottom is not null)
        {
            await ConversationContext.ScrollToBottom.Invoke();
        }
    }
}
