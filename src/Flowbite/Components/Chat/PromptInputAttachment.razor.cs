using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Renders a single attachment chip within the prompt input.
/// </summary>
public partial class PromptInputAttachment : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private PromptAttachment? _previousAttachment;
    private CancellationTokenSource? _previewCancellation;
    private string? _previewDataUrl;

    /// <summary>
    /// The attachment data to render.
    /// </summary>
    [Parameter, EditorRequired]
    public PromptAttachment? Data { get; set; }

    [CascadingParameter] private PromptInputContext Context { get; set; } = default!;

    private string BaseClasses =>
        "group relative flex min-w-0 items-center gap-3 overflow-hidden rounded-2xl border border-gray-200/90 bg-white/95 px-3.5 py-2 shadow-sm transition motion-reduce:transition-none " +
        "hover:border-gray-300 dark:border-white/10 dark:bg-slate-950/85";

    private bool HasPreview => !string.IsNullOrEmpty(_previewDataUrl);

    private string PreviewAltText =>
        Data?.Name is { Length: > 0 } name ? $"{name} preview" : "Attachment preview";

    protected override async Task OnParametersSetAsync()
    {
        if (ReferenceEquals(_previousAttachment, Data))
        {
            return;
        }

        _previousAttachment = Data;
        CancelPreview();
        _previewDataUrl = null;

        if (Data?.SupportsPreview == true)
        {
            var cancellation = new CancellationTokenSource();
            _previewCancellation = cancellation;

            try
            {
                var preview = await Data.GetPreviewDataUrlAsync(cancellation.Token).ConfigureAwait(false);
                if (!cancellation.IsCancellationRequested)
                {
                    _previewDataUrl = preview;
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch
            {
                // Ignore preview errors and fall back to the non-preview rendering.
            }
        }
    }

    private string FormatSize(long bytes)
    {
        if (bytes < 1024)
        {
            return $"{bytes} B";
        }

        double size = bytes;
        string[] units = { "KB", "MB", "GB" };
        int unitIndex = 0;

        while (size >= 1024 && unitIndex < units.Length - 1)
        {
            size /= 1024;
            unitIndex++;
        }

        return $"{size:0.##} {units[unitIndex]}";
    }

    private Task RemoveAsync()
    {
        if (Data is not null)
        {
            Context.RemoveAttachment(Data.Id);
        }

        return Task.CompletedTask;
    }

    private void CancelPreview()
    {
        _previewCancellation?.Cancel();
        _previewCancellation?.Dispose();
        _previewCancellation = null;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        CancelPreview();
    }
}
