using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Heading component for rendering semantic HTML headings (h1-h6) with consistent styling.
/// </summary>
public partial class Heading
{
    /// <summary>
    /// Gets or sets the HTML heading tag to render (h1-h6).
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading Tag="HeadingTag.H1"&gt;Page Title&lt;/Heading&gt;
    /// &lt;Heading Tag="HeadingTag.H3"&gt;Section Title&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public HeadingTag Tag { get; set; } = HeadingTag.H1;

    /// <summary>
    /// Gets or sets the child content of the heading.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading Tag="HeadingTag.H2"&gt;My Heading Text&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the heading element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Generates the appropriate CSS classes for the heading based on its tag and properties.
    /// </summary>
    private string GetHeadingClasses()
    {
        var baseClasses = "font-bold text-gray-900 dark:text-white";

        var sizeClasses = Tag switch
        {
            HeadingTag.H1 => "text-5xl font-extrabold",
            HeadingTag.H2 => "text-4xl",
            HeadingTag.H3 => "text-3xl",
            HeadingTag.H4 => "text-2xl",
            HeadingTag.H5 => "text-xl",
            HeadingTag.H6 => "text-lg",
            _ => "text-5xl font-extrabold"
        };

        return CombineClasses(baseClasses, sizeClasses);
    }
}

/// <summary>
/// Defines the available HTML heading tag types.
/// </summary>
public enum HeadingTag
{
    /// <summary>
    /// H1 heading (largest, typically page title).
    /// </summary>
    H1,

    /// <summary>
    /// H2 heading (section title).
    /// </summary>
    H2,

    /// <summary>
    /// H3 heading (subsection title).
    /// </summary>
    H3,

    /// <summary>
    /// H4 heading (minor subsection title).
    /// </summary>
    H4,

    /// <summary>
    /// H5 heading (minor heading).
    /// </summary>
    H5,

    /// <summary>
    /// H6 heading (smallest heading).
    /// </summary>
    H6
}
