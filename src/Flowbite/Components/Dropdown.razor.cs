using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;
using Flowbite.Common;
using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// A flexible dropdown component with customizable trigger, placement, and behavior.
/// </summary>
/// <remarks>
/// Supports various configurations like inline display, custom triggers, and menu placement.
/// </remarks>
public partial class Dropdown : IDisposable
{
    private bool isDisposed;

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
    /// Gets the item slot classes for DropdownItem components.
    /// </summary>
    internal string? ItemSlotClasses => Slots?.Item;

    private async Task ToggleDropdown(MouseEventArgs args)
    {
        if (OnTriggerClick.HasDelegate)
        {
            await OnTriggerClick.InvokeAsync(args);
        }

        IsOpen = !IsOpen;
        await IsOpenChanged.InvokeAsync(IsOpen);
    }

    private async Task HandleKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Escape")
        {
            await CloseDropdown();
        }
    }

    public async Task CloseDropdown()
    {
        if (IsOpen)
        {
            IsOpen = false;
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
        var positionClass = Placement switch
        {
            DropdownPlacement.Top or DropdownPlacement.TopStart or DropdownPlacement.TopEnd => "bottom-full mb-2",
            DropdownPlacement.Right or DropdownPlacement.RightStart or DropdownPlacement.RightEnd => "left-full ml-2",
            DropdownPlacement.Left or DropdownPlacement.LeftStart or DropdownPlacement.LeftEnd => "right-full mr-2",
            _ => "top-full mt-2" // Bottom is default
        };

        var alignmentClass = Placement switch
        {
            DropdownPlacement.TopStart or DropdownPlacement.BottomStart => "origin-top-left start-0",
            DropdownPlacement.TopEnd or DropdownPlacement.BottomEnd => "origin-top-right end-0",
            DropdownPlacement.RightStart or DropdownPlacement.LeftStart => "origin-top-left top-0",
            DropdownPlacement.RightEnd or DropdownPlacement.LeftEnd => "origin-bottom-left bottom-0",
            DropdownPlacement.Top or DropdownPlacement.Bottom => "origin-top-left start-0",
            DropdownPlacement.Right => "origin-top-right top-1/2 -translate-y-1/2",
            _ => "origin-top-left top-1/2 -translate-y-1/2" // Left is default
        };

        var baseClasses = string.Join(" ", new[]
        {
            "absolute min-w-[180px] rounded shadow focus:outline-none",
            "divide-y divide-gray-100",
            "border border-gray-200 bg-white text-gray-900 dark:border-none dark:bg-gray-700 dark:text-white",
            positionClass,
            alignmentClass
        });

        var zClass = string.IsNullOrWhiteSpace(MenuClass) ? "z-10" : MenuClass;

        return MergeClasses(baseClasses, zClass, Slots?.Menu);
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
        }
    }
}
