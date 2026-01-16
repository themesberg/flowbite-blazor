using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Toggle button that reveals the list of sources.
/// </summary>
public partial class SourcesTrigger : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Optional custom label. Defaults to "Used n sources".
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    [CascadingParameter] private SourcesContext SourcesContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex w-full items-center justify-start gap-2 rounded-full border border-transparent bg-gray-100 px-3 py-2 " +
        "text-sm font-medium text-gray-700 transition motion-reduce:transition-none hover:bg-gray-200 focus:outline-none focus:ring-2 focus:ring-primary-500 " +
        "dark:bg-gray-800 dark:text-gray-200";

    private string ContentId => SourcesContext.ContentId;

    private string GetChevronClasses() => SourcesContext.IsOpen ? "h-4 w-4 rotate-180 transition motion-reduce:transition-none" : "h-4 w-4 transition motion-reduce:transition-none";

    private string GetLabel() => Label ?? $"Used {SourcesContext.Count} source{(SourcesContext.Count == 1 ? string.Empty : "s")}";

    private Task HandleClick()
    {
        SourcesContext.Toggle();
        return Task.CompletedTask;
    }
}
