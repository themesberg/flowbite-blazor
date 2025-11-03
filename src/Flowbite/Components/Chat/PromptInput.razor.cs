using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Flowbite.Components.Chat;

/// <summary>
/// High-level prompt authoring surface with support for text, attachments, and compound child components.
/// </summary>
public partial class PromptInput : Flowbite.Base.FlowbiteComponentBase, IAsyncDisposable
{
    private readonly string ContainerClasses =
        "relative flex w-full flex-col gap-3 rounded-[26px] border border-gray-200/80 bg-white/95 px-5 py-4 shadow-[0_18px_40px_-24px_rgba(15,23,42,0.35)] " +
        "dark:border-white/10 dark:bg-slate-950/70 dark:shadow-[0_24px_50px_-30px_rgba(15,23,42,0.9)]";

    private readonly string DragActiveClasses =
        "ring-2 ring-primary-500/70 ring-offset-2 ring-offset-white dark:ring-offset-slate-950";

    private PromptInputContext _context = default!;
    private InputFile? _fileInput;
    private IJSObjectReference? _module;
    private bool _isDragActive;
    private bool _suppressContextTextCallback;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary>
    /// Child content rendered inside the input surface.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Raised when the prompt is submitted.
    /// </summary>
    [Parameter]
    public EventCallback<PromptInputMessage> OnSubmit { get; set; }

    /// <summary>
    /// Enables support for multiple attachments. Defaults to true.
    /// </summary>
    [Parameter]
    public bool Multiple { get; set; } = true;

    /// <summary>
    /// Allows dropping files anywhere on the prompt input surface.
    /// </summary>
    [Parameter]
    public bool GlobalDrop { get; set; }

    /// <summary>
    /// The current prompt text.
    /// </summary>
    [Parameter]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Raised when the prompt text changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> TextChanged { get; set; }

    /// <summary>
    /// Raised when attachments are added or removed.
    /// </summary>
    [Parameter]
    public EventCallback<IReadOnlyList<PromptAttachment>> AttachmentsChanged { get; set; }

    /// <summary>
    /// Additional attributes applied to the outer container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private bool AllowMultipleFiles => Multiple;

    protected override void OnInitialized()
    {
        _context = new PromptInputContext(OpenFilePickerAsync, SubmitAsync, Multiple);
        _context.SetText(Text ?? string.Empty);
        _context.AttachmentsChanged += HandleContextAttachmentsChanged;
        _context.TextChanged += HandleContextTextChanged;
    }

    protected override void OnParametersSet()
    {
        _context.Multiple = Multiple;

        var desiredText = Text ?? string.Empty;
        if (!string.Equals(desiredText, _context.Text, StringComparison.Ordinal))
        {
            _suppressContextTextCallback = true;
            _context.SetText(desiredText);
            _suppressContextTextCallback = false;
        }
    }

    private Task HandleFileChangedAsync(InputFileChangeEventArgs args)
    {
        if (args.FileCount == 0)
        {
            return Task.CompletedTask;
        }

        if (AllowMultipleFiles)
        {
            _context.AddFiles(args.GetMultipleFiles());
        }
        else if (args.FileCount > 0)
        {
            var file = args.File;
            if (file is not null)
            {
                _context.ClearAttachments();
                _context.AddFiles(new[] { file });
            }
        }

        return Task.CompletedTask;
    }

    private async Task HandleSubmitAsync()
    {
        if (string.IsNullOrWhiteSpace(_context.Text) && _context.Attachments.Count == 0)
        {
            return;
        }

        await SubmitAsync();
    }

    private async Task SubmitAsync()
    {
        var message = new PromptInputMessage
        {
            Text = _context.Text,
            Files = _context.Attachments.Select(static a => a.File).ToList()
        };

        if (OnSubmit.HasDelegate)
        {
            await OnSubmit.InvokeAsync(message);
        }

        _context.ClearAttachments();
        _context.SetText(string.Empty);
    }

    private async Task OpenFilePickerAsync()
    {
        if (_fileInput is null)
        {
            return;
        }

        await EnsureModuleAsync();
        if (_module is not null)
        {
            await _module.InvokeVoidAsync("openFilePicker", _fileInput.Element);
        }
    }

    private async Task EnsureModuleAsync()
    {
        _module ??= await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/Flowbite/js/promptInput.js");
    }

    private async void HandleContextTextChanged()
    {
        if (_suppressContextTextCallback)
        {
            return;
        }

        if (TextChanged.HasDelegate)
        {
            await InvokeAsync(() => TextChanged.InvokeAsync(_context.Text));
        }
    }

    private async void HandleContextAttachmentsChanged()
    {
        if (AttachmentsChanged.HasDelegate)
        {
            var snapshot = _context.Attachments.ToList().AsReadOnly();
            await InvokeAsync(() => AttachmentsChanged.InvokeAsync(snapshot));
        }
        StateHasChanged();
    }

    private void HandleDragEnter(DragEventArgs args)
    {
        if (!GlobalDrop)
        {
            return;
        }

        _isDragActive = true;
        StateHasChanged();
    }

    private void HandleDragLeave(DragEventArgs args)
    {
        if (!GlobalDrop)
        {
            return;
        }

        _isDragActive = false;
        StateHasChanged();
    }

    private void HandleDragOver(DragEventArgs args)
    {
        if (!GlobalDrop)
        {
            return;
        }
        _isDragActive = true;
    }

    private void HandleDrop(DragEventArgs args)
    {
        if (!GlobalDrop)
        {
            return;
        }
        _isDragActive = false;
        StateHasChanged();
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        _context.AttachmentsChanged -= HandleContextAttachmentsChanged;
        _context.TextChanged -= HandleContextTextChanged;

        if (_module is not null)
        {
            await _module.DisposeAsync();
        }
    }
}
