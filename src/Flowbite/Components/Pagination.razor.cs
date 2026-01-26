using Flowbite.Base;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// A pagination component for navigating through pages of data.
/// </summary>
/// <remarks>
/// The Pagination component provides a standard way to navigate through paginated data.
/// It supports customizable page sizes, visible page counts, and includes built-in
/// accessibility features for keyboard navigation and screen readers.
/// </remarks>
/// <example>
/// <code>
/// &lt;Pagination CurrentPage="@currentPage"
///             PageSize="10"
///             TotalItems="@totalItems"
///             OnPageChange="@HandlePageChange" /&gt;
///
/// @code {
///     private int currentPage = 1;
///     private int totalItems = 100;
///
///     private void HandlePageChange(int page) =&gt; currentPage = page;
/// }
/// </code>
/// </example>
public partial class Pagination : FlowbiteComponentBase
{
    /// <summary>
    /// The current active page number (1-based).
    /// </summary>
    [Parameter]
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// The number of items displayed per page.
    /// </summary>
    [Parameter]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// The total number of items across all pages.
    /// </summary>
    [Parameter]
    public int TotalItems { get; set; }

    /// <summary>
    /// Event callback invoked when the page changes.
    /// </summary>
    [Parameter]
    public EventCallback<int> OnPageChange { get; set; }

    /// <summary>
    /// Maximum number of page buttons to display.
    /// </summary>
    [Parameter]
    public int MaxVisiblePages { get; set; } = 5;

    /// <summary>
    /// Whether to show the "Showing X-Y of Z" information.
    /// </summary>
    [Parameter]
    public bool ShowInfo { get; set; } = true;

    /// <summary>
    /// Accessibility label for the navigation element.
    /// </summary>
    [Parameter]
    public string AriaLabel { get; set; } = "Pagination navigation";

    /// <summary>
    /// Custom content for the previous button.
    /// </summary>
    [Parameter]
    public RenderFragment? PreviousContent { get; set; }

    /// <summary>
    /// Custom content for the next button.
    /// </summary>
    [Parameter]
    public RenderFragment? NextContent { get; set; }

    /// <summary>
    /// Whether to show First/Last page navigation buttons.
    /// </summary>
    [Parameter]
    public bool ShowFirstLast { get; set; } = false;

    /// <summary>
    /// Custom content for the first button.
    /// </summary>
    [Parameter]
    public RenderFragment? FirstContent { get; set; }

    /// <summary>
    /// Custom content for the last button.
    /// </summary>
    [Parameter]
    public RenderFragment? LastContent { get; set; }

    /// <summary>
    /// Whether to show ellipsis indicators when pages are skipped.
    /// </summary>
    [Parameter]
    public bool ShowEllipsis { get; set; } = true;

    /// <summary>
    /// The size variant of the pagination buttons.
    /// </summary>
    [Parameter]
    public PaginationSize Size { get; set; } = PaginationSize.Default;

    /// <summary>
    /// Whether to show the go-to-page input.
    /// </summary>
    [Parameter]
    public bool ShowGoToPage { get; set; } = false;

    /// <summary>
    /// Label for the go-to-page input.
    /// </summary>
    [Parameter]
    public string GoToPageLabel { get; set; } = "Go to";

    /// <summary>
    /// Whether to show the page size selector.
    /// </summary>
    [Parameter]
    public bool ShowPageSizeSelector { get; set; } = false;

    /// <summary>
    /// Options for the page size selector.
    /// </summary>
    [Parameter]
    public int[] PageSizeOptions { get; set; } = [10, 20, 50, 100];

    /// <summary>
    /// Event callback invoked when the page size changes.
    /// </summary>
    [Parameter]
    public EventCallback<int> OnPageSizeChange { get; set; }

    // Internal state for go-to-page input
    private string _goToPageInput = "";

    // Computed properties
    private int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling((double)TotalItems / PageSize);
    private int StartItem => TotalItems == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;
    private int EndItem => Math.Min(CurrentPage * PageSize, TotalItems);
    private bool HasPreviousPage => CurrentPage > 1;
    private bool HasNextPage => CurrentPage < TotalPages;
    private bool CanGoFirst => CurrentPage > 1;
    private bool CanGoLast => CurrentPage < TotalPages;

    private string ComputedClass => MergeClasses(
        ElementClass.Empty()
            .Add("flex items-center justify-between pt-4")
            .Add("flex-wrap gap-4", when: ShowGoToPage || ShowPageSizeSelector)
            .Add(Class)
    );

    // Ellipsis markers (negative numbers that won't conflict with page numbers)
    private const int LeftEllipsisMarker = -1;
    private const int RightEllipsisMarker = -2;

