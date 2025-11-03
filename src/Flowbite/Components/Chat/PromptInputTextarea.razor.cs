using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Multiline text area bound to the parent prompt input context.
/// </summary>
public partial class PromptInputTextarea : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private string _value = string.Empty;

    /// <summary>
    /// Placeholder text shown when the textarea is empty.
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Ask anything...";

    /// <summary>
    /// Number of visible text rows.
    /// </summary>
    [Parameter]
    public int Rows { get; set; } = 3;

    /// <summary>
    /// Additional attributes applied to the textarea element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Fired when the input value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnChanged { get; set; }

    [CascadingParameter] private PromptInputContext Context { get; set; } = default!;

    private string BaseClasses =>
        "min-h-[120px] w-full resize-none rounded-xl border border-transparent bg-gray-100 px-4 py-3 " +
        "text-sm text-gray-900 outline-none focus:border-primary-500 focus:bg-white focus:ring-2 focus:ring-primary-500 " +
        "dark:bg-gray-800 dark:text-gray-100 dark:focus:bg-gray-900";

    protected override void OnInitialized()
    {
        _value = Context.Text;
        Context.TextChanged += HandleContextTextChanged;
    }

    private async Task HandleInputAsync(ChangeEventArgs args)
    {
        var next = args.Value?.ToString() ?? string.Empty;
        _value = next;
        Context.SetText(next);

        if (OnChanged.HasDelegate)
        {
            await OnChanged.InvokeAsync(next);
        }
    }

    private void HandleContextTextChanged()
    {
        var next = Context.Text;
        if (!string.Equals(_value, next, StringComparison.Ordinal))
        {
            _value = next;
            InvokeAsync(StateHasChanged);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Context.TextChanged -= HandleContextTextChanged;
    }
}
