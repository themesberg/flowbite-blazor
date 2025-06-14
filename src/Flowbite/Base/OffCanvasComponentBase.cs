using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Flowbite.Base;

/// <summary>
/// Base class for off-canvas components such as modals, drawers, and toasts.
/// Provides common functionality for visibility management and focus handling.
/// </summary>
public abstract class OffCanvasComponentBase : FlowbiteComponentBase
{
    /// <summary>
    /// JavaScript runtime for interop operations.
    /// </summary>
    [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;
    
    /// <summary>
    /// Unique identifier for the component.
    /// </summary>
    protected string Id { get; } = $"flowbite-offcanvas-{Guid.NewGuid()}";
    
    /// <summary>
    /// Indicates whether the component is currently visible.
    /// </summary>
    protected bool IsVisible { get; set; }
    
    /// <summary>
    /// Initializes the component after first render.
    /// </summary>
    /// <param name="firstRender">True if this is the first time the method is invoked.</param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitializeComponentAsync();
        }
    }
    
    /// <summary>
    /// Initializes the component with any required setup.
    /// </summary>
    protected virtual Task InitializeComponentAsync()
    {
        // Base initialization - can be overridden by derived classes
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Shows the component and traps focus within it.
    /// </summary>
    protected async Task ShowAsync()
    {
        IsVisible = true;
        await InvokeAsync(StateHasChanged);
        await JSRuntime.InvokeVoidAsync("flowbiteBlazor.trapFocus", Id);
    }
    
    /// <summary>
    /// Hides the component and restores focus to the previously focused element.
    /// </summary>
    protected async Task HideAsync()
    {
        IsVisible = false;
        await InvokeAsync(StateHasChanged);
        await JSRuntime.InvokeVoidAsync("flowbiteBlazor.restoreFocus");
    }
    
    /// <summary>
    /// Handles the Escape key press to close the component if dismissible.
    /// </summary>
    /// <param name="args">Keyboard event arguments.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected virtual Task HandleEscapeKeyAsync(KeyboardEventArgs args)
    {
        // To be implemented by derived classes
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Cleans up any resources used by the component.
    /// </summary>
    public virtual ValueTask DisposeAsync()
    {
        if (JSRuntime != null)
        {
            return JSRuntime.InvokeVoidAsync("flowbiteBlazor.cleanupElement", Id);
        }
        
        return ValueTask.CompletedTask;
    }
}
