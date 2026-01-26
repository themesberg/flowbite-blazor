
#### EmptyState Component

The EmptyState component provides visual feedback when no data is available, with support for icons, images, descriptions, and action buttons.

**Parameters:**
- `Title` - The title text (default: "No results found")
- `Description` - Optional description text
- `Icon` - Custom icon content (RenderFragment)
- `Image` - Custom image/illustration content, takes precedence over Icon (RenderFragment)
- `Action` - Primary action button content (RenderFragment)
- `SecondaryAction` - Secondary action button content (RenderFragment)

```razor
<!-- Basic empty state -->
<EmptyState Title="No items yet" />

<!-- With description -->
<EmptyState Title="No users found"
            Description="Try adjusting your search criteria or add a new user." />

<!-- With action button -->
<EmptyState Title="No products"
            Description="Get started by adding your first product.">
    <Action>
        <Button>
            <PlusIcon class="w-4 h-4 mr-2" />
            Add Product
        </Button>
    </Action>
</EmptyState>

<!-- With primary and secondary actions -->
<EmptyState Title="No projects"
            Description="Create a new project or import from another tool.">
    <Action>
        <Button>Create Project</Button>
    </Action>
    <SecondaryAction>
        <Button Color="ButtonColor.Gray">Import</Button>
    </SecondaryAction>
</EmptyState>

<!-- With custom icon -->
<EmptyState Title="No search results"
            Description="We couldn't find anything matching your search.">
    <Icon>
        <SearchIcon class="w-12 h-12 text-gray-400 dark:text-gray-500" />
    </Icon>
</EmptyState>

<!-- With custom image/illustration -->
<EmptyState Title="No notifications"
            Description="You're all caught up!">
    <Image>
        <svg class="w-32 h-32 text-gray-300" viewBox="0 0 24 24" stroke="currentColor">
            <!-- SVG paths -->
        </svg>
    </Image>
</EmptyState>
```
