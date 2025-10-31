
namespace Flowbite.Components.Typography;

/// <summary>
/// Span component for rendering inline text with styling.
/// Supports custom sizes, weights, colors, and text decorations.
/// </summary>
public partial class Span
{
    /// <summary>
    /// Gets or sets the text size.
    /// </summary>
    [Parameter]
    public TextSize Size { get; set; } = TextSize.Base;

    /// <summary>
    /// Gets or sets the font weight.
    /// </summary>
    [Parameter]
    public FontWeight? Weight { get; set; }

    /// <summary>
    /// Gets or sets a gradient color effect for the span text.
    /// </summary>
    [Parameter]
    public GradientColor Gradient { get; set; } = GradientColor.None;

    /// <summary>
    /// Gets or sets custom color classes for the span.
    /// Overrides the default gray-700/gray-400 color scheme.
    /// </summary>
    [Parameter]
    public string? CustomColor { get; set; }

    /// <summary>
    /// Gets or sets whether to render as italic text.
    /// </summary>
    [Parameter]
    public bool Italic { get; set; }

    /// <summary>
    /// Gets or sets whether to render as underlined text.
    /// </summary>
    [Parameter]
    public bool Underline { get; set; }

    /// <summary>
    /// Gets or sets whether to render with line-through decoration.
    /// </summary>
    [Parameter]
    public bool LineThrough { get; set; }

    /// <summary>
    /// Gets or sets whether to render as uppercase text.
    /// </summary>
    [Parameter]
    public bool Uppercase { get; set; }

    /// <summary>
    /// Gets or sets whether to render as lowercase text.
    /// </summary>
    [Parameter]
    public bool Lowercase { get; set; }

    /// <summary>
    /// Gets or sets whether to render as capitalized text.
    /// </summary>
    [Parameter]
    public bool Capitalize { get; set; }

    /// <summary>
    /// Gets or sets the child content of the span.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the span element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Generates the appropriate CSS classes for the span based on its properties.
    /// </summary>
    private string GetSpanClasses()
    {
        var classes = new List<string>();

        // Add gradient classes if specified
        if (Gradient != GradientColor.None)
        {
            classes.Add(GetGradientClasses(Gradient));
        }
        else if (!string.IsNullOrEmpty(CustomColor))
        {
            // Add custom color if specified
            classes.Add(CustomColor);
        }

        // Add size class
        classes.Add(GetTextSizeClass(Size));

        // Add weight class if specified
        if (Weight.HasValue)
        {
            classes.Add(GetFontWeightClass(Weight.Value));
        }

        // Add italic style
        if (Italic)
        {
            classes.Add("italic");
        }

        // Add underline style
        if (Underline)
        {
            classes.Add("underline");
        }

        // Add line-through style
        if (LineThrough)
        {
            classes.Add("line-through");
        }

        // Add text transform styles
        if (Uppercase)
        {
            classes.Add("uppercase");
        }
        else if (Lowercase)
        {
            classes.Add("lowercase");
        }
        else if (Capitalize)
        {
            classes.Add("capitalize");
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
