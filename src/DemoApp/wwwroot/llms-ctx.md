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



#### Form Component Examples

The available form components are:
- Label - For form field labels with required/disabled states
- TextInput - Text input with sizes, states, and addons
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
     - Color: TextInputColor - Success, Failure, etc for validation
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the input
     - HelperText: string - Help text below input
     - AddonLeft/Right: string - Text addons
     - Icon/RightIcon: Component - Icon components
     - Shadow: bool - Adds shadow effect -->
<TextInput Id="email" Type="email" Placeholder="name@flowbite.com" Required="true" />
<TextInput Size="TextInputSize.Small" Placeholder="Small input" />
<TextInput Color="TextInputColor.Success" Value="Success input" HelperText="Success message" />
<TextInput AddonLeft="https://" AddonRight=".com" Placeholder="flowbite" />

<!-- Textarea Component
     Parameters:
     - Id: string - Textarea identifier
     - Rows: int - Number of visible text rows
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the textarea
     - HelperText: string - Help text below textarea
     - Shadow: bool - Adds shadow effect -->
<Textarea Id="comment" 
          Rows="4" 
          Placeholder="Write your thoughts here..." 
          Required="true" />

<!-- Select Component
     Parameters:
     - Id: string - Select identifier
     - Size: TextInputSize - Small, Medium, Large
     - Color: SelectColor - Success, Failure, etc
     - Disabled: bool - Disables the select
     - HelperText: string - Help text below select
     - Icon: Type - Icon component type
     - Shadow: bool - Adds shadow effect -->
<Select Id="countries" @bind-Value="selectedCountry">
    <option value="">Choose a country</option>
    <option value="US">United States</option>
    <option value="CA">Canada</option>
</Select>

<!-- Checkbox Component
     Parameters:
     - Id: string - Checkbox identifier
     - Checked: bool - Checked state
     - Disabled: bool - Disables the checkbox
     - Required: bool - Makes field required -->
<div class="flex items-center gap-2">
    <Checkbox Id="remember" />
    <Label For="remember">Remember me</Label>
</div>

<!-- Radio Component
     Parameters:
     - Id: string - Radio identifier
     - Name: string - Groups radio buttons
     - Value: bool - Selected state
     - Disabled: bool - Disables the radio
     - Required: bool - Makes field required -->
<div class="flex items-center gap-2">
    <Radio Id="option1" Name="group" Value="true" />
    <Label For="option1">Option 1</Label>
</div>

<!-- FileInput Component
     Parameters:
     - Id: string - Input identifier
     - HelperText: string - Help text below input
     - Color: FileInputColor - Success, Failure, etc
     - Disabled: bool - Disables the input
     - Shadow: bool - Adds shadow effect -->
<FileInput Id="file" 
           HelperText="Upload your profile picture" />

<!-- ToggleSwitch Component
     Parameters:
     - Checked: bool - Toggle state
     - Label: string - Label text
     - Disabled: bool - Disables the toggle
     - Name: string - Form field name -->
<ToggleSwitch @bind-Checked="isEnabled" 
              Label="Enable notifications" />

<!-- RangeSlider Component
     Parameters:
     - Id: string - Slider identifier
     - Size: RangeSliderSize - Small, Medium, Large
     - Value: double - Current value
     - Min: double - Minimum value (default: 0)
     - Max: double - Maximum value (default: 100)
     - Step: double - Step increment (default: 1)
     - Disabled: bool - Disables the slider -->
<RangeSlider Id="default-range"
             Size="RangeSliderSize.Medium" />

#### Form Validation Examples

The Flowbite Blazor library supports both built-in DataAnnotations validation and custom validation scenarios:

##### Basic Form Validation with DataAnnotations

```razor
<!-- Basic form with DataAnnotations validation
     Features:
     - Uses EditForm for form handling
     - DataAnnotationsValidator for validation
     - ValidationMessage for error display
     - Real-time validation feedback -->
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

##### Custom Validation

```razor
<!-- Custom validation example
     Features:
     - Custom validator component
     - Field-level validation
     - Custom validation messages -->
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <CustomValidator @ref="customValidator" />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="username" Value="Username" />
            </div>
            <TextInput TValue="string" 
                      Id="username" 
                      @bind-Value="model.Username" />
            <ValidationMessage For="@(() => model.Username)" />
        </div>
        <Button Type="submit">Submit</Button>
    </div>
