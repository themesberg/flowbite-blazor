using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provides a lightweight custom select experience for choosing models.
/// </summary>
public partial class PromptInputModelSelect : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private readonly PromptInputModelSelectContext _context;

    public PromptInputModelSelect()
    {
        _context = new PromptInputModelSelectContext(OnValueChangedInternalAsync);
        _context.StateChanged += HandleStateChanged;
    }

    /// <summary>
    /// The current selected value.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Raised when the selected value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Content comprising trigger, value, and menu.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "relative inline-flex";

    protected override void OnParametersSet()
    {
        _context.UpdateValue(Value);
    }

    private Task OnValueChangedInternalAsync(string? value)
    {
        return ValueChanged.HasDelegate
            ? ValueChanged.InvokeAsync(value)
            : Task.CompletedTask;
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        _context.StateChanged -= HandleStateChanged;
    }
}
