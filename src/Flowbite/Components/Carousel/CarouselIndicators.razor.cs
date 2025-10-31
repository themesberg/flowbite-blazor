using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Indicator buttons for a carousel component, allowing direct navigation to specific slides.
/// </summary>
public partial class CarouselIndicators
{
    /// <summary>
    /// Gets or sets the cascaded carousel state.
    /// </summary>
    [CascadingParameter]
    private CarouselState? CarouselState { get; set; }

    /// <summary>
    /// Gets or sets the position of the indicators. Default is Bottom.
    /// </summary>
    [Parameter]
    public CarouselIndicatorPosition Position { get; set; } = CarouselIndicatorPosition.Bottom;

    private void HandleIndicatorClick(int index)
    {
        CarouselState?.GoToSlide(index);
    }

    private string GetIndicatorButtonClass(int index)
    {
        bool isActive = index == CarouselState?.CurrentIndex;
        
        string baseClass = "w-3 h-3 rounded-full";
        string stateClass = isActive 
            ? "bg-white dark:bg-gray-800" 
            : "bg-white/50 dark:bg-gray-800/50 hover:bg-white dark:hover:bg-gray-800";
        
        return $"{baseClass} {stateClass}";
    }

    /// <inheritdoc />
    protected override string DefaultClass
    {
        get
        {
            string positionClass = Position switch
            {
                CarouselIndicatorPosition.Top => "top-5",
                CarouselIndicatorPosition.Bottom => "bottom-5",
                _ => "bottom-5"
            };

            return $"absolute z-30 flex -translate-x-1/2 space-x-3 rtl:space-x-reverse left-1/2 {positionClass}";
        }
    }
}
