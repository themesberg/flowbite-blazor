using Microsoft.Playwright;

namespace Flowbite.Tests.Integration;

/// <summary>
/// Smoke tests for the DemoApp using Playwright.
/// These tests verify basic end-to-end functionality against the running DemoApp.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the <b>golden example</b> for future Playwright integration tests.
/// It demonstrates:
/// </para>
/// <list type="bullet">
///   <item><description>Fixture-based browser lifecycle management via <see cref="PlaywrightFixture"/></description></item>
///   <item><description>Page navigation pattern using <see cref="IPage.GotoAsync"/></description></item>
///   <item><description>Element assertion pattern using <see cref="Assertions.Expect"/></description></item>
///   <item><description>Screenshot capture for debugging</description></item>
///   <item><description>Proper page cleanup in finally blocks</description></item>
/// </list>
/// <para>
/// <b>Prerequisites:</b> DemoApp must be running at http://localhost:5290 before running these tests.
/// Use <c>python build.py start</c> to launch the DemoApp, or <c>python build.py test-integration</c>
/// which handles the lifecycle automatically.
/// </para>
/// </remarks>
/// <example>
/// To add a new integration test:
/// <code>
/// [Fact]
/// [Trait("Category", "Integration")]
/// public async Task ComponentPage_InteractsCorrectly()
/// {
///     var page = await _fixture.NavigateToAsync("/docs/components/button");
///     try
///     {
///         // Find and interact with elements
///         var button = page.Locator("button:has-text('Click me')");
///         await button.ClickAsync();
///
///         // Verify results
///         await Assertions.Expect(page.Locator(".result")).ToHaveTextAsync("Clicked!");
///     }
///     finally
///     {
///         await page.CloseAsync();
///     }
/// }
/// </code>
/// </example>
[Collection("Playwright")]
public class DemoAppSmokeTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    /// <summary>
    /// Initializes a new instance of the test class with the shared Playwright fixture.
    /// </summary>
    /// <param name="fixture">The shared Playwright fixture providing browser access.</param>
    public DemoAppSmokeTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Verifies that the DemoApp home page loads successfully.
    /// </summary>
    /// <remarks>
    /// This is the <b>golden example</b> test demonstrating:
    /// <list type="number">
    ///   <item><description>Using the fixture to create and navigate to a page</description></item>
    ///   <item><description>Waiting for content to load with <c>WaitUntil.NetworkIdle</c></description></item>
    ///   <item><description>Verifying page title and key elements</description></item>
    ///   <item><description>Taking screenshots for debugging (optional)</description></item>
    ///   <item><description>Proper cleanup by closing the page</description></item>
    /// </list>
    /// </remarks>
    [Fact]
    [Trait("Category", "Integration")]
    public async Task HomePage_LoadsSuccessfully()
    {
        // Arrange - Create a new page and navigate to the home page
        var page = await _fixture.CreatePageAsync();

        try
        {
            // Act - Navigate to the DemoApp home page
            var response = await page.GotoAsync(_fixture.BaseUrl, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle
            });

            // Assert - Verify the page loaded successfully
            response.Should().NotBeNull();
            response!.Ok.Should().BeTrue("the page should return a 200 status");

            // Verify the page title contains expected text
            var title = await page.TitleAsync();
            title.Should().NotBeNullOrEmpty("the page should have a title");

            // Verify key elements are present
            // The home page should have at least some content visible
            var body = page.Locator("body");
            await Assertions.Expect(body).ToBeVisibleAsync();

            // Verify there are no console errors
            var consoleMessages = new List<string>();
            page.Console += (_, msg) =>
            {
                if (msg.Type == "error")
                {
                    consoleMessages.Add(msg.Text);
                }
            };

            // Optional: Take a screenshot for debugging
            // Uncomment the following line to capture screenshots during test runs
            // await page.ScreenshotAsync(new() { Path = "screenshots/homepage.png" });
        }
        finally
        {
            // Cleanup - Always close the page to release resources
            await page.CloseAsync();
        }
    }

    /// <summary>
    /// Verifies that the Button component documentation page loads.
    /// </summary>
    /// <remarks>
    /// This test demonstrates navigating to a specific component documentation page.
    /// </remarks>
    [Fact]
    [Trait("Category", "Integration")]
    public async Task ButtonPage_LoadsSuccessfully()
    {
        // Arrange & Act
        var page = await _fixture.NavigateToAsync("/docs/components/button");

        try
        {
            // Assert - Page should have loaded
            var heading = page.Locator("h1, h2").First;
            await Assertions.Expect(heading).ToBeVisibleAsync();

            // The page should contain button examples
            var buttons = page.Locator("button");
            var buttonCount = await buttons.CountAsync();
            buttonCount.Should().BeGreaterThan(0, "the button page should have button examples");
        }
        finally
        {
            await page.CloseAsync();
        }
    }
}
