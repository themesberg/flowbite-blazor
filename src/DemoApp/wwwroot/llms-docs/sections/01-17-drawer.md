
#### Drawer Examples

__Default Drawer:__

```razor
<Button OnClick="() => showDefaultDrawer = true">Show drawer</Button>

<Drawer Show="@showDefaultDrawer" OnClose="() => showDefaultDrawer = false">
    <DrawerHeader>
        Default Drawer
    </DrawerHeader>
    <DrawerItems>
        <p class="mb-6 text-sm text-gray-500 dark:text-gray-400">
            This is a default drawer that slides in from the left side of the page.
        </p>
        <div class="grid grid-cols-2 gap-4">
            <Button Color="ButtonColor.Gray" OnClick="() => showDefaultDrawer = false">
                Cancel
            </Button>
            <Button OnClick="() => showDefaultDrawer = false">
                Accept
            </Button>
        </div>
    </DrawerItems>
</Drawer>

@code {
    private bool showDefaultDrawer = false;
}

```

__Drawer Options:__

```csharp

public enum DrawerPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    Center,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}
```

```razor
<div class="flex flex-wrap gap-4">
    <Button OnClick="() => showLeftDrawer = true">Left drawer</Button>
    <Button OnClick="() => showRightDrawer = true">Right drawer</Button>
    <Button OnClick="() => showTopDrawer = true">Top drawer</Button>
    <Button OnClick="() => showBottomDrawer = true">Bottom drawer</Button>
</div>

<Drawer Show="@showLeftDrawer" OnClose="() => showLeftDrawer = false" Position="DrawerPosition.Left">
    <DrawerHeader>Left Drawer</DrawerHeader>
    <DrawerItems>
        <p class="mb-6 text-sm text-gray-500 dark:text-gray-400">
            This drawer slides in from the left side of the page.
        </p>
        <Button OnClick="() => showLeftDrawer = false">Close</Button>
    </DrawerItems>
</Drawer>

<Drawer Show="@showRightDrawer" OnClose="() => showRightDrawer = false" Position="DrawerPosition.Right">
    <DrawerHeader>Right Drawer</DrawerHeader>
    <DrawerItems>
        <p class="mb-6 text-sm text-gray-500 dark:text-gray-400">
            This drawer slides in from the right side of the page.
        </p>
        <Button OnClick="() => showRightDrawer = false">Close</Button>
    </DrawerItems>
</Drawer>

<Drawer Show="@showTopDrawer" OnClose="() => showTopDrawer = false" Position="DrawerPosition.Top">
    <DrawerHeader>Top Drawer</DrawerHeader>
    <DrawerItems>
        <p class="mb-6 text-sm text-gray-500 dark:text-gray-400">
            This drawer slides in from the top of the page.
        </p>
        <Button OnClick="() => showTopDrawer = false">Close</Button>
    </DrawerItems>
</Drawer>

<Drawer Show="@showBottomDrawer" OnClose="() => showBottomDrawer = false" Position="DrawerPosition.Bottom">
    <DrawerHeader>Bottom Drawer</DrawerHeader>
    <DrawerItems>
        <p class="mb-6 text-sm text-gray-500 dark:text-gray-400">
            This drawer slides in from the bottom of the page.
        </p>
        <Button OnClick="() => showBottomDrawer = false">Close</Button>
    </DrawerItems>
</Drawer>

@code {
    private bool showLeftDrawer = false;
    private bool showRightDrawer = false;
    private bool showTopDrawer = false;
    private bool showBottomDrawer = false;
}
```

