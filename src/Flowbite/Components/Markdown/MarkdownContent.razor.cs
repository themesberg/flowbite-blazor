using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Flowbite.Components.Markdown;

/// <summary>
/// Visual variant for markdown content rendering.
/// </summary>
public enum MarkdownVariant
{
    /// <summary>
    /// Normal variant with standard colors.
    /// </summary>
    Normal,

    /// <summary>
    /// Muted variant with gray-400 colors for reasoning content.
    /// </summary>
    Muted
}

/// <summary>
/// Component for rendering Markdown content with syntax highlighting support.
/// </summary>
public partial class MarkdownContent : ComponentBase
{
    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .DisableHtml()
        .Build();

    private string? _lastMarkdown;
    private string _htmlContent = string.Empty;
    private ElementReference _markdownContainer;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary>
    /// The markdown content to render.
    /// </summary>
    [Parameter]
    public string? Markdown { get; set; }

    /// <summary>
    /// Additional CSS classes to apply to the container.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Visual variant for the markdown content (Normal or Muted).
    /// </summary>
    [Parameter]
    public MarkdownVariant Variant { get; set; } = MarkdownVariant.Normal;

    private string GetMarkdownClass()
    {
        return Variant == MarkdownVariant.Muted
            ? "markdown-content markdown-content-muted"
            : "markdown-content";
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // Only reprocess if markdown has changed
        if (_lastMarkdown != Markdown)
        {
            _lastMarkdown = Markdown;
            _htmlContent = string.IsNullOrWhiteSpace(Markdown)
                ? string.Empty
                : Markdig.Markdown.ToHtml(Markdown, Pipeline);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // Apply syntax highlighting to code blocks after render
        if (!string.IsNullOrWhiteSpace(_htmlContent))
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("highlightMarkdownCode", _markdownContainer);
            }
            catch (JSException)
            {
                // Silently ignore if highlight.js is not loaded
            }
        }
    }
}
