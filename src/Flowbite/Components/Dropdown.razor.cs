using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;
using Flowbite.Common;
using Flowbite.Services;
using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// A flexible dropdown component with customizable trigger, placement, and behavior.
/// </summary>
/// <remarks>
/// Supports various configurations like inline display, custom triggers, and menu placement.
/// Uses Floating UI for viewport-aware positioning with automatic flip and shift behavior.
/// Implements WAI-ARIA menu pattern with full keyboard navigation support.
/// </remarks>
public partial class Dropdown : IAsyncDisposable
{
    private bool _disposed;
    private bool _initialized;
    private string? _actualPlacement;
    private int _focusedIndex = -1;
    private readonly List<DropdownItem> _registeredItems = new();
    private string _typeAheadBuffer = string.Empty;
    private DateTime _typeAheadLastKeyTime = DateTime.MinValue;
    private const int TypeAheadTimeoutMs = 500;

    [Inject]
    private IFloatingService FloatingService { get; set; } = default!;

    /// <summary>
    /// Defines the label content for the dropdown trigger.
    /// </summary>
    /// <remarks>
    /// Allows custom content to be displayed as the dropdown's label or trigger text.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown&gt;
    ///     &lt;Label&gt;
    ///         Select an option
    ///     &lt;/Label&gt;
    ///     &lt;ChildContent&gt;
    ///         &lt;DropdownItem&gt;Option 1&lt;/DropdownItem&gt;
    ///         &lt;DropdownItem&gt;Option 2&lt;/DropdownItem&gt;
    ///     &lt;/ChildContent&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? Label { get; set; }

    /// <summary>
    /// Defines the content of the dropdown menu.
    /// </summary>
    /// <remarks>
    /// Contains the list of dropdown items or custom content to be displayed in the dropdown menu.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown&gt;
    ///     &lt;Label&gt;Actions&lt;/Label&gt;
    ///     &lt;ChildContent&gt;
    ///         &lt;DropdownItem&gt;Edit&lt;/DropdownItem&gt;
    ///         &lt;DropdownItem&gt;Delete&lt;/DropdownItem&gt;
    ///     &lt;/ChildContent&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Determines whether an arrow icon should be displayed next to the dropdown label.
    /// </summary>
    /// <remarks>
    /// When true, adds a visual indicator that the dropdown can be expanded.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown ArrowIcon="true"&gt;
    ///     &lt;!-- Dropdown with arrow icon --&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool ArrowIcon { get; set; } = true;

    /// <summary>
    /// Determines if the dropdown trigger should be displayed inline with other content.
    /// </summary>
    /// <remarks>
    /// When true, adjusts the styling to fit within text or inline elements.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown Inline="true"&gt;
    ///     &lt;!-- Inline dropdown trigger --&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Inline { get; set; }

    /// <summary>
    /// Specifies the size of the dropdown trigger and menu.
    /// </summary>
    /// <remarks>
    /// Controls the padding, text size, and overall dimensions of the dropdown.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown Size="DropdownSize.Large"&gt;
    ///     &lt;!-- Large dropdown --&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public DropdownSize Size { get; set; } = DropdownSize.Medium;

    /// <summary>
    /// Determines the placement of the dropdown menu relative to the trigger.
    /// </summary>
    /// <remarks>
    /// Allows precise positioning of the dropdown menu in different directions.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown Placement="DropdownPlacement.Top"&gt;
    ///     &lt;!-- Dropdown menu positioned above the trigger --&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public DropdownPlacement Placement { get; set; } = DropdownPlacement.Bottom;

    /// <summary>
    /// Controls whether the dropdown menu should automatically close when an item is clicked.
    /// </summary>
    /// <remarks>
    /// When true, the dropdown will close after a menu item is selected.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown DismissOnClick="true"&gt;
    ///     &lt;!-- Dropdown will close after item selection --&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool DismissOnClick { get; set; } = true;

