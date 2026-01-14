using Flowbite.Common;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Components;

/// <summary>
/// Represents a collapsible section in the sidebar that can contain nested items.
/// Features smooth height-based animations for expand/collapse transitions.
/// </summary>
public partial class SidebarCollapse : IDisposable
{
    private System.Threading.Timer? _animationTimer;
    private bool _disposed;

    /// <summary>
    /// Gets or sets the JavaScript runtime for interop calls.
    /// </summary>
    [Inject]
    private IJSRuntime JS { get; set; } = default!;

    /// <summary>
    /// Gets or sets whether the collapse section is initially open.
    /// </summary>
    [Parameter]
    public bool InitiallyOpen { get; set; }

    /// <summary>
    /// Gets or sets a callback that is invoked when the collapse state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> OnStateChanged { get; set; }

    /// <summary>
    /// Gets whether the content is currently visible (expanded or expanding).
    /// </summary>
    public bool IsExpanded => _state is CollapseState.Expanded or CollapseState.Expanding;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _state = InitiallyOpen ? CollapseState.Expanded : CollapseState.Collapsed;
        base.OnInitialized();
    }

    /// <summary>
    /// Toggles the collapse state with smooth animation.
    /// </summary>
    private async Task ToggleStateAsync()
    {
        // Cancel any pending animation timer
        CancelAnimationTimer();

        switch (_state)
        {
            case CollapseState.Collapsed:
                // Measure content height, then start expanding
                _height = await _contentRef.GetScrollHeightAsync(JS);
                _state = CollapseState.Expanding;
                StartAnimationTimer();
                break;

            case CollapseState.Expanded:
                // Measure current height, set it explicitly, then collapse
                _height = await _contentRef.GetScrollHeightAsync(JS);
                StateHasChanged();
                await Task.Yield(); // Allow height to be rendered before collapse
                _state = CollapseState.Collapsing;
                _height = 0;
                StartAnimationTimer();
                break;

            case CollapseState.Expanding:
                // Mid-animation: reverse to collapsing
                _state = CollapseState.Collapsing;
                _height = 0;
                StartAnimationTimer();
                break;

            case CollapseState.Collapsing:
                // Mid-animation: reverse to expanding
                _height = await _contentRef.GetScrollHeightAsync(JS);
                _state = CollapseState.Expanding;
                StartAnimationTimer();
                break;
        }

        StateHasChanged();

        if (OnStateChanged.HasDelegate)
        {
            await OnStateChanged.InvokeAsync(IsExpanded);
        }
    }

    /// <summary>
    /// Starts a fallback timer to finalize the animation state.
    /// This ensures the animation completes even if transitionend doesn't fire.
    /// </summary>
    private void StartAnimationTimer()
    {
        // 350ms to give a small buffer beyond the 300ms CSS animation
        _animationTimer = new System.Threading.Timer(
            _ => _ = InvokeAsync(FinalizeTransition),
            null,
            350,
            Timeout.Infinite
        );
    }

    /// <summary>
    /// Cancels any pending animation timer.
    /// </summary>
    private void CancelAnimationTimer()
    {
        _animationTimer?.Dispose();
        _animationTimer = null;
    }

    /// <summary>
    /// Finalizes the animation by transitioning to the target state.
    /// Called by both the transitionend event and the fallback timer.
    /// </summary>
    private void FinalizeTransition()
    {
        CancelAnimationTimer();

        var newState = _state switch
        {
            CollapseState.Expanding => CollapseState.Expanded,
            CollapseState.Collapsing => CollapseState.Collapsed,
            _ => _state
        };

        // Only update if state actually changed
        if (newState != _state)
        {
            _state = newState;
            StateHasChanged();
        }
    }

    /// <summary>
    /// Handles the CSS transition end event to finalize state.
    /// </summary>
    private void HandleTransitionEnd()
    {
        FinalizeTransition();
    }

    /// <summary>
    /// Gets the inline style for the collapsible content based on current state.
    /// </summary>
    private string GetContentStyle() => _state switch
    {
        CollapseState.Expanding => $"height: {_height}px",
        CollapseState.Collapsing => "height: 0px",
        CollapseState.Expanded => "height: auto",
        CollapseState.Collapsed => "height: 0px",
        _ => "height: 0px"
    };

    /// <inheritdoc/>
    public void Dispose()
    {
        if (!_disposed)
        {
            CancelAnimationTimer();
            _disposed = true;
        }
    }
}
