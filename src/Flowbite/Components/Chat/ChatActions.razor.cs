using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Container for message level action buttons (copy, regenerate, etc).
/// </summary>
public partial class ChatActions : Flowbite.Base.FlowbiteComponentBase
{
    private string BaseClasses => "flex items-center gap-1";

    /// <summary>
    /// Content to render inside the actions container.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
