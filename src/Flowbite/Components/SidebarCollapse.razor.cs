namespace Flowbite.Components;

/// <summary>
/// Represents a collapsible section in the sidebar that can contain nested items.
/// </summary>
public partial class SidebarCollapse
{
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

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _isOpen = InitiallyOpen;
        base.OnInitialized();
    }

    private async Task ToggleState()
    {
        _isOpen = !_isOpen;
        if (OnStateChanged.HasDelegate)
        {
            await OnStateChanged.InvokeAsync(_isOpen);
        }
    }
}
