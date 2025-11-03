using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Flowbite.Icons;

namespace Flowbite.Components.Chat;

/// <summary>
/// Registers the trigger button rendered by the prompt input action menu.
/// </summary>
public partial class PromptInputActionMenuTrigger : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    /// <summary>
    /// Optional custom trigger content; defaults to a plus icon.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the trigger button.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private PromptInputActionMenu Owner { get; set; } = default!;

    [CascadingParameter] private PromptInputActionMenuContext MenuContext { get; set; } = default!;

    private string BaseClasses =>
        "inline-flex h-10 w-10 items-center justify-center rounded-full border border-gray-200/80 bg-white/95 text-gray-500 " +
        "transition hover:border-gray-300 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-primary-400/60 " +
        "dark:border-white/10 dark:bg-slate-950/60 dark:text-gray-300 dark:hover:text-white";

    protected override void OnInitialized()
    {
        MenuContext.OpenChanged += HandleOpenChanged;
    }

    protected override void OnParametersSet()
    {
        Owner.RegisterTrigger(BuildTrigger());
    }

    private RenderFragment BuildTrigger() => builder =>
    {
        builder.OpenElement(0, "button");
        builder.AddAttribute(1, "type", "button");
        builder.AddAttribute(2, "class", CombineClasses(BaseClasses));
        builder.AddAttribute(3, "aria-label", "Open action menu");
        builder.AddAttribute(4, "aria-haspopup", "menu");
        builder.AddAttribute(5, "aria-expanded", MenuContext.IsOpen.ToString().ToLowerInvariant());
        builder.AddMultipleAttributes(6, AdditionalAttributes);

        if (ChildContent is not null)
        {
            builder.AddContent(7, ChildContent);
        }
        else
        {
            builder.OpenComponent<PlusIcon>(8);
            builder.AddAttribute(9, "class", "h-4 w-4");
            builder.AddAttribute(10, "aria-hidden", true);
            builder.CloseComponent();
        }

        builder.CloseElement();
    };

    private void HandleOpenChanged() => InvokeAsync(StateHasChanged);

    /// <inheritdoc />
    public void Dispose()
    {
        MenuContext.OpenChanged -= HandleOpenChanged;
    }
}
