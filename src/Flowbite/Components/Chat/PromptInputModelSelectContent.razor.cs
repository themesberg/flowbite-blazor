using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Dropdown list container for model selection.
/// </summary>
public partial class PromptInputModelSelectContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// List items content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the list container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string BaseClasses =>
        "absolute left-0 top-full z-30 mt-2 w-48 rounded-xl border border-gray-200 bg-white py-2 shadow-lg " +
        "dark:border-gray-700 dark:bg-gray-800";
}
