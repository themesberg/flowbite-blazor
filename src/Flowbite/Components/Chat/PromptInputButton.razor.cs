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
    /// Click callback.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    private string GetButtonClasses()
    {
        var baseClasses = Variant switch
        {
            PromptInputButtonVariant.Default => "inline-flex h-11 items-center gap-2 rounded-full bg-primary-600 px-4 text-sm font-medium text-white shadow-lg transition motion-reduce:transition-none hover:bg-primary-500 focus:outline-none focus:ring-2 focus:ring-primary-400/70 dark:bg-primary-500 dark:hover:bg-primary-400",
            _ => "inline-flex h-10 items-center gap-2 rounded-full border border-gray-200/80 bg-white/95 px-3 text-sm font-medium text-gray-600 transition-colors motion-reduce:transition-none " +
                 "hover:border-gray-300 hover:bg-white focus:outline-none focus:ring-2 focus:ring-primary-400/60 dark:border-white/10 dark:bg-slate-950/50 dark:text-gray-200 dark:hover:text-white"
        };

        if (Active)
        {
            baseClasses += " border-transparent bg-primary-600 text-white shadow-lg hover:bg-primary-500 dark:bg-primary-600 dark:hover:bg-primary-400";
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
