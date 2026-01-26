using Flowbite.Base;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// A skeleton loader component for displaying placeholder content during loading states.
/// </summary>
/// <remarks>
/// The Skeleton component provides visual feedback while content is being loaded.
/// It supports various preset shapes and custom dimensions, with built-in animation
/// and dark mode support. Animation respects prefers-reduced-motion accessibility setting.
/// </remarks>
/// <example>
/// <code>
/// &lt;Skeleton Variant="SkeletonVariant.Text" /&gt;
/// &lt;Skeleton Variant="SkeletonVariant.Avatar" /&gt;
/// &lt;Skeleton Variant="SkeletonVariant.Text" Lines="3" /&gt;
/// &lt;Skeleton Width="w-48" Height="h-32" /&gt;
/// </code>
/// </example>
public partial class Skeleton : FlowbiteComponentBase
{
    /// <summary>
    /// The child content to render inside the skeleton.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The preset variant shape for the skeleton.
    /// </summary>
    [Parameter]
    public SkeletonVariant Variant { get; set; } = SkeletonVariant.Text;

    /// <summary>
    /// Custom width using Tailwind classes (e.g., "w-48", "w-full").
    /// Overrides the variant's default width.
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// Custom height using Tailwind classes (e.g., "h-4", "h-32").
    /// Overrides the variant's default height.
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// Whether to show the pulse animation. Default is true.
    /// Animation respects the user's prefers-reduced-motion setting.
    /// </summary>
    [Parameter]
    public bool Animated { get; set; } = true;

    /// <summary>
    /// Accessibility label for screen readers.
    /// </summary>
    [Parameter]
    public string AriaLabel { get; set; } = "Loading content";

    /// <summary>
    /// Number of lines to render for Text variant. Default is 1.
    /// Only applies when Variant is Text.
    /// </summary>
    [Parameter]
    public int Lines { get; set; } = 1;

    /// <summary>
    /// Spacing between lines when Lines > 1. Uses Tailwind spacing classes.
    /// </summary>
    [Parameter]
    public string LineSpacing { get; set; } = "space-y-2.5";

    // Whether we should render multiple lines
    private bool IsMultiLine => Variant == SkeletonVariant.Text && Lines > 1;

    private string ComputedClass => MergeClasses(
        ElementClass.Empty()
            .Add("bg-gray-200 dark:bg-gray-700")
            .Add(GetVariantClasses())
            .Add("animate-pulse motion-reduce:animate-none", when: Animated)
            .Add(Width)
            .Add(Height)
            .Add(Class)
    );

    private string? ComputedStyle => Style;

    // Class for the container when rendering multiple lines
    private string MultiLineContainerClass => MergeClasses(
        ElementClass.Empty()
            .Add(LineSpacing)
            .Add(Class)
    );

    // Class for each line in a multi-line skeleton
    private string GetLineClass(int lineIndex)
    {
        // Vary the width of lines for more natural appearance
        var widthClass = lineIndex switch
        {
            _ when lineIndex == Lines - 1 => "w-4/5", // Last line is shorter
            _ when lineIndex % 2 == 1 => "w-11/12", // Odd lines slightly shorter
            _ => "w-full" // Even lines full width
        };

        return MergeClasses(
            ElementClass.Empty()
                .Add("bg-gray-200 dark:bg-gray-700 h-4 rounded")
                .Add("animate-pulse motion-reduce:animate-none", when: Animated)
                .Add(widthClass)
        );
    }

    private string GetVariantClasses() => Variant switch
    {
        SkeletonVariant.Text => "h-4 rounded",
        SkeletonVariant.Avatar => "rounded-full w-10 h-10",
        SkeletonVariant.Thumbnail => "w-24 h-24 rounded-lg",
        SkeletonVariant.Button => "h-10 w-24 rounded-lg",
        SkeletonVariant.Card => "h-48 w-full rounded-lg",
        SkeletonVariant.Input => "h-10 w-full rounded-lg",
        SkeletonVariant.Custom => "rounded",
        _ => "h-4 rounded"
    };
}

/// <summary>
/// Predefined skeleton shape variants.
/// </summary>
public enum SkeletonVariant
{
    /// <summary>Text placeholder - thin horizontal bar</summary>
    Text,
    /// <summary>Circular avatar placeholder</summary>
    Avatar,
    /// <summary>Square image thumbnail placeholder</summary>
    Thumbnail,
    /// <summary>Button-shaped placeholder</summary>
    Button,
    /// <summary>Large card placeholder</summary>
    Card,
    /// <summary>Input field placeholder</summary>
    Input,
    /// <summary>Custom dimensions via Width/Height parameters</summary>
    Custom
}
