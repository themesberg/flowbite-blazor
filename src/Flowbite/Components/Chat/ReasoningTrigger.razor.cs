using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Button that toggles visibility of reasoning content.
/// </summary>
public partial class ReasoningTrigger : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    /// <summary>
    /// Optional custom trigger content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private ReasoningContext Context { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex items-center space-x-2 text-sm text-gray-700 dark:text-gray-400 transition hover:text-black dark:hover:text-white ";

    protected override void OnInitialized()
    {
        Context.StateChanged += HandleStateChanged;
    }

    private void HandleStateChanged() => InvokeAsync(StateHasChanged);

    private string GetMessage()
    {
        if (Context.IsStreaming)
        {
            return "Thinking...";
        }

        if (Context.DurationSeconds is int seconds && seconds > 0)
        {
            return $"Thought for {seconds}s";
        }

        return "Thought for a moment";
    }

    private string GetChevronClasses() => Context.IsOpen ? "h-4 w-4 transition" : "h-4 w-4 rotate-180 transition";

    private async Task HandleClick()
    {
        if (Context.ToggleAsync is not null)
        {
            await Context.ToggleAsync.Invoke();
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Context.StateChanged -= HandleStateChanged;
    }
}
