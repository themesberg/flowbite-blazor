using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Components.Chat;

/// <summary>
/// Wraps conversational content and provides shared scroll behaviors for related components.
/// </summary>
public partial class Conversation : Flowbite.Base.FlowbiteComponentBase, IAsyncDisposable
{
    private readonly ConversationContext _context = new();
    private IJSObjectReference? _module;
    private DotNetObjectReference<Conversation>? _dotNetReference;

    /// <summary>
    /// Child content to render inside the conversation container.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes for the conversation container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    private string BaseClasses => "relative flex flex-col gap-4";

    protected override void OnInitialized()
    {
        _context.ScrollToBottom = ScrollToBottomAsync;
        _context.ContentRegistered += HandleContentRegistered;
    }

    private async void HandleContentRegistered(ElementReference element)
    {
        try
        {
            await EnsureModuleAsync();
            _dotNetReference ??= DotNetObjectReference.Create(this);
            if (_module is not null)
            {
                await _module.InvokeVoidAsync("registerConversation", element, _dotNetReference);
            }
        }
        catch (JSDisconnectedException)
        {
            // Fail silently if JS runtime is unavailable (e.g., prerendering)
        }
    }

    private async Task ScrollToBottomAsync()
    {
        if (!_context.HasContentElement)
        {
            return;
        }

        await EnsureModuleAsync();
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("scrollToBottom", _context.ContentElement);
        }
    }

    private async Task EnsureModuleAsync()
    {
        _module ??= await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/Flowbite/js/chatConversation.js");
    }

    /// <inheritdoc />
    [JSInvokable]
    public void UpdateIsAtBottom(bool isAtBottom)
    {
        _context.UpdateIsAtBottom(isAtBottom);
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            if (_context.HasContentElement)
            {
                await _module.InvokeVoidAsync("unregisterConversation", _context.ContentElement);
            }
            await _module.DisposeAsync();
        }
        _dotNetReference?.Dispose();
        _context.ContentRegistered -= HandleContentRegistered;
    }
}
