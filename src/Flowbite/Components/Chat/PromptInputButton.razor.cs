using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components.Chat;

/// <summary>
/// Small utility button used within the prompt input footer.
/// </summary>
public partial class PromptInputButton : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// The button variant style.
    /// </summary>
    [Parameter]
    public PromptInputButtonVariant Variant { get; set; } = PromptInputButtonVariant.Ghost;

    /// <summary>
    /// Indicates the button is active/toggled.
    /// </summary>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Disables the button when true.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Button type attribute.
    /// </summary>
    [Parameter]
    public string Type { get; set; } = "button";

    /// <summary>
    /// Optional icon rendered before the content.
    /// </summary>
    [Parameter]
    public Type? Icon { get; set; }

    /// <summary>
    /// Button content.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Click callback.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    private string GetButtonClasses()
    {
        var baseClasses = Variant switch
        {
            PromptInputButtonVariant.Default => "inline-flex items-center rounded-full bg-primary-600 px-3 py-2 text-sm font-medium text-white transition hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-primary-500",
            _ => "inline-flex items-center rounded-full border border-transparent px-3 py-2 text-sm font-medium text-gray-600 transition hover:bg-gray-100 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-primary-500 dark:text-gray-300 dark:hover:bg-gray-800 dark:hover:text-white"
        };

        if (Active)
        {
            baseClasses += " bg-gray-900 text-white hover:bg-gray-900 dark:bg-gray-100 dark:text-gray-900";
        }

        if (Disabled)
        {
            baseClasses += " cursor-not-allowed opacity-60";
        }

        return baseClasses;
    }

    private async Task HandleClickAsync(MouseEventArgs args)
    {
        if (Disabled)
        {
            return;
        }

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }
}
