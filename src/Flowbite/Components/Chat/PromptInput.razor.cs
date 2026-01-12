using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
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
    // relative flex w-full max-w-4xl mx-auto flex-col gap-3 rounded-xl border border-gray-200/80 bg-white/95 px-5 py-4 shadow-[0_18px_40px_-24px_rgba(15,23,42,0.35)] dark:border-white/10 dark:bg-slate-950/70 dark:shadow-[0_24px_50px_-30px_rgba(15,23,42,0.9)]

    // w-full overflow-hidden rounded-xl border border-border bg-background p-3 shadow-xs transition-all duration-200 focus-within:border-border hover:border-muted-foreground/50

    private readonly string ContainerClasses =
        "relative flex-none flex w-full max-w-4xl mx-auto flex-col gap-3 rounded-xl shadow-md border border-gray-300 bg-white/95 dark:border-white/10 dark:bg-slate-950/70 p-3 hover:border-gray-500/80 dark:hover:border-white/20";

    private readonly string DragActiveClasses =
        "ring-2 ring-primary-500/70 ring-offset-2 ring-offset-white dark:ring-offset-slate-950";

    private ElementReference _container;
    private PromptInputContext _context = default!;
    private InputFile? _fileInput;
    private IJSObjectReference? _module;
    private DotNetObjectReference<PromptInput>? _dotNetReference;
    private bool _isDragActive;
    private bool _suppressContextTextCallback;
    private Action? _attachmentsChangedHandler;
    private Action? _textChangedHandler;
    private readonly HashSet<string> _attachmentIds = new();
    private readonly Dictionary<string, IAsyncDisposable> _attachmentResources = new();

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

    private bool AllowMultipleFiles => Multiple;

    protected override void OnInitialized()
    {
        _context = new PromptInputContext(OpenFilePickerAsync, SubmitAsync, Multiple, CreateAttachment);
        _context.SetText(Text ?? string.Empty);
        _textChangedHandler = () => _ = HandleContextTextChangedAsync();
        _attachmentsChangedHandler = () => _ = HandleContextAttachmentsChangedAsync();
        if (_textChangedHandler is not null)
        {
            _context.TextChanged += _textChangedHandler;
        }
        if (_attachmentsChangedHandler is not null)
        {
            _context.AttachmentsChanged += _attachmentsChangedHandler;
        }
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await EnsureModuleAsync().ConfigureAwait(false);
        if (_module is null)
        {
            return;
        }

        if (firstRender)
        {
            _dotNetReference = DotNetObjectReference.Create(this);
            await _module.InvokeVoidAsync(
                "registerPromptInput",
                _container,
                _dotNetReference,
                new PromptInputRegistrationOptions
                {
                    GlobalDrop = GlobalDrop
                });
        }
        else
        {
            await _module.InvokeVoidAsync(
                "updatePromptInputOptions",
                _container,
                new PromptInputRegistrationOptions
                {
                    GlobalDrop = GlobalDrop
                });
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

    private PromptAttachment CreateAttachment(IBrowserFile file) =>
        file is IAsyncDisposable disposable
            ? new PromptAttachment(file, disposable)
            : new PromptAttachment(file);

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

    private async Task HandleContextTextChangedAsync()
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

    private async Task HandleContextAttachmentsChangedAsync()
    {
        foreach (var attachment in _context.Attachments)
        {
            if (_attachmentIds.Add(attachment.Id) && attachment.DisposableResource is IAsyncDisposable disposable)
            {
                _attachmentResources[attachment.Id] = disposable;
            }
        }

        var removedIds = _attachmentIds
            .Where(id => _context.Attachments.All(attachment => attachment.Id != id))
            .ToList();

        foreach (var removedId in removedIds)
        {
            _attachmentIds.Remove(removedId);
            if (_attachmentResources.Remove(removedId, out var disposable))
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
            }
        }

        if (AttachmentsChanged.HasDelegate)
        {
            var snapshot = _context.Attachments.ToList().AsReadOnly();
            await InvokeAsync(() => AttachmentsChanged.InvokeAsync(snapshot));
        }

        await InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public void SetDragState(bool isActive)
    {
        if (_isDragActive == isActive)
        {
            return;
        }

        _isDragActive = isActive;
        _ = InvokeAsync(StateHasChanged);
    }

    [JSInvokable]
    public async Task ReceiveFiles(PromptInputJsFileDescriptor[] descriptors)
    {
        if (descriptors is null || descriptors.Length == 0)
        {
            return;
        }

        var files = new List<IBrowserFile>(descriptors.Length);

        for (var i = 0; i < descriptors.Length; i++)
        {
            var descriptor = descriptors[i];

            if (!AllowMultipleFiles && _context.Attachments.Count + files.Count >= 1)
            {
                if (descriptor.Stream is not null)
                {
                    await descriptor.Stream.DisposeAsync().ConfigureAwait(false);
                }

                for (var j = i + 1; j < descriptors.Length; j++)
                {
                    if (descriptors[j].Stream is not null)
                    {
                        await descriptors[j].Stream.DisposeAsync().ConfigureAwait(false);
                    }
                }

                break;
            }

            if (descriptor.Size <= 0 || descriptor.Stream is null)
            {
                if (descriptor.Stream is not null)
                {
                    await descriptor.Stream.DisposeAsync().ConfigureAwait(false);
                }

                continue;
            }

            files.Add(new JsStreamBrowserFile(
                descriptor.Name,
                descriptor.Size,
                descriptor.ContentType,
                descriptor.LastModified,
                descriptor.Stream));
        }

        if (files.Count == 0)
        {
            return;
        }

        if (!AllowMultipleFiles)
        {
            _context.ClearAttachments();
        }

        _context.AddFiles(files);
    }

    private async Task DisposeAttachmentResourcesAsync()
    {
        if (_attachmentResources.Count == 0)
        {
            return;
        }

        var disposables = _attachmentResources.Values.ToList();
        _attachmentResources.Clear();
        _attachmentIds.Clear();

        foreach (var disposable in disposables)
        {
            await disposable.DisposeAsync().ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_attachmentsChangedHandler is not null)
        {
            _context.AttachmentsChanged -= _attachmentsChangedHandler;
        }

        if (_textChangedHandler is not null)
        {
            _context.TextChanged -= _textChangedHandler;
        }

        await DisposeAttachmentResourcesAsync().ConfigureAwait(false);

        if (_module is not null)
        {
            try
            {
                await _module.InvokeVoidAsync("unregisterPromptInput", _container);
            }
            catch (JSDisconnectedException)
            {
                // Ignore if JS runtime is no longer available.
            }

            await _module.DisposeAsync();
        }

        _dotNetReference?.Dispose();
    }

    private sealed class PromptInputRegistrationOptions
    {
        [JsonPropertyName("globalDrop")]
        public bool GlobalDrop { get; init; }
    }

    public sealed class PromptInputJsFileDescriptor
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; init; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; init; } = string.Empty;

        [JsonPropertyName("lastModified")]
        public long LastModified { get; init; }

        [JsonPropertyName("stream")]
        public IJSStreamReference Stream { get; init; } = default!;
    }

    private sealed class JsStreamBrowserFile : IBrowserFile, IAsyncDisposable
    {
        private readonly IJSStreamReference _streamReference;
        private readonly DateTimeOffset _lastModified;

        public JsStreamBrowserFile(
            string name,
            long size,
            string contentType,
            long lastModified,
            IJSStreamReference streamReference)
        {
            Name = name;
            Size = size;
            ContentType = string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType;
            _streamReference = streamReference;
            _lastModified = NormalizeTimestamp(lastModified);
        }

        public string Name { get; }

        public DateTimeOffset LastModified => _lastModified;

        public long Size { get; }

        public string ContentType { get; }

        public string? RelativePath => null;

        public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
        {
            if (Size > maxAllowedSize)
            {
                throw new IOException($"Supplied file with size {Size} bytes exceeds the maximum of {maxAllowedSize} bytes.");
            }

            return _streamReference.OpenReadStreamAsync(maxAllowedSize, cancellationToken)
                .AsTask()
                .GetAwaiter()
                .GetResult();
        }

        public ValueTask DisposeAsync() => _streamReference.DisposeAsync();

        private static DateTimeOffset NormalizeTimestamp(long timestamp)
        {
            try
            {
                if (timestamp <= 0)
                {
                    return DateTimeOffset.UtcNow;
                }

                return DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            }
            catch (ArgumentOutOfRangeException)
            {
                return DateTimeOffset.UtcNow;
            }
        }
    }
}
