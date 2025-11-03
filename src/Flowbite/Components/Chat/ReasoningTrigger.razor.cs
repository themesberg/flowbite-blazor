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
        "inline-flex w-full items-center gap-2 rounded-full border border-transparent bg-gray-100 px-3 py-2 text-sm " +
        "text-gray-700 transition hover:bg-gray-200 focus:outline-none focus:ring-2 focus:ring-primary-500 " +
        "dark:bg-gray-800 dark:text-gray-200";

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
            return seconds == 1 ? "Thought for 1 second" : $"Thought for {seconds} seconds";
        }

        return "Thought for a moment";
    }

    private string GetChevronClasses() => Context.IsOpen ? "h-4 w-4 rotate-180 transition" : "h-4 w-4 transition";

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
