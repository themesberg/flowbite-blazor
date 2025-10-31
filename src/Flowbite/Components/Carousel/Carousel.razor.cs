using Microsoft.AspNetCore.Components;
using Flowbite.Base;
using System.Timers;

namespace Flowbite.Components.Carousel;

/// <summary>
/// A carousel component for displaying a collection of slides with navigation controls and indicators.
/// </summary>
public partial class Carousel : IDisposable
{
    private System.Timers.Timer? _autoAdvanceTimer;
    private CarouselState _carouselState = null!;
    private int _currentIndex;
    private int _slideCount;

    /// <summary>
    /// Gets or sets the current slide index (zero-based). Supports two-way binding.
    /// </summary>
    [Parameter]
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the event callback that is invoked when the current slide index changes.
    /// </summary>
    [Parameter]
    public EventCallback<int> IndexChanged { get; set; }

    /// <summary>
    /// Gets or sets the interval in milliseconds for automatic slide advancement.
    /// Set to null to disable auto-advance. Default is null.
    /// </summary>
    [Parameter]
    public int? AutoAdvanceInterval { get; set; }

    /// <summary>
    /// Gets or sets the transition duration in milliseconds. Default is 1000ms.
    /// </summary>
    [Parameter]
    public int TransitionDuration { get; set; } = 1000;

    /// <summary>
    /// Gets or sets the event callback that is invoked when a slide change occurs.
    /// </summary>
    [Parameter]
    public EventCallback<int> OnSlideChanged { get; set; }

    /// <summary>
    /// Gets or sets the child content to be rendered inside the carousel.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets additional HTML attributes to be applied to the carousel container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void OnInitialized()
    {
        _currentIndex = Index;
        InitializeCarouselState();
    }

    protected override void OnParametersSet()
    {
        
        // Sync external Index parameter changes
        if (_currentIndex != Index)
        {
            _currentIndex = Index;
            InitializeCarouselState();
        }

        // Setup or update auto-advance timer
        ConfigureAutoAdvance();
    }

    /// <summary>
    /// Registers a slide with the carousel.
    /// </summary>
    internal void RegisterSlide()
    {
        _slideCount++;
        InitializeCarouselState();
        StateHasChanged();
    }

    /// <summary>
    /// Unregisters a slide from the carousel.
    /// </summary>
    internal void UnregisterSlide()
    {
        _slideCount--;
        if (_currentIndex >= _slideCount && _slideCount > 0)
        {
            _currentIndex = _slideCount - 1;
        }
        InitializeCarouselState();
        StateHasChanged();
    }

    private void InitializeCarouselState()
    {
        _carouselState = new CarouselState(
            currentIndex: _currentIndex,
            totalSlides: _slideCount,
            goToSlide: GoToSlide,
            nextSlide: NextSlide,
            previousSlide: PreviousSlide
        );
    }

    private void ConfigureAutoAdvance()
    {
        _autoAdvanceTimer?.Stop();
        _autoAdvanceTimer?.Dispose();
        _autoAdvanceTimer = null;

        if (AutoAdvanceInterval.HasValue && AutoAdvanceInterval.Value > 0)
        {
            _autoAdvanceTimer = new System.Timers.Timer(AutoAdvanceInterval.Value);
            _autoAdvanceTimer.Elapsed += OnAutoAdvanceTimerElapsed;
            _autoAdvanceTimer.AutoReset = true;
            _autoAdvanceTimer.Start();
        }
    }

    private async void OnAutoAdvanceTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        await InvokeAsync(() =>
        {
            NextSlide();
            StateHasChanged();
        });
    }

    private void GoToSlide(int index)
    {
        if (index < 0 || index >= _slideCount || index == _currentIndex)
        {
            return;
        }

        _currentIndex = index;
        InitializeCarouselState();
        
        _ = IndexChanged.InvokeAsync(_currentIndex);
        _ = OnSlideChanged.InvokeAsync(_currentIndex);
        
        StateHasChanged();
    }

    private void NextSlide()
    {
        int nextIndex = (_currentIndex + 1) % _slideCount;
        if (_slideCount > 0)
        {
            GoToSlide(nextIndex);
        }
    }

    private void PreviousSlide()
    {
        int previousIndex = _currentIndex - 1;
        if (previousIndex < 0)
        {
            previousIndex = _slideCount - 1;
        }
        if (_slideCount > 0)
        {
            GoToSlide(previousIndex);
        }
    }

    private string GetCarouselClasses()
    {
        return CombineClasses("grid overflow-hidden relative rounded-lg h-56 sm:h-64 xl:h-80 2xl:h-96");
    }

    public void Dispose()
    {
        if (_autoAdvanceTimer != null)
        {
            _autoAdvanceTimer.Stop();
            _autoAdvanceTimer.Elapsed -= OnAutoAdvanceTimerElapsed;
            _autoAdvanceTimer.Dispose();
            _autoAdvanceTimer = null;
        }
    }
}
