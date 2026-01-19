<project title="Flowbite Blazor" summary="A comprehensive Blazor component library that ports the Flowbite React component library to ASP.NET Blazor 8.0/9.0. Built on Tailwind CSS v4, it provides strongly-typed Blazor components that implement Flowbite design patterns while maintaining consistency with the React implementation. The library offers a rich set of accessible, dark-mode compatible components with built-in ARIA support, smart positioning via Floating UI, and TailwindMerge-powered class conflict resolution.">

## Project Links

- [Github Repository](https://github.com/peakflames/flowbite-blazor)
- [Documentation Site](https://flowbite-blazor.peakflames.org/docs/components/{{COMPONENT_NAME}})

## Current Version

**v0.2.2-beta** - Requires Tailwind CSS v4 and .NET 8 or .NET 9

## Features

### Core Features
- **Tailwind CSS v4 Integration** - CSS-first configuration with `@theme` directive
- **Dark Mode Support** - Automatic dark mode through Tailwind CSS classes
- **Built-in Accessibility** - ARIA attributes and full keyboard navigation support
- **Responsive Design** - Mobile-first components that work everywhere
- **Native Blazor Events** - Seamless integration with Blazor's event system
- **Strong Typing** - Full type safety and IntelliSense support
- **No Node.js Required** - Simple MSBuild integration for Tailwind CSS
- **Extended Icons** - Optional package for additional icon components

### New in v0.2.x

- **TailwindMerge Integration** - Automatic class conflict resolution via `MergeClasses()`
- **Slot System** - Fine-grained component customization with typed slot classes
- **Floating UI Positioning** - Smart viewport-aware positioning for Dropdown, Tooltip, and Popover
- **Debounced Input** - Built-in debouncing for TextInput search scenarios
- **Animation State Machine** - Smooth height-based animations for SidebarCollapse
- **Lazy JavaScript Modules** - On-demand loading of JS modules for better performance
- **motion-reduce Support** - Respects user's reduced motion preferences

## Service Registration

Register Flowbite services in your `Program.cs`:

```csharp
using Flowbite.Services;

builder.Services.AddFlowbite();
```

This registers:
- `TwMerge` - TailwindMerge service for class conflict resolution
- `IFloatingService` - Floating UI positioning for dropdowns/tooltips
- `IClipboardService` - Lazy-loaded clipboard operations
- `IElementService` - Lazy-loaded DOM element utilities
- `IFocusManagementService` - Lazy-loaded focus trap and management
- `IModalService` - Programmatic modal control
- `IDrawerService` - Programmatic drawer control
- `IToastService` - Programmatic toast notifications

</project>


<docs>


<doc title="UI Components" description="Blazor UI Components">

## Available Components

Flowbite Blazor provides the following set of UI components:
- Alert
- Avatar
- Badge
- Breadcrumb
- Button
- Card
- Drawer
- Dropdown
- Form Components
   - TextInput
   - TextArea
   - Select
   - Checkbox
   - Radio
   - FileInput
   - ToggeSwitch
   - RangeSlider
- Modal
- Icons
- Navbar
- QuizGrid
- Sidebar
- Spinner
- Tooltip


### Components



#### Alert Examples

The available Alert colors are:
- AlertColor.Info
- AlertColor.Gray
- AlertColor.Failure
- AlertColor.Success
- AlertColor.Warning
- AlertColor.Red
- AlertColor.Green
- AlertColor.Yellow
- AlertColor.Blue
- AlertColor.Primary
- AlertColor.Pink
- AlertColor.Lime
- AlertColor.Dark
- AlertColor.Indigo
- AlertColor.Purple
- AlertColor.Teal
- AlertColor.Light

```razor
<!-- Default alert with emphasis -->
<Alert Color="AlertColor.Info"
       TextEmphasis="Info alert!"
       Text="More info about this info alert goes here." />

<!-- Alert with icon and border accent -->
<Alert Color="AlertColor.Warning"
       Icon="@(new EyeIcon())"
       WithBorderAccent="true"
       TextEmphasis="Warning alert!"
       Text="More info about this warning alert goes here." />

<!-- Alert with custom content -->
<Alert Color="AlertColor.Info">
    <CustomContent>
        <div class="flex items-center">
            <span class="text-lg font-medium">Requirements:</span>
        </div>
    </CustomContent>
    <AdditionalContent>
        <div class="mt-2 mb-4 text-sm">
            <ul class="list-disc list-inside">
                <li>Minimum 10 characters</li>
                <li>At least one special character</li>
            </ul>
        </div>
    </AdditionalContent>
</Alert>
```





#### Avatar Examples

```razor
<!-- Different sizes -->
<Avatar Size="AvatarSize.Small" 
        Rounded="true"
        Image="path/to/small-image.jpg"
        Alt="Small user photo" />

<Avatar Size="AvatarSize.Large"
        Rounded="true"
        Image="path/to/large-image.jpg"
        Alt="Large user photo" />

<!-- With status indicator -->
<div class="relative">
    <Avatar Size="AvatarSize.Large"
            Rounded="true"
            Image="path/to/image.jpg"
            Alt="User photo" />
    <span class="absolute bottom-0 right-0 h-3.5 w-3.5 rounded-full border-2 border-white bg-green-400"></span>
</div>
```




#### Badge Examples

The available Badge colors are:
- BadgeColor.Primary,
- BadgeColor.Info,
- BadgeColor.Gray,
- BadgeColor.Success,
- BadgeColor.Warning,
- BadgeColor.Indigo,
- BadgeColor.Purple,
- BadgeColor.Pink

The available Badge sizes are:
- BadgeSize.ExtraSmall,
- BadgeSize.Small


```razor
<!-- Default badges with different colors -->
<div class="flex flex-wrap gap-2">
    <Badge Color="BadgeColor.Info">Info</Badge>
    ....
</div>

<!-- Badge as link -->
<div class="flex flex-wrap gap-2">
    <Badge Href="#">Primary</Badge>
    <Badge Color="BadgeColor.Gray" Href="#">Gray</Badge>
    ...
</div>

<!-- Badge with icon -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())">2 minutes ago</Badge>
    <Badge Color="BadgeColor.Gray" Icon="@(new ClockIcon())">In progress</Badge>
    ...
</div>

<!-- Badge with icon only -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())" />
    <Badge Icon="@(new CheckIcon())" Color="BadgeColor.Gray" />
    ...
</div>

<!-- Different sizes -->
<div class="flex flex-wrap items-center gap-2">
    <Badge Size="BadgeSize.ExtraSmall">Extra small</Badge>
    <Badge Size="BadgeSize.Small">Small</Badge>
</div>
```




#### Breadcrumb Examples

```razor
<Breadcrumb>
    <BreadcrumbItem Href="/">
        <HomeIcon class="w-4 h-4 mr-2" />
        Home
    </BreadcrumbItem>
    <BreadcrumbItem Href="/category">Category</BreadcrumbItem>
    <BreadcrumbItem>Current Page</BreadcrumbItem>
</Breadcrumb>
```




#### Button Examples

The available Button colors are:
- ButtonColor.Default (blue)
- ButtonColor.Primary (customizable via @theme)
- ButtonColor.Gray
- ButtonColor.Dark
- ButtonColor.Light
- ButtonColor.Green
- ButtonColor.Red
- ButtonColor.Yellow
- ButtonColor.Purple

The available Button sizes are:
- ButtonSize.Small
- ButtonSize.Medium
- ButtonSize.Large

The available Button variants are:
- ButtonVariant.Default (filled)
- ButtonVariant.Outline (bordered)

```razor
<!-- Default button with color -->
<Button Color="ButtonColor.Green" Size="ButtonSize.Large">
    Click me
</Button>

<!-- Outline button with icon -->
<Button Variant="ButtonVariant.Outline" Color="ButtonColor.Primary" Icon="@(new InfoIcon())">
    Info with icon
</Button>

<!-- Loading state -->
<Button Loading="true" Color="ButtonColor.Green">
    Processing...
</Button>

<!-- Link button -->
<Button Href="https://flowbite.com" Target="_blank" Color="ButtonColor.Purple">
    Visit Website
</Button>

<!-- Full-width button -->
<Button Color="ButtonColor.Dark" Class="w-full">
    Block Button
</Button>

<!-- Pill-shaped button -->
<Button Color="ButtonColor.Primary" Pill="true">
    Rounded Button
</Button>

<!-- Primary color (uses @theme customization) -->
<Button Color="ButtonColor.Primary">
    Brand Color
</Button>
```

### Button Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Color` | `ButtonColor` | `Default` | Color variant |
| `Size` | `ButtonSize` | `Medium` | Size variant |
| `Variant` | `ButtonVariant` | `Default` | Visual style (Default or Outline) |
| `Disabled` | `bool` | `false` | Disables the button |
| `Loading` | `bool` | `false` | Shows loading state |
| `Pill` | `bool` | `false` | Fully rounded corners |
| `Icon` | `IconBase?` | `null` | Icon to display |
| `Href` | `string?` | `null` | Renders as link when set |
| `Target` | `string?` | `null` | Link target (_blank, _self) |
| `Type` | `string` | `"button"` | HTML button type |
| `Class` | `string?` | `null` | Additional CSS classes |
| `OnClick` | `EventCallback<MouseEventArgs>` | - | Click handler |




#### Card Examples

```razor
<!-- Basic card with CTA button -->
<Card Class="max-w-sm">
    <h5 class="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Noteworthy technology acquisitions 2021
    </h5>
    <p class="font-normal text-gray-700 dark:text-gray-400">
        Here are the biggest enterprise technology acquisitions of 2021 so far.
    </p>
    <Button>
        <div class="inline-flex items-center">
            Read more
            <svg class="ml-2 h-4 w-4" fill="currentColor" viewBox="0 0 20 20">
                <path fillRule="evenodd" d="M10.293 3.293a1 1 0 011.414 0l6 6a1 1 0 010 1.414l-6 6a1 1 0 01-1.414-1.414L14.586 11H3a1 1 0 110-2h11.586l-4.293-4.293a1 1 0 010-1.414z" clipRule="evenodd" />
            </svg>
        </div>
    </Button>
</Card>

<!-- Card with image -->
<Card Class="max-w-sm"
      ImgSrc="path/to/image.jpg"
      ImgAlt="Meaningful alt text">
    <h5 class="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Card with image
    </h5>
    <p class="font-normal text-gray-700 dark:text-gray-400">
        Here are the biggest enterprise technology acquisitions of 2021 so far.
    </p>
</Card>

<!-- Horizontal card layout -->
<Card Class="max-w-sm"
      ImgSrc="path/to/image.jpg"
      ImgAlt="Meaningful alt text"
      Horizontal="true">
    <h5 class="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Horizontal card layout
    </h5>
    <p class="font-normal text-gray-700 dark:text-gray-400">
        Perfect for news articles or blog post previews.
    </p>
</Card>

<!-- Card with Slots for custom styling -->
<Card Slots="@(new CardSlots {
    Base = "shadow-xl rounded-xl",
    Image = "object-top",
    Body = "p-8"
})">
    <h5 class="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
        Custom styled card
    </h5>
    <p class="font-normal text-gray-700 dark:text-gray-400">
        Using CardSlots for fine-grained styling control.
    </p>
</Card>

<!-- Clickable card -->
<Card Href="/products/123" Class="max-w-sm">
    <h5 class="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">
        Click to view product
    </h5>
</Card>
```

### Card Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `ImgSrc` | `string?` | `null` | Image source URL |
| `ImgAlt` | `string?` | `null` | Image alt text |
| `Horizontal` | `bool` | `false` | Horizontal layout on larger screens |
| `Href` | `string?` | `null` | Makes card clickable |
| `Slots` | `CardSlots?` | `null` | Per-element class customization |
| `Class` | `string?` | `null` | Additional CSS classes |

### CardSlots Properties

| Slot | Description |
|------|-------------|
| `Base` | The card container |
| `Image` | The card's image element |
| `Body` | The content wrapper |





#### Dropdown Examples

Dropdown uses Floating UI for smart viewport-aware positioning with automatic flip and shift behavior.

```razor
<!-- Default dropdown -->
<Dropdown>
    <Label>Dropdown menu</Label>
    <ChildContent>
        <DropdownHeader>
            <span class="block text-sm">Bonnie Green</span>
            <span class="block truncate text-sm font-medium">name@flowbite.com</span>
        </DropdownHeader>
        <DropdownItem Icon="@(new UserIcon())" OnClick="@HandleProfileClick">Profile</DropdownItem>
        <DropdownItem Icon="@(new GearIcon())" OnClick="@HandleSettingsClick">Settings</DropdownItem>
        <DropdownDivider />
        <DropdownItem Icon="@(new ArrowRightToBracketIcon())" OnClick="@HandleSignOutClick">
            Sign out
        </DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with custom trigger -->
<Dropdown>
    <CustomTrigger>
        <Avatar Alt="User settings"
                ImageUrl="https://flowbite.com/docs/images/people/profile-picture-5.jpg"
                Rounded="true" />
    </CustomTrigger>
    <ChildContent>
        <DropdownItem>Profile</DropdownItem>
        <DropdownItem>Settings</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with Slots for custom styling -->
<Dropdown Slots="@(new DropdownSlots {
    Trigger = "bg-blue-600 hover:bg-blue-700",
    Menu = "w-64 shadow-xl",
    Item = "hover:bg-blue-50"
})">
    <Label>Styled Dropdown</Label>
    <ChildContent>
        <DropdownItem>Option 1</DropdownItem>
        <DropdownItem>Option 2</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with placement and offset -->
<Dropdown Placement="DropdownPlacement.TopEnd" Offset="12">
    <Label>Top End</Label>
    <ChildContent>
        <DropdownItem>Item 1</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with disabled auto-positioning -->
<Dropdown
    Placement="DropdownPlacement.Bottom"
    DisableFlip="true"
    DisableShift="true">
    <Label>Fixed Position</Label>
    <ChildContent>
        <DropdownItem>Fixed at bottom</DropdownItem>
    </ChildContent>
</Dropdown>
```

### Dropdown Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Size` | `DropdownSize` | `Medium` | Trigger and menu size |
| `Placement` | `DropdownPlacement` | `Bottom` | Menu placement |
| `Inline` | `bool` | `false` | Inline trigger styling |
| `ArrowIcon` | `bool` | `true` | Show dropdown arrow |
| `DismissOnClick` | `bool` | `true` | Close on item click |
| `DisableFlip` | `bool` | `false` | Disable viewport flip |
| `DisableShift` | `bool` | `false` | Disable viewport shift |
| `Offset` | `int` | `8` | Distance from trigger |
| `Slots` | `DropdownSlots?` | `null` | Per-element classes |
| `MenuClass` | `string?` | `null` | Menu CSS classes |
| `IsOpenChanged` | `EventCallback<bool>` | - | Open state changed |
| `OnTriggerClick` | `EventCallback<MouseEventArgs>` | - | Trigger click |

### DropdownSlots Properties

| Slot | Description |
|------|-------------|
| `Base` | The dropdown container |
| `Trigger` | The trigger button |
| `Menu` | The dropdown menu panel |
| `Item` | Classes passed to DropdownItem components |

### Keyboard Navigation

- **Enter/Space**: Open dropdown, select item
- **ArrowDown/ArrowUp**: Navigate items
- **Home/End**: Jump to first/last item
- **Escape**: Close dropdown
- **Tab**: Close and move focus
- **Type-ahead**: Type to jump to matching item

### Floating UI Behavior

The dropdown automatically:
- **Flips** to opposite side when near viewport edge
- **Shifts** along axis to stay visible
- Updates position on scroll/resize




#### Navbar Examples

```razor

<!-- Add dropdown menus to navigation items and include a user profile dropdown for additional interactions. -- >
<Navbar>
    <NavbarBrand Href="/">
        <img src="/images/logo.svg" class="mr-3 h-6 sm:h-9" alt="Flowbite Logo" />
        <span class="self-center text-xl font-semibold whitespace-nowrap dark:text-white">Flowbite</span>
    </NavbarBrand>
    <div class="flex md:order-2">
        <Dropdown ArrowIcon="false" Inline="true">
            <Label>
                <Avatar Alt="User settings" ImageUrl="https://flowbite.com/docs/images/people/profile-picture-5.jpg" Rounded />
            </Label>
            <ChildContent>
                <DropdownHeader>
                    <span class="block text-sm">Bonnie Green</span>
                    <span class="block truncate text-sm font-medium">name@flowbite.com</span>
                </DropdownHeader>
                <DropdownItem>Dashboard</DropdownItem>
                <DropdownItem>Settings</DropdownItem>
                <DropdownItem>Earnings</DropdownItem>
                <DropdownDivider />
                <DropdownItem>Sign out</DropdownItem>
            </ChildContent>
        </Dropdown>
        <NavbarToggle />
    </div>
    <NavbarCollapse>
        <NavbarLink Href="/" Active>Home</NavbarLink>
        <NavbarLink HasDropdown>
            <ChildContent>Features</ChildContent>
            <DropdownContent>
                <DropdownItem>Analytics</DropdownItem>
                <DropdownItem>Automation</DropdownItem>
                <DropdownItem>Reports</DropdownItem>
            </DropdownContent>
        </NavbarLink>
        <NavbarLink Href="/pricing">Pricing</NavbarLink>
        <NavbarLink HasDropdown>
            <ChildContent>Resources</ChildContent>
            <DropdownContent>
                <DropdownHeader>Documentation</DropdownHeader>
                <DropdownItem>Getting Started</DropdownItem>
                <DropdownItem>Components</DropdownItem>
                <DropdownDivider />
                <DropdownHeader>Help</DropdownHeader>
                <DropdownItem>Support</DropdownItem>
                <DropdownItem>Contact</DropdownItem>
            </DropdownContent>
        </NavbarLink>
    </NavbarCollapse>
</Navbar>
```




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




#### Spinner Examples

The available Button colors are:
- None specified is the same as SpinnerColor.Info
- SpinnerColor.Info,
- SpinnerColor.Success,
- SpinnerColor.Warning,
- SpinnerColor.Failure,
- SpinnerColor.Pink,
- SpinnerColor.Purple,
- SpinnerColor.Gray

The available Button sizes are:
- SpinnerSize.Xs,
- SpinnerSize.Sm,
- SpinnerSize.Md,
- SpinnerSize.Lg,
- SpinnerSize.Xl


```razor
<!-- Different sizes -->
<Spinner Size="SpinnerSize.Small" />
<Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Success" />
<Spinner Size="SpinnerSize.Large" Color="SpinnerColor.Warning" />

<!-- With text -->
<div class="flex items-center">
    <Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Primary" />
    <span class="ml-2">Loading...</span>
</div>
```




#### Tabs Examples

```razor
<!-- Default tabs -->
<Tabs>
    <TabListContent>
        <Tab Index="0">Profile</Tab>
        <Tab Index="1">Dashboard</Tab>
        <Tab Index="2">Settings</Tab>
        <Tab Index="3" Disabled>Contacts</Tab>
    </TabListContent>
    <TabPanelsContent>
        <TabPanel Index="0">
            <p>Profile content here...</p>
        </TabPanel>
        <TabPanel Index="1">
            <p>Dashboard content here...</p>
        </TabPanel>
        <TabPanel Index="2">
            <p>Settings content here...</p>
        </TabPanel>
        <TabPanel Index="3">
            <p>Disabled tab content...</p>
        </TabPanel>
    </TabPanelsContent>
</Tabs>

<!-- Tabs with underline -->
<Tabs Variant="TabVariant.Underline">
    <TabListContent>
        <Tab Index="0">Profile</Tab>
        <Tab Index="1">Dashboard</Tab>
        <Tab Index="2">Settings</Tab>
    </TabListContent>
    <TabPanelsContent>
        <TabPanel Index="0">
            <p>Profile content here...</p>
        </TabPanel>
        <TabPanel Index="1">
            <p>Dashboard content here...</p>
        </TabPanel>
        <TabPanel Index="2">
            <p>Settings content here...</p>
        </TabPanel>
    </TabPanelsContent>
</Tabs>

<!-- Pills style tabs -->
<Tabs Variant="TabVariant.Pills">
    <TabListContent>
        <Tab Index="0">Profile</Tab>
        <Tab Index="1">Dashboard</Tab>
        <Tab Index="2">Settings</Tab>
    </TabListContent>
    <TabPanelsContent>
        <TabPanel Index="0">
            <p>Profile content here...</p>
        </TabPanel>
        <TabPanel Index="1">
            <p>Dashboard content here...</p>
        </TabPanel>
        <TabPanel Index="2">
            <p>Settings content here...</p>
        </TabPanel>
    </TabPanelsContent>
</Tabs>

<!-- Full width tabs -->
<Tabs Variant="TabVariant.FullWidth">
    <TabListContent>
        <Tab Index="0">Profile</Tab>
        <Tab Index="1">Dashboard</Tab>
        <Tab Index="2">Settings</Tab>
    </TabListContent>
    <TabPanelsContent>
        <TabPanel Index="0">
            <p>Profile content here...</p>
        </TabPanel>
        <TabPanel Index="1">
            <p>Dashboard content here...</p>
        </TabPanel>
        <TabPanel Index="2">
            <p>Settings content here...</p>
        </TabPanel>
    </TabPanelsContent>
</Tabs>

<!-- Tabs with icons -->
<Tabs>
    <TabListContent>
        <Tab Index="0" Icon="@(new UserIcon())">Profile</Tab>
        <Tab Index="1" Icon="@(new ChartIcon())">Dashboard</Tab>
        <Tab Index="2" Icon="@(new GearIcon())">Settings</Tab>
    </TabListContent>
    <TabPanelsContent>
        <TabPanel Index="0">
            <p>Profile content here...</p>
        </TabPanel>
        <TabPanel Index="1">
            <p>Dashboard content here...</p>
        </TabPanel>
        <TabPanel Index="2">
            <p>Settings content here...</p>
        </TabPanel>
    </TabPanelsContent>
</Tabs>
```




#### Tooltip Examples

Tooltip uses Floating UI for smart viewport-aware positioning with automatic flip and shift behavior.

```razor
<!-- Basic tooltip -->
<Tooltip Content="This is a basic tooltip">
    <Button>Hover me</Button>
</Tooltip>

<!-- Different placements -->
<div class="flex items-center gap-4">
    <Tooltip Content="Top tooltip" Placement="TooltipPlacement.Top">
        <Button>Top</Button>
    </Tooltip>
    <Tooltip Content="Bottom tooltip" Placement="TooltipPlacement.Bottom">
        <Button>Bottom</Button>
    </Tooltip>
    <Tooltip Content="Left tooltip" Placement="TooltipPlacement.Left">
        <Button>Left</Button>
    </Tooltip>
    <Tooltip Content="Right tooltip" Placement="TooltipPlacement.Right">
        <Button>Right</Button>
    </Tooltip>
</div>

<!-- Theme variations -->
<div class="flex items-center gap-4">
    <Tooltip Content="Dark theme" Theme="dark">
        <Button>Dark</Button>
    </Tooltip>
    <Tooltip Content="Light theme" Theme="light">
        <Button Color="ButtonColor.Light">Light</Button>
    </Tooltip>
</div>

<!-- Different triggers -->
<div class="flex items-center gap-4">
    <Tooltip Content="Hover to show" Trigger="hover">
        <Button>Hover Trigger</Button>
    </Tooltip>
    <Tooltip Content="Click to show" Trigger="click">
        <Button>Click Trigger</Button>
    </Tooltip>
</div>

<!-- Without arrow -->
<Tooltip Content="No arrow tooltip" Arrow="false">
    <Button>No Arrow</Button>
</Tooltip>

<!-- Animation options -->
<div class="flex flex-wrap gap-4">
    <Tooltip Content="Tooltip content" Animation="@null">
        <Button>Not animated</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-150">
        <Button>Fast animation</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-300">
        <Button>Normal speed</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-500">
        <Button>Slow animation</Button>
    </Tooltip>
</div>
```

### Tooltip Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Content` | `string` | **Required** | Tooltip text |
| `Placement` | `TooltipPlacement` | `Top` | Position relative to trigger |
| `Theme` | `string` | `"dark"` | Visual theme (dark, light) |
| `Trigger` | `string` | `"hover"` | Trigger mode (hover, click) |
| `Arrow` | `bool` | `true` | Show arrow pointer |
| `Animation` | `string?` | `"duration-300"` | CSS transition duration |
| `Class` | `string?` | `null` | Additional CSS classes |

### TooltipPlacement Options

| Value | Description |
|-------|-------------|
| `Auto` | Auto-select best placement |
| `Top` | Above the trigger |
| `Bottom` | Below the trigger |
| `Left` | To the left of trigger |
| `Right` | To the right of trigger |

### Floating UI Behavior

The tooltip automatically:
- **Flips** to opposite side when near viewport edge
- **Shifts** along axis to stay visible
- Updates position on scroll/resize

### Keyboard Support

- **Escape**: Close tooltip (when visible)
- Shows on focus for keyboard accessibility




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



#### Form Component Examples

The available form components are:
- Label - For form field labels with required/disabled states
- TextInput - Text input with sizes, states, addons, and debouncing
- Textarea - Multi-line text input
- Select - Dropdown selection with sizes and states
- Checkbox - Single checkbox with label
- Radio - Radio button groups
- FileInput - File upload input
- ToggleSwitch - Toggle switch with label
- RangeSlider - Range slider with sizes

```razor
<!-- Label Component
     Parameters:
     - For: string - Associates label with form control
     - Value: string - Label text content
     - Required: bool - Shows required indicator
     - Disabled: bool - Grays out the label
     - Class: string - Additional CSS classes -->
<Label For="email" Value="Your email" Required="true" />
<Label For="disabled" Value="Disabled label" Disabled="true" />

<!-- TextInput Component
     Parameters:
     - Id: string - Input identifier
     - Type: string - Input type (text, email, password, etc)
     - Size: TextInputSize - Small, Medium, Large
     - Color: TextInputColor - Gray, Success, Failure, Warning, Info
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the input
     - HelperText: string - Help text below input
     - AddonLeft/Right: string - Text addons
     - Icon/RightIcon: IconBase - Icon components
     - Shadow: bool - Adds shadow effect
     - Behavior: InputBehavior - OnChange (default) or OnInput
     - DebounceDelay: int - Debounce delay in milliseconds -->
<TextInput Id="email" Type="email" Placeholder="name@flowbite.com" Required="true" />
<TextInput Size="TextInputSize.Small" Placeholder="Small input" />
<TextInput Color="TextInputColor.Success" Value="Success input" HelperText="Success message" />
<TextInput AddonLeft="https://" AddonRight=".com" Placeholder="flowbite" />
```

### TextInput Debouncing

For search-as-you-type scenarios, use the `Behavior` and `DebounceDelay` parameters:

```razor
<!-- Standard form input (fires on blur/Enter) -->
<TextInput @bind-Value="Username" Placeholder="Username" />

<!-- Search with debouncing (fires 300ms after typing stops) -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search products..."
    Icon="@(new SearchIcon())" />

@code {
    private string SearchQuery { get; set; } = "";

    // ValueChanged fires 300ms after user stops typing
    // Previous pending calls are automatically cancelled
}

<!-- Instant validation without debounce -->
<TextInput
    @bind-Value="Email"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="0"
    Type="email"
    Color="@GetEmailValidationColor()" />
```

### InputBehavior Options

| Value | Description |
|-------|-------------|
| `OnChange` (default) | Fire on blur or Enter key - standard form behavior |
| `OnInput` | Fire on every keystroke (subject to DebounceDelay) |

```razor
<!-- Textarea Component -->
<Textarea Id="comment"
          Rows="4"
          Placeholder="Write your thoughts here..."
          Required="true" />

<!-- Select Component -->
<Select Id="countries" @bind-Value="selectedCountry">
    <option value="">Choose a country</option>
    <option value="US">United States</option>
    <option value="CA">Canada</option>
</Select>

<!-- Checkbox Component -->
<div class="flex items-center gap-2">
    <Checkbox Id="remember" />
    <Label For="remember">Remember me</Label>
</div>

<!-- Radio Component -->
<div class="flex items-center gap-2">
    <Radio Id="option1" Name="group" Value="true" />
    <Label For="option1">Option 1</Label>
</div>

<!-- FileInput Component -->
<FileInput Id="file"
           HelperText="Upload your profile picture" />

<!-- ToggleSwitch Component -->
<ToggleSwitch @bind-Checked="isEnabled"
              Label="Enable notifications" />

<!-- RangeSlider Component -->
<RangeSlider Id="default-range"
             Size="RangeSliderSize.Medium" />
```

#### Form Validation Examples

The Flowbite Blazor library supports both built-in DataAnnotations validation and custom validation scenarios:

##### Basic Form Validation with DataAnnotations

```razor
<!-- Basic form with DataAnnotations validation -->
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="email" Value="Email address" />
            </div>
            <TextInput TValue="string"
                      Id="email"
                      @bind-Value="model.Email"
                      Type="email" />
            <ValidationMessage For="@(() => model.Email)" />
        </div>
        <div>
            <div class="mb-2 block">
                <Label For="password" Value="Password" />
            </div>
            <TextInput TValue="string"
                      Id="password"
                      @bind-Value="model.Password"
                      Type="password" />
            <ValidationMessage For="@(() => model.Password)" />
        </div>
        <Button Type="submit">Submit</Button>
    </div>
</EditForm>

@code {
    private class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = "";
    }
}
```

##### Form State Management

```razor
<!-- Form state management example -->
<EditForm EditContext="@editContext" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="name" Value="Name" />
            </div>
            <TextInput TValue="string"
                      Id="name"
                      Value="@name"
                      ValueChanged="@OnNameChanged" />
            <ValidationMessage For="@(() => model.Name)" />
        </div>
        <div class="flex gap-2">
            <Button Type="submit"
                    Disabled="@(!editContext?.IsModified() ?? true)">
                Submit
            </Button>
            <Button Color="ButtonColor.Light"
                    OnClick="@ResetForm">
                Reset
            </Button>
        </div>
        <div class="text-sm text-gray-500">
            Form Status: @(editContext?.IsModified() ?? false ? "Modified" : "Unmodified")
        </div>
    </div>
</EditForm>

@code {
    private EditContext? editContext;
    private string? name;

    protected override void OnInitialized()
    {
        editContext = new EditContext(model);
        name = model.Name;
    }

    private void OnNameChanged(string? value)
    {
        name = value;
        model.Name = value ?? "";
        editContext?.NotifyFieldChanged(editContext.Field(nameof(model.Name)));
    }

    private void ResetForm()
    {
        model = new Model();
        editContext = new EditContext(model);
        name = model.Name;
    }
}
```

### TextInput Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Value` | `TValue?` | `null` | Bound value |
| `ValueChanged` | `EventCallback<TValue>` | - | Value change callback |
| `Type` | `string` | `"text"` | HTML input type |
| `Size` | `TextInputSize` | `Medium` | Size variant |
| `Color` | `TextInputColor` | `Gray` | Color/state variant |
| `Placeholder` | `string?` | `null` | Placeholder text |
| `Disabled` | `bool` | `false` | Disabled state |
| `Required` | `bool` | `false` | Required field |
| `Shadow` | `bool` | `false` | Shadow effect |
| `HelperText` | `string?` | `null` | Help text below input |
| `Icon` | `IconBase?` | `null` | Left icon |
| `RightIcon` | `IconBase?` | `null` | Right icon |
| `AddonLeft` | `string?` | `null` | Left text addon |
| `AddonRight` | `string?` | `null` | Right text addon |
| `Behavior` | `InputBehavior` | `OnChange` | When to fire ValueChanged |
| `DebounceDelay` | `int` | `0` | Debounce delay (ms) |
| `Pattern` | `string?` | `null` | Regex validation pattern |
| `InputMode` | `string?` | `null` | Mobile keyboard hint |
| `Class` | `string?` | `null` | Additional CSS classes |

The form validation system integrates seamlessly with Blazor's built-in form handling while providing additional features for debouncing, custom validation scenarios, and state management. All form components support validation states through their Color properties and automatically integrate with ValidationMessage components.




#### QuickGrid Examples

Leverage the Microsoft's QuickGrid Blazor componet and achieve Flowbite styling by applying the flowbite-grid CSS class and the Theme="flowbite".

```razor
@* the parent div must have both the grid and flowbite-grid classes *@
<div class="grid flowbite-grid">
    @* Set the Theme property to flowbite *@
    <QuickGrid Theme="flowbite">
    ...
    </QuickGrid>
</div>
```

Example:

```razor
<!-- Basic QuickGrid-->
<div class="grid flowbite-grid text-xs">
    <QuickGrid Items="@_pokemon" Theme="flowbite">
        <PropertyColumn Property="@(p => p.Id)" Title="#" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Name)" Title="Name" />
        <PropertyColumn Property="@(p => p.Type1)" Title="Type" />
        <PropertyColumn Property="@(p => p.HP)" Title="HP" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Attack)" Title="Attack" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Defense)" Title="Defense" Align="Align.Center" />
    </QuickGrid>
</div>


```




#### Modal Dialog Examples


__Default Modal:__

```razor
<div class="space-y-4">
    <div class="flex items-center gap-4">
        <Button OnClick="@(() => showDefaultModal = true)">Open Modal</Button>

        @if (termsAccepted != null)
        {
            <div class="@GetChoiceAlertClass()" role="alert">
                <span class="font-medium">@(termsAccepted.Value ? "Accepted" : "Declined")</span> the Terms of Service
            </div>
        }
    </div>

    <Modal Show="showDefaultModal" ShowChanged="(value) => showDefaultModal = value">
        <ModalHeader>
            <h3>Terms of Service</h3>
        </ModalHeader>
        <ModalBody>
            <div class="space-y-6">
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    With less than a month to go before the European Union enacts new consumer privacy laws...
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => HandleTermsChoice(false))" Color="ButtonColor.Gray" Class="mr-2">Decline</Button>
                <Button OnClick="@(() => HandleTermsChoice(true))">Accept</Button>
            </div>
        </ModalFooter>
    </Modal>
</div>

@code {
    private bool showDefaultModal = false;
    private bool? termsAccepted = null;

    private void HandleTermsChoice(bool accepted)
    {
        termsAccepted = accepted;
        showDefaultModal = false;
    }
}
```

__Modal with Slots:__

```razor
<Modal Show="@show" Slots="@(new ModalSlots {
    Backdrop = "bg-gray-900/70",
    Content = "rounded-xl shadow-2xl",
    Header = "border-b-2 border-blue-500",
    Body = "p-8",
    Footer = "bg-gray-50 dark:bg-gray-800"
})">
    <ModalHeader>Custom Styled Header</ModalHeader>
    <ModalBody>
        Content with extra padding from slot customization.
    </ModalBody>
    <ModalFooter>
        <Button OnClick="@(() => show = false)">Close</Button>
    </ModalFooter>
</Modal>
```

__Modal Options:__

```csharp
public enum ModalSize
{
    Small,
    Medium,
    Large,
    ExtraLarge,
    TwoExtraLarge,
    ThreeExtraLarge,
    FourExtraLarge,
    FiveExtraLarge,
    SixExtraLarge,
    SevenExtraLarge,
    Default = TwoExtraLarge
}

public enum ModalPosition
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

__Sized and Positioned Modal:__

```razor
<Modal Show="showModal"
       ShowChanged="(value) => showModal = value"
       Size="ModalSize.FourExtraLarge"
       Position="ModalPosition.TopCenter">
    <ModalHeader>
        <h3>Large Top-Center Modal</h3>
    </ModalHeader>
    <ModalBody>
        <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
            Content here...
        </p>
    </ModalBody>
    <ModalFooter>
        <div class="flex justify-end w-full">
            <Button OnClick="@(() => showModal = false)">Close</Button>
        </div>
    </ModalFooter>
</Modal>
```

### Modal Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Show` | `bool` | `false` | Visibility state |
| `ShowChanged` | `EventCallback<bool>` | - | State change callback |
| `OnClose` | `EventCallback` | - | Close callback |
| `Title` | `string?` | `null` | Modal title |
| `Size` | `ModalSize` | `Default` | Size variant |
| `Position` | `ModalPosition` | `Center` | Screen position |
| `Dismissible` | `bool` | `true` | Close on Escape/backdrop |
| `Slots` | `ModalSlots?` | `null` | Per-element classes |
| `BackdropClass` | `string?` | `null` | Backdrop CSS classes |
| `ModalClass` | `string?` | `null` | Modal container CSS |

### ModalSlots Properties

| Slot | Description |
|------|-------------|
| `Base` | The modal root (same as ModalClass) |
| `Backdrop` | The background overlay |
| `Content` | The modal dialog container |
| `Header` | Passed to ModalHeader components |
| `Body` | Passed to ModalBody components |
| `Footer` | Passed to ModalFooter components |

### Keyboard Support

- **Escape**: Close modal (when Dismissible="true")
- Focus trap: Tab cycles within modal




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




#### Toast Examples

__Default Toast:__

```razor
<Button OnClick="@(() => ToastService.Show("Hi there 👋!"))">Show Default Toast</Button>
```

__Toast Types:__

The `ToastService` provides helper methods to show different types of toasts.

```csharp
public enum ToastType
{
    Default,
    Info,
    Success,
    Warning,
    Error
}
```

```razor
<div class="flex flex-wrap gap-2">
    <Button OnClick="@(() => ToastService.ShowSuccess("Item successfully created."))">Show Success Toast</Button>
    <Button OnClick="@(() => ToastService.ShowError("Something went wrong!"))">Show Error Toast</Button>
    <Button OnClick="@(() => ToastService.ShowWarning("Warning: Low disk space."))">Show Warning Toast</Button>
    <Button OnClick="@(() => ToastService.ShowInfo("New version available."))">Show Info Toast</Button>
</div>
```

__Toast Positioning:__

Positioning is controlled by the `Position` parameter on the `<ToastHost />` component.

```csharp
public enum ToastPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}
```

```razor
<!-- This dedicated host is for the positioning demo only. -->
<ToastHost Position="@currentPosition" HostId="position-demo" />

<div class="flex flex-wrap gap-2">
    <Button OnClick="@(() => SetPosition(ToastPosition.TopLeft))" Color="Button.ButtonColor.Primary">Top Left</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.TopCenter))" Color="Button.ButtonColor.Primary">Top Center</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.TopRight))" Color="Button.ButtonColor.Primary">Top Right</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomLeft))" Color="Button.ButtonColor.Primary">Bottom Left</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomCenter))" Color="Button.ButtonColor.Primary">Bottom Center</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomRight))" Color="Button.ButtonColor.Primary">Bottom Right</Button>
</div>

@code {
    private ToastPosition currentPosition = ToastPosition.TopRight;

    private void SetPosition(ToastPosition newPosition)
    {
        currentPosition = newPosition;
        ToastService.Show($"Position set to {newPosition}", hostId: "position-demo");
    }
}



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




#### Heading Examples

The Heading component renders semantic HTML headings (h1-h6) with consistent styling.

**Available heading tags:**
- HeadingTag.H1 (default) - text-5xl, font-extrabold
- HeadingTag.H2 - text-4xl, font-bold
- HeadingTag.H3 - text-3xl, font-bold
- HeadingTag.H4 - text-2xl, font-bold
- HeadingTag.H5 - text-xl, font-bold
- HeadingTag.H6 - text-lg, font-bold

**Optional parameters:**
- `Size` (TextSize?) - Override default tag-based sizing
- `Weight` (FontWeight?) - Override default weight
- `Gradient` (GradientColor) - Apply gradient text effect
- `CustomColor` (string?) - Custom Tailwind color classes

**Default colors:**
- Light mode: text-gray-900
- Dark mode: text-white

```razor
<!-- Basic headings -->
<Heading Tag="HeadingTag.H1">Page Title</Heading>
<Heading Tag="HeadingTag.H2">Section Heading</Heading>
<Heading Tag="HeadingTag.H3">Subsection Heading</Heading>

<!-- Custom size (semantic HTML with visual control) -->
<Heading Tag="HeadingTag.H2" Size="TextSize.XXXXXXXXXL">
    Extra Large H2
</Heading>

<!-- Custom weight -->
<Heading Tag="HeadingTag.H2" Weight="FontWeight.Light">
    Light Heading
</Heading>

<!-- Gradient heading -->
<Heading Tag="HeadingTag.H1" Gradient="GradientColor.PurpleToBlue">
    Gradient Title
</Heading>

<!-- Custom color -->
<Heading Tag="HeadingTag.H2" CustomColor="text-blue-600 dark:text-blue-400">
    Blue Heading
</Heading>

<!-- Combined styling -->
<Heading Tag="HeadingTag.H1"
         Size="TextSize.XXXXXXL"
         Weight="FontWeight.Black"
         Gradient="GradientColor.PurpleToBlue">
    Large Gradient Title
</Heading>
```




#### Paragraph Examples

The Paragraph component renders paragraph text with flexible styling options.

**Parameters:**
- `Size` (TextSize) - Text size (default: Base)
- `Weight` (FontWeight?) - Font weight
- `Leading` (LineHeight?) - Line height spacing
- `Align` (TextAlign?) - Text alignment
- `Tracking` (LetterSpacing?) - Letter spacing
- `Space` (Whitespace?) - Whitespace handling
- `Gradient` (GradientColor) - Gradient text effect
- `CustomColor` (string?) - Custom color classes
- `FirstLetterUpper` (bool) - Drop cap styling
- `Opacity` (int?) - Opacity level (0-100)
- `Italic` (bool) - Italic styling
- `Underline` (bool) - Underline decoration

**Default colors:**
- Light mode: text-gray-700
- Dark mode: text-gray-400

```razor
<!-- Basic paragraph -->
<Paragraph>
    This is a paragraph with default styling.
</Paragraph>

<!-- Custom size and weight -->
<Paragraph Size="TextSize.LG" Weight="FontWeight.Medium">
    Larger, medium weight paragraph text.
</Paragraph>

<!-- Text alignment -->
<Paragraph Align="TextAlign.Center">
    Center-aligned paragraph text.
</Paragraph>

<!-- Line height (leading) -->
<Paragraph Leading="LineHeight.Relaxed">
    Paragraph with relaxed line spacing for improved readability.
</Paragraph>

<!-- Italic and underline -->
<Paragraph Italic="true">
    Italic paragraph text for emphasis.
</Paragraph>

<!-- First letter uppercase (drop cap) -->
<Paragraph FirstLetterUpper="true" Size="TextSize.LG" Leading="LineHeight.Relaxed">
    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
</Paragraph>

<!-- Gradient paragraph -->
<Paragraph Gradient="GradientColor.PurpleToBlue"
           Size="TextSize.XL"
           Weight="FontWeight.Bold">
    Gradient paragraph text
</Paragraph>

<!-- Custom color -->
<Paragraph CustomColor="text-blue-600 dark:text-blue-400">
    Custom colored paragraph text.
</Paragraph>

<!-- Letter spacing -->
<Paragraph Tracking="LetterSpacing.Wide">
    Paragraph with wider letter spacing.
</Paragraph>
```




#### Span Examples

The Span component provides inline text styling within larger text blocks.

**Parameters:**
- `Size` (TextSize) - Text size (default: Base)
- `Weight` (FontWeight?) - Font weight
- `Gradient` (GradientColor) - Gradient text effect
- `CustomColor` (string?) - Custom color classes
- `Italic` (bool) - Italic styling
- `Underline` (bool) - Underline decoration
- `LineThrough` (bool) - Strikethrough decoration
- `Uppercase` (bool) - Transform to uppercase
- `Lowercase` (bool) - Transform to lowercase
- `Capitalize` (bool) - Capitalize each word

**Default behavior:**
- No default color (inherits from parent)
- Use `CustomColor` to apply specific colors

```razor
<!-- Inline emphasis -->
<p class="text-base text-gray-700 dark:text-gray-400">
    This is regular text with <Span Weight="FontWeight.Bold">bold text</Span> inline.
</p>

<!-- Size variations -->
<p class="text-base text-gray-700 dark:text-gray-400">
    Regular text with <Span Size="TextSize.SM">smaller</Span> and
    <Span Size="TextSize.XL">larger</Span> inline text.
</p>

<!-- Text decorations -->
<p class="text-base text-gray-700 dark:text-gray-400">
    Text with <Span Italic="true">italic</Span>,
    <Span Underline="true">underline</Span>, and
    <Span LineThrough="true">strikethrough</Span>.
</p>

<!-- Text transform -->
<p class="text-base text-gray-700 dark:text-gray-400">
    Transform to <Span Uppercase="true">uppercase</Span>,
    <Span Lowercase="true">LOWERCASE</Span>, or
    <Span Capitalize="true">capitalize words</Span>.
</p>

<!-- Custom colors -->
<p class="text-base text-gray-700 dark:text-gray-400">
    Inline colors:
    <Span CustomColor="text-blue-600">blue</Span>,
    <Span CustomColor="text-green-600">green</Span>,
    <Span CustomColor="text-red-600">red</Span>.
</p>

<!-- Gradient text -->
<p class="text-xl text-gray-700 dark:text-gray-400">
    Create <Span Gradient="GradientColor.PurpleToBlue"
                  Size="TextSize.XXL"
                  Weight="FontWeight.Bold">gradient highlights</Span> inline.
</p>

<!-- Combined styling for pricing -->
<p class="text-lg text-gray-700 dark:text-gray-400">
    Was <Span CustomColor="text-red-600" Weight="FontWeight.Bold" LineThrough="true">$99.99</Span>
    now <Span CustomColor="text-green-600" Size="TextSize.XL" Weight="FontWeight.Bold">$79.99</Span>!
</p>
```



#### Carousel Examples

The Carousel component allows you to create image slideshows or content carousels with navigation controls and indicators.

**Main Components:**
- `Carousel` - Main container component
- `CarouselItem` - Individual slide wrapper
- `CarouselControls` - Previous/Next navigation buttons
- `CarouselIndicators` - Dot indicators for direct slide navigation

**Available CarouselImageFit options:**
- CarouselImageFit.Cover (default)
- CarouselImageFit.Contain
- CarouselImageFit.Fill
- CarouselImageFit.ScaleDown
- CarouselImageFit.None

**Available CarouselIndicatorPosition options:**
- CarouselIndicatorPosition.Bottom (default)
- CarouselIndicatorPosition.Top

```razor
<!-- Basic carousel with images -->
<Carousel>
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselItem ImageSrc="/images/slide3.jpg" ImageAlt="Slide 3" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

<!-- Auto-advancing carousel (3 second interval) -->
<Carousel AutoAdvanceInterval="3000">
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

<!-- Custom content slides -->
<Carousel>
    <CarouselItem>
        <div class="flex items-center justify-center h-full bg-blue-500 text-white">
            <h3>Custom Content</h3>
        </div>
    </CarouselItem>
    <CarouselItem>
        <div class="flex items-center justify-center h-full bg-green-500 text-white">
            <h3>Another Slide</h3>
        </div>
    </CarouselItem>
    <CarouselControls />
    <CarouselIndicators Position="CarouselIndicatorPosition.Top" />
</Carousel>

<!-- Controlled carousel with two-way binding -->
<Carousel @bind-Index="currentSlide">
    <CarouselItem ImageSrc="/images/slide1.jpg" ImageAlt="Slide 1" />
    <CarouselItem ImageSrc="/images/slide2.jpg" ImageAlt="Slide 2" />
    <CarouselItem ImageSrc="/images/slide3.jpg" ImageAlt="Slide 3" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>

@code {
    private int currentSlide = 0;
}

<!-- Image fit options -->
<Carousel>
    <CarouselItem 
        ImageSrc="/images/slide1.jpg" 
        ImageAlt="Cover fit" 
        ImageFit="CarouselImageFit.Cover" />
    <CarouselItem 
        ImageSrc="/images/slide2.jpg" 
        ImageAlt="Contain fit" 
        ImageFit="CarouselImageFit.Contain" />
    <CarouselControls />
    <CarouselIndicators />
</Carousel>
```

**Key Parameters:**
- `Index` / `@bind-Index` - Current slide index (zero-based)
- `AutoAdvanceInterval` - Auto-advance interval in milliseconds (null = disabled)
- `TransitionDuration` - Transition duration in milliseconds (default: 1000)
- `OnSlideChanged` - Event callback when slide changes
- `ImageSrc` - Image URL for CarouselItem
- `ImageAlt` - Alt text for image
- `ImageFit` - How image fits in container
- `Position` - Indicator position (Top/Bottom)




</doc>

<doc title="Using Icons" description="Working with Flowbite Blazor icons">

## Icon Packages

Flowbite Blazor provides two icon packages:

1. Core Icons - Built into the main package
2. Extended Icons - Additional icons in a separate package

### Basic Usage

```razor
<!-- Using core icons -->
<Button>
    <HomeIcon class="w-5 h-5 mr-2" />
    Home
</Button>

<!-- Using icons in components -->
<Alert Icon="@(new InfoIcon())" Color="AlertColor.Info">
    Important information
</Alert>

```

### List of Icon Components

### Flowbite Icon Blazor Components

```razor
<AppleIcon />
<ArrowDownIcon />
<ArrowLeftIcon />
<ArrowRightIcon />
<ArrowRightToBracketIcon />
<ArrowUpIcon />
<ArrowUpRightFromSquareIcon />
<AwsIcon />
<BarsIcon />
<BellIcon />
<BlueskyIcon />
<CalendarMonthIcon />
<ChartIcon />
<CheckCircleIcon />
<CheckIcon />
<ChevronDownIcon />
<ChevronLeftIcon />
<ChevronRightIcon />
<ChevronUpIcon />
<ClipboardArrowIcon />
<ClockIcon />
<CloseCircleIcon />
<CloseCircleSolidIcon />
<CloseIcon />
<CodeBranchIcon />
<CompressIcon />
<DatabaseIcon />
<DiscordIcon />
<DotsHorizontalIcon />
<DotsVerticalIcon />
<DownloadIcon />
<EditIcon />
<EnvelopeIcon />
<ExclamationSolidIcon />
<ExclamationTriangleIcon />
<ExpandIcon />
<EyeIcon />
<EyeSlashIcon />
<FacebookIcon />
<FileCopyIcon />
<FileExportIcon />
<FileIcon />
<FileImportIcon />
<FilterIcon />
<FloppyDiskAltIcon />
<FloppyDiskIcon />
<FolderIcon />
<ForwardIcon />
<GearIcon />
<GithubIcon />
<GitlabIcon />
<GoogleIcon />
<GridIcon />
<HamburgerIcon />
<HeartIcon />
<HomeIcon />
<ImageIcon />
<InfoCircleIcon />
<InfoIcon />
<InstagramIcon />
<LinkedinIcon />
<ListIcon />
<LockIcon />
<LockOpenIcon />
<MapPinIcon />
<MessagesIcon />
<PaperClipIcon />
<PencilIcon />
<PhoneIcon />
<PlayIcon />
<PlusIcon />
<PrinterIcon />
<QuestionCircleIcon />
<RedditIcon />
<RefreshIcon />
<RocketIcon />
<SearchIcon />
<ShareNodesIcon />
<SortIcon />
<StarIcon />
<StopIcon />
<TableRowIcon />
<TrashBinIcon />
<TwitterIcon />
<UndoIcon />
<UploadIcon />
<UserCircleIcon />
<UserIcon />
<UserSolidIcon />
<WhatsappIcon />
<WindowsIcon />
<XIcon />
<YoutubeIcon />
```


</doc>



<doc title="Common Patterns" description="Common patterns and best practices for Flowbite Blazor">

## TailwindMerge Integration

Flowbite Blazor uses TailwindMerge.NET to intelligently resolve conflicting Tailwind CSS classes. User-provided classes override component defaults.

### How It Works

Components use `MergeClasses()` in their code-behind to combine base classes with user overrides:

```csharp
// Component internal pattern
private string GetButtonClasses() =>
    MergeClasses(
        ElementClass.Empty()
            .Add("px-4 py-2 rounded-lg")  // Component defaults
            .Add("bg-blue-500", when: Color == ButtonColor.Primary)
            .Add(Class)  // User's Class parameter wins
    );
```

### Usage in Your Code

When you add a `Class` parameter, TailwindMerge resolves conflicts:

```razor
<!-- Component default: p-4 -->
<!-- Your override: p-8 -->
<!-- Result: p-8 (user wins, no duplicate padding) -->
<Card Class="p-8">
    Content with larger padding
</Card>

<!-- Spacing conflict resolved -->
<Button Class="px-6 py-3">
    Custom padded button
</Button>
```

### ElementClass Fluent Builder

For custom components, use `ElementClass` for readable conditional classes:

```csharp
// In your component's code-behind
private string GetClasses() => MergeClasses(
    ElementClass.Empty()
        .Add("base-class")
        .Add("conditional-class", when: SomeCondition)
        .Add("another-class", when: !SomeCondition)
        .Add(Class)
);
```

---

## Slot System

Slots provide fine-grained control over component styling without modifying component source code.

### Available Slot Types

| Component | Slot Class | Available Slots |
|-----------|-----------|-----------------|
| Card | `CardSlots` | Base, Image, Body |
| Dropdown | `DropdownSlots` | Base, Trigger, Menu, Item |
| Modal | `ModalSlots` | Base, Backdrop, Content, Header, Body, Footer |
| AccordionItem | `AccordionItemSlots` | Trigger, Content |

### Usage Pattern

```razor
<!-- Card with custom slots -->
<Card Slots="@(new CardSlots {
    Base = "shadow-xl rounded-xl",
    Image = "object-top",
    Body = "p-8"
})">
    <h5>Card Title</h5>
    <p>Card content with extra padding</p>
