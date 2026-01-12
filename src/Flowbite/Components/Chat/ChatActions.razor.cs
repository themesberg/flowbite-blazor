using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Container for message level action buttons (copy, regenerate, etc).
/// </summary>
public partial class ChatActions : Flowbite.Base.FlowbiteComponentBase
{
    private string BaseClasses => "mt-2 flex items-center gap-1";

    /// <summary>
    /// Content to render inside the actions container.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

}
