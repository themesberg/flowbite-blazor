namespace Flowbite.Components;

/// <summary>
/// Defines the type options for a toast notification.
/// </summary>
public enum ToastType
{
    /// <summary>
    /// Default toast type with no icon.
    /// </summary>
    Default,
    
    /// <summary>
    /// Informational toast with blue info icon.
    /// </summary>
    Info,
    
    /// <summary>
    /// Success toast with green checkmark icon.
    /// </summary>
    Success,
    
    /// <summary>
    /// Warning toast with yellow warning icon.
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error toast with red X icon.
    /// </summary>
    Error
}

/// <summary>
/// Defines the position options for toast notifications.
/// </summary>
public enum ToastPosition
{
    /// <summary>
    /// Position toasts at the top left of the screen.
    /// </summary>
    TopLeft,
    
    /// <summary>
    /// Position toasts at the top center of the screen.
    /// </summary>
    TopCenter,
    
    /// <summary>
    /// Position toasts at the top right of the screen (default).
    /// </summary>
    TopRight,
    
    /// <summary>
    /// Position toasts at the bottom left of the screen.
    /// </summary>
    BottomLeft,
    
    /// <summary>
    /// Position toasts at the bottom center of the screen.
    /// </summary>
    BottomCenter,
    
    /// <summary>
    /// Position toasts at the bottom right of the screen.
    /// </summary>
    BottomRight
}
