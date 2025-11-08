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
        .UseMathematics()
        .UseAdvancedExtensions()
        .UseDiagrams()
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
            
            if (string.IsNullOrWhiteSpace(Markdown))
            {
                _htmlContent = string.Empty;
            }
            else
            {
                // Pre-process to unwrap latex code blocks that AI models commonly use
                var processedMarkdown = UnwrapLatexCodeBlocks(Markdown);
                _htmlContent = Markdig.Markdown.ToHtml(processedMarkdown, Pipeline);
            }
        }
    }

    private static string UnwrapLatexCodeBlocks(string markdown)
    {
        // Replace ```latex code blocks with unwrapped math expressions
        // This handles the common pattern where AI models wrap LaTeX in code fences
        var pattern = @"```latex\s*([\s\S]*?)\s*```";
        return System.Text.RegularExpressions.Regex.Replace(markdown, pattern, match =>
        {
            var content = match.Groups[1].Value.Trim();
            // Return the content as-is so Markdig's math extension can process it
            return content;
        });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        // Apply syntax highlighting, mermaid diagrams, and math rendering after render
        if (!string.IsNullOrWhiteSpace(_htmlContent))
        {
            try
            {
                // Apply syntax highlighting to code blocks
                await JSRuntime.InvokeVoidAsync("highlightMarkdownCode", _markdownContainer);
                
                // Render Mermaid diagrams
                await JSRuntime.InvokeVoidAsync("renderMermaid");
                
                // Render mathematical equations
                await JSRuntime.InvokeVoidAsync("renderMath", _markdownContainer);
            }
            catch (JSException)
            {
                // Silently ignore if libraries are not loaded
            }
        }
    }
}
