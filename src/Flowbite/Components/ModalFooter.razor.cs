using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Footer component for the Modal dialog.
/// </summary>
public partial class ModalFooter
{
    /// <summary>
    /// Gets or sets the content of the footer.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    
    /// <summary>
    /// Gets the CSS classes for the footer.
    /// </summary>
    /// <returns>The CSS classes for the footer.</returns>
    private string GetFooterClasses()
    {
        return CombineClasses(
            "flex items-center space-x-2 rounded-b border-t border-gray-200 p-6 dark:border-gray-600",
            Class
        );
    }
}
