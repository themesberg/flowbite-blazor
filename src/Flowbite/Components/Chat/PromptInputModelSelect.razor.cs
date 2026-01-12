using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provides a lightweight custom select experience for choosing models.
/// </summary>
public partial class PromptInputModelSelect : Flowbite.Base.FlowbiteComponentBase, IDisposable, IAsyncDisposable
{
    private readonly PromptInputModelSelectContext _context;
    private ElementReference _elementRef;
    private DotNetObjectReference<PromptInputModelSelect>? _dotNetRef;
    private IJSObjectReference? _jsModule;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    public PromptInputModelSelect()
    {
        _context = new PromptInputModelSelectContext(OnValueChangedInternalAsync);
        _context.StateChanged += HandleStateChanged;
    }

    /// <summary>
    /// The current selected value.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Raised when the selected value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Content comprising trigger, value, and menu.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "relative inline-flex z-[75]";

    protected override void OnParametersSet()
    {
        _context.UpdateValue(Value);
    }

    private Task OnValueChangedInternalAsync(string? value)
    {
        return ValueChanged.HasDelegate
            ? ValueChanged.InvokeAsync(value)
            : Task.CompletedTask;
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Flowbite/js/promptInputModelSelect.js");
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
