using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components;

public class SkeletonTests : FlowbiteTestContext
{
    [Fact]
    public void Skeleton_RendersWithDefaultAttributes()
    {
        var cut = RenderComponent<Skeleton>();

        cut.Markup.Should().Contain("role=\"status\"");
        cut.Markup.Should().Contain("aria-busy=\"true\"");
        cut.Markup.Should().Contain("animate-pulse");
        cut.Markup.Should().Contain("bg-gray-200");
        cut.Markup.Should().Contain("dark:bg-gray-700");
    }

    [Theory]
    [InlineData(SkeletonVariant.Text, "h-4")]
    [InlineData(SkeletonVariant.Avatar, "rounded-full")]
    [InlineData(SkeletonVariant.Thumbnail, "w-24")]
    [InlineData(SkeletonVariant.Button, "h-10")]
    [InlineData(SkeletonVariant.Card, "h-48")]
    [InlineData(SkeletonVariant.Input, "h-10")]
    public void Skeleton_VariantAppliesCorrectClasses(SkeletonVariant variant, string expectedClass)
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.Variant, variant));

        cut.Markup.Should().Contain(expectedClass);
    }

    [Fact]
    public void Skeleton_AnimatedFalse_NoAnimationClass()
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.Animated, false));

        cut.Markup.Should().NotContain("animate-pulse");
    }

    [Fact]
    public void Skeleton_CustomDimensions_OverridesDefaults()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Width, "w-64")
            .Add(s => s.Height, "h-16"));

        cut.Markup.Should().Contain("w-64");
        cut.Markup.Should().Contain("h-16");
    }

    [Fact]
    public void Skeleton_CustomClass_IsMerged()
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.Class, "my-custom-class"));

        cut.Markup.Should().Contain("my-custom-class");
    }

    [Fact]
    public void Skeleton_HasAccessibilityFeatures()
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.AriaLabel, "Loading user data"));

        cut.Markup.Should().Contain("aria-label=\"Loading user data\"");
        cut.Markup.Should().Contain("sr-only");
        cut.Markup.Should().Contain("Loading...");
    }

    // New tests for enhanced features

    [Fact]
    public void Skeleton_InputVariant_HasCorrectClasses()
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.Variant, SkeletonVariant.Input));

        cut.Markup.Should().Contain("h-10");
        cut.Markup.Should().Contain("w-full");
        cut.Markup.Should().Contain("rounded-lg");
    }

    [Fact]
    public void Skeleton_MotionReduce_HasMotionReduceClass()
    {
        var cut = RenderComponent<Skeleton>(p => p.Add(s => s.Animated, true));

        cut.Markup.Should().Contain("motion-reduce:animate-none");
    }

    [Fact]
    public void Skeleton_MultiLine_RendersMultipleLines()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Variant, SkeletonVariant.Text)
            .Add(s => s.Lines, 3));

        // Should render 3 div elements for lines
        var lines = cut.FindAll("div[class*='h-4']");
        lines.Should().HaveCount(3);
    }

    [Fact]
    public void Skeleton_MultiLine_VariesLineWidths()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Variant, SkeletonVariant.Text)
            .Add(s => s.Lines, 3));

        // Last line should be shorter (w-4/5)
        cut.Markup.Should().Contain("w-4/5");
    }

    [Fact]
    public void Skeleton_SingleLine_NoMultiLineContainer()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Variant, SkeletonVariant.Text)
            .Add(s => s.Lines, 1));

        // Should not have the space-y spacing class for multi-line
        cut.Markup.Should().NotContain("space-y-2.5");
    }

    [Fact]
    public void Skeleton_MultiLine_CustomSpacing()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Variant, SkeletonVariant.Text)
            .Add(s => s.Lines, 3)
            .Add(s => s.LineSpacing, "space-y-4"));

        cut.Markup.Should().Contain("space-y-4");
    }

    [Fact]
    public void Skeleton_NonTextVariant_IgnoresLines()
    {
        var cut = RenderComponent<Skeleton>(p => p
            .Add(s => s.Variant, SkeletonVariant.Avatar)
            .Add(s => s.Lines, 3));

        // Should render single avatar, not multiple lines
        var divs = cut.FindAll("div");
        divs.Should().HaveCount(1);
    }
}