</Card>

<!-- Dropdown with custom trigger and menu -->
<Dropdown Slots="@(new DropdownSlots {
    Trigger = "bg-blue-600 hover:bg-blue-700",
    Menu = "w-64 shadow-xl",
    Item = "hover:bg-blue-50"
})">
    <Label>Actions</Label>
    <ChildContent>
        <DropdownItem>Edit</DropdownItem>
        <DropdownItem>Delete</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Modal with custom styling -->
<Modal Show="@show" Slots="@(new ModalSlots {
    Backdrop = "bg-gray-900/70",
    Content = "rounded-xl shadow-2xl",
    Header = "border-b-2 border-blue-500",
    Body = "p-8"
})">
    <ModalHeader>Custom Header</ModalHeader>
    <ModalBody>Content here</ModalBody>
    <ModalFooter>Footer content</ModalFooter>
</Modal>
```

---

## Debounced Input Pattern

For search-as-you-type scenarios, use the `Behavior` and `DebounceDelay` parameters to reduce API calls.

### Basic Usage

```razor
<!-- Standard input (fires on blur/Enter) -->
<TextInput @bind-Value="username" />

<!-- Search input with debouncing (fires 300ms after typing stops) -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search..." />

@code {
    private string SearchQuery { get; set; } = "";

    // Called 300ms after user stops typing
    // Previous pending calls are cancelled automatically
}
```

### InputBehavior Options

| Value | Description |
|-------|-------------|
| `OnChange` (default) | Fire on blur or Enter key - standard form behavior |
| `OnInput` | Fire on every keystroke (subject to DebounceDelay) |

### Best Practices

```razor
<!-- Search with debouncing -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search products..." />

