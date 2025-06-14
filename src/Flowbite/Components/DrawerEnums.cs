namespace Flowbite.Components;

/// <summary>
/// Defines the position options for a drawer.
/// </summary>
public enum DrawerPosition
{
    /// <summary>
    /// Position the drawer at the top of the screen.
    /// </summary>
    Top,
    
    /// <summary>
    /// Position the drawer at the right of the screen.
    /// </summary>
    Right,
    
    /// <summary>
    /// Position the drawer at the bottom of the screen.
    /// </summary>
    Bottom,
    
    /// <summary>
    /// Position the drawer at the left of the screen (default).
    /// </summary>
    Left
}

/// <summary>
/// Defines the placement options for a drawer.
/// </summary>
public enum DrawerPlacement
{
    /// <summary>
    /// The drawer is placed inside the document flow.
    /// </summary>
    Static,
    
    /// <summary>
    /// The drawer is placed outside the document flow (default).
    /// </summary>
    Fixed
}
