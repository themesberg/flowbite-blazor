using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Registers menu content for the prompt action menu overlay.
/// </summary>
public partial class PromptInputActionMenuContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Menu content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the content container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputActionMenu Owner { get; set; } = default!;

    protected override void OnParametersSet()
    {
        Owner.RegisterMenu(ChildContent, AdditionalAttributes);
    }
}
