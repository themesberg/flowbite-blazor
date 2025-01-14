using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class NavbarToggle
{
    /// <summary>
    /// Child content to be rendered inside the toggle button.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