</EditForm>

@code {
    private CustomValidator? customValidator;

    private async Task HandleValidSubmit()
    {
        // Example of custom validation logic
        if (customModel.Username.Contains(" "))
        {
            customValidator?.DisplayError("Username", "Username cannot contain spaces");
            return;
        }
        await SubmitForm();
    }
}
```

##### Form State Management

```razor
<!-- Form state management example
     Features:
     - Dirty state tracking
     - Change tracking
     - Reset functionality
     - EditContext integration -->
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

The form validation system integrates seamlessly with Blazor's built-in form handling while providing additional features for custom validation scenarios and state management. All form components support validation states through their Color properties and automatically integrate with ValidationMessage components.




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
                    With less than a month to go before the European Union enacts new consumer privacy laws for its citizens,
                    companies around the world are updating their terms of service agreements to comply.
                </p>
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    The European Union's General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                    to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                    soon as possible of high-risk data breaches that could personally affect them.
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => HandleTermsChoice(false))" Color="ButtonColor.Gray" class="mr-2">Decline</Button>
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
    
    private string GetChoiceAlertClass()
    {
        return termsAccepted == true
            ? "p-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400"
            : "p-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400";
    }
}

```

__Model Options:__

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

```razor
<div class="space-y-4">
        
    <Modal Show="showDefaultModal" ShowChanged="(value) => showDefaultModal = value" Size="ModalSize.FourExtraLarge" Position="ModalPosition.TopCenter">
        <ModalHeader>
            <h3>Terms of Service</h3>
        </ModalHeader>
        <ModalBody>
            <div class="space-y-6">
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    With less than a month to go before the European Union enacts new consumer privacy laws for its citizens,
                    companies around the world are updating their terms of service agreements to comply.
                </p>
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    The European Union's General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                    to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                    soon as possible of high-risk data breaches that could personally affect them.
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => showSizedModal = false)" Color="ButtonColor.Gray" class="mr-2">Decline</Button>
                <Button OnClick="@(() => showSizedModal = false)">Accept</Button>
            </div>
        </ModalFooter>
    </Modal>
</div>

@code {
    private bool showDefaultModal = false;
}
```





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



<doc title="Common Patterns" description="Common patterns and best practices">

## Best Practices

### Event Handling

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
    <ChildContent>
        <DropdownItem OnClick="@(() => HandleItemClick(item.Id))">
            @item.Name
        </DropdownItem>
    </ChildContent>
</Dropdown>
```

### Dark Mode

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

</doc>



<doc title="Quick Start" description="Zero to Hero to get setup, configured, and running">

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

1. Create a new project using dotnet new and add some packages
2. Download the tailwindcss cli exe to the tools folder
3. Tweak the csproj file for flowbite, tailwindcss, and use of preferred pre-rendering package
4. Tweak the Program.cs
5. Tweak the wwwroot/index.html
6. Tweak the wwwroot/css/app.css
7. Tweak the _Imports.razor
8. Tweak the tailwind.config.js
9. Determine what do with the Pages/Home.razor

The sections below provide the exact details.

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

## 2. Download the tailwindcss cli
__For Window Platform:__
```sh
# pwd is the {{PROJECT_DIR_ROOT}}
cd {{PROJECT_NAME}}; mkdir {{PROJECT_NAME}}/tools; cd tools
Invoke-WebRequest -Uri https://github.com/tailwindlabs/tailwindcss/releases/download/v3.4.15/tailwindcss-windows-x64.exe -OutFile tailwindcss.exe -UseBasicParsing
cd ../..
# pwd is the {{PROJECT_DIR_ROOT}}
```

__For MacOS:__
```sh
# pwd is the {{PROJECT_DIR_ROOT}}
cd {{PROJECT_NAME}} && mkdir {{PROJECT_NAME}}/tools && cd tools
curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/download/v3.4.15/tailwindcss-macos-arm64
chmod +x tailwindcss-macos-arm64 
mv tailwindcss-macos-arm64 tailwindcss
cd ../..
# pwd is the {{PROJECT_DIR_ROOT}}
```

