using Flowbite.Base;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// An empty state component for displaying placeholder content when no data is available.
/// </summary>
/// <remarks>
/// The EmptyState component provides a consistent pattern for showing users that a list,
/// table, or section is empty. It supports custom icons, titles, descriptions, and
/// action buttons to guide users on next steps.
/// </remarks>
/// <example>
/// <code>
/// &lt;EmptyState Title="No users found"
///             Description="Try adjusting your search criteria."&gt;
///     &lt;Action&gt;
///         &lt;Button OnClick="@AddUser"&gt;Add user&lt;/Button&gt;
///     &lt;/Action&gt;
/// &lt;/EmptyState&gt;
/// </code>
/// </example>
public partial class EmptyState : FlowbiteComponentBase
{
    /// <summary>
    /// The title text displayed in the empty state.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "No results found";

    /// <summary>
    /// Optional description text providing additional context.
    /// </summary>
    [Parameter]
    public string? Description { get; set; }

    /// <summary>
    /// Custom icon content to replace the default inbox icon.
    /// </summary>
    [Parameter]
    public RenderFragment? Icon { get; set; }

    /// <summary>
    /// Optional action content, typically containing buttons.
    /// </summary>
    [Parameter]
    public RenderFragment? Action { get; set; }

    private string ComputedClass => MergeClasses(
        ElementClass.Empty()
            .Add("flex flex-col items-center justify-center py-12 px-4")
            .Add(Class)
    );
}
