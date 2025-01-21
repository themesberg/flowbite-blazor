
#### Table Examples

```razor
<!-- Basic Table with Striped rows and hovering effects-->
<Table Striped="true" Hoverable="true">
    <TableHead>
        <TableRow>
            <TableHeadCell>Product name</TableHeadCell>
            <TableHeadCell>Color</TableHeadCell>
            <TableHeadCell>Category</TableHeadCell>
            <TableHeadCell>Price</TableHeadCell>
            <TableHeadCell>
                <span class="sr-only">Edit</span>
            </TableHeadCell>
        </TableRow>
    </TableHead>
    <TableBody class="divide-y">
        <TableRow class="bg-white dark:border-gray-700 dark:bg-gray-800">
            <TableCell class="whitespace-nowrap font-medium text-gray-900 dark:text-white">Apple MacBook Pro 17"</TableCell>
            <TableCell>Sliver</TableCell>
            <TableCell>Laptop</TableCell>
            <TableCell>$2999</TableCell>
            <TableCell>
                <a href="#" class="font-medium text-primary-600 hover:underline dark:text-primary-500">Edit</a>
            </TableCell>
        </TableRow>
        ... More rows...
    </TableBody>
</Table>


<!-- Table with Row selection with checkboxes -->
<Table>
    <TableHead>
        <TableRow>
            <TableCheckboxColumn TItem="Product" 
                               Items="@_products"
                               IsSelected="@(p => p.IsSelected)"
                               OnSelectionChange="@OnSelectionChange" />
            <TableHeadCell>Product</TableHeadCell>
            <TableHeadCell>Category</TableHeadCell>
            <TableHeadCell Align="right">Price</TableHeadCell>
        </TableRow>
    </TableHead>
    <TableBody>
        @foreach (var product in _products)
        {
            <TableRow>
                <TableCell>
                    <input type="checkbox"
                           class="h-4 w-4 rounded border-gray-300 bg-gray-100 text-blue-600 focus:ring-2 focus:ring-blue-500 dark:border-gray-600 dark:bg-gray-700 dark:ring-offset-gray-800 dark:focus:ring-blue-600"
                           checked="@product.IsSelected"
                           @onchange="@(e => OnSelectionChange(product, e.Value is bool value && value))" />
                </TableCell>
                <TableCell IsFirstColumn="true">@product.Name</TableCell>
                <TableCell>@product.Category</TableCell>
                <TableCell Align="right">@product.Price</TableCell>
            </TableRow>
        }
    </TableBody>
</Table>

@code {
    private List<Product> _products = new()
    {
        new() { Name = "Apple MacBook Pro 17\"", Category = "Laptop", Price = "$2999" },
        new() { Name = "Microsoft Surface Pro", Category = "Tablet", Price = "$999" },
        new() { Name = "Magic Mouse 2", Category = "Accessories", Price = "$99" }
    };

    private void OnSelectionChange(Product product, bool selected)
    {
        product.IsSelected = selected;
        StateHasChanged();
    }

    private class Product
    {
        public bool IsSelected { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
    }
}

```
