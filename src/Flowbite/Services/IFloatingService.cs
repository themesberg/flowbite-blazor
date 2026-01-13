namespace Flowbite.Services;

/// <summary>
/// Service interface for managing floating element positioning using Floating UI.
/// </summary>
/// <remarks>
/// This service provides viewport-aware positioning for dropdowns, tooltips, and popovers.
/// It integrates with @floating-ui/dom to automatically flip and shift elements when they
/// would otherwise overflow the viewport.
/// </remarks>
public interface IFloatingService : IAsyncDisposable
{
    /// <summary>
    /// Initializes floating positioning for an element pair.
    /// </summary>
    /// <param name="id">Unique identifier for this floating instance.</param>
    /// <param name="options">Positioning configuration options.</param>
    /// <returns>
    /// A task that resolves to the actual placement after applying flip/shift middleware,
    /// or null if the elements could not be found.
    /// </returns>
    /// <remarks>
    /// The trigger element should have attribute: data-floating-trigger="{id}"
    /// The floating element should have attribute: data-floating-element="{id}"
    /// Optionally, an arrow element can have: data-floating-arrow="{id}"
    /// </remarks>
    Task<string?> InitializeAsync(string id, FloatingOptions? options = null);

    /// <summary>
    /// Manually triggers a position update for a floating element.
    /// </summary>
    /// <param name="id">The floating instance identifier.</param>
    /// <remarks>
    /// Position updates happen automatically via autoUpdate when initialized,
    /// but this method can be called to force an immediate update.
    /// </remarks>
    Task UpdatePositionAsync(string id);

    /// <summary>
    /// Destroys a floating instance and cleans up resources.
    /// </summary>
    /// <param name="id">The floating instance identifier.</param>
    /// <remarks>
    /// Always call this method when the floating element is no longer needed
    /// to prevent memory leaks. This is typically done in the component's
    /// DisposeAsync method.
    /// </remarks>
    Task DestroyAsync(string id);

    /// <summary>
    /// Checks if a floating instance exists.
    /// </summary>
    /// <param name="id">The floating instance identifier.</param>
    /// <returns>True if the instance exists, false otherwise.</returns>
    Task<bool> ExistsAsync(string id);

    /// <summary>
    /// Gets the current placement of a floating element.
    /// </summary>
    /// <param name="id">The floating instance identifier.</param>
    /// <returns>The current placement string, or null if not found.</returns>
    /// <remarks>
    /// The placement may differ from the configured placement if flip middleware
    /// adjusted it to fit within the viewport.
    /// </remarks>
    Task<string?> GetPlacementAsync(string id);
}