<!-- Instant validation (no debounce needed) -->
<TextInput
    @bind-Value="Email"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="0"
    Color="@GetValidationColor()" />

<!-- Form field (default - fires on blur) -->
<TextInput @bind-Value="Username" />
```

---

## Lazy Service Injection

Flowbite services are lazy-loaded for better performance. Inject them as needed:

```csharp
@inject ClipboardService Clipboard
@inject IFloatingService Floating

@code {
    private async Task CopyToClipboard(string text)
    {
        // Module loads on first use
        var success = await Clipboard.CopyToClipboardAsync(text);
    }
}
```

---

## Event Handling

Components use standard Blazor event handling:

```razor
<Button OnClick="@HandleClick">
    Click me
</Button>

@code {
    private void HandleClick()
    {
        // Handle the click event
    }
}

<!-- With parameters -->
<Dropdown>
    <Label>Actions</Label>
    <ChildContent>
        <DropdownItem OnClick="@(() => HandleItemClick(item.Id))">
            @item.Name
        </DropdownItem>
    </ChildContent>
</Dropdown>
```

---

## Dark Mode

Enable dark mode by adding the 'dark' class to any parent element:

```razor
<!-- Dark mode container -->
<div class="dark">
    <!-- Components will use dark mode styles -->
    <Alert Color="AlertColor.Info">
        This alert uses dark mode styles
    </Alert>

    <Card>
        <h5 class="text-gray-900 dark:text-white">
            Dark mode card
        </h5>
        <p class="text-gray-700 dark:text-gray-400">
            Content adapts automatically
        </p>
    </Card>
