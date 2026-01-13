using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Drawer component for displaying content in a sliding panel.
/// </summary>
public partial class Drawer
{
    /// <summary>
    /// Gets or sets the title of the drawer.
    /// </summary>
    [Parameter] public string? Title { get; set; }
    
    /// <summary>
    /// Gets or sets the content of the drawer.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Gets or sets the header content of the drawer.
    /// </summary>
    [Parameter] public RenderFragment? HeaderContent { get; set; }
    
    /// <summary>
    /// Gets or sets whether the drawer is visible.
    /// </summary>
    [Parameter] public bool Show { get; set; }
    
    /// <summary>
    /// Gets or sets the callback for when the Show parameter changes.
    /// </summary>
    [Parameter] public EventCallback<bool> ShowChanged { get; set; }
    
    /// <summary>
    /// Gets or sets the callback for when the drawer is closed.
    /// </summary>
    [Parameter] public EventCallback OnClose { get; set; }
    
    /// <summary>
    /// Gets or sets the position of the drawer.
    /// </summary>
    [Parameter] public DrawerPosition Position { get; set; } = DrawerPosition.Left;
    
    /// <summary>
    /// Gets or sets whether to show a backdrop behind the drawer.
    /// </summary>
    [Parameter] public bool Backdrop { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether to show a small part of the drawer when it is closed.
    /// </summary>
    [Parameter] public bool Edge { get; set; } = false;
    
    /// <summary>
    /// Gets or sets whether the drawer can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    [Parameter] public bool Dismissible { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether to allow body scrolling when the drawer is open.
    /// </summary>
    [Parameter] public bool BodyScrolling { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    [Parameter] public string? DataTestId { get; set; }
    
    
    /// <summary>
    /// The drawer context to share with child components.
    /// </summary>
    private DrawerContext? _drawerContext;
    
    /// <summary>
    /// Method invoked when the component is initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        _drawerContext = new DrawerContext(Id, Dismissible, CloseAsync, ToggleAsync);
        base.OnInitialized();
    }
    
    /// <summary>
    /// Method invoked when the component parameters are set.
    /// </summary>
    protected override void OnParametersSet()
    {
        // Update the context if parameters change
        _drawerContext = new DrawerContext(Id, Dismissible, CloseAsync, ToggleAsync);
        base.OnParametersSet();
    }
    
    /// <summary>
    /// Method invoked when the component parameters are set.
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        if (Show != IsVisible)
        {
            if (Show)
                await ShowAsync();
            else
                await HideAsync();
        }
    }
    
    /// <summary>
    /// Shows the drawer.
    /// </summary>
    protected new async Task ShowAsync()
    {
        if (!BodyScrolling)
        {
            await JSRuntime.InvokeVoidAsync("flowbiteBlazor.disableBodyScroll");
        }
        
        await base.ShowAsync();
    }
    
    /// <summary>
    /// Hides the drawer.
    /// </summary>
    protected new async Task HideAsync()
    {
        if (!BodyScrolling)
        {
            await JSRuntime.InvokeVoidAsync("flowbiteBlazor.enableBodyScroll");
        }
        
        await base.HideAsync();
    }
    
    /// <summary>
    /// Handles the close button click event.
    /// </summary>
    private async Task HandleCloseClick()
    {
        await CloseAsync();
    }
    
    /// <summary>
    /// Handles the backdrop click event.
    /// </summary>
    private async Task HandleBackdropClick()
    {
        if (Dismissible)
        {
            await CloseAsync();
        }
    }
    
    /// <summary>
    /// Handles the escape key press event.
    /// </summary>
    /// <param name="args">The keyboard event arguments.</param>
    private async Task HandleEscapeKey(KeyboardEventArgs args)
    {
        await HandleEscapeKeyAsync(args);
    }
    
    /// <summary>
    /// Handles the escape key press event.
    /// </summary>
    /// <param name="args">The keyboard event arguments.</param>
    protected override async Task HandleEscapeKeyAsync(KeyboardEventArgs args)
    {
        if (args.Key == "Escape" && Dismissible)
        {
            await CloseAsync();
        }
    }
    
    /// <summary>
    /// Closes the drawer.
    /// </summary>
    private async Task CloseAsync()
    {
        await ShowChanged.InvokeAsync(false);
        await OnClose.InvokeAsync();
    }
    
    /// <summary>
    /// Toggles the drawer between open and closed states.
    /// </summary>
    private async Task ToggleAsync()
    {       
        // Toggle the internal IsVisible state directly
        IsVisible = !IsVisible;
        
        // Also update the parent's binding
        await ShowChanged.InvokeAsync(!Show);
        
        // If we're closing, invoke OnClose
        if (!IsVisible)
        {
            await OnClose.InvokeAsync();
        }
        
        // Force a UI update
        await InvokeAsync(StateHasChanged);
    }
    
    /// <summary>
    /// Gets the CSS classes for the drawer.
    /// </summary>
    /// <returns>The CSS classes for the drawer.</returns>
    private string GetDrawerClasses()
    {
        var baseClasses = "fixed z-[70] overflow-y-auto bg-white p-4 transition-transform duration-300 ease-in-out motion-reduce:transition-none dark:bg-gray-800";
        var positionClasses = GetPositionClasses();
        
        // When drawer is completely hidden (not visible AND not in edge mode), make it invisible and non-interactive
        // This keeps the element in the DOM for CSS transitions while hiding it visually
        var visibilityClasses = (!IsVisible && !Edge) ? "invisible pointer-events-none" : "";
        
        return CombineClasses(baseClasses, positionClasses, visibilityClasses, Class);
    }
    
    /// <summary>
    /// Gets the CSS classes for the drawer position.
    /// </summary>
    /// <returns>The CSS classes for the drawer position.</returns>
    private string GetPositionClasses()
    {
        if (IsVisible)
        {
            // When drawer is fully visible, use transform-none for all positions
            return Position switch
            {
                DrawerPosition.Top => "left-0 right-0 top-0 w-full transform-none",
                DrawerPosition.Right => "right-0 top-0 h-screen w-80 transform-none",
                DrawerPosition.Bottom => "bottom-0 left-0 right-0 w-full transform-none",
                DrawerPosition.Left => "left-0 top-0 h-screen w-80 transform-none",
                _ => "left-0 top-0 h-screen w-80 transform-none" // Default to Left
            };
        }
        else if (Edge)
        {
            // When in edge mode and not visible, show a small part of the drawer
            return Position switch
            {
                DrawerPosition.Top => "left-0 right-0 top-0 w-full -translate-y-[calc(100%-3rem)]",
                DrawerPosition.Right => "right-0 top-0 h-screen w-80 translate-x-[calc(100%-3rem)]",
                DrawerPosition.Bottom => "bottom-0 left-0 right-0 w-full translate-y-[calc(100%-3rem)]",
                DrawerPosition.Left => "left-0 top-0 h-screen w-80 -translate-x-[calc(100%-3rem)]",
                _ => "left-0 top-0 h-screen w-80 -translate-x-[calc(100%-3rem)]" // Default to Left
            };
        }
        else
        {
            // When not visible and not in edge mode, hide completely
            return Position switch
            {
                DrawerPosition.Top => "left-0 right-0 top-0 w-full -translate-y-full",
                DrawerPosition.Right => "right-0 top-0 h-screen w-80 translate-x-full",
                DrawerPosition.Bottom => "bottom-0 left-0 right-0 w-full translate-y-full",
                DrawerPosition.Left => "left-0 top-0 h-screen w-80 -translate-x-full",
                _ => "left-0 top-0 h-screen w-80 -translate-x-full" // Default to Left
            };
        }
    }
}
