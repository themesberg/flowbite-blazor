using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Scrollable container that holds the conversation message stream.
/// </summary>
public partial class ConversationContent : Flowbite.Base.FlowbiteComponentBase
{
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

    /// <summary>
    /// Determines how automatic scrolling behaves when new content is rendered.
    /// </summary>
    [Parameter]
    public ConversationAutoScrollBehavior AutoScrollBehavior { get; set; } = ConversationAutoScrollBehavior.Always;

    [CascadingParameter] private ConversationContext ConversationContext { get; set; } = default!;

    private string BaseClasses => "relative flex-1";

    private string? RoleAttribute => HasAdditionalAttribute("role") ? null : "log";

    private string? AriaLiveAttribute => HasAdditionalAttribute("aria-live") ? null : "polite";

    private string? AriaRelevantAttribute => HasAdditionalAttribute("aria-relevant") ? null : "additions text";

    private string? AriaAtomicAttribute => HasAdditionalAttribute("aria-atomic") ? null : "false";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!AutoScroll || ConversationContext.ScrollToBottom is null)
        {
            return;
        }

        if (AutoScrollBehavior == ConversationAutoScrollBehavior.Always ||
            (AutoScrollBehavior == ConversationAutoScrollBehavior.StickToBottom && ConversationContext.IsAtBottom))
        {
            await ConversationContext.ScrollToBottom.Invoke();
        }
    }

    private bool HasAdditionalAttribute(string attributeName)
    {
        if (AdditionalAttributes is null || AdditionalAttributes.Count == 0)
        {
            return false;
        }

        foreach (var key in AdditionalAttributes.Keys)
        {
            if (string.Equals(key, attributeName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}
