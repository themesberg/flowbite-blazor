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
    /// Additional attributes applied to the button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

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
            "inline-flex w-full items-center justify-center rounded-full bg-primary-600 px-4 py-2 text-sm font-semibold " +
            "text-white shadow-sm transition focus:outline-none focus:ring-2 focus:ring-primary-500 sm:w-auto";

        if (Disabled || Status is PromptSubmissionStatus.Submitting or PromptSubmissionStatus.Streaming)
        {
            classes += " opacity-70";
        }

        return classes;
    }
}
