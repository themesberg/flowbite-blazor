using Microsoft.AspNetCore.Components;
using Flowbite.Base;
using Flowbite.Utilities;

namespace Flowbite.Components.Carousel;

/// <summary>
/// Represents an individual slide within a carousel component.
/// </summary>
public partial class CarouselItem : FlowbiteComponentBase, IDisposable
{
    private int _assignedIndex = -1;
    private bool _isRegistered;

    /// <summary>
    /// Gets or sets the cascaded carousel instance.
    /// </summary>
    [CascadingParameter]
    private Carousel? ParentCarousel { get; set; }

    /// <summary>
    /// Gets or sets the cascaded carousel state.
    /// </summary>
    [CascadingParameter]
    private CarouselState? CarouselState { get; set; }

    /// <summary>
    /// Gets or sets the image source URL. If provided, an image will be rendered.
    /// </summary>
    [Parameter]
    public string? ImageSrc { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for the image.
    /// </summary>
    [Parameter]
    public string ImageAlt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional title text for the image.
    /// </summary>
    [Parameter]
    public string? ImageTitle { get; set; }

    /// <summary>
    /// Gets or sets how the image should fit within its container. Default is Cover.
    /// </summary>
    [Parameter]
    public CarouselImageFit ImageFit { get; set; } = CarouselImageFit.Cover;

    /// <summary>
    /// Gets or sets the custom content to be rendered in the slide.
    /// Only used if ImageSrc is not provided.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        if (ParentCarousel != null && !_isRegistered)
        {
            _assignedIndex = ParentCarousel.RegisterSlide();
            _isRegistered = true;
        }
    }

    private string GetSlideClasses()
    {
        bool isActive = _assignedIndex >= 0 && CarouselState?.CurrentIndex == _assignedIndex;

        // Base classes with transform + opacity transition for smooth sliding effect
        // Active slide: fully visible and centered
        // Inactive slide: faded out with slight scale down
        return MergeClasses(
            ElementClass.Empty()
                .Add("absolute inset-0 transition-all duration-500 ease-in-out motion-reduce:transition-none")
                .Add("opacity-100 z-10 scale-100", when: isActive)
                .Add("opacity-0 pointer-events-none scale-95", when: !isActive)
                .Add(Class)
        );
    }

    private string GetImageClass()
    {
        string fitClass = ImageFit switch
        {
            CarouselImageFit.Cover => "object-cover",
            CarouselImageFit.Contain => "object-contain",
            CarouselImageFit.Fill => "object-fill",
            CarouselImageFit.ScaleDown => "object-scale-down",
            CarouselImageFit.None => "object-none",
            _ => "object-cover"
        };

        return $"block w-full h-full {fitClass}";
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (ParentCarousel != null && _isRegistered)
        {
            ParentCarousel.UnregisterSlide();
            _isRegistered = false;
        }
    }
}
