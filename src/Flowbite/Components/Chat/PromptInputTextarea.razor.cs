using System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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
    public int Rows { get; set; } = 0;

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
        "min-h-[80px] w-full resize-none rounded-xl border border-gray-200 bg-white/85 px-4 py-3 text-base text-gray-900 " +
        "transition focus:border-primary-500 focus:bg-white focus:outline-none focus:ring-2 focus:ring-primary-400/60 " +
        "dark:border-white/10 dark:bg-slate-950/50 dark:text-slate-50 dark:focus:border-primary-400 dark:focus:ring-primary-400/40";

    protected override void OnInitialized()
    {
        _value = Context.Text;
        Context.TextChanged += HandleContextTextChanged;
    }

    private Task HandleKeyDownAsync(KeyboardEventArgs args)
    {
        if (args.Key is null)
        {
            return Task.CompletedTask;
        }

        if (string.Equals(args.Key, "Enter", StringComparison.Ordinal) &&
            !args.ShiftKey &&
            !args.AltKey &&
            !args.CtrlKey &&
            !args.MetaKey)
        {
            if (!string.IsNullOrWhiteSpace(Context.Text) || Context.Attachments.Count > 0)
            {
                return SubmitFromKeyboardAsync();
            }

            if (_value.Length > 0)
            {
                _value = string.Empty;
                Context.SetText(string.Empty);
                return InvokeAsync(StateHasChanged);
            }

            return Task.CompletedTask;
        }

        if (string.Equals(args.Key, "Backspace", StringComparison.Ordinal) &&
            string.IsNullOrEmpty(Context.Text) &&
            Context.Attachments.Count > 0)
        {
            var lastAttachment = Context.Attachments[^1];
            Context.RemoveAttachment(lastAttachment.Id);
        }

        return Task.CompletedTask;
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

    private async Task SubmitFromKeyboardAsync()
    {
        await Context.SubmitAsync();
        _value = Context.Text;
        await InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Context.TextChanged -= HandleContextTextChanged;
    }
}