</div>
```

---

## Floating UI Positioning

Dropdown, Tooltip, and Popover components use Floating UI for smart positioning:

```razor
<!-- Dropdown auto-flips when near viewport edge -->
<Dropdown Placement="DropdownPlacement.Bottom">
    <Label>Menu</Label>
    <ChildContent>
        <DropdownItem>Option 1</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Disable auto-positioning for fixed placement -->
<Dropdown
    Placement="DropdownPlacement.Top"
    DisableFlip="true"
    DisableShift="true">
    ...
</Dropdown>

<!-- Tooltip with offset -->
<Tooltip Content="Help text" Placement="TooltipPlacement.Right">
    <Button>Hover me</Button>
</Tooltip>
```

### Floating UI Features

| Feature | Description |
|---------|-------------|
| Flip | Auto-flips placement when near viewport edge |
| Shift | Shifts along axis to stay in viewport |
| Offset | Configurable distance from trigger |
| Auto-update | Recalculates position on scroll/resize |

---

## Keyboard Navigation

Interactive components support full keyboard navigation:

### Dropdown
- `Enter`/`Space`: Open dropdown, select item
- `ArrowDown`/`ArrowUp`: Navigate items
- `Home`/`End`: Jump to first/last item
- `Escape`: Close dropdown
- `Tab`: Close and move focus
- Type-ahead: Type to jump to matching item

### Tooltip
- `Escape`: Close tooltip

### Modal
- `Escape`: Close modal (when Dismissible="true")
- Focus trap: Tab cycles within modal

---

## motion-reduce Support

Components respect the user's reduced motion preference:

```css
/* Applied automatically via Tailwind */
.motion-reduce:transition-none { }
```

Animations are disabled for users with `prefers-reduced-motion: reduce` in their OS settings.

</doc>



<doc title="Quick Start" description="Zero to Hero guide for setup, configuration, and running Flowbite Blazor with Tailwind v4">

# Scaffold a Flowbite Blazor WebAssembly Standalone App

## Project Structure

PROJECT_DIR_ROOT
|---PROJECT_NAME/
|   |---Layout/
|   |---Pages/
|   |   |---Home.razor # @page "/" route
|   |---Properties/
|   |---tools/
|   |   |---tailwindcss.exe
|   |---wwwroot/
|   ...
|   ...
|   |---PROJECT_NAME.csproj
|   |---tailwind.config.js
|---README.md


# Overview

This is an overview with more details in the below sections.

1. Create a new project using dotnet new and add packages
2. Download the Tailwind CSS v4 CLI to the tools folder
3. Configure the csproj file for Flowbite and Tailwind CSS
4. Configure Program.cs with Flowbite services
5. Configure wwwroot/index.html with scripts and styles
6. Configure wwwroot/css/app.css with Tailwind v4 directives
7. Configure _Imports.razor with Flowbite namespaces
8. Configure tailwind.config.js (minimal v4 config)
9. Determine what to do with Pages/Home.razor

## 1. Create a new project

```sh
# pwd is the {{PROJECT_DIR_ROOT}}
dotnet new blazorwasm --empty -o {{PROJECT_NAME}}
cd {{PROJECT_NAME}}
dotnet add package Flowbite --prerelease
dotnet add package BlazorWasmPreRendering.Build -v 5.0.0
cd ..
# pwd is the {{PROJECT_DIR_ROOT}}
```

## 2. Download the Tailwind CSS v4 CLI

__For Windows Platform:__
```sh
# pwd is the {{PROJECT_DIR_ROOT}}
cd {{PROJECT_NAME}}; mkdir tools; cd tools
Invoke-WebRequest -Uri https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-windows-x64.exe -OutFile tailwindcss.exe -UseBasicParsing
cd ../..
# pwd is the {{PROJECT_DIR_ROOT}}
```

__For MacOS:__
```sh
# pwd is the {{PROJECT_DIR_ROOT}}
cd {{PROJECT_NAME}} && mkdir tools && cd tools
curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-macos-arm64
chmod +x tailwindcss-macos-arm64
mv tailwindcss-macos-arm64 tailwindcss
cd ../..
# pwd is the {{PROJECT_DIR_ROOT}}
```

## 3. Configure the csproj file

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <TargetFramework>{{leave as what the user has chosen, net8.0 or net9.0}}</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
    <BlazorEnableTimeZoneSupport>false</BlazorEnableTimeZoneSupport>
    <Version>0.0.1-alpha.1</Version>
  </PropertyGroup>

  <PropertyGroup>
    <!-- Required for BlazorWasmPrerendering.Build package -->
    <BlazorWasmPrerenderingDeleteLoadingContents>true</BlazorWasmPrerenderingDeleteLoadingContents>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="8.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="8.0.0" PrivateAssets="all" />
    <PackageReference Include="Flowbite" Version="0.2.*-*" />
    <PackageReference Include="BlazorWasmPreRendering.Build" Version="5.0.0" />
  </ItemGroup>

  <!-- Tailwind CSS v4 Build Target -->
  <Target Name="Tailwind" BeforeTargets="Build" Condition="'$(OS)' == 'Windows_NT'">
    <Exec Command=".\tools\tailwindcss.exe -i ./wwwroot/css/app.css -o ./wwwroot/css/app.min.css" />
  </Target>

  <Target Name="DisableTailwindOnPublish" BeforeTargets="Publish">
    <PropertyGroup>
      <DisableTailwind>true</DisableTailwind>
    </PropertyGroup>
  </Target>

  <ItemGroup>
    <UpToDateCheckBuilt Include="wwwroot/css/app.css" Set="Css" />
    <UpToDateCheckBuilt Include="wwwroot/css/app.min.css" Set="Css" />
    <UpToDateCheckBuilt Include="tailwind.config.js" Set="Css" />
  </ItemGroup>

  <ItemGroup>
    <None Remove="wwwroot\css\app.css" />
    <None Remove="wwwroot\css\app.min.css" />
    <None Remove="tools\tailwindcss.exe" />
  </ItemGroup>

</Project>
```

