using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Container for displaying cited sources within an assistant response.
/// </summary>
public partial class Sources : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private readonly SourcesContext _context = new();

    /// <summary>
    /// Number of sources available.
    /// </summary>
    [Parameter]
    public int Count { get; set; }

    /// <summary>
    /// Source content including trigger and list.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the wrapper element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "flex flex-col gap-2";

    protected override void OnInitialized()
    {
        _context.StateChanged += HandleStateChanged;
        _context.SetCount(Count);
    }

    protected override void OnParametersSet()
    {
        _context.SetCount(Count);
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        _context.StateChanged -= HandleStateChanged;
    }
}
