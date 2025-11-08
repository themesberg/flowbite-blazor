using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Simple animated spinner used for chat interactions.
/// </summary>
public partial class Loader : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Loader size in pixels. Defaults to 16.
    /// </summary>
    [Parameter]
    public int Size { get; set; } = 16;

    /// <summary>
    /// Additional attributes applied to the wrapper element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "inline-flex items-center justify-center text-primary-500";
}