## 4. Configure Program.cs

```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PROJECT_NAME;
using Flowbite.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Required for prerendering (BlazorWasmPreRendering.Build)
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();

// Extract service-registration to static local function for prerendering
static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

    // Register Flowbite services (TailwindMerge, FloatingService, etc.)
    services.AddFlowbite();
}
```

## 5. Configure wwwroot/index.html

```html
<!DOCTYPE html>
<html lang="en" class="dark">

    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>PROJECT_NAME</title>
        <base href="/" />
        <link rel="stylesheet" href="css/app.min.css" />
        <link rel="stylesheet" href="_content/Flowbite/flowbite.min.css" />
        <link rel="icon" type="image/png" sizes="32x32" href="favicon.png">

        <script>
            // Dark mode initialization
            if (localStorage.getItem('color-theme') === 'dark' ||
                (!('color-theme' in localStorage) &&
                 window.matchMedia('(prefers-color-scheme: dark)').matches)) {
                document.documentElement.classList.add('dark');
            } else {
                document.documentElement.classList.remove('dark')
            }
        </script>

    </head>

    <body class="dark:bg-gray-900 antialiased">

        <div id="app">
            <svg class="loading-progress">
                <circle r="40%" cx="50%" cy="50%" />
                <circle r="40%" cx="50%" cy="50%" />
            </svg>
            <div class="loading-progress-text">Loading...</div>
        </div>

        <div id="blazor-error-ui">
            An unhandled error has occurred.
            <a href="." class="reload">Reload</a>
            <span class="dismiss">🗙</span>
        </div>

        <!-- Blazor WebAssembly -->
        <script src="_framework/blazor.webassembly.js"></script>

        <!-- Floating UI (required for Dropdown, Tooltip positioning) -->
        <script src="https://cdn.jsdelivr.net/npm/@floating-ui/core@1.6.9"></script>
        <script src="https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.6.13"></script>

        <!-- Flowbite Blazor JS -->
        <script src="_content/Flowbite/flowbite.js"></script>

        <!-- Optional: Your app-specific JS -->
        <script src="/js/app.js"></script>
    </body>

</html>
```

