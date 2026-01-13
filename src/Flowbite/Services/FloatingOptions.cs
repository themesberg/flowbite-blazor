namespace Flowbite.Services;

/// <summary>
/// Configuration options for floating element positioning.
/// </summary>
/// <param name="Placement">Initial placement relative to the trigger element.
/// Valid values: top, top-start, top-end, bottom, bottom-start, bottom-end,
/// left, left-start, left-end, right, right-start, right-end.
/// Default: "bottom".</param>
/// <param name="Offset">Distance in pixels between the trigger and floating element. Default: 8.</param>
/// <param name="EnableFlip">Whether to flip placement when constrained by viewport. Default: true.</param>
/// <param name="EnableShift">Whether to shift along the axis when constrained. Default: true.</param>
/// <param name="ShiftPadding">Padding in pixels from viewport edge for shift middleware. Default: 8.</param>
public record FloatingOptions(
    string Placement = "bottom",
    int Offset = 8,
    bool EnableFlip = true,
    bool EnableShift = true,
    int ShiftPadding = 8
);
