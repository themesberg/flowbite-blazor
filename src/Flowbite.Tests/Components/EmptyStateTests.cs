using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components;

public class EmptyStateTests : FlowbiteTestContext
{
    [Fact]
    public void EmptyState_RendersWithDefaultTitle()
    {
        var cut = RenderComponent<EmptyState>();

        cut.Find("h3").TextContent.Should().Be("No results found");
    }

    [Fact]
    public void EmptyState_RendersCustomTitle()
    {
        var cut = RenderComponent<EmptyState>(p => p.Add(e => e.Title, "No users"));

        cut.Find("h3").TextContent.Should().Be("No users");
    }

    [Fact]
    public void EmptyState_RendersDescription_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Description, "Try adding some items."));

        cut.Find("p").TextContent.Should().Contain("Try adding some items.");
    }

    [Fact]
    public void EmptyState_HidesDescription_WhenNotProvided()
    {
        var cut = RenderComponent<EmptyState>();

        cut.FindAll("p").Should().BeEmpty();
    }

    [Fact]
    public void EmptyState_RendersDefaultIcon_WhenNoIconProvided()
    {
        var cut = RenderComponent<EmptyState>();

        cut.Find("svg").Should().NotBeNull();
    }

    [Fact]
    public void EmptyState_RendersCustomIcon_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Icon, "<span class=\"custom-icon\">Icon</span>"));

        cut.Markup.Should().Contain("custom-icon");
    }

    [Fact]
    public void EmptyState_RendersAction_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Action, "<button>Add Item</button>"));

        cut.Markup.Should().Contain("Add Item");
    }

    [Fact]
    public void EmptyState_AppliesCustomClass()
    {
        var cut = RenderComponent<EmptyState>(p => p.Add(e => e.Class, "my-class"));

        cut.Markup.Should().Contain("my-class");
    }

    // New tests for Image and SecondaryAction

    [Fact]
    public void EmptyState_RendersImage_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Image, "<img src=\"illustration.png\" class=\"illustration\" />"));

        cut.Markup.Should().Contain("illustration");
    }

    [Fact]
    public void EmptyState_ImageTakesPrecedence_OverIcon()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Image, "<img src=\"illustration.png\" class=\"illustration\" />")
            .Add(e => e.Icon, "<span class=\"custom-icon\">Icon</span>"));

        // Image should be rendered
        cut.Markup.Should().Contain("illustration");
        // Icon should not be rendered
        cut.Markup.Should().NotContain("custom-icon");
        // The circular icon container should not be present
        cut.Markup.Should().NotContain("rounded-full bg-gray-100");
    }

    [Fact]
    public void EmptyState_NoIconContainer_WhenImageProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Image, "<img src=\"illustration.png\" />"));

        // The rounded icon container should not be present
        cut.FindAll(".rounded-full").Should().BeEmpty();
    }

    [Fact]
    public void EmptyState_RendersSecondaryAction_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.SecondaryAction, "<button>Cancel</button>"));

        cut.Markup.Should().Contain("Cancel");
    }

    [Fact]
    public void EmptyState_RendersBothActions_WhenProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Action, "<button>Primary</button>")
            .Add(e => e.SecondaryAction, "<button>Secondary</button>"));

        cut.Markup.Should().Contain("Primary");
        cut.Markup.Should().Contain("Secondary");
    }

    [Fact]
    public void EmptyState_ActionsContainer_HasFlexGap_WhenBothProvided()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.Action, "<button>Primary</button>")
            .Add(e => e.SecondaryAction, "<button>Secondary</button>"));

        cut.Markup.Should().Contain("flex");
        cut.Markup.Should().Contain("gap-3");
    }

    [Fact]
    public void EmptyState_OnlySecondaryAction_RendersInContainer()
    {
        var cut = RenderComponent<EmptyState>(p => p
            .Add(e => e.SecondaryAction, "<button>Secondary</button>"));

        cut.Markup.Should().Contain("Secondary");
        cut.Markup.Should().Contain("flex");
    }
}
