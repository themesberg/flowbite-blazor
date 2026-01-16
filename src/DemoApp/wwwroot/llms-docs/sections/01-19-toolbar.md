#### Toolbar Examples

The Toolbar component provides a flexible container for grouping actions, buttons, inputs, and other controls with consistent spacing and layout. It supports left-aligned content and optional right-aligned content.

**Toolbar Parameters:**
- `ChildContent`: Main content area (left side of toolbar)
- `End`: Optional content for the right side of the toolbar
- `Embedded`: When true, removes background and border styling for embedded use (default: false)

**ToolbarButton Colors:**
- ToolbarButtonColor.Gray
- ToolbarButtonColor.Dark

**ToolbarButton Sizes:**
- ToolbarButtonSize.ExtraSmall
- ToolbarButtonSize.Small
- ToolbarButtonSize.Medium
- ToolbarButtonSize.Large

```razor
<!-- Default toolbar with styled background -->
<Toolbar>
    <ChildContent>
        <Button Size="ButtonSize.Small">Action 1</Button>
        <Button Size="ButtonSize.Small">Action 2</Button>
        <Button Size="ButtonSize.Small">Action 3</Button>
    </ChildContent>
</Toolbar>

<!-- Embedded toolbar (transparent background) -->
<Toolbar Embedded="true">
    <ChildContent>
        <TextInput Placeholder="Search..." Class="w-64" />
        <Button Size="ButtonSize.Small" Class="ml-2">Search</Button>
    </ChildContent>
</Toolbar>

<!-- Toolbar with end content -->
<Toolbar Embedded="true">
    <ChildContent>
        <TextInput Placeholder="Search for users..." Class="w-80" />
    </ChildContent>
    <End>
        <Button Size="ButtonSize.Small">
            <PlusIcon Size="IconSize.Small" Class="mr-2" />
            Add User
        </Button>
    </End>
</Toolbar>

<!-- Toolbar with icon buttons -->
<Toolbar Embedded="true">
    <ChildContent>
        <div class="flex items-center space-x-1">
            <ToolbarButton AriaLabel="Settings">
                <CogIcon Size="IconSize.Large" />
            </ToolbarButton>
            <ToolbarButton AriaLabel="Delete">
                <TrashBinIcon Size="IconSize.Large" />
            </ToolbarButton>
            <ToolbarButton AriaLabel="Notifications">
                <BellIcon Size="IconSize.Large" />
            </ToolbarButton>
        </div>
    </ChildContent>
</Toolbar>

<!-- ToolbarButton with different sizes -->
<div class="flex items-center space-x-2">
    <ToolbarButton Size="ToolbarButtonSize.ExtraSmall" AriaLabel="Extra small">
        <CogIcon Size="IconSize.Small" />
    </ToolbarButton>
    <ToolbarButton Size="ToolbarButtonSize.Small" AriaLabel="Small">
        <CogIcon Size="IconSize.Small" />
    </ToolbarButton>
    <ToolbarButton Size="ToolbarButtonSize.Medium" AriaLabel="Medium">
        <CogIcon Size="IconSize.Medium" />
    </ToolbarButton>
    <ToolbarButton Size="ToolbarButtonSize.Large" AriaLabel="Large">
        <CogIcon Size="IconSize.Large" />
    </ToolbarButton>
</div>

<!-- Complete CRUD table toolbar -->
<Toolbar Embedded="true" Class="w-full py-4">
    <ChildContent>
        <TextInput Placeholder="Search for users..." Class="me-4 w-80 border xl:w-96" />
        
        <div class="border-l border-gray-100 pl-2 dark:border-gray-700 flex items-center space-x-1">
            <ToolbarButton AriaLabel="Settings">
                <CogIcon Size="IconSize.Large" />
            </ToolbarButton>
            <ToolbarButton AriaLabel="Delete">
                <TrashBinIcon Size="IconSize.Large" />
            </ToolbarButton>
            <ToolbarButton AriaLabel="More options">
                <DotsVerticalIcon Size="IconSize.Large" />
            </ToolbarButton>
        </div>
    </ChildContent>
    <End>
        <div class="flex items-center space-x-2">
            <Button Size="ButtonSize.Small" Class="gap-2 px-3 whitespace-nowrap">
                <PlusIcon Size="IconSize.Small" />
                Add user
            </Button>
            <Button Size="ButtonSize.Small"
                    Variant="ButtonVariant.Outline"
                    Class="gap-2 px-3">
                <DownloadIcon Size="IconSize.Medium" />
                Export
            </Button>
        </div>
    </End>
</Toolbar>
