using Flowbite.Utilities;
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
    /// Optional icon to display at the start of the alert.
    /// The icon will inherit the alert's color scheme.
    /// </summary>
    [Parameter]
    public IconBase? Icon { get; set; }
    
    
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
            await DrawerContext.ToggleAsync();
        }
        
        await OnClick.InvokeAsync();
    }
    
    /// <summary>
    /// Gets or sets whether the drawer is in edge mode.
    /// </summary>
    [CascadingParameter(Name = "EdgeMode")] public bool EdgeMode { get; set; }
    
    /// <summary>
    /// Gets or sets whether the drawer is visible.
    /// </summary>
    [CascadingParameter(Name = "IsVisible")] public bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets whether the drawer is visible.
    /// </summary>
    [CascadingParameter(Name = "Position")] public DrawerPosition Position { get; set; }
    
    /// <summary>
    /// Gets the CSS classes for the header.
    /// </summary>
    /// <returns>The CSS classes for the header.</returns>
    private string GetHeaderClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add("absolute top-0 left-0 right-0 bottom-0 bg-gray-50 dark:bg-gray-800 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-700 rounded-md p-2 pl-3 flex flex-col justify-between", when: EdgeMode && !IsVisible)
                .Add(Class)
        );
    }
}
