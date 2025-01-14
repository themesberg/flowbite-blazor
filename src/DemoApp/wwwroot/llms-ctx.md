<project title="Flowbite Blazor" summary="A comprehensive Blazor component library that ports the Flowbite React component library to ASP.NET Blazor 8.0. Built on TailwindCSS, it provides strongly-typed Blazor components that implement Flowbite design patterns while maintaining consistency with the React implementation. The library offers a rich set of accessible, dark-mode compatible components with built-in ARIA support.">

## Features

- 🎨 TailwindCSS Integration - Beautiful, responsive designs out of the box
- 🌙 Dark Mode Support - Automatic dark mode through TailwindCSS classes
- ♿ Built-in Accessibility - ARIA attributes and keyboard navigation included
- 📱 Responsive Design - Mobile-first components that work everywhere
- 🚀 Native Blazor Events - Seamless integration with Blazor's event system
- 🎯 Strong Typing - Full type safety and IntelliSense support
- 📦 No Node.js Required - Simple MSBuild integration for TailwindCSS
- 🔌 Extended Icons - Optional package for additional icon components


<docs>

<doc title="Quick Start" desc="Getting started with Flowbite Blazor">

## Installation

```bash
# Install Flowbite Blazor packages
dotnet add package Flowbite.Blazor
dotnet add package Flowbite.ExtendedIcons  # Optional: Additional icons
```

Configure your .csproj file with TailwindCSS build targets:

```xml
<PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
    <BlazorEnableTimeZoneSupport>false</BlazorEnableTimeZoneSupport>
    <PostCSSConfig>postcss.config.js</PostCSSConfig>
    <TailwindConfig>tailwind.config.js</TailwindConfig>
</PropertyGroup>

<!-- Tailwind CSS Build Targets -->
<Target Name="Tailwind" BeforeTargets="Build" Condition="'$(OS)' == 'Windows_NT'">
    <Exec Command="..\..\tools\tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/app.min.css --minify --postcss" />
</Target>

<Target Name="TailwindWatch" BeforeTargets="Build" Condition="'$(OS)' == 'Windows_NT'">
    <Exec Command="..\..\tools\tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/app.min.css --watch --postcss" />
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
```

Add to _Imports.razor:

```razor
@using Flowbite
@using Flowbite.Components
@using Flowbite.ExtendedIcons  // If using extended icons
```

Configure in Program.cs:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFlowbiteBlazor();
```

Add the references to Flowbite CSS file to the <head> element

```html
<link rel="stylesheet" href="_content/Flowbite/flowbite.min.css" />
```

Add the reference to Flowbite Javascript file to end of your <body> element

```html
<script src="_content/Flowbite/flowbite.js"></script>
```

> **Note**
>
> TailwindCSS is integrated via MSBuild targets and uses a local tailwindcss. No Node.js installation is required.

> **Note**
>
> The AddFlowbiteBlazor() method automatically registers all Flowbite components and services. You don't need to register components individually.

> **Note**
>
> The  Flowbite CSS file and Javascript file are mandatory.

## Available Components

Flowbite Blazor provides a comprehensive set of UI components:

### Interactive Components

#### Button Examples

```razor
<!-- Default button with color -->
<Button Color="ButtonColor.Primary" Size="ButtonSize.Large">
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

#### Alert Examples

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

### Navigation Components

#### Navbar Examples

```razor
<Navbar>
    <NavbarBrand>
        <img src="logo.svg" class="h-6 mr-3" alt="Logo" />
        <span class="self-center text-xl font-semibold">Brand</span>
    </NavbarBrand>
    <NavbarToggle />
    <NavbarCollapse>
        <NavbarLink Href="#" Active>Home</NavbarLink>
        <NavbarLink Href="#">About</NavbarLink>
        <NavbarLink Href="#">Services</NavbarLink>
        <NavbarLink Href="#">Pricing</NavbarLink>
        <NavbarLink Href="#">Contact</NavbarLink>
    </NavbarCollapse>
</Navbar>
```

#### Sidebar Examples

```razor
<!-- Sidebar with logo and collapse -->
<Sidebar>
    <SidebarLogo>
        <img src="logo.svg" class="h-6 mr-3" alt="Logo" />
        <span class="self-center text-xl font-semibold">Brand</span>
    </SidebarLogo>
    
    <!-- Simple items -->
    <SidebarItem Href="#" Icon="@(new HomeIcon())">Dashboard</SidebarItem>
    <SidebarItem Href="#" Icon="@(new ChartPieIcon())">Analytics</SidebarItem>
    
    <!-- Grouped items -->
    <SidebarGroup Text="Management">
        <SidebarItem Href="#" Icon="@(new UsersIcon())">Users</SidebarItem>
        <SidebarItem Href="#" Icon="@(new GearIcon())">Settings</SidebarItem>
    </SidebarGroup>
    
    <!-- CTA section -->
    <SidebarCTA>
        <div class="mb-3 text-sm">
            Preview the new Flowbite dashboard navigation!
        </div>
        <Button Color="ButtonColor.Primary" class="w-full">
            Upgrade to Pro
        </Button>
    </SidebarCTA>
</Sidebar>
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

### Display Components

#### Spinner Examples

```razor
<!-- Different sizes -->
<Spinner Size="SpinnerSize.Small" Color="SpinnerColor.Info" />
<Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Success" />
<Spinner Size="SpinnerSize.Large" Color="SpinnerColor.Warning" />

