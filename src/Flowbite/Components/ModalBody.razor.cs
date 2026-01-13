using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Body component for the Modal dialog.
/// </summary>
public partial class ModalBody
{
    /// <summary>
    /// Gets or sets the content of the body.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    
    /// <summary>
    /// Gets the CSS classes for the body.
    /// </summary>
    /// <returns>The CSS classes for the body.</returns>
    private string GetBodyClasses()
    {
        return MergeClasses(
            "flex-1 overflow-auto p-6",
            Class
        );
    }
}