## 3. Tweak the csproj file

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <TargetFramework>{{leave as what the user has chosen}}</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
    <BlazorEnableTimeZoneSupport>false</BlazorEnableTimeZoneSupport>
    <PostCSSConfig>postcss.config.js</PostCSSConfig>
    <TailwindConfig>tailwind.config.js</TailwindConfig>
    <Version>0.0.1-alpha.1</Version>
  </PropertyGroup>

  <PropertyGroup>
    <!-- Requird part of using the BlazorWasmPrerending.Build package. Peforms static site generation to be used on first render making lightning fast initial loads -->
    <BlazorWasmPrerenderingDeleteLoadingContents>true</BlazorWasmPrerenderingDeleteLoadingContents>
  </PropertyGroup>

  <ItemGroup>

    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="8.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="8.0.0" PrivateAssets="all" />
    <PackageReference Include="Flowbite" Version="0.0.*-*" />
    <!-- Peforms static site generation to be used on first render making lightning fast initial loads -->
    <PackageReference Include="BlazorWasmPreRendering.Build" Version="5.0.0" />
  </ItemGroup>

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

## 4. Tweak the Program.cs

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

// Required for prerendering (BlazorWasmPreRendering.Build)
// extract the service-registration process to the static local function.
static void ConfigureServices(IServiceCollection services, string baseAddress)
{
  services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
  services.AddFlowbite();
}

