using Microsoft.Playwright;

namespace Flowbite.Tests.Integration;

/// <summary>
/// Shared Playwright fixture for integration tests.
/// Provides browser instance management and reuse across tests.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// [Collection("Playwright")]
/// public class MyTests : IClassFixture&lt;PlaywrightFixture&gt;
/// {
///     private readonly PlaywrightFixture _fixture;
///
///     public MyTests(PlaywrightFixture fixture)
///     {
///         _fixture = fixture;
///     }
///
///     [Fact]
///     [Trait("Category", "Integration")]
///     public async Task MyTest()
///     {
///         var page = await _fixture.Browser.NewPageAsync();
///         // ... test code
///     }
/// }
/// </code>
/// </remarks>
public class PlaywrightFixture : IAsyncLifetime
{
    /// <summary>
    /// The Playwright instance for creating browsers.
    /// </summary>
    public IPlaywright Playwright { get; private set; } = null!;

    /// <summary>
    /// The shared browser instance for creating pages.
    /// </summary>
    public IBrowser Browser { get; private set; } = null!;

    /// <summary>
    /// The base URL for the DemoApp.
    /// </summary>
    public string BaseUrl { get; } = "http://localhost:5290";

    /// <summary>
    /// Initializes the Playwright browser instance.
    /// </summary>
    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        // Use Chromium for consistent cross-platform testing
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true, // Run headless by default
            SlowMo = 0       // No delay between actions
        });
    }

    /// <summary>
    /// Cleans up the browser and Playwright instances.
    /// </summary>
    public async Task DisposeAsync()
    {
        if (Browser != null)
        {
            await Browser.CloseAsync();
        }

        Playwright?.Dispose();
    }

    /// <summary>
    /// Creates a new browser page with default viewport settings.
    /// </summary>
    /// <returns>A new page instance.</returns>
    public async Task<IPage> CreatePageAsync()
    {
        var page = await Browser.NewPageAsync(new BrowserNewPageOptions
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
        });

        return page;
    }

    /// <summary>
    /// Creates a new browser page and navigates to the specified path.
    /// </summary>
    /// <param name="path">The relative path to navigate to (e.g., "/docs/components/button").</param>
    /// <returns>A page navigated to the specified URL.</returns>
    public async Task<IPage> NavigateToAsync(string path)
    {
        var page = await CreatePageAsync();
        var url = path.StartsWith("/") ? $"{BaseUrl}{path}" : $"{BaseUrl}/{path}";
        await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
        return page;
    }
}

/// <summary>
/// Collection definition for Playwright tests.
/// Ensures tests in this collection share the PlaywrightFixture instance.
/// </summary>
[CollectionDefinition("Playwright")]
public class PlaywrightCollection : ICollectionFixture<PlaywrightFixture>
{
    // This class has no code, and is never created.
    // Its purpose is to be the place to apply [CollectionDefinition]
    // and all the ICollectionFixture<> interfaces.
}
