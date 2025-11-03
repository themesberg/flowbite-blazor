using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provider component for the prompt input action menu.
/// </summary>
public partial class PromptInputActionMenu : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private readonly PromptInputActionMenuContext _context = new();

    /// <summary>
    /// Content for the menu provider.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "relative inline-flex";

    protected override void OnInitialized()
    {
        _context.OpenChanged += HandleOpenChanged;
    }

    private void HandleOpenChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        _context.OpenChanged -= HandleOpenChanged;
    }
}
