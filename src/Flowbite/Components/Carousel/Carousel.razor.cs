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
    private int _nextSlideIndex;
    private int _lastReceivedIndex;
    private bool _hasProcessedInitialParameters;

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
    /// Set to null to disable auto-advance. Default is 5000ms.
    /// </summary>
    [Parameter]
    public int? AutoAdvanceInterval { get; set; } = 5000;

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
        _currentIndex = ClampIndex(Index);
        _lastReceivedIndex = Index;
    }

    protected override void OnParametersSet()
    {
        int parameterIndex = Index;
        bool shouldClamp = false;

        if (!_hasProcessedInitialParameters)
        {
            _hasProcessedInitialParameters = true;
            _currentIndex = parameterIndex;
            _lastReceivedIndex = parameterIndex;
            shouldClamp = true;
        }
        else if (parameterIndex != _lastReceivedIndex)
        {
            _currentIndex = parameterIndex;
            _lastReceivedIndex = parameterIndex;
            shouldClamp = true;
        }
        else if (_currentIndex < 0 || (_slideCount > 0 && _currentIndex >= _slideCount))
        {
            shouldClamp = true;
        }

        InitializeCarouselState(ensureIndexInRange: shouldClamp);

        ConfigureAutoAdvance();
    }

    /// <summary>
    /// Registers a slide with the carousel and returns its assigned index.
    /// </summary>
    /// <returns>The assigned index for the slide.</returns>
    internal int RegisterSlide()
    {
        int assignedIndex = _nextSlideIndex;
        _nextSlideIndex++;
        _slideCount++;
        InitializeCarouselState(ensureIndexInRange: true);
        ConfigureAutoAdvance();
        StateHasChanged();
        return assignedIndex;
    }

    /// <summary>
    /// Unregisters a slide from the carousel.
    /// </summary>
    internal void UnregisterSlide()
    {
        if (_slideCount > 0)
        {
            _slideCount--;
        }

        if (_slideCount <= 0)
        {
            _slideCount = 0;
            if (_currentIndex != 0)
            {
                _currentIndex = 0;
            }
            _nextSlideIndex = 0;
        }

        InitializeCarouselState(ensureIndexInRange: true);
        ConfigureAutoAdvance();
        StateHasChanged();
    }

    private void InitializeCarouselState(bool ensureIndexInRange = false)
    {
        int previousIndex = _currentIndex;

        if (ensureIndexInRange)
        {
            _currentIndex = ClampIndex(_currentIndex);
        }

        _carouselState = new CarouselState(
            currentIndex: _currentIndex,
            totalSlides: _slideCount,
            goToSlide: GoToSlide,
            nextSlide: NextSlide,
            previousSlide: PreviousSlide
        );

        if (ensureIndexInRange && previousIndex != _currentIndex)
        {
            NotifyIndexChanged();
        }
    }

    private void ConfigureAutoAdvance()
    {
        _autoAdvanceTimer?.Stop();
        _autoAdvanceTimer?.Dispose();
        _autoAdvanceTimer = null;

        if (AutoAdvanceInterval.HasValue && AutoAdvanceInterval.Value > 0 && _slideCount > 1)
        {
            _autoAdvanceTimer = new System.Timers.Timer(AutoAdvanceInterval.Value);
            _autoAdvanceTimer.Elapsed += OnAutoAdvanceTimerElapsed;
            _autoAdvanceTimer.AutoReset = true;
            _autoAdvanceTimer.Start();
        }
    }

    /// <summary>
    /// Pauses the auto-advance timer temporarily.
    /// </summary>
    internal void PauseAutoAdvance()
    {
        _autoAdvanceTimer?.Stop();
    }

    /// <summary>
    /// Resumes the auto-advance timer.
    /// </summary>
    internal void ResumeAutoAdvance()
    {
        if (AutoAdvanceInterval.HasValue && AutoAdvanceInterval.Value > 0 && _slideCount > 1)
        {
            _autoAdvanceTimer?.Start();
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
        NotifyIndexChanged();
        
        StateHasChanged();
    }

    private void NextSlide()
    {
        if (_slideCount > 0)
        {
            int nextIndex = (_currentIndex + 1) % _slideCount;
            GoToSlide(nextIndex);
        }
    }

    private void PreviousSlide()
    {
        if (_slideCount > 0)
        {
            int previousIndex = _currentIndex - 1;
            if (previousIndex < 0)
            {
                previousIndex = _slideCount - 1;
            }
            GoToSlide(previousIndex);
        }
    }

    private string GetCarouselClasses()
    {
        return CombineClasses("grid overflow-hidden relative rounded-lg h-56 sm:h-64 xl:h-80 2xl:h-96");
    }

    private int ClampIndex(int value)
    {
        if (_slideCount <= 0)
        {
            return Math.Max(0, value);
        }

        if (value < 0)
        {
            return 0;
        }

        if (value >= _slideCount)
        {
            return _slideCount - 1;
        }

        return value;
    }

    private void NotifyIndexChanged()
    {
        _ = IndexChanged.InvokeAsync(_currentIndex);
        _ = OnSlideChanged.InvokeAsync(_currentIndex);
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
