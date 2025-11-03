using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Displays the current model selection or a placeholder when none is selected.
/// </summary>
public partial class PromptInputModelSelectValue : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    /// <summary>
    /// Placeholder text displayed when no option is selected.
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Select a model";

    /// <summary>
    /// Additional attributes applied to the span element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string DisplayText => ModelContext.DisplayLabel ?? Placeholder;

    private string BaseClasses => "text-sm font-medium text-gray-600 dark:text-gray-200";

    protected override void OnInitialized()
    {
        ModelContext.StateChanged += HandleStateChanged;
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        ModelContext.StateChanged -= HandleStateChanged;
    }
}
