using Microsoft.AspNetCore.Components;

namespace Flowbite.Base;

/// <summary>
/// Base class for icon components providing common functionality.
/// </summary>
public abstract class IconBase : FlowbiteComponentBase
{
    /// <summary>
    /// Whether the icon should be hidden from screen readers.
    /// </summary>
    [Parameter]
    public bool AriaHidden { get; set; } = true;

    /// <summary>
    /// The stroke width of the SVG icon.
    /// </summary>
    [Parameter]
    public double StrokeWidth { get; set; } = 2.0;

    /// <summary>
    /// Gets the combined CSS classes including base and additional classes.
    /// </summary>
    protected string CombinedClassNames
    {
        get
        {
            return CombineClasses(Class is not null ? Class : "w-6 h-6 text-current");
        }
    }
}
