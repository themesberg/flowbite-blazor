using Flowbite.Components;
using Flowbite.Tests.TestSetup;
using static Flowbite.Components.Skeleton;

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
}
