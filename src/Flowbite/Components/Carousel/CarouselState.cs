namespace Flowbite.Components;

/// <summary>
/// Represents the shared state for a carousel component and its child components.
/// This class is used as a cascading value to share navigation state and actions.
/// </summary>
public class CarouselState
{
    /// <summary>
    /// Gets the current slide index (zero-based).
    /// </summary>
    public int CurrentIndex { get; }

    /// <summary>
    /// Gets the total number of slides in the carousel.
    /// </summary>
    public int TotalSlides { get; }

    /// <summary>
    /// Gets the action to navigate to a specific slide by index.
    /// </summary>
    public Action<int> GoToSlide { get; }

    /// <summary>
    /// Gets the action to navigate to the next slide.
    /// </summary>
    public Action NextSlide { get; }

    /// <summary>
    /// Gets the action to navigate to the previous slide.
    /// </summary>
    public Action PreviousSlide { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarouselState"/> class.
    /// </summary>
    /// <param name="currentIndex">The current slide index.</param>
    /// <param name="totalSlides">The total number of slides.</param>
    /// <param name="goToSlide">The action to navigate to a specific slide.</param>
    /// <param name="nextSlide">The action to navigate to the next slide.</param>
    /// <param name="previousSlide">The action to navigate to the previous slide.</param>
    public CarouselState(
        int currentIndex,
        int totalSlides,
        Action<int> goToSlide,
        Action nextSlide,
        Action previousSlide)
    {
        CurrentIndex = currentIndex;
        TotalSlides = totalSlides;
        GoToSlide = goToSlide;
        NextSlide = nextSlide;
        PreviousSlide = previousSlide;
    }
}