    /// <summary>
    /// Allows a custom trigger element to be used instead of the default dropdown trigger.
    /// </summary>
    /// <remarks>
    /// Provides full flexibility in designing the dropdown's trigger element.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown&gt;
    ///     &lt;CustomTrigger&gt;
    ///         &lt;Button Color="ButtonColor.Primary"&gt;Custom Trigger&lt;/Button&gt;
    ///     &lt;/CustomTrigger&gt;
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? CustomTrigger { get; set; }

    /// <summary>
    /// Event callback triggered when the dropdown's open state changes.
    /// </summary>
    /// <remarks>
    /// Allows parent components to react to dropdown open/close events.
    /// Provides the current open state as a boolean value.
    /// </remarks>
    /// <accessibility>
    /// Useful for updating aria-expanded attributes or managing focus states.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;Dropdown IsOpenChanged="@HandleDropdownStateChange"&gt;
    ///     &lt;!-- Dropdown content --&gt;
    /// &lt;/Dropdown&gt;
    /// 
    /// @code {
    ///     private void HandleDropdownStateChange(bool isOpen)
    ///     {
    ///         // Handle dropdown state change
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    /// <summary>
    /// Event callback triggered when the dropdown trigger is clicked.
    /// </summary>
    /// <remarks>
    /// Allows custom handling of trigger click events before the dropdown toggles.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown OnTriggerClick="@HandleTriggerClick"&gt;
    ///     &lt;!-- Dropdown content --&gt;
    /// &lt;/Dropdown&gt;
    /// 
    /// @code {
    ///     private async Task HandleTriggerClick(MouseEventArgs args)
    ///     {
    ///         // Custom trigger click handling
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<MouseEventArgs> OnTriggerClick { get; set; }

    private string Id = $"dropdown-{Guid.NewGuid()}";
    private bool IsOpen;

    /// <summary>
    /// Additional classes applied to the dropdown menu surface.
    /// </summary>
    [Parameter]
    public string? MenuClass { get; set; }

    /// <summary>
    /// Slot configuration for per-element class customization.
    /// </summary>
    /// <remarks>
    /// Use slots to override default styling for specific parts of the dropdown:
    /// - Base: The dropdown container
    /// - Trigger: The trigger button
    /// - Menu: The dropdown menu panel
    /// - Item: Classes passed to DropdownItem components
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Dropdown Slots="@(new DropdownSlots {
    ///     Trigger = "bg-blue-600",
    ///     Menu = "w-64 shadow-xl"
    /// })"&gt;
    ///     ...
    /// &lt;/Dropdown&gt;
    /// </code>
    /// </example>
    [Parameter]
    public DropdownSlots? Slots { get; set; }

    /// <summary>
    /// Disables the automatic flip behavior when the dropdown would overflow the viewport.
    /// </summary>
    /// <remarks>
    /// When disabled, the dropdown will maintain its configured placement even if it overflows.
    /// </remarks>
    [Parameter]
    public bool DisableFlip { get; set; }

    /// <summary>
    /// Disables the automatic shift behavior when the dropdown would overflow the viewport.
    /// </summary>
    /// <remarks>
    /// When disabled, the dropdown will not shift along its axis to stay visible.
    /// </remarks>
    [Parameter]
    public bool DisableShift { get; set; }

    /// <summary>
    /// Distance in pixels between the trigger and the dropdown menu.
    /// </summary>
    [Parameter]
    public int Offset { get; set; } = 8;

