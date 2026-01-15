
#### Sidebar Examples

SidebarCollapse uses a CSS animation state machine for smooth height-based transitions.

```razor
<!-- Sidebar example that comprehensively demonstrates the majority of features -->
<div class="flex">
    <!-- Mobile menu button -->
    <Button Color="ButtonColor.Dark" Class="lg:hidden mb-3">
        <BarsIcon Class="w-5 h-5" />
    </Button>
</div>

<Sidebar CollapseMode="SidebarCollapseMode.Responsive">
    <SidebarLogo
        Href="#"
        ImgSrc="/images/logo.svg">
        Flowbite
    </SidebarLogo>

    <SidebarItemGroup>
        <SidebarItem
            Href="#"
            Icon="@(new HomeIcon())">
            Dashboard
        </SidebarItem>

        <SidebarCollapse
            Label="Analytics"
            Icon="@(new ChartPieIcon())"
            InitiallyOpen="true">
            <SidebarItem
                Href="#"
                Icon="@(new ChartLineUpIcon())">
                Overview
            </SidebarItem>
            <SidebarItem
                Href="#"
                Icon="@(new ChartMixedIcon())">
                Reports
            </SidebarItem>
        </SidebarCollapse>

        <SidebarItem
            Href="#"
            Icon="@(new BriefcaseIcon())">
            Users
        </SidebarItem>

        <SidebarItem
            Href="#"
            Icon="@(new CartIcon())">
            Products
        </SidebarItem>
    </SidebarItemGroup>

    <SidebarItemGroup>
        <SidebarItem
            Href="#"
            Icon="@(new CogIcon())">
            Settings
        </SidebarItem>

        <SidebarItem
            Href="#"
            Icon="@(new BellIcon())">
            Help Center
        </SidebarItem>
    </SidebarItemGroup>

    <SidebarCTA>
        <div class="mb-3 text-sm text-blue-900 dark:text-blue-400">
            Preview the new Flowbite dashboard navigation!
        </div>
        <Button Color="ButtonColor.Default" Class="w-full">
            Upgrade to Pro
        </Button>
    </SidebarCTA>
</Sidebar>


<!-- Multi-Level Sidebar with deep nested navigation -->
<Sidebar>
    <SidebarItem
        Href="/dashboard"
        Icon="@(new HomeIcon())">
        Dashboard
    </SidebarItem>

    <SidebarCollapse
        Label="Settings"
        Icon="@(new CogIcon())">
        <SidebarItem
            Href="/settings/profile"
            Icon="@(new BriefcaseIcon())">
            Profile
        </SidebarItem>
        <SidebarCollapse
            Label="System"
            Icon="@(new ServerIcon())">
            <SidebarItem
                Href="/settings/system/general"
                Icon="@(new AdjustmentsHorizontalIcon())">
                General
            </SidebarItem>
            <SidebarCollapse
                Label="Security"
                Icon="@(new ShieldCheckIcon())">
                <SidebarItem Href="/settings/system/security/permissions">
                    Permissions
                </SidebarItem>
                <SidebarItem Href="/settings/system/security/authentication">
                    Authentication
                </SidebarItem>
            </SidebarCollapse>
        </SidebarCollapse>
    </SidebarCollapse>

    <SidebarItem
        Href="/help"
        Icon="@(new BellIcon())">
        Help
    </SidebarItem>
</Sidebar>
```

### SidebarCollapse Animation

SidebarCollapse uses a 4-state animation machine for smooth transitions:

| State | Description |
|-------|-------------|
| `Collapsed` | Height = 0, content hidden |
| `Expanding` | Height animating from 0 to scrollHeight |
| `Expanded` | Height = auto, content visible |
| `Collapsing` | Height animating from scrollHeight to 0 |

The animation respects `prefers-reduced-motion` for accessibility.

### SidebarCollapse Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Label` | `string` | - | Display text |
| `Icon` | `IconBase?` | `null` | Icon component |
| `InitiallyOpen` | `bool` | `false` | Start expanded |
| `OnStateChanged` | `EventCallback<bool>` | - | State change callback |

### Sidebar Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `CollapseMode` | `SidebarCollapseMode` | - | Responsive behavior |
| `Class` | `string?` | `null` | Additional CSS classes |
