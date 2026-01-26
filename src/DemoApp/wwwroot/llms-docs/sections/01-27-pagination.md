
#### Pagination Component

The Pagination component provides navigation through pages of data with built-in accessibility features.

**Parameters:**
- `CurrentPage` - Current active page number (1-based)
- `PageSize` - Items per page (default: 10)
- `TotalItems` - Total number of items
- `OnPageChange` - EventCallback when page changes
- `MaxVisiblePages` - Max page buttons to show (default: 5)
- `ShowInfo` - Show "Showing X-Y of Z" text (default: true)
- `ShowFirstLast` - Show First/Last navigation buttons (default: false)
- `ShowEllipsis` - Show ellipsis for large page ranges (default: true)
- `ShowGoToPage` - Show go-to-page input (default: false)
- `GoToPageLabel` - Label for go-to-page input (default: "Go to")
- `ShowPageSizeSelector` - Show items-per-page dropdown (default: false)
- `PageSizeOptions` - Array of page size options (default: [10, 20, 50, 100])
- `OnPageSizeChange` - EventCallback when page size changes
- `Size` - Button size variant (Small, Default, Large)
- `PreviousContent` / `NextContent` - Custom content for nav buttons
- `FirstContent` / `LastContent` - Custom content for first/last buttons

**Size Variants:**
- `PaginationSize.Small` - Compact pagination
- `PaginationSize.Default` - Standard size
- `PaginationSize.Large` - Larger buttons

```razor
<!-- Basic pagination -->
<Pagination CurrentPage="@currentPage"
            PageSize="10"
            TotalItems="100"
            OnPageChange="@(p => currentPage = p)" />

<!-- With First/Last buttons -->
<Pagination CurrentPage="@currentPage"
            PageSize="10"
            TotalItems="200"
            ShowFirstLast="true"
            OnPageChange="@(p => currentPage = p)" />

<!-- With go-to-page input -->
<Pagination CurrentPage="@currentPage"
            PageSize="10"
            TotalItems="500"
            ShowGoToPage="true"
            OnPageChange="@(p => currentPage = p)" />

<!-- With page size selector -->
<Pagination CurrentPage="@currentPage"
            PageSize="@pageSize"
            TotalItems="500"
            ShowPageSizeSelector="true"
            PageSizeOptions="@(new[] { 10, 25, 50, 100 })"
            OnPageChange="@(p => currentPage = p)"
            OnPageSizeChange="@(s => { pageSize = s; currentPage = 1; })" />

<!-- Small size -->
<Pagination Size="PaginationSize.Small" ... />

<!-- Large size -->
<Pagination Size="PaginationSize.Large" ... />

<!-- Full featured -->
<Pagination CurrentPage="@currentPage"
            PageSize="@pageSize"
            TotalItems="1500"
            ShowFirstLast="true"
            ShowGoToPage="true"
            ShowPageSizeSelector="true"
            OnPageChange="@(p => currentPage = p)"
            OnPageSizeChange="@(s => { pageSize = s; currentPage = 1; })" />

@code {
    private int currentPage = 1;
    private int pageSize = 10;
}
```
