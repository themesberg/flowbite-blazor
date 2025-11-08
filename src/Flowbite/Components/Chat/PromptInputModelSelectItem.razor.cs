using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Individual selectable model option within the dropdown.
/// </summary>
public partial class PromptInputModelSelectItem : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// The option value emitted when selected.
    /// </summary>
    [Parameter, EditorRequired]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// The label displayed when no custom content is provided.
    /// </summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Optional custom item content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the option element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputModelSelectContext ModelContext { get; set; } = default!;

    private string GetItemClasses()
    {
        var isActive = string.Equals(ModelContext.Value, Value, StringComparison.Ordinal);
        var baseClasses =
            "flex w-full items-center gap-2 rounded-xl px-3 py-2 text-sm text-left transition-colors hover:bg-gray-100/80 " +
            "dark:hover:bg-slate-900/70";
        return isActive
            ? baseClasses + " bg-gray-100 font-semibold text-gray-900 dark:bg-slate-900/80 dark:text-white"
            : baseClasses + " text-gray-600 dark:text-gray-200";
    }

    protected override void OnInitialized()
    {
        ModelContext.RegisterOption(Value, Label);
    }

    private async Task HandleClickAsync()
    {
        await ModelContext.SelectAsync(Value, Label);
    }
}
