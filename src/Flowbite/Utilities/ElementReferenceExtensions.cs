using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Utilities;

/// <summary>
/// Extension methods for <see cref="ElementReference"/> providing JavaScript interop utilities.
/// </summary>
public static class ElementReferenceExtensions
{
    /// <summary>
    /// Gets the scroll height of an element asynchronously.
    /// Used for calculating the content height for smooth collapse/expand animations.
    /// </summary>
    /// <param name="elementRef">The element reference to measure.</param>
    /// <param name="js">The JavaScript runtime to use for interop.</param>
    /// <returns>The scroll height in pixels, or 0 if the element is null.</returns>
    /// <example>
    /// <code>
    /// [Inject] private IJSRuntime JS { get; set; } = default!;
    /// private ElementReference _contentRef;
    ///
    /// var height = await _contentRef.GetScrollHeightAsync(JS);
    /// </code>
    /// </example>
    public static async ValueTask<int> GetScrollHeightAsync(
        this ElementReference elementRef,
        IJSRuntime js)
    {
        return await js.InvokeAsync<int>("flowbiteBlazor.getScrollHeight", elementRef);
    }
}
