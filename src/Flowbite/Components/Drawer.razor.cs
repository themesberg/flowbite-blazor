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
        _drawerContext = new DrawerContext(Id, Dismissible, CloseAsync);
        base.OnInitialized();
    }
    
    /// <summary>
    /// Method invoked when the component parameters are set.
    /// </summary>
    protected override void OnParametersSet()
    {
        // Update the context if parameters change
        _drawerContext = new DrawerContext(Id, Dismissible, CloseAsync);
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
    /// Gets the CSS classes for the drawer.
    /// </summary>
    /// <returns>The CSS classes for the drawer.</returns>
    private string GetDrawerClasses()
    {
        // p-16 fixed inset-0 z-50 h-screen md:inset-0 md:h-full flex bg-gray-900/50 dark:bg-gray-900/80 items-center justify-center
        var baseClasses = "fixed z-[70] overflow-y-auto bg-white p-4 transition-transform dark:bg-gray-800";
        var positionClasses = GetPositionClasses();
        var edgeClass = Edge && !IsVisible ? "bottom-16" : "";
        
        return CombineClasses(baseClasses, positionClasses, edgeClass, Class);
    }
    
    /// <summary>
    /// Gets the CSS classes for the drawer position.
    /// </summary>
    /// <returns>The CSS classes for the drawer position.</returns>
    private string GetPositionClasses()
    {
        return Position switch
        {
            DrawerPosition.Top when IsVisible => "left-0 right-0 top-0 w-full transform-none",
            DrawerPosition.Top => "left-0 right-0 top-0 w-full -translate-y-full",
            
            DrawerPosition.Right when IsVisible => "right-0 top-0 h-screen w-80 transform-none",
            DrawerPosition.Right => "right-0 top-0 h-screen w-80 translate-x-full",
            
            DrawerPosition.Bottom when IsVisible => "bottom-0 left-0 right-0 w-full transform-none",
            DrawerPosition.Bottom => "bottom-0 left-0 right-0 w-full translate-y-full",
            
            DrawerPosition.Left when IsVisible => "left-0 top-0 h-screen w-80 transform-none",
            DrawerPosition.Left => "left-0 top-0 h-screen w-80 -translate-x-full",
            
            _ => "left-0 top-0 h-screen w-80 -translate-x-full" // Default to Left
        };
    }
}
