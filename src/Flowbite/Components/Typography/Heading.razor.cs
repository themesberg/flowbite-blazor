
namespace Flowbite.Components.Typography;

/// <summary>
/// Heading component for rendering semantic HTML headings (h1-h6) with consistent styling.
/// Supports custom sizes, weights, gradients, and colors.
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
    /// Gets or sets an optional text size that overrides the default tag-based sizing.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading Tag="HeadingTag.H2" Size="TextSize.XXXXXL"&gt;Large H2&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public TextSize? Size { get; set; }

    /// <summary>
    /// Gets or sets an optional font weight that overrides the default weight.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading Tag="HeadingTag.H3" Weight="FontWeight.Light"&gt;Light Heading&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public FontWeight? Weight { get; set; }

    /// <summary>
    /// Gets or sets a gradient color effect for the heading text.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading Gradient="GradientColor.PurpleToBlue"&gt;Gradient Heading&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public GradientColor Gradient { get; set; } = GradientColor.None;

    /// <summary>
    /// Gets or sets custom color classes for the heading.
    /// Overrides the default gray-900/white color scheme.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Heading CustomColor="text-blue-600 dark:text-blue-400"&gt;Blue Heading&lt;/Heading&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? CustomColor { get; set; }

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
        var classes = new List<string>();

        // Add gradient classes if specified
        if (Gradient != GradientColor.None)
        {
            classes.Add(GetGradientClasses(Gradient));
        }
        else
        {
            // Add color classes only if no gradient
            classes.Add(CustomColor ?? "text-gray-900 dark:text-white");
        }

        // Add size classes
        if (Size.HasValue)
        {
            classes.Add(GetTextSizeClass(Size.Value));
        }
        else
        {
            // Default tag-based sizing
            classes.Add(Tag switch
            {
                HeadingTag.H1 => "text-5xl",
                HeadingTag.H2 => "text-4xl",
                HeadingTag.H3 => "text-3xl",
                HeadingTag.H4 => "text-2xl",
                HeadingTag.H5 => "text-xl",
                HeadingTag.H6 => "text-lg",
                _ => "text-5xl"
            });
        }

        // Add weight classes
        if (Weight.HasValue)
        {
            classes.Add(GetFontWeightClass(Weight.Value));
        }
        else
        {
            // Default weight (extrabold for H1, bold for others)
            classes.Add(Tag == HeadingTag.H1 ? "font-extrabold" : "font-bold");
        }

        return CombineClasses(classes.ToArray());
    }

    /// <summary>
    /// Gets the Tailwind CSS class for a given text size.
    /// </summary>
    private static string GetTextSizeClass(TextSize size) => size switch
    {
        TextSize.XS => "text-xs",
        TextSize.SM => "text-sm",
        TextSize.Base => "text-base",
        TextSize.LG => "text-lg",
        TextSize.XL => "text-xl",
        TextSize.XXL => "text-2xl",
        TextSize.XXXL => "text-3xl",
        TextSize.XXXXL => "text-4xl",
        TextSize.XXXXXL => "text-5xl",
        TextSize.XXXXXXL => "text-6xl",
        TextSize.XXXXXXXL => "text-7xl",
        TextSize.XXXXXXXXL => "text-8xl",
        TextSize.XXXXXXXXXL => "text-9xl",
        _ => "text-base"
    };

    /// <summary>
    /// Gets the Tailwind CSS class for a given font weight.
    /// </summary>
    private static string GetFontWeightClass(FontWeight weight) => weight switch
    {
        FontWeight.Thin => "font-thin",
        FontWeight.ExtraLight => "font-extralight",
        FontWeight.Light => "font-light",
        FontWeight.Normal => "font-normal",
        FontWeight.Medium => "font-medium",
        FontWeight.SemiBold => "font-semibold",
        FontWeight.Bold => "font-bold",
        FontWeight.ExtraBold => "font-extrabold",
        FontWeight.Black => "font-black",
        _ => "font-normal"
    };

    /// <summary>
    /// Gets the Tailwind CSS classes for a given gradient color.
    /// </summary>
    private static string GetGradientClasses(GradientColor gradient) => gradient switch
    {
        GradientColor.PurpleToBlue => "bg-gradient-to-r from-purple-600 to-blue-500 bg-clip-text text-transparent",
        GradientColor.CyanToBlue => "bg-gradient-to-r from-cyan-500 to-blue-500 bg-clip-text text-transparent",
        GradientColor.GreenToBlue => "bg-gradient-to-r from-green-400 to-blue-500 bg-clip-text text-transparent",
        GradientColor.PurpleToPink => "bg-gradient-to-r from-purple-500 to-pink-500 bg-clip-text text-transparent",
        GradientColor.RedToYellow => "bg-gradient-to-r from-red-500 to-yellow-500 bg-clip-text text-transparent",
        GradientColor.TealToLime => "bg-gradient-to-r from-teal-500 to-lime-500 bg-clip-text text-transparent",
        _ => ""
    };
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
