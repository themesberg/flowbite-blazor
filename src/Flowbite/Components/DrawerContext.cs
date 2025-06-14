using System;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Context class for sharing state between Drawer and its child components.
/// </summary>
public class DrawerContext
{
    /// <summary>
    /// Gets the unique identifier for the drawer.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets whether the drawer can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    public bool Dismissible { get; }
    
    /// <summary>
    /// Gets the function to close the drawer.
    /// </summary>
    public Func<Task> CloseAsync { get; }
    
    /// <summary>
    /// Gets the function to toggle the drawer.
    /// </summary>
    public Func<Task> ToggleAsync { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawerContext"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the drawer.</param>
    /// <param name="dismissible">Whether the drawer can be dismissed by clicking outside or pressing Escape.</param>
    /// <param name="closeAsync">The function to close the drawer.</param>
    /// <param name="toggleAsync">The function to toggle the drawer.</param>
    public DrawerContext(string id, bool dismissible, Func<Task> closeAsync, Func<Task> toggleAsync)
    {
        Id = id;
        Dismissible = dismissible;
        CloseAsync = closeAsync;
        ToggleAsync = toggleAsync;
    }
}
