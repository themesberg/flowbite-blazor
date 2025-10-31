
namespace Flowbite.Components.Typography;

/// <summary>
/// Paragraph component for rendering text content with consistent styling.
/// Supports custom sizes, weights, alignment, colors, and gradients.
/// </summary>
public partial class Paragraph
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
    /// Gets or sets the line height.
    /// </summary>
    [Parameter]
    public LineHeight? Leading { get; set; }

    /// <summary>
    /// Gets or sets the text alignment.
    /// </summary>
    [Parameter]
    public TextAlign? Align { get; set; }

    /// <summary>
    /// Gets or sets the letter spacing (tracking).
    /// </summary>
    [Parameter]
    public LetterSpacing? Tracking { get; set; }

    /// <summary>
    /// Gets or sets the whitespace handling.
    /// </summary>
    [Parameter]
    public Whitespace? Space { get; set; }

    /// <summary>
    /// Gets or sets a gradient color effect for the paragraph text.
    /// </summary>
    [Parameter]
    public GradientColor Gradient { get; set; } = GradientColor.None;

    /// <summary>
    /// Gets or sets custom color classes for the paragraph.
    /// Overrides the default gray-700/gray-400 color scheme.
    /// </summary>
    [Parameter]
    public string? CustomColor { get; set; }

    /// <summary>
    /// Gets or sets whether to render with first letter styling.
    /// </summary>
    [Parameter]
    public bool FirstLetterUpper { get; set; }

    /// <summary>
    /// Gets or sets the opacity level (0-100).
    /// </summary>
    [Parameter]
    public int? Opacity { get; set; }

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
    /// Gets or sets the child content of the paragraph.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the paragraph element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Generates the appropriate CSS classes for the paragraph based on its properties.
    /// </summary>
    private string GetParagraphClasses()
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
            classes.Add(CustomColor ?? "text-gray-700 dark:text-gray-400");
        }

        // Add size class
        classes.Add(GetTextSizeClass(Size));

        // Add weight class if specified
        if (Weight.HasValue)
        {
            classes.Add(GetFontWeightClass(Weight.Value));
        }

        // Add line height if specified
        if (Leading.HasValue)
        {
            classes.Add(GetLineHeightClass(Leading.Value));
        }

        // Add text alignment if specified
        if (Align.HasValue)
        {
            classes.Add(GetTextAlignClass(Align.Value));
        }

        // Add letter spacing if specified
        if (Tracking.HasValue)
        {
            classes.Add(GetLetterSpacingClass(Tracking.Value));
        }

        // Add whitespace handling if specified
        if (Space.HasValue)
        {
            classes.Add(GetWhitespaceClass(Space.Value));
        }

        // Add opacity if specified
        if (Opacity.HasValue)
        {
            classes.Add($"opacity-{Opacity.Value}");
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

        // Add first letter uppercase
        if (FirstLetterUpper)
        {
            classes.Add("first-letter:uppercase first-letter:text-7xl first-letter:font-bold first-letter:mr-1 first-letter:float-left");
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
    /// Gets the Tailwind CSS class for a given line height.
    /// </summary>
    private static string GetLineHeightClass(LineHeight leading) => leading switch
    {
        LineHeight.None => "leading-none",
        LineHeight.Tight => "leading-tight",
        LineHeight.Snug => "leading-snug",
        LineHeight.Normal => "leading-normal",
        LineHeight.Relaxed => "leading-relaxed",
        LineHeight.Loose => "leading-loose",
        LineHeight.Three => "leading-3",
        LineHeight.Four => "leading-4",
        LineHeight.Five => "leading-5",
        LineHeight.Six => "leading-6",
        LineHeight.Seven => "leading-7",
        LineHeight.Eight => "leading-8",
        LineHeight.Nine => "leading-9",
        LineHeight.Ten => "leading-10",
        _ => "leading-normal"
    };

    /// <summary>
    /// Gets the Tailwind CSS class for text alignment.
    /// </summary>
    private static string GetTextAlignClass(TextAlign align) => align switch
    {
        TextAlign.Left => "text-left",
        TextAlign.Center => "text-center",
        TextAlign.Right => "text-right",
        TextAlign.Justify => "text-justify",
        _ => "text-left"
    };

    /// <summary>
    /// Gets the Tailwind CSS class for letter spacing.
    /// </summary>
    private static string GetLetterSpacingClass(LetterSpacing tracking) => tracking switch
    {
        LetterSpacing.Tighter => "tracking-tighter",
        LetterSpacing.Tight => "tracking-tight",
        LetterSpacing.Normal => "tracking-normal",
        LetterSpacing.Wide => "tracking-wide",
        LetterSpacing.Wider => "tracking-wider",
        LetterSpacing.Widest => "tracking-widest",
        _ => "tracking-normal"
    };

    /// <summary>
    /// Gets the Tailwind CSS class for whitespace handling.
    /// </summary>
    private static string GetWhitespaceClass(Whitespace space) => space switch
    {
        Whitespace.Normal => "whitespace-normal",
        Whitespace.Nowrap => "whitespace-nowrap",
        Whitespace.Pre => "whitespace-pre",
        Whitespace.PreLine => "whitespace-pre-line",
        Whitespace.PreWrap => "whitespace-pre-wrap",
        _ => "whitespace-normal"
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
