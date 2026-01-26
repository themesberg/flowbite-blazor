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

    // Computed properties
    private int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling((double)TotalItems / PageSize);
    private int StartItem => TotalItems == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;
    private int EndItem => Math.Min(CurrentPage * PageSize, TotalItems);
    private bool HasPreviousPage => CurrentPage > 1;
    private bool HasNextPage => CurrentPage < TotalPages;

    private string ComputedClass => MergeClasses(
        ElementClass.Empty()
            .Add("flex items-center justify-between pt-4")
            .Add(Class)
    );

    private IEnumerable<int> VisiblePages
    {
        get
        {
            var half = MaxVisiblePages / 2;
            var start = Math.Max(1, CurrentPage - half);
            var end = Math.Min(TotalPages, start + MaxVisiblePages - 1);

            if (end - start + 1 < MaxVisiblePages)
                start = Math.Max(1, end - MaxVisiblePages + 1);

            return Enumerable.Range(start, end - start + 1);
        }
    }

    // Button CSS classes
    private const string ButtonBaseClass = "flex items-center justify-center px-3 h-8 leading-tight border";

    private string PreviousButtonClass => MergeClasses(
        ElementClass.Empty()
            .Add(ButtonBaseClass)
            .Add("ms-0 rounded-s-lg")
            .Add("text-gray-500 bg-white border-gray-300 hover:bg-gray-100 hover:text-gray-700")
            .Add("dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white")
            .Add("disabled:opacity-50 disabled:cursor-not-allowed")
    );

    private string NextButtonClass => MergeClasses(
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

    // Navigation methods
    private Task PreviousPageAsync() => GoToPageAsync(CurrentPage - 1);
    private Task NextPageAsync() => GoToPageAsync(CurrentPage + 1);

    private async Task GoToPageAsync(int page)
    {
        if (page >= 1 && page <= TotalPages && page != CurrentPage)
        {
            await OnPageChange.InvokeAsync(page);
        }
    }
}