## 6. Configure wwwroot/css/app.css (Tailwind v4)

**IMPORTANT:** Tailwind v4 uses CSS-first configuration with `@import` and `@theme` directives.

```css
/* Tailwind v4 CSS-first configuration */
@import "tailwindcss";

/* Configure content sources for class scanning */
@source "../**/*.razor";
@source "../**/*.html";
@source "../**/*.cshtml";

/* Primary color customization via @theme */
@theme {
    --color-primary-50: #eff6ff;
    --color-primary-100: #dbeafe;
    --color-primary-200: #bfdbfe;
    --color-primary-300: #93c5fd;
    --color-primary-400: #60a5fa;
    --color-primary-500: #3b82f6;
    --color-primary-600: #2563eb;
    --color-primary-700: #1d4ed8;
    --color-primary-800: #1e40af;
    --color-primary-900: #1e3a8a;
    --color-primary-950: #172554;
}

/* Microsoft Blazor validation styles */
.validation-message {
    @apply text-red-600 dark:text-red-500;
}

.blazor-error-boundary {
    background: url(data:image/svg+xml;base64,...) no-repeat 1rem/1.8rem, #b32121;
    padding: 1rem 1rem 1rem 3.7rem;
    color: white;
}

.blazor-error-boundary::after {
    content: "An error has occurred."
}

#blazor-error-ui {
    color-scheme: light only;
    background: lightyellow;
    bottom: 0;
    box-shadow: 0 -1px 2px rgba(0, 0, 0, 0.2);
    box-sizing: border-box;
    display: none;
    left: 0;
    padding: 0.6rem 1.25rem 0.7rem 1.25rem;
    position: fixed;
    width: 100%;
    z-index: 1000;
}

#blazor-error-ui .dismiss {
    cursor: pointer;
    position: absolute;
    right: 0.75rem;
    top: 0.5rem;
}
```

