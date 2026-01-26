using Flowbite.Components;
using Flowbite.Tests.TestSetup;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Tests.Components;

public class PaginationTests : FlowbiteTestContext
{
    [Fact]
    public void Pagination_RendersWithAccessibility()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10));

        cut.Find("nav").GetAttribute("aria-label").Should().Be("Pagination navigation");
    }

    [Fact]
    public void Pagination_ShowsCorrectInfo()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 2)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10));

        cut.Markup.Should().Contain("11-20");
        cut.Markup.Should().Contain("of");
        cut.Markup.Should().Contain("100");
    }

    [Fact]
    public void Pagination_HidesInfo_WhenShowInfoFalse()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowInfo, false));

        cut.Markup.Should().NotContain("Showing");
    }

    [Fact]
    public void Pagination_DisablesPrevious_OnFirstPage()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 100));

        var prevButton = cut.FindAll("button").First();
        prevButton.HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void Pagination_DisablesNext_OnLastPage()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 10)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10));

        var nextButton = cut.FindAll("button").Last();
        nextButton.HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public async Task Pagination_InvokesCallback_OnPageClick()
    {
        var pageChanged = 0;
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.OnPageChange, EventCallback.Factory.Create<int>(this, (page) => pageChanged = page)));

        // Click page 2 button
        var page2Button = cut.FindAll("button").First(b => b.TextContent == "2");
        await cut.InvokeAsync(() => page2Button.Click());

        pageChanged.Should().Be(2);
    }

    [Fact]
    public void Pagination_ShowsCorrectPageCount()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 50)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.MaxVisiblePages, 5)
            .Add(pg => pg.ShowEllipsis, false));

        // Should show 5 pages
        var pageButtons = cut.FindAll("button").Where(b => int.TryParse(b.TextContent, out _)).ToList();
        pageButtons.Should().HaveCount(5);
    }

    [Fact]
    public void Pagination_MarksCurrentPage_AsCurrent()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 3)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowEllipsis, false));

        var currentPageButton = cut.FindAll("button").First(b => b.TextContent == "3");
        currentPageButton.GetAttribute("aria-current").Should().Be("page");
    }

    [Fact]
    public void Pagination_AppliesCustomClass()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.Class, "my-pagination"));

        cut.Find("nav").ClassList.Should().Contain("my-pagination");
    }

    // First/Last Button Tests

    [Fact]
    public void Pagination_ShowsFirstLastButtons_WhenEnabled()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 5)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowFirstLast, true));

        // Should have 4 navigation buttons: First, Previous, Next, Last
        var buttons = cut.FindAll("button");
        buttons.First().GetAttribute("aria-label").Should().Be("First page");
        buttons.Last().GetAttribute("aria-label").Should().Be("Last page");
    }

    [Fact]
    public void Pagination_DisablesFirstButton_OnFirstPage()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 1)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowFirstLast, true));

        var firstButton = cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "First page");
        firstButton.HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void Pagination_DisablesLastButton_OnLastPage()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 10)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowFirstLast, true));

        var lastButton = cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "Last page");
        lastButton.HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public async Task Pagination_FirstButton_GoesToFirstPage()
    {
        var pageChanged = 0;
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 5)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowFirstLast, true)
            .Add(pg => pg.OnPageChange, EventCallback.Factory.Create<int>(this, (page) => pageChanged = page)));

        var firstButton = cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "First page");
        await cut.InvokeAsync(() => firstButton.Click());

        pageChanged.Should().Be(1);
    }

    [Fact]
    public async Task Pagination_LastButton_GoesToLastPage()
    {
        var pageChanged = 0;
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 5)
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowFirstLast, true)
            .Add(pg => pg.OnPageChange, EventCallback.Factory.Create<int>(this, (page) => pageChanged = page)));

        var lastButton = cut.FindAll("button").First(b => b.GetAttribute("aria-label") == "Last page");
        await cut.InvokeAsync(() => lastButton.Click());

        pageChanged.Should().Be(10);
    }

    // Ellipsis Tests

    [Fact]
    public void Pagination_ShowsEllipsis_ForLargeDatasets()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 50)
            .Add(pg => pg.TotalItems, 1000)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowEllipsis, true));

        cut.Markup.Should().Contain("...");
    }

    [Fact]
    public void Pagination_HidesEllipsis_WhenDisabled()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 50)
            .Add(pg => pg.TotalItems, 1000)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowEllipsis, false));

        cut.Markup.Should().NotContain("...");
    }

    [Fact]
    public void Pagination_AlwaysShowsFirstAndLastPage_WithEllipsis()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 50)
            .Add(pg => pg.TotalItems, 1000)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowEllipsis, true));

        var pageButtons = cut.FindAll("button").Where(b => int.TryParse(b.TextContent, out _)).ToList();
        pageButtons.First().TextContent.Should().Be("1");
        pageButtons.Last().TextContent.Should().Be("100");
    }

    // Size Variant Tests

    [Fact]
    public void Pagination_SmallSize_HasCorrectClasses()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.Size, PaginationSize.Small));

        cut.Markup.Should().Contain("h-6");
        cut.Markup.Should().Contain("text-xs");
    }

    [Fact]
    public void Pagination_LargeSize_HasCorrectClasses()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.Size, PaginationSize.Large));

        cut.Markup.Should().Contain("h-10");
        cut.Markup.Should().Contain("text-base");
    }

    [Fact]
    public void Pagination_DefaultSize_HasCorrectClasses()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.Size, PaginationSize.Default));

        cut.Markup.Should().Contain("h-8");
        cut.Markup.Should().Contain("text-sm");
    }

    // Go to Page Tests

    [Fact]
    public void Pagination_ShowsGoToPage_WhenEnabled()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowGoToPage, true));

        cut.Find("input[type='number']").Should().NotBeNull();
        cut.Markup.Should().Contain("Go to");
    }

    [Fact]
    public void Pagination_HidesGoToPage_WhenDisabled()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowGoToPage, false));

        cut.FindAll("input[type='number']").Should().BeEmpty();
    }

    // Page Size Selector Tests

    [Fact]
    public void Pagination_ShowsPageSizeSelector_WhenEnabled()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.ShowPageSizeSelector, true)
            .Add(pg => pg.PageSizeOptions, new[] { 10, 20, 50 }));

        var select = cut.Find("select");
        select.Should().NotBeNull();

        var options = cut.FindAll("select option");
        options.Should().HaveCount(3);
    }

    [Fact]
    public async Task Pagination_PageSizeSelector_InvokesCallback()
    {
        var newPageSize = 0;
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.TotalItems, 100)
            .Add(pg => pg.PageSize, 10)
            .Add(pg => pg.ShowPageSizeSelector, true)
            .Add(pg => pg.PageSizeOptions, new[] { 10, 20, 50 })
            .Add(pg => pg.OnPageSizeChange, EventCallback.Factory.Create<int>(this, (size) => newPageSize = size)));

        var select = cut.Find("select");
        await cut.InvokeAsync(() => select.Change("20"));

        newPageSize.Should().Be(20);
    }
}
