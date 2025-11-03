using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Dropdown content for the prompt input action menu.
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

    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "absolute left-0 top-full z-30 mt-2 w-40 rounded-xl border border-gray-200 bg-white p-2 shadow-lg " +
        "dark:border-gray-700 dark:bg-gray-800";
}
