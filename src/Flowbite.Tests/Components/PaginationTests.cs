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
            .Add(pg => pg.MaxVisiblePages, 5));

        // Should show 5 pages
        var pageButtons = cut.FindAll("button").Where(b => int.TryParse(b.TextContent, out _)).ToList();
        pageButtons.Should().HaveCount(5);
    }

    [Fact]
    public void Pagination_MarksCurrentPage_AsCurrent()
    {
        var cut = RenderComponent<Pagination>(p => p
            .Add(pg => pg.CurrentPage, 3)
            .Add(pg => pg.TotalItems, 100));

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
}
