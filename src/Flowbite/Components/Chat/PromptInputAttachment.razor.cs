using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Renders a single attachment chip within the prompt input.
/// </summary>
public partial class PromptInputAttachment : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// The attachment data to render.
    /// </summary>
    [Parameter, EditorRequired]
    public PromptAttachment? Data { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputContext Context { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex min-w-0 items-center gap-2 rounded-full border border-gray-200/80 bg-white/90 px-3 py-1.5 text-sm text-gray-700 shadow-sm transition " +
        "hover:border-gray-300 dark:border-white/10 dark:bg-slate-950/60 dark:text-gray-200";

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
}