## 5. Tweak the wwwroot/index.html

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

            if (localStorage.getItem('color-theme') === 'dark' || (!('color-theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
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
        <script src="_framework/blazor.webassembly.js"></script>
        <script src="/js/app.js"></script>
        <script src="_content/Flowbite/flowbite.js"></script>
    </body>

</html>
```

### 6. Tweak the wwwroot/css/app.css

```css
@tailwind base;
@tailwind components;
@tailwind utilities;


/* Microsoft Blazor  ------------------------------------------------------------------------------------------------------------------ */
.valid.modified:not([type=checkbox]) {
    outline: 1px solid #26b050;
}

.invalid {
    outline: 1px solid #e50000;
}

.validation-message {
    color: #e50000;
}

.blazor-error-boundary {
    background: url(data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNTYiIGhlaWdodD0iNDkiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyIgeG1sbnM6eGxpbms9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkveGxpbmsiIG92ZXJmbG93PSJoaWRkZW4iPjxkZWZzPjxjbGlwUGF0aCBpZD0iY2xpcDAiPjxyZWN0IHg9IjIzNSIgeT0iNTEiIHdpZHRoPSI1NiIgaGVpZ2h0PSI0OSIvPjwvY2xpcFBhdGg+PC9kZWZzPjxnIGNsaXAtcGF0aD0idXJsKCNjbGlwMCkiIHRyYW5zZm9ybT0idHJhbnNsYXRlKC0yMzUgLTUxKSI+PHBhdGggZD0iTTI2My41MDYgNTFDMjY0LjcxNyA1MSAyNjUuODEzIDUxLjQ4MzcgMjY2LjYwNiA1Mi4yNjU4TDI2Ny4wNTIgNTIuNzk4NyAyNjcuNTM5IDUzLjYyODMgMjkwLjE4NSA5Mi4xODMxIDI5MC41NDUgOTIuNzk1IDI5MC42NTYgOTIuOTk2QzI5MC44NzcgOTMuNTEzIDI5MSA5NC4wODE1IDI5MSA5NC42NzgyIDI5MSA5Ny4wNjUxIDI4OS4wMzggOTkgMjg2LjYxNyA5OUwyNDAuMzgzIDk5QzIzNy45NjMgOTkgMjM2IDk3LjA2NTEgMjM2IDk0LjY3ODIgMjM2IDk0LjM3OTkgMjM2LjAzMSA5NC4wODg2IDIzNi4wODkgOTMuODA3MkwyMzYuMzM4IDkzLjAxNjIgMjM2Ljg1OCA5Mi4xMzE0IDI1OS40NzMgNTMuNjI5NCAyNTkuOTYxIDUyLjc5ODUgMjYwLjQwNyA1Mi4yNjU4QzI2MS4yIDUxLjQ4MzcgMjYyLjI5NiA1MSAyNjMuNTA2IDUxWk0yNjMuNTg2IDY2LjAxODNDMjYwLjczNyA2Ni4wMTgzIDI1OS4zMTMgNjcuMTI0NSAyNTkuMzEzIDY5LjMzNyAyNTkuMzEzIDY5LjYxMDIgMjU5LjMzMiA2OS44NjA4IDI1OS4zNzEgNzAuMDg4N0wyNjEuNzk1IDg0LjAxNjEgMjY1LjM4IDg0LjAxNjEgMjY3LjgyMSA2OS43NDc1QzI2Ny44NiA2OS43MzA5IDI2Ny44NzkgNjkuNTg3NyAyNjcuODc5IDY5LjMxNzkgMjY3Ljg3OSA2Ny4xMTgyIDI2Ni40NDggNjYuMDE4MyAyNjMuNTg2IDY2LjAxODNaTTI2My41NzYgODYuMDU0N0MyNjEuMDQ5IDg2LjA1NDcgMjU5Ljc4NiA4Ny4zMDA1IDI1OS43ODYgODkuNzkyMSAyNTkuNzg2IDkyLjI4MzcgMjYxLjA0OSA5My41Mjk1IDI2My41NzYgOTMuNTI5NSAyNjYuMTE2IDkzLjUyOTUgMjY3LjM4NyA5Mi4yODM3IDI2Ny4zODcgODkuNzkyMSAyNjcuMzg3IDg3LjMwMDUgMjY2LjExNiA4Ni4wNTQ3IDI2My41NzYgODYuMDU0N1oiIGZpbGw9IiNGRkU1MDAiIGZpbGwtcnVsZT0iZXZlbm9kZCIvPjwvZz48L3N2Zz4=) no-repeat 1rem/1.8rem, #b32121;
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

### 7. Tweak the _Imports.razor

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
@using Flowbite.Base
@using Flowbite.Components
@using Flowbite.Components.Tabs
@using Flowbite.Components.Table
@using Flowbite.Icons
@using Flowbite.Services
@using static Flowbite.Components.Button
@using static Flowbite.Components.Tooltip
@using static Flowbite.Components.Avatar
@using static Flowbite.Components.Sidebar
@using static Flowbite.Components.SidebarCTA
@using static Flowbite.Components.Dropdown
@using PROJECT_NAME
@using PROJECT_NAME.Layout

# if the project creates it's own components uncomment this out
# @using PROJECT_NAME.Components
```

### 8. Tweak the tailwind.config.js (v3)

ULTRA IMPORTANT: Flowbite Blazor is compatible only with Tailwind v3

```js
/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "App.razor",
        "./wwwroot/**/*.{razor,html,cshtml,cs}",
        "./Layout/**/*.{razor,html,cshtml,cs}",
        "./Pages/**/*.{razor,html,cshtml,cs}",
        "./Components/**/*.{razor,html,cshtml,cs}"
    ],
    darkMode: 'class',
    safelist: [
        "md:bg-transparent",
        "md:block",
        "md:border-0",
        "md:dark:hover:bg-transparent",
        "md:dark:hover:text-white",
        "md:flex-row",
        "md:font-medium",
        "md:hidden",
        "md:hover:bg-transparent",
        "md:hover:text-primary-700",
        "md:mt-0",
        "md:p-0",
        "md:space-x-8",
        "md:text-primary-700",
        "md:text-sm",
        "md:w-auto"
    ],
    theme: {
        extend: {
            colors: {
                primary: { "50": "#eff6ff", "100": "#dbeafe", "200": "#bfdbfe", "300": "#93c5fd", "400": "#60a5fa", "500": "#3b82f6", "600": "#2563eb", "700": "#1d4ed8", "800": "#1e40af", "900": "#1e3a8a", "950": "#172554" }
            },
            maxHeight: {
                'table-xl': '60rem',
            }
        },
        fontFamily: {
            'body': [
                ... font names ...
            ],
            'sans': [
                ... font names ...
            ],
            'mono': [
                ... font names ...
            ]
        }
    }
}
```

### 9. Determine where to place the `/` route

You MUST decide where to place the `/` route. The `dotnet new` generates a `Pages/Home.razor` file that contains the `/` route. You MUST decide whether to keep and replace the contents of this file or DELETE th Home.razor file and create a new file for the `/` route.

</doc>



</docs>

</project>

