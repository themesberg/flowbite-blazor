using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Collapsible container that surfaces assistant reasoning traces.
/// </summary>
public partial class Reasoning : Flowbite.Base.FlowbiteComponentBase
{
    private readonly ReasoningContext _context = new();
    private bool _open;
    private bool _isControlled;

    /// <summary>
    /// Indicates the reasoning content is still streaming.
    /// </summary>
    [Parameter]
    public bool IsStreaming { get; set; }

    /// <summary>
    /// Number of seconds spent thinking, if available.
    /// </summary>
    [Parameter]
    public int? DurationSeconds { get; set; }

    /// <summary>
    /// Sets the open state when using a controlled pattern.
    /// </summary>
    [Parameter]
    public bool? Open { get; set; }

    /// <summary>
    /// Raised when the open state changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> OpenChanged { get; set; }

    /// <summary>
    /// Initial open state for uncontrolled usage. Defaults to true.
    /// </summary>
    [Parameter]
    public bool DefaultOpen { get; set; } = true;

    /// <summary>
    /// Child content typically including trigger and content components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string BaseClasses => "flex flex-col gap-3 text-sm mb-4";

    protected override void OnInitialized()
    {
        _open = Open ?? DefaultOpen;
        _context.IsOpen = _open;
        _context.IsStreaming = IsStreaming;
        _context.DurationSeconds = DurationSeconds;
        _context.ToggleAsync = ToggleAsync;
    }

    protected override void OnParametersSet()
    {
        _isControlled = Open.HasValue || OpenChanged.HasDelegate;

        if (Open.HasValue)
        {
            _open = Open.Value;
        }

        _context.IsOpen = _open;
        _context.IsStreaming = IsStreaming;
        _context.DurationSeconds = DurationSeconds;
        _context.NotifyChanged();
    }

    private async Task ToggleAsync()
    {
        var next = !_open;

        if (_isControlled)
        {
            if (OpenChanged.HasDelegate)
            {
                await OpenChanged.InvokeAsync(next);
            }
        }
        else
        {
            _open = next;
            _context.IsOpen = _open;
            _context.NotifyChanged();
            await InvokeAsync(StateHasChanged);

            if (OpenChanged.HasDelegate)
            {
                await OpenChanged.InvokeAsync(_open);
            }
        }
    }
}