    /// <summary>
    /// Gets the item slot classes for DropdownItem components.
    /// </summary>
    internal string? ItemSlotClasses => Slots?.Item;

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (IsOpen && !_initialized)
        {
            await InitializeFloatingAsync();
        }
    }

    private async Task InitializeFloatingAsync()
    {
        var placement = GetFloatingPlacement();
        var options = new FloatingOptions(
            Placement: placement,
            Offset: Offset,
            EnableFlip: !DisableFlip,
            EnableShift: !DisableShift,
            ShiftPadding: 8
        );

        _actualPlacement = await FloatingService.InitializeAsync(Id, options);
        _initialized = true;
    }

    private string GetFloatingPlacement() => Placement switch
    {
        DropdownPlacement.Top => "top",
        DropdownPlacement.TopStart => "top-start",
        DropdownPlacement.TopEnd => "top-end",
        DropdownPlacement.Bottom => "bottom",
        DropdownPlacement.BottomStart => "bottom-start",
        DropdownPlacement.BottomEnd => "bottom-end",
        DropdownPlacement.Left => "left",
        DropdownPlacement.LeftStart => "left-start",
        DropdownPlacement.LeftEnd => "left-end",
        DropdownPlacement.Right => "right",
        DropdownPlacement.RightStart => "right-start",
        DropdownPlacement.RightEnd => "right-end",
        _ => "bottom"
    };

    private async Task ToggleDropdown(MouseEventArgs args)
    {
        if (OnTriggerClick.HasDelegate)
        {
            await OnTriggerClick.InvokeAsync(args);
        }

        if (IsOpen)
        {
            await CloseDropdown();
        }
        else
        {
            IsOpen = true;
            _initialized = false; // Reset so OnAfterRenderAsync initializes floating
            await IsOpenChanged.InvokeAsync(true);
        }
    }

    private async Task HandleKeyDown(KeyboardEventArgs args)
    {
        switch (args.Key)
        {
            case "Escape":
                await CloseDropdown();
                break;

            case "ArrowDown":
                if (!IsOpen)
                {
                    // Open dropdown and focus first item
                    await OpenDropdown();
                    _focusedIndex = GetNextFocusableIndex(-1);
                }
                else
                {
                    // Move focus to next item
                    _focusedIndex = GetNextFocusableIndex(_focusedIndex);
                }
                StateHasChanged();
                break;

            case "ArrowUp":
                if (IsOpen)
                {
                    // Move focus to previous item
                    _focusedIndex = GetPreviousFocusableIndex(_focusedIndex);
                    StateHasChanged();
                }
                break;

            case "Home":
                if (IsOpen)
                {
                    // Move focus to first item
                    _focusedIndex = GetNextFocusableIndex(-1);
                    StateHasChanged();
                }
                break;

            case "End":
                if (IsOpen)
                {
                    // Move focus to last item
                    _focusedIndex = GetPreviousFocusableIndex(_registeredItems.Count);
                    StateHasChanged();
                }
                break;

            case "Enter":
            case " ":
                if (IsOpen && _focusedIndex >= 0 && _focusedIndex < _registeredItems.Count)
                {
                    // Select the focused item
                    var item = _registeredItems[_focusedIndex];
                    if (!item.Disabled)
                    {
                        await item.InvokeClick();
                    }
                }
                else if (!IsOpen)
                {
                    // Open dropdown
                    await OpenDropdown();
                    _focusedIndex = GetNextFocusableIndex(-1);
                    StateHasChanged();
                }
                break;

            case "Tab":
                // Close dropdown and let focus move naturally
                if (IsOpen)
                {
                    await CloseDropdown();
                }
                break;

            default:
                // Type-ahead search
                if (IsOpen && args.Key.Length == 1 && char.IsLetterOrDigit(args.Key[0]))
                {
                    HandleTypeAhead(args.Key);
                }
                break;
        }
    }

    /// <summary>
    /// Determines if the default browser behavior should be prevented for keyboard events.
    /// </summary>
    private bool ShouldPreventDefault => IsOpen && _focusedIndex >= 0;

    private async Task OpenDropdown()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            _initialized = false;
            _focusedIndex = -1;
            await IsOpenChanged.InvokeAsync(true);
        }
    }

    private int GetNextFocusableIndex(int currentIndex)
    {
        if (_registeredItems.Count == 0) return -1;

        var startIndex = currentIndex + 1;
        for (var i = 0; i < _registeredItems.Count; i++)
        {
            var index = (startIndex + i) % _registeredItems.Count;
            if (!_registeredItems[index].Disabled)
            {
                return index;
            }
        }
        return currentIndex; // No focusable items found, stay where we are
    }

    private int GetPreviousFocusableIndex(int currentIndex)
    {
        if (_registeredItems.Count == 0) return -1;

        var startIndex = currentIndex - 1;
        if (startIndex < 0) startIndex = _registeredItems.Count - 1;

        for (var i = 0; i < _registeredItems.Count; i++)
        {
            var index = (startIndex - i + _registeredItems.Count) % _registeredItems.Count;
            if (!_registeredItems[index].Disabled)
            {
                return index;
            }
        }
        return currentIndex; // No focusable items found, stay where we are
    }

    private void HandleTypeAhead(string key)
    {
        var now = DateTime.Now;
        if ((now - _typeAheadLastKeyTime).TotalMilliseconds > TypeAheadTimeoutMs)
        {
            _typeAheadBuffer = string.Empty;
        }
        _typeAheadLastKeyTime = now;
        _typeAheadBuffer += key.ToLowerInvariant();

        // Find the first item that starts with the type-ahead buffer
        for (var i = 0; i < _registeredItems.Count; i++)
        {
            var item = _registeredItems[i];
            if (!item.Disabled && item.GetTextContent()?.ToLowerInvariant().StartsWith(_typeAheadBuffer) == true)
            {
                _focusedIndex = i;
                StateHasChanged();
                break;
            }
        }
    }

    /// <summary>
    /// Registers a dropdown item for keyboard navigation.
    /// </summary>
    internal void RegisterItem(DropdownItem item)
    {
        if (!_registeredItems.Contains(item))
        {
            _registeredItems.Add(item);
        }
    }

    /// <summary>
    /// Unregisters a dropdown item from keyboard navigation.
    /// </summary>
    internal void UnregisterItem(DropdownItem item)
    {
        _registeredItems.Remove(item);
    }

    /// <summary>
    /// Checks if the specified item is currently focused.
    /// </summary>
    internal bool IsItemFocused(DropdownItem item)
    {
        var index = _registeredItems.IndexOf(item);
        return index >= 0 && index == _focusedIndex;
    }

    /// <summary>
    /// Closes the dropdown menu and cleans up the floating position.
    /// </summary>
    public async Task CloseDropdown()
    {
        if (IsOpen)
        {
            IsOpen = false;
            _initialized = false;
            await FloatingService.DestroyAsync(Id);
            await IsOpenChanged.InvokeAsync(false);
        }
    }

    private string GetContainerClasses()
    {
        return MergeClasses(
            "relative inline-block text-left",
            Slots?.Base
        );
    }

    private ButtonSize GetButtonSize()
    {
        return Size switch
        {
            DropdownSize.Small => ButtonSize.Small,
            DropdownSize.Large => ButtonSize.Large,
            _ => ButtonSize.Medium
        };
    }

    private string GetTriggerClasses()
    {
        var sizeClasses = Size switch
        {
            DropdownSize.Small => "px-2 py-1 text-xs",
            DropdownSize.Large => "px-6 py-3 text-base",
            _ => "px-4 py-2 text-sm" // Medium is default
        };

        var baseClasses = Inline
            ? $"inline-flex items-center text-gray-500 hover:text-gray-600 dark:text-gray-400 dark:hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-blue-500 {sizeClasses}"
            : $"inline-flex justify-center w-full rounded-md border border-gray-300 shadow-sm {sizeClasses} bg-white dark:bg-gray-800 font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-blue-500";

        return MergeClasses(baseClasses, Slots?.Trigger);
    }

    private string GetMenuClasses()
    {
        // Base classes for the floating menu - positioning is handled by Floating UI
        var baseClasses = string.Join(" ", new[]
        {
            "min-w-[180px] rounded shadow focus:outline-none",
            "divide-y divide-gray-100",
            "border border-gray-200 bg-white text-gray-900 dark:border-none dark:bg-gray-700 dark:text-white"
        });

        var zClass = string.IsNullOrWhiteSpace(MenuClass) ? "z-50" : MenuClass;

        return MergeClasses(baseClasses, zClass, Slots?.Menu);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            _disposed = true;
            if (_initialized)
            {
                await FloatingService.DestroyAsync(Id);
            }
        }
    }
}
