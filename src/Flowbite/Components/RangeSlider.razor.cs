using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public enum RangeSliderSize
{
    Small,
    Medium,
    Large
}

public partial class RangeSlider
{
    private ElementReference _inputRef;

    /// <summary>
    /// Gets or sets the ID of the range slider.
    /// </summary>
    [Parameter] public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the current value of the range slider.
    /// </summary>
    [Parameter] public double Value { get; set; }

    /// <summary>
    /// Event callback for when the value changes.
    /// </summary>
    [Parameter] public EventCallback<double> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the range slider is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets the size of the range slider.
    /// </summary>
    [Parameter] public RangeSliderSize Size { get; set; } = RangeSliderSize.Medium;

    /// <summary>
    /// Gets or sets the minimum value of the range slider.
    /// </summary>
    [Parameter] public double Min { get; set; }

    /// <summary>
    /// Gets or sets the maximum value of the range slider.
    /// </summary>
    [Parameter] public double Max { get; set; } = 100;

    /// <summary>
    /// Gets or sets the step value of the range slider.
    /// </summary>
    [Parameter] public double Step { get; set; } = 1;

    private string GetRootClasses()
    {
        return "relative";
    }

    private string GetInputClasses()
    {
        var classes = new List<string>
        {
            "w-full bg-gray-200 rounded-lg appearance-none cursor-pointer dark:bg-gray-700",
            "focus:outline-none focus:ring-0",
            "[&::-webkit-slider-thumb]:appearance-none",
            "[&::-webkit-slider-thumb]:rounded-full",
            "[&::-webkit-slider-thumb]:bg-primary-700",
            "[&::-webkit-slider-thumb]:dark:bg-primary-500",
            "[&::-webkit-slider-thumb]:hover:bg-primary-600",
            "[&::-webkit-slider-thumb]:dark:hover:bg-primary-400",
            "[&::-moz-range-thumb]:rounded-full",
            "[&::-moz-range-thumb]:border-0",
            "[&::-moz-range-thumb]:bg-primary-700",
            "[&::-moz-range-thumb]:dark:bg-primary-500",
            "[&::-moz-range-thumb]:hover:bg-primary-600",
            "[&::-moz-range-thumb]:dark:hover:bg-primary-400"
        };

        var sizeClasses = Size switch
        {
            RangeSliderSize.Small => new[]
            {
                "h-1",
                "[&::-webkit-slider-thumb]:h-2 [&::-webkit-slider-thumb]:w-2",
                "[&::-moz-range-thumb]:h-2 [&::-moz-range-thumb]:w-2"
            },
            RangeSliderSize.Large => new[]
            {
                "h-3",
                "[&::-webkit-slider-thumb]:h-4 [&::-webkit-slider-thumb]:w-4",
                "[&::-moz-range-thumb]:h-4 [&::-moz-range-thumb]:w-4"
            },
            _ => new[]
            {
                "h-2",
                "[&::-webkit-slider-thumb]:h-3 [&::-webkit-slider-thumb]:w-3",
                "[&::-moz-range-thumb]:h-3 [&::-moz-range-thumb]:w-3"
            }
        };

        classes.AddRange(sizeClasses);

        if (Disabled)
        {
            classes.Add("cursor-not-allowed opacity-50");
            classes.Add("[&::-webkit-slider-thumb]:cursor-not-allowed");
            classes.Add("[&::-moz-range-thumb]:cursor-not-allowed");
        }

        return string.Join(" ", classes);
    }
}
