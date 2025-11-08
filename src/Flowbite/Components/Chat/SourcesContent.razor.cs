using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Collapsible list of source links.
/// </summary>
public partial class SourcesContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Source items content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private SourcesContext SourcesContext { get; set; } = default!;

    private string BaseClasses => "mt-2 flex flex-col gap-2 text-sm text-gray-700 dark:text-gray-200";
}
