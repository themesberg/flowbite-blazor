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
}
