using Microsoft.AspNetCore.Components;
using Flowbite.Services;
using Flowbite.Base;
using Flowbite.Components;

namespace DemoApp.Components;

public partial class ComponentExample : FlowbiteComponentBase
{
    /// <summary>
    /// The title of the example.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = "Unspecified";

    /// <summary>
    /// The description of the example.
    /// </summary>
    [Parameter]
    public string? Description { get; set; }

    /// <summary>
    /// The content to display in the preview section.
    /// </summary>
    [Parameter]
    public RenderFragment? PreviewContent { get; set; }

    /// <summary>
    /// Whether to show the code section.
    /// </summary>
    [Parameter]
    public bool ShowCode { get; set; } = true;

    /// <summary>
    /// The Razor source code to display.
    /// </summary>
    [Parameter]
    public string? RazorCode { get; set; }

    /// <summary>
    /// The C# source code to display.
    /// </summary>
    [Parameter]
    public string? CSharpCode { get; set; }

    /// <summary>
    /// The HTML source code to display.
    /// </summary>
    [Parameter]
    public string? HtmlCode { get; set; }

    /// <summary>
    /// The programming language for syntax highlighting.
    /// </summary>
    [Parameter]
    public string Language { get; set; } = "razor";

    /// <summary>
    /// List of supported programming languages for switching.
    /// </summary>
    [Parameter]
    public string[] SupportedLanguages { get; set; } = new[] { "razor", "html", "csharp" };

    /// <summary>
    /// The currently selected language.
    /// </summary>
    private string _selectedLanguage = "razor";

    /// <summary>
    /// Called when the component is initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _selectedLanguage = Language;
    }

    /// <summary>
    /// Callback when code is copied to clipboard.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnCodeCopied { get; set; }

 
    /// <summary>
    /// Get the code text to copy
    /// </summary>
    /// <returns>Defaults to razor element's code if none selected</returns>
    private string? CodeToCopy()
    {
        var code = _selectedLanguage switch
        {
            "razor" => RazorCode,
            "html" => HtmlCode,
            "csharp" => CSharpCode,
            _ => RazorCode
        };

        return code;
    }

    protected string TitleSlug => Title.Replace(" ", "-")
                                       .Replace("&", "and")
                                       .Replace("'", "")
                                       .Replace("\"", "")
                                       .Replace("_", "-")
                                       .ToLower();

    [Inject]
    protected NavigationManager? NavigationManager { get; set; }

    protected string TitleHref => $"{NavigationManager?.Uri}#{TitleSlug}";
}