## 7. Configure _Imports.razor

```razor
@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Sections
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.AspNetCore.Components.WebAssembly.Http
@using Microsoft.JSInterop

@* Flowbite namespaces *@
@using Flowbite.Base
@using Flowbite.Components
@using Flowbite.Components.Tabs
@using Flowbite.Components.Table
@using Flowbite.Icons
@using Flowbite.Services
@using Flowbite.Common

@* Static imports for component enums *@
@using static Flowbite.Components.Button
@using static Flowbite.Components.Tooltip
@using static Flowbite.Components.Avatar
@using static Flowbite.Components.Sidebar
@using static Flowbite.Components.SidebarCTA
@using static Flowbite.Components.Dropdown

@* Project namespaces *@
@using PROJECT_NAME
@using PROJECT_NAME.Layout

@* Uncomment if project has custom components *@
@* @using PROJECT_NAME.Components *@
```

## 8. Configure tailwind.config.js (Tailwind v4)

**Note:** Tailwind v4 uses CSS-first configuration. The JS config is minimal:

```js
/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'class'
}
```

Most configuration is now done in your CSS file using `@theme`, `@source`, and other directives.

## 9. Determine where to place the `/` route

You MUST decide where to place the `/` route. The `dotnet new` generates a `Pages/Home.razor` file that contains the `/` route. You MUST decide whether to keep and replace the contents of this file or DELETE the Home.razor file and create a new file for the `/` route.

## Key Differences from Tailwind v3

| Aspect | Tailwind v3 | Tailwind v4 |
|--------|-------------|-------------|
| Config | `tailwind.config.js` (JavaScript) | CSS `@theme` directive |
| Content | `content: [...]` in JS | `@source` directive in CSS |
| Plugins | `require('flowbite/plugin')` | `@plugin "flowbite"` |
| Colors | `theme.extend.colors` | CSS custom properties in `@theme` |
| Import | `@tailwind base/components/utilities` | `@import "tailwindcss"` |

</doc>



</docs>

</project>

