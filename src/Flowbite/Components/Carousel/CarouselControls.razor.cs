using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Navigation controls for a carousel component, providing previous and next buttons.
/// </summary>
public partial class CarouselControls
{
    /// <summary>
    /// Gets or sets the cascaded carousel state.
    /// </summary>
    [CascadingParameter]
    private CarouselState? CarouselState { get; set; }

    /// <summary>
    /// Gets or sets custom content for the previous button.
    /// If not provided, a default chevron icon will be used.
    /// </summary>
    [Parameter]
    public RenderFragment? PreviousContent { get; set; }

    /// <summary>
    /// Gets or sets custom content for the next button.
    /// If not provided, a default chevron icon will be used.
    /// </summary>
    [Parameter]
    public RenderFragment? NextContent { get; set; }

    private void HandlePreviousClick()
    {
        CarouselState?.PreviousSlide();
    }

    private void HandleNextClick()
    {
        CarouselState?.NextSlide();
    }

    private string GetPreviousButtonClass()
    {
        return "absolute top-0 start-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none";
    }

    private string GetNextButtonClass()
    {
        return "absolute top-0 end-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none";
    }

    /// <inheritdoc />
    protected override string DefaultClass => string.Empty;
}
