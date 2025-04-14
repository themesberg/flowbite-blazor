using System;

namespace Flowbite.Components;

/// <summary>
/// Represents options for configuring a toast notification.
/// </summary>
public class ToastOptions
{
    /// <summary>
    /// Gets or sets the type of the toast.
    /// </summary>
    public ToastType Type { get; set; } = ToastType.Default;
    
    /// <summary>
    /// Gets or sets the duration in milliseconds before the toast auto-hides.
    /// Set to 0 to disable auto-hiding.
    /// </summary>
    public int Duration { get; set; } = 3000;
    
    /// <summary>
    /// Gets or sets whether the toast has a close button.
    /// </summary>
    public bool CloseButton { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the toast.
    /// </summary>
    public string? Class { get; set; }
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    public string? DataTestId { get; set; }
}

/// <summary>
/// Represents a toast message.
/// </summary>
public class ToastMessage
{
    /// <summary>
    /// Gets or sets the unique identifier for the toast.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the message content.
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the title of the toast.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Gets or sets the type of the toast.
    /// </summary>
    public ToastType Type { get; set; } = ToastType.Default;
    
    /// <summary>
    /// Gets or sets the duration in milliseconds before the toast auto-hides.
    /// Set to 0 to disable auto-hiding.
    /// </summary>
    public int Duration { get; set; } = 3000;
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the toast.
    /// </summary>
    public string? Class { get; set; }
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    public string? DataTestId { get; set; }
    
    /// <summary>
    /// Gets or sets the custom content for the toast.
    /// </summary>
    public RenderFragment? CustomContent { get; set; }
}
