using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provider component for the prompt input action menu.
/// </summary>
public partial class PromptInputActionMenu : Flowbite.Base.FlowbiteComponentBase, IDisposable, IAsyncDisposable
{
    private readonly PromptInputActionMenuContext _context = new();
    private ElementReference _elementRef;
    private DotNetObjectReference<PromptInputActionMenu>? _dotNetRef;
    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    public PromptInputActionMenu()
    {
        _context.StateChanged += HandleStateChanged;
    }

    /// <summary>
    /// Content for the menu provider.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "relative inline-flex z-[75]";

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Flowbite/js/promptInputActionMenu.js");
            await _jsModule.InvokeVoidAsync("initialize", _elementRef, _dotNetRef);
        }

        if (_context.IsOpen && _jsModule != null)
        {
            await _jsModule.InvokeVoidAsync("setupClickOutside", _elementRef, _dotNetRef);
        }
    }

    [JSInvokable]
    public void OnClickOutside()
    {
        _context.Close();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _context.StateChanged -= HandleStateChanged;
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        _context.StateChanged -= HandleStateChanged;

        if (_jsModule != null)
        {
            await _jsModule.InvokeVoidAsync("dispose");
            await _jsModule.DisposeAsync();
        }

        _dotNetRef?.Dispose();
    }
}