    /// <summary>
    /// Returns visible page numbers with negative markers for ellipsis positions.
    /// -1 = left ellipsis, -2 = right ellipsis
    /// </summary>
    private IEnumerable<int> VisiblePages
    {
        get
        {
            if (!ShowEllipsis || TotalPages <= MaxVisiblePages)
            {
                // Simple case: show all pages
                var half = MaxVisiblePages / 2;
                var start = Math.Max(1, CurrentPage - half);
                var end = Math.Min(TotalPages, start + MaxVisiblePages - 1);

                if (end - start + 1 < MaxVisiblePages)
                    start = Math.Max(1, end - MaxVisiblePages + 1);

                return Enumerable.Range(start, end - start + 1);
            }

            // Ellipsis logic: always show first page, last page, and pages around current
            var pages = new List<int>();
            var siblingCount = 1; // Pages on each side of current

            // Always add first page
            pages.Add(1);

            // Calculate the range around current page
            var siblingsStart = Math.Max(2, CurrentPage - siblingCount);
            var siblingsEnd = Math.Min(TotalPages - 1, CurrentPage + siblingCount);

            // Show left ellipsis?
            if (siblingsStart > 2)
            {
                pages.Add(LeftEllipsisMarker);
            }

            // Add sibling pages (includes page 2 if siblingsStart == 2)
            for (var i = siblingsStart; i <= siblingsEnd; i++)
            {
                if (i > 1 && i < TotalPages && !pages.Contains(i))
                    pages.Add(i);
            }

            // Show right ellipsis?
            if (siblingsEnd < TotalPages - 1)
            {
                pages.Add(RightEllipsisMarker);
            }

            // Always add last page if more than 1 page
            if (TotalPages > 1 && !pages.Contains(TotalPages))
                pages.Add(TotalPages);

            return pages;
        }
    }

    // Size-dependent classes
    private string GetSizeClasses() => Size switch
    {
        PaginationSize.Small => "px-2 h-6 text-xs",
        PaginationSize.Large => "px-4 h-10 text-base",
        _ => "px-3 h-8 text-sm"
    };

    private string GetIconSizeClass() => Size switch
    {
        PaginationSize.Small => "w-3 h-3",
        PaginationSize.Large => "w-5 h-5",
        _ => "w-4 h-4"
    };

    // Button CSS classes
    private string ButtonBaseClass => $"flex items-center justify-center leading-tight border {GetSizeClasses()}";

    private string FirstButtonClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("ms-0 rounded-s-lg")
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
            .Add("disabled:opacity-50 disabled:cursor-not-allowed")
    );

    private string PreviousButtonClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("ms-0 rounded-s-lg", when: !ShowFirstLast)
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
            .Add("disabled:opacity-50 disabled:cursor-not-allowed")
    );

    private string NextButtonClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("rounded-e-lg", when: !ShowFirstLast)
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
            .Add("disabled:opacity-50 disabled:cursor-not-allowed")
    );

    private string LastButtonClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("rounded-e-lg")
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
            .Add("disabled:opacity-50 disabled:cursor-not-allowed")
    );

    private string ActivePageClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("text-primary-600 bg-primary-50 border-gray-300")
            .Add("hover:bg-primary-100 hover:text-primary-700")
            .Add("dark:border-gray-700 dark:bg-gray-700 dark:text-white")
    );

    private string InactivePageClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
    );

    private string EllipsisClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("text-gray-400 bg-white border-gray-300 cursor-default")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-500")
    );

    private string GetPageSizeSelectorSizeClasses() => Size switch
    {
        PaginationSize.Small => "py-1.5 pl-2.5 text-xs",
        PaginationSize.Large => "py-2.5 pl-4 text-base",
        _ => "py-2 pl-3 text-sm"
    };

    private string PageSizeSelectorClass => MergeClasses(
        ElementClass.Empty()
            .Add("appearance-none rounded-lg border focus:outline-none focus:ring-1")
            .Add("bg-arrow-down-icon bg-[length:10px_10px] bg-[right_0.75rem_center] bg-no-repeat pr-8")
            .Add(GetPageSizeSelectorSizeClasses())
            .Add("border-gray-300 bg-gray-50 text-gray-900 focus:border-primary-500 focus:ring-primary-500")
            .Add("dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:focus:border-primary-500 dark:focus:ring-primary-500")
    );

    // Navigation methods
    private Task FirstPageAsync() => GoToPageAsync(1);
    private Task LastPageAsync() => GoToPageAsync(TotalPages);
    private Task PreviousPageAsync() => GoToPageAsync(CurrentPage - 1);
    private Task NextPageAsync() => GoToPageAsync(CurrentPage + 1);

    private async Task GoToPageAsync(int page)
    {
        if (page >= 1 && page <= TotalPages && page != CurrentPage)
        {
            await OnPageChange.InvokeAsync(page);
        }
    }

    private async Task HandleGoToPageAsync()
    {
        if (int.TryParse(_goToPageInput, out var page))
        {
            await GoToPageAsync(page);
            _goToPageInput = "";
        }
    }

    private async Task HandleGoToPageKeydownAsync(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await HandleGoToPageAsync();
        }
    }

    private async Task HandlePageSizeChangeAsync(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var newSize))
        {
            await OnPageSizeChange.InvokeAsync(newSize);
        }
    }
}

/// <summary>
/// Size variants for pagination buttons.
/// </summary>
public enum PaginationSize
{
    /// <summary>Small pagination buttons (h-6, text-xs)</summary>
    Small,
    /// <summary>Default pagination buttons (h-8, text-sm)</summary>
    Default,
    /// <summary>Large pagination buttons (h-10, text-base)</summary>
    Large
}
