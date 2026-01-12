using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flowbite.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Flowbite.Components;

/// <summary>
/// Searchable combobox that mirrors Flowbite's headless combobox pattern.
/// </summary>
public partial class Combobox : FlowbiteComponentBase, IAsyncDisposable
{
    private readonly List<ComboboxItemRegistration> _items = new();
    private readonly string _instanceId = $"flowbite-combobox-{Guid.NewGuid():N}";
    private readonly string _optionsListId;

    private bool _isOpen;
    private string _searchText = string.Empty;
    private bool _shouldFocusSearch;
    private bool _itemsInvalidated;

    private ElementReference _searchInputRef;
    private ElementReference _rootRef;

    private IJSObjectReference? _jsModule;
    private DotNetObjectReference<Combobox>? _dotNetRef;
    private bool _outsideClickRegistered;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    public Combobox()
    {
        _optionsListId = $"{_instanceId}-list";
    }

    /// <summary>
    /// Gets or sets the selected value.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Occurs when the selected value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the placeholder displayed when no option is selected.
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Select option";

    /// <summary>
    /// Gets or sets the placeholder displayed inside the search field.
    /// </summary>
    [Parameter]
    public string SearchPlaceholder { get; set; } = "Search options";

    /// <summary>
    /// Gets or sets the empty state text rendered when no matches are found.
    /// </summary>
    [Parameter]
    public string EmptyText { get; set; } = "No results found";

    /// <summary>
    /// Optional helper text displayed beneath the component.
    /// </summary>
    [Parameter]
    public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets whether the combobox is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Child content used to declare <see cref="ComboboxItem"/> components.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Raised whenever the search query changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnSearchChanged { get; set; }

    private string SearchInputId => $"{_instanceId}-search";

    private string DisplayLabel => SelectedLabel ?? Placeholder;

    private string? SelectedLabel => _items.FirstOrDefault(i => string.Equals(i.Value, Value, StringComparison.Ordinal))?.Label;

    private IReadOnlyList<ComboboxItemRegistration> FilteredItems =>
        string.IsNullOrWhiteSpace(_searchText)
            ? _items
            : _items.Where(item => item.Label.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();

    private string TriggerClasses => CombineClasses(
        "flex w-full items-center justify-between rounded-lg border border-gray-300 bg-gray-50 px-3 py-2.5 text-left text-sm text-gray-900 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white",
        Disabled ? "cursor-not-allowed opacity-50" : null);

    private string OptionsListId => _optionsListId;

    internal void RegisterItem(ComboboxItemRegistration registration)
    {
        _items.Add(registration);
        RequestRender();
    }

    internal void UnregisterItem(ComboboxItemRegistration registration)
    {
        _items.Remove(registration);
        RequestRender();
    }

    internal void NotifyItemsChanged() => RequestRender();

    private void RequestRender()
    {
        if (_itemsInvalidated)
        {
            return;
        }

        _itemsInvalidated = true;
        _ = InvokeAsync(() =>
        {
            _itemsInvalidated = false;
            StateHasChanged();
        });
    }

    private bool IsSelected(ComboboxItemRegistration registration) =>
        string.Equals(Value, registration.Value, StringComparison.Ordinal);

    private string GetOptionClasses(ComboboxItemRegistration item)
    {
        var baseClasses =
            "flex w-full items-center gap-2 px-3 py-2 text-sm text-left transition-colors motion-reduce:transition-none hover:bg-gray-100 focus:bg-gray-100 dark:hover:bg-gray-700";

        if (item.Disabled)
        {
            return baseClasses + " cursor-not-allowed text-gray-400 dark:text-gray-500";
        }

        if (IsSelected(item))
        {
            return baseClasses + " bg-gray-100 font-medium text-gray-900 dark:bg-gray-700 dark:text-white";
        }

        return baseClasses + " text-gray-700 dark:text-gray-200";
    }

    private async Task ToggleAsync()
    {
        if (Disabled)
        {
            return;
        }

        _isOpen = !_isOpen;
        if (_isOpen)
        {
            _shouldFocusSearch = true;
            await EnsureOutsideClickAsync();
        }
        else
        {
            _searchText = string.Empty;
            await ReleaseOutsideClickAsync();
        }
    }

    private async Task HandleOptionClickAsync(ComboboxItemRegistration registration)
    {
        if (registration.Disabled)
        {
            return;
        }

        Value = registration.Value;
        await ValueChanged.InvokeAsync(Value);
        _isOpen = false;
        _searchText = string.Empty;
        await ReleaseOutsideClickAsync();
    }

    private async Task HandleSearchInput(ChangeEventArgs args)
    {
        _searchText = args.Value?.ToString() ?? string.Empty;
        if (OnSearchChanged.HasDelegate)
        {
            await OnSearchChanged.InvokeAsync(_searchText);
        }
    }

    private Task HandleTriggerKeyDown(KeyboardEventArgs args)
    {
        if (args.Key is "Enter" or " " or "Spacebar" or "ArrowDown")
        {
            return ToggleAsync();
        }

        return Task.CompletedTask;
    }

    private async Task HandleSearchKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Escape")
        {
            _isOpen = false;
            _searchText = string.Empty;
            await ReleaseOutsideClickAsync();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_isOpen && _shouldFocusSearch && _searchInputRef.Context is not null)
        {
            _shouldFocusSearch = false;
            await _searchInputRef.FocusAsync();
        }

        if (_isOpen)
        {
            await EnsureOutsideClickAsync();
        }
        else
        {
            await ReleaseOutsideClickAsync();
        }
    }

    private async Task EnsureOutsideClickAsync()
    {
        if (_outsideClickRegistered)
        {
            return;
        }

        _jsModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Flowbite/js/combobox.js");
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await _jsModule.InvokeVoidAsync("registerOutsideClick", _instanceId, _rootRef, _dotNetRef);
        _outsideClickRegistered = true;
    }

    private async Task ReleaseOutsideClickAsync()
    {
        if (!_outsideClickRegistered || _jsModule is null)
        {
            return;
        }

        await _jsModule.InvokeVoidAsync("unregisterOutsideClick", _instanceId);
        _outsideClickRegistered = false;
    }

    [JSInvokable]
    public async Task HandleOutsideClickAsync()
    {
        _isOpen = false;
        _searchText = string.Empty;
        await ReleaseOutsideClickAsync();
        await InvokeAsync(StateHasChanged);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_jsModule is not null)
        {
            await ReleaseOutsideClickAsync();
            await _jsModule.DisposeAsync();
        }

        _dotNetRef?.Dispose();
    }
}
