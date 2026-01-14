using Flowbite.Common;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Components;

/// <summary>
/// Represents a collapsible section in the sidebar that can contain nested items.
/// Features smooth height-based animations for expand/collapse transitions.
/// </summary>
public partial class SidebarCollapse
{
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
        switch (_state)
        {
            case CollapseState.Collapsed:
                // Measure content height, then start expanding
                _height = await _contentRef.GetScrollHeightAsync(JS);
                _state = CollapseState.Expanding;
                break;

            case CollapseState.Expanded:
                // Measure current height, set it explicitly, then collapse
                _height = await _contentRef.GetScrollHeightAsync(JS);
                StateHasChanged();
                await Task.Yield(); // Allow height to be rendered before collapse
                _state = CollapseState.Collapsing;
                _height = 0;
                break;

            case CollapseState.Expanding:
                // Mid-animation: reverse to collapsing
                _state = CollapseState.Collapsing;
                _height = 0;
                break;

            case CollapseState.Collapsing:
                // Mid-animation: reverse to expanding
                _height = await _contentRef.GetScrollHeightAsync(JS);
                _state = CollapseState.Expanding;
                break;
        }

        StateHasChanged();

        if (OnStateChanged.HasDelegate)
        {
            await OnStateChanged.InvokeAsync(IsExpanded);
        }
    }

    /// <summary>
    /// Handles the CSS transition end event to finalize state.
    /// </summary>
    private void HandleTransitionEnd()
    {
        _state = _state switch
        {
            CollapseState.Expanding => CollapseState.Expanded,
            CollapseState.Collapsing => CollapseState.Collapsed,
            _ => _state
        };
        StateHasChanged();
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
}
