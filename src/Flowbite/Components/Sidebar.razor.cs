namespace Flowbite.Components;

/// <summary>
/// A sidebar navigation component that provides various display modes and responsive behaviors.
/// </summary>
/// <example>
/// Basic usage:
/// <code>
/// <Sidebar>
///     <SidebarItem Href="/dashboard" Icon="@(new HomeIcon())">Dashboard</SidebarItem>
/// </Sidebar>
/// </code>
/// </example>
public partial class Sidebar
{
    /// <summary>
    /// Defines how the sidebar behaves when collapsed
    /// </summary>
    public enum SidebarCollapseBehavior
    {
        /// <summary>
        /// Collapses the sidebar while keeping minimal width
        /// </summary>
        Collapse,

        /// <summary>
        /// Completely hides the sidebar
        /// </summary>
        Hide
    }

    /// <summary>
    /// Defines how the sidebar responds to different screen sizes
    /// </summary>
    public enum SidebarCollapseMode
    {
        /// <summary>
        /// Sidebar maintains its state regardless of screen size
        /// </summary>
        None,

        /// <summary>
        /// Sidebar automatically collapses on mobile devices and expands on larger screens
        /// </summary>
        Responsive
    }

    /// <summary>
    /// The behavior when the sidebar is collapsed. Default is Collapse.
    /// </summary>
    [Parameter]
    public SidebarCollapseBehavior CollapseBehavior { get; set; } = SidebarCollapseBehavior.Collapse;

    /// <summary>
    /// Determines how the sidebar responds to different screen sizes. Default is None.
    /// </summary>
    [Parameter]
    public SidebarCollapseMode CollapseMode { get; set; } = SidebarCollapseMode.None;

    /// <summary>
    /// Whether the sidebar is collapsed. This can be bound using @bind-IsCollapsed.
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// Callback invoked when the collapse state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsCollapsedChanged { get; set; }

    /// <summary>
    /// The content to be rendered inside the sidebar. This can include SidebarItem, 
    /// SidebarCollapse, SidebarItemGroup, SidebarLogo, and SidebarCTA components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetClasses()
    {
        var classes = new List<string>
        {
            "h-full w-64",
            "transition-transform",
            IsCollapsed && CollapseBehavior == SidebarCollapseBehavior.Collapse ? "-translate-x-full" : "",
            CollapseMode == SidebarCollapseMode.Responsive ? "lg:translate-x-0" : ""
        };

        return string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c)));
    }

    /// <summary>
    /// Toggles the collapsed state of the sidebar.
    /// </summary>
    public async Task ToggleAsync()
    {
        IsCollapsed = !IsCollapsed;
        await IsCollapsedChanged.InvokeAsync(IsCollapsed);
    }
}
