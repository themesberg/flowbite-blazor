using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Represents a single cited reference.
/// </summary>
public partial class Source : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Destination URL of the source.
    /// </summary>
    [Parameter, EditorRequired]
    public string Href { get; set; } = string.Empty;

    /// <summary>
    /// Source display content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the anchor element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "inline-flex items-center gap-2 text-primary-600 hover:underline dark:text-primary-300";
}
