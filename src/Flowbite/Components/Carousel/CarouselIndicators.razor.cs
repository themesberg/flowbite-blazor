using Microsoft.AspNetCore.Components;
using Flowbite.Base;
using Flowbite.Utilities;

namespace Flowbite.Components.Carousel;

/// <summary>
/// Indicator buttons for a carousel component, allowing direct navigation to specific slides.
/// </summary>
public partial class CarouselIndicators : FlowbiteComponentBase
{
    /// <summary>
    /// Gets or sets the cascaded carousel state.
    /// </summary>
    [CascadingParameter]
    public CarouselState? State { get; set; }

    /// <summary>
    /// Gets or sets the position of the indicators. Default is Bottom.
    /// </summary>
    [Parameter]
    public CarouselIndicatorPosition Position { get; set; } = CarouselIndicatorPosition.Bottom;

    private void HandleIndicatorClick(int index)
    {
        State?.GoToSlide(index);
    }

    private string GetIndicatorButtonClass(int index)
    {
        bool isActive = index == State?.CurrentIndex;

        return MergeClasses(
            ElementClass.Empty()
                .Add("w-3 h-3 rounded-full transition-all motion-reduce:transition-none")
                .Add("bg-white dark:bg-gray-800", when: isActive)
                .Add("bg-white/50 dark:bg-gray-800/50 hover:bg-white dark:hover:bg-gray-800", when: !isActive)
        );
    }

    private string GetIndicatorContainerClass()
    {
        string positionClass = Position switch
        {
            CarouselIndicatorPosition.Top => "top-5",
            CarouselIndicatorPosition.Bottom => "bottom-5",
            _ => "bottom-5"
        };

        return MergeClasses(
            ElementClass.Empty()
                .Add("absolute z-30 flex -translate-x-1/2 space-x-3 rtl:space-x-reverse left-1/2")
                .Add(positionClass)
                .Add(Class)
        );
    }
}
