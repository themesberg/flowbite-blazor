using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Renders the list of attachments associated with the prompt input.
/// </summary>
public partial class PromptInputAttachments : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private IReadOnlyList<PromptAttachment> _attachments = Array.Empty<PromptAttachment>();
    private bool _hasAttachments;

    /// <summary>
    /// Template used to render each attachment.
    /// </summary>
    [Parameter]
    public RenderFragment<PromptAttachment>? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the attachment container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputContext Context { get; set; } = default!;

    private string BaseClasses => "flex flex-wrap gap-2 rounded-2xl bg-gray-100/80 p-2 dark:bg-slate-950/50";

    protected override void OnInitialized()
    {
        UpdateAttachments();
        Context.AttachmentsChanged += HandleAttachmentsChanged;
    }

    private void HandleAttachmentsChanged()
    {
        UpdateAttachments();
        InvokeAsync(StateHasChanged);
    }

    private void UpdateAttachments()
    {
        _attachments = Context.Attachments.ToList().AsReadOnly();
        _hasAttachments = _attachments.Count > 0;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Context.AttachmentsChanged -= HandleAttachmentsChanged;
    }
}
