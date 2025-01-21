<project title="Flowbite Blazor" summary="A comprehensive Blazor component library that ports the Flowbite React component library to ASP.NET Blazor 8.0. Built on TailwindCSS, it provides strongly-typed Blazor components that implement Flowbite design patterns while maintaining consistency with the React implementation. The library offers a rich set of accessible, dark-mode compatible components with built-in ARIA support.">

## Project Links

- [Github Repository](https://github.com/peakflames/flowbite-blazor)
- [Documenation Site](https://flowbite-blazor.peakflames.org/docs/components/{{COMPONENT_NAME}})


## Features

- TailwindCSS Integration - Beautiful, responsive designs out of the box
- Dark Mode Support - Automatic dark mode through TailwindCSS classes
- Built-in Accessibility - ARIA attributes and keyboard navigation included
- Responsive Design - Mobile-first components that work everywhere
- Native Blazor Events - Seamless integration with Blazor's event system
- Strong Typing - Full type safety and IntelliSense support
- No Node.js Required - Simple MSBuild integration for TailwindCSS
- Extended Icons - Optional package for additional icon components


<docs>

<doc title="Quick Start" description="Getting started with Flowbite Blazor">

## Installation

Quickly scaffold a new project using the using the CLI. The following project types include:
1. Blazor WebAssembly Standalone App
2. Desktop Application using Blazor and Photino.NET

### Scaffold a Blazor WebAssembly Standalone App

- __For Window Platform:__

    ```powershell
    dotnet new install Flowbite.Blazor.Templates
    dotnet new flowbite-blazor-wasm -o {{PROJECT_NAME}};
    cd {{PROJECT_NAME}}
    mkdir .\tools -Force
    cd .\tools
    Invoke-WebRequest -Uri https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-windows-x64.exe -OutFile tailwindcss.exe -UseBasicParsing
    cd ..
    dotnet build
    ```

- __For Mac OSX Arm64:__

    ```zsh
    dotnet new install Flowbite.Blazor.Templates
    dotnet new flowbite-blazor-wasm -o {{PROJECT_NAME}};
    cd {{PROJECT_NAME}}
    mkdir ./tools
    cd ./tools
    curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-macos-arm64
    chmod +x tailwindcss-macos-arm64 
    mv tailwindcss-macos-arm64 tailwindcss
    cd ..
    dotnet build
    ```

### Scaffold a Desktop Application using Blazor and Photino.NET

- __For Window Platform:__

    ```powershell
    dotnet new install Flowbite.Blazor.Templates
    dotnet new flowbite-blazor-desktop -o {{PROJECT_NAME}};
    cd {{PROJECT_NAME}}
    mkdir .\src\{{PROJECT_NAME}}\tools -Force;
    cd .\src\{{PROJECT_NAME}}\tools;
    Invoke-WebRequest -Uri https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-windows-x64.exe -OutFile tailwindcss.exe -UseBasicParsing ;
    cd ..\..\..
    dotnet build
    ```

- __For Mac OSX Arm64:__

    ```zsh
    dotnet new install Flowbite.Blazor.Templates
    dotnet new flowbite-blazor-desktop -o {{PROJECT_NAME}};
    cd {{PROJECT_NAME}}
    mkdir -p ./src/{{PROJECT_NAME}}/tools
    cd ./src/{{PROJECT_NAME}}/tools 
    curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-macos-arm64
    chmod +x tailwindcss-macos-arm64
    mv tailwindcss-macos-arm64 tailwindcss && cd ../../..
    dotnet build
    ```

</doc>

<doc title="UI Components" description="Blazor UI Components">

## Available Components

Flowbite Blazor provides the following set of UI components:
- Alert
- Avatar
- Badge
- Breadcrumb
- Button
- Card
- Dropdown
- Navbar
- Spinner
- Sidebar
- Tabs
- Tooltip
- Table

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
- None specified is the same as ButtonColor.Default
- ButtonColor.Gray
- ButtonColor.Primary
- ButtonColor.Dark
- ButtonColor.Light
- ButtonColor.Green
- ButtonColor.Red
- ButtonColor.Yellow
- ButtonColor.Purple

The available Button sizes are:
- ButtonSize.Small,
- ButtonSize.Medium,
- ButtonSize.Large

```razor
<!-- Default button with color -->
<Button Color="ButtonColor.Green" Size="ButtonSize.Large">
    Click me
</Button>

<!-- Outline button with icon -->
<Button Style="ButtonStyle.Outline" Color="ButtonColor.Info" Icon="@(new InfoIcon())">
    Info with icon
</Button>

<!-- Loading state -->
<Button Loading="true" Color="ButtonColor.Success">
    Processing...
</Button>

<!-- Link button -->
<Button Href="https://flowbite.com" Target="_blank" Color="ButtonColor.Purple">
    Visit Website
</Button>

<!-- Full-width button -->
<Button Color="ButtonColor.Dark" class="w-full">
    Block Button
</Button>
```




#### Card Examples

```razor
<!-- Basic card with CTA button -->
<Card class="max-w-sm">
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
<Card class="max-w-sm"
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
<Card class="max-w-sm"
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

<!-- E-commerce product card -->
<Card class="max-w-sm"
      ImgSrc="path/to/product.jpg"
      ImgAlt="Product image">
    <div class="px-5 pb-5">
        <h5 class="text-xl font-semibold tracking-tight text-gray-900 dark:text-white">
            Apple Watch Series 7 GPS
        </h5>
        <div class="mt-2.5 mb-5 flex items-center">
            <span class="ml-3 rounded bg-blue-100 px-2.5 py-0.5 text-xs font-semibold text-blue-800 dark:bg-blue-200 dark:text-blue-800">
                5.0
            </span>
        </div>
        <div class="flex items-center justify-between">
            <span class="text-3xl font-bold text-gray-900 dark:text-white">$599</span>
            <Button Color="ButtonColor.Default">
                Add to cart
            </Button>
        </div>
    </div>
</Card>
```






#### Dropdown Examples

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
```




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

```razor
<!-- Sidebar example that comprehensively demonstration the majority of features. This includes logos, icons, dropdowns, item grouping, responsiveness, CTA region with a button -->
<div class="flex">
        <!-- Mobile menu button -->
        <Button Color="ButtonColor.Dark" class="lg:hidden mb-3">
            <BarsIcon class="w-5 h-5" />
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
                Icon="@(new ChartPieIcon())">
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
                Preview the new Flowbite dashboard navigation! You can turn the new navigation off for a limited time in your profile.
            </div>
            <Button Color="ButtonColor.Default" class="w-full">
                Upgrade to Pro
            </Button>
        </SidebarCTA>
    </Sidebar>


<!-- Mulit-Level Sidebar  that demonstrates deep nested navigation with multiple levels of dropdowns, perfect for complex application hierarchies. -->
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
                <SidebarItem 
                    Href="/settings/system/security/permissions"
                    Icon="@(new BadgeCheckIcon())">
                    Permissions
                </SidebarItem>
                <SidebarItem 
                    Href="/settings/system/security/authentication"
                    Icon="@(new BellIcon())">
                    Authentication
                </SidebarItem>
                <SidebarItem 
                    Href="/settings/system/security/encryption"
                    Icon="@(new ShieldCheckIcon())">
                    Encryption
                </SidebarItem>
            </SidebarCollapse>
            <SidebarItem 
                Href="/settings/system/backup"
                Icon="@(new CloudArrowUpIcon())">
                Backup
            </SidebarItem>
        </SidebarCollapse>
        <SidebarItem 
            Href="/settings/notifications"
            Icon="@(new BellIcon())">
            Notifications
        </SidebarItem>
    </SidebarCollapse>
    
    <SidebarItem 
        Href="/help"
        Icon="@(new BellIcon())">
        Help
    </SidebarItem>
</Sidebar>

```







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

<!-- Style variations -->
<div class="flex items-center gap-4">
    <Tooltip Content="Dark style" Style="dark">
        <Button>Dark</Button>
    </Tooltip>
    <Tooltip Content="Light style" Style="light">
        <Button Color="ButtonColor.Light">Light</Button>
    </Tooltip>
    <Tooltip Content="Auto style (adapts to dark mode)" Style="auto">
        <Button>Auto</Button>
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



</docs>

</project>

