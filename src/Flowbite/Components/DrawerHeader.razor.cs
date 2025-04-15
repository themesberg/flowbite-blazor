using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Header component for the Drawer.
/// </summary>
public partial class DrawerHeader
{
    /// <summary>
    /// Gets or sets the content of the header.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Gets or sets the icon to display in the header.
    /// </summary>
    [Parameter] public RenderFragment? Icon { get; set; }
    
    
    /// <summary>
    /// Gets or sets whether clicking the header toggles the drawer when in edge mode.
    /// </summary>
    [Parameter] public bool ToggleOnClick { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the callback for when the header is clicked.
    /// </summary>
    [Parameter] public EventCallback OnClick { get; set; }
    
    /// <summary>
    /// Gets the drawer context from the cascading value.
    /// </summary>
    [CascadingParameter] private DrawerContext? DrawerContext { get; set; }
    
    /// <summary>
    /// Gets the drawer ID from the drawer context.
    /// </summary>
    private string DrawerId => DrawerContext?.Id ?? "";
    
    /// <summary>
    /// Handles the click event on the header.
    /// </summary>
    private async Task HandleClick()
    {
        if (ToggleOnClick && DrawerContext != null)
        {
            await DrawerContext.CloseAsync();
        }
        
        await OnClick.InvokeAsync();
    }
    
    /// <summary>
    /// Gets the CSS classes for the header.
    /// </summary>
    /// <returns>The CSS classes for the header.</returns>
    private string GetHeaderClasses()
    {
        return CombineClasses(Class);
    }
}
