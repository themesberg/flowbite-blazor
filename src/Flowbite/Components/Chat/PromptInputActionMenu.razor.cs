using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Flowbite.Components;
using Flowbite.Icons;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provider component for the prompt input action menu.
/// </summary>
public partial class PromptInputActionMenu : Flowbite.Base.FlowbiteComponentBase, IDisposable
{
    private readonly PromptInputActionMenuContext _context = new();
    private RenderFragment? _triggerFragment;
    private RenderFragment? _contentFragment;
    private Dictionary<string, object>? _contentAttributes;
    private Dropdown? _dropdown;

    /// <summary>
    /// Content for the menu provider.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string BaseClasses => "relative inline-flex";

    protected override void OnInitialized()
    {
        _context.OpenChanged += HandleOpenChanged;
        _context.Configure(CloseMenuAsync);
    }

    private void HandleOpenChanged() => InvokeAsync(StateHasChanged);

    internal void RegisterTrigger(RenderFragment fragment)
    {
        var shouldRefresh = _triggerFragment is null;
        _triggerFragment = fragment;

        if (shouldRefresh)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    internal void RegisterMenu(RenderFragment? fragment, Dictionary<string, object>? attributes)
    {
        var shouldRefresh = _contentFragment is null && fragment is not null;
        _contentFragment = fragment;
        _contentAttributes = attributes;

        if (shouldRefresh)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    private RenderFragment BuildTriggerFragment() => _triggerFragment ?? DefaultTrigger;

    private RenderFragment DefaultTrigger => builder =>
    {
        builder.OpenElement(0, "button");
        builder.AddAttribute(1, "type", "button");
        builder.AddAttribute(2, "class", "inline-flex h-10 w-10 items-center justify-center rounded-full border border-gray-200/80 bg-white/95 text-gray-500 transition hover:border-gray-300 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-primary-400/60 dark:border-white/10 dark:bg-slate-950/60 dark:text-gray-300 dark:hover:text-white");
        builder.AddAttribute(3, "aria-label", "Open action menu");
        builder.AddAttribute(4, "aria-haspopup", "menu");
        builder.AddAttribute(5, "aria-expanded", _context.IsOpen.ToString().ToLowerInvariant());
        builder.OpenComponent<PlusIcon>(6);
        builder.AddAttribute(7, "class", "h-4 w-4");
        builder.AddAttribute(8, "aria-hidden", true);
        builder.CloseComponent();
        builder.CloseElement();
    };

    private async Task HandleDropdownStateChanged(bool isOpen)
    {
        _context.UpdateIsOpen(isOpen);
        await InvokeAsync(StateHasChanged);
    }

    private Task CloseMenuAsync()
    {
        if (_dropdown is null)
        {
            return Task.CompletedTask;
        }

        return _dropdown.CloseDropdown();
    }

    internal string GetMenuClasses() =>
        "flex w-48 flex-col gap-1 rounded-2xl border border-gray-200/80 bg-white/98 p-2 shadow-[0_24px_45px_-28px_rgba(15,23,42,0.55)] focus:outline-none dark:border-white/10 dark:bg-slate-950/90";

    /// <inheritdoc />
    public void Dispose()
    {
        _context.OpenChanged -= HandleOpenChanged;
    }
}
