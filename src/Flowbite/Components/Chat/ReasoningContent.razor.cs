using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Collapsible reasoning body content.
/// </summary>
public partial class ReasoningContent : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    /// <summary>
    /// Optional text content rendered inside the reasoning panel.
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Child content rendered when <see cref="Text"/> is not provided.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the content container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private ReasoningContext Context { get; set; } = default!;

    private string BaseClasses => "";

    protected override void OnInitialized()
    {
        Context.StateChanged += HandleStateChanged;
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        Context.StateChanged -= HandleStateChanged;
    }
}
