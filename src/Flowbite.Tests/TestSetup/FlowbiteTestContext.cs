using Flowbite.Services;

namespace Flowbite.Tests.TestSetup;

/// <summary>
/// Base test context with Flowbite services pre-configured.
/// Inherit from this class for component tests that require Flowbite services.
/// </summary>
/// <remarks>
/// This context automatically registers:
/// - TailwindMerge service for class conflict resolution
/// - Flowbite modal, drawer, and toast services
/// - Floating UI service for positioning
/// - Lazy-loaded JS module services
///
/// JSInterop is set to loose mode by default, which ignores unmocked JS calls.
/// </remarks>
public class FlowbiteTestContext : TestContext
{
    /// <summary>
    /// Initializes a new instance of the FlowbiteTestContext with Flowbite services registered.
    /// </summary>
    public FlowbiteTestContext()
    {
        // Register all Flowbite services
        Services.AddFlowbite();

        // Set JSInterop to loose mode to ignore unmocked JS calls
        // This prevents tests from failing due to missing JS mocks
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
