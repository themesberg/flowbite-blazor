using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Submit button tailored for the prompt input component.
/// </summary>
public partial class PromptInputSubmit : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Current submission status which drives visual feedback.
    /// </summary>
    [Parameter]
    public PromptSubmissionStatus Status { get; set; } = PromptSubmissionStatus.Idle;

    /// <summary>
    /// Disables the button when true.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Label displayed when idle.
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "Send";

    /// <summary>
    /// Label displayed while submitting.
    /// </summary>
    [Parameter]
    public string SubmittingLabel { get; set; } = "Sending";

    /// <summary>
    /// Label displayed while streaming a response.
    /// </summary>
    [Parameter]
    public string StreamingLabel { get; set; } = "Streaming";

    /// <summary>
    /// Label displayed when submission failed.
    /// </summary>
    [Parameter]
    public string ErrorLabel { get; set; } = "Retry";

    /// <summary>
    /// Optional custom content rendered when the submission status is idle.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetLabel() => Status switch
    {
        PromptSubmissionStatus.Submitting => SubmittingLabel,
        PromptSubmissionStatus.Streaming => StreamingLabel,
        PromptSubmissionStatus.Error => ErrorLabel,
        _ => Label
    };

    private string GetButtonClasses()
    {
        var classes =
            "h-10 w-10 items-center gap-2 rounded-full bg-primary-600 text-sm font-medium text-white shadow-lg " +
            "transition hover:bg-primary-500 focus:outline-none focus:ring-2 focus:ring-primary-400/70 " +
            "dark:bg-primary-500 dark:hover:bg-primary-400";

        if (Disabled || Status is PromptSubmissionStatus.Submitting or PromptSubmissionStatus.Streaming)
        {
            classes += " cursor-not-allowed opacity-60";
        }

        return classes;
    }
}