<!-- With text -->
<div class="flex items-center">
    <Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Primary" />
    <span class="ml-2">Loading...</span>
</div>
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

#### Badge Examples

```razor
<!-- Default badges with different colors -->
<div class="flex flex-wrap gap-2">
    <Badge>Primary</Badge>
    <Badge Color="BadgeColor.Info">Info</Badge>
    <Badge Color="BadgeColor.Gray">Gray</Badge>
    <Badge Color="BadgeColor.Failure">Failure</Badge>
    <Badge Color="BadgeColor.Success">Success</Badge>
    <Badge Color="BadgeColor.Warning">Warning</Badge>
    <Badge Color="BadgeColor.Indigo">Indigo</Badge>
    <Badge Color="BadgeColor.Purple">Purple</Badge>
    <Badge Color="BadgeColor.Pink">Pink</Badge>
</div>

<!-- Badge as link -->
<div class="flex flex-wrap gap-2">
    <Badge Href="#">Primary</Badge>
    <Badge Color="BadgeColor.Gray" Href="#">Gray</Badge>
    <Badge Color="BadgeColor.Success" Href="#">Success</Badge>
</div>

<!-- Badge with icon -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())">2 minutes ago</Badge>
    <Badge Color="BadgeColor.Gray" Icon="@(new ClockIcon())">In progress</Badge>
    <Badge Color="BadgeColor.Success" Icon="@(new CheckCircleIcon())">Completed</Badge>
</div>

<!-- Badge with icon only -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())" />
    <Badge Icon="@(new CheckIcon())" Color="BadgeColor.Gray" />
    <Badge Icon="@(new CheckIcon())" Size="BadgeSize.Small" />
    <Badge Color="BadgeColor.Gray" Icon="@(new CheckIcon())" Size="BadgeSize.Small" />
</div>

<!-- Different sizes -->
<div class="flex flex-wrap items-center gap-2">
    <Badge Size="BadgeSize.ExtraSmall">Extra small</Badge>
    <Badge Size="BadgeSize.Small">Small</Badge>
    <Badge Size="BadgeSize.ExtraSmall" Icon="@(new CheckIcon())">With icon</Badge>
    <Badge Size="BadgeSize.Small" Icon="@(new CheckIcon())">With icon</Badge>
</div>
```

## Dark Mode Support

All components are designed with dark mode in mind:

```razor
<!-- Dark mode is handled automatically through TailwindCSS classes -->
<div class="bg-white dark:bg-gray-800">
    <Alert Color="AlertColor.Info">
        This alert adapts to dark mode automatically
    </Alert>
    
    <Button Color="ButtonColor.Primary">
        This button also adapts to dark mode
    </Button>
</div>

<!-- Toggle dark mode with TailwindCSS -->
<div class="dark">
    <!-- Components in this div will use dark mode styles -->
    <Navbar>
        <!-- Navbar content -->
    </Navbar>
</div>
```

</doc>



<doc title="Using Icons" desc="Working with Flowbite Blazor icons">

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

### Extended Icons

Install the additional icons package:

```bash
dotnet add package Flowbite.ExtendedIcons
```

Then add to _Imports.razor:

```razor
@using Flowbite.ExtendedIcons
```

Now you can use any of the extended icons:

```razor
<Button>
    <ChartPieIcon class="w-5 h-5 mr-2" />
    Statistics
</Button>

<SidebarItem Icon="@(new DashboardIcon())">
    Dashboard
</SidebarItem>
```

</doc>



<doc title="Common Patterns" desc="Common patterns and best practices">

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

### Accessibility

Components include built-in accessibility features:

```razor
<!-- Automatic ARIA attributes -->
<Alert Color="AlertColor.Warning" 
       IsDismissible="true"
       Role="alert">
    <p>Warning: This action cannot be undone.</p>
</Alert>

<!-- Keyboard navigation -->
<Dropdown>
    <Label>Accessible Menu</Label>
    <ChildContent>
        <!-- Tab and arrow key navigation supported -->
        <DropdownItem>Profile</DropdownItem>
        <DropdownItem>Settings</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Screen reader text -->
<Button>
    <span class="sr-only">Close menu</span>
    <XMarkIcon class="w-5 h-5" />
</Button>
```

</doc>



</docs>

</project>

