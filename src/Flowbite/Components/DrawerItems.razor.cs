using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Items container component for the Drawer.
/// </summary>
public partial class DrawerItems
{
    /// <summary>
    /// Gets or sets the content of the items container.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    
    /// <summary>
    /// Gets the drawer context from the cascading value.
    /// </summary>
    [CascadingParameter] private DrawerContext? DrawerContext { get; set; }
    
    /// <summary>
    /// Gets the CSS classes for the items container.
    /// </summary>
    /// <returns>The CSS classes for the items container.</returns>
    private string GetItemsClasses()
    {
        return CombineClasses(Class);
    }
}
