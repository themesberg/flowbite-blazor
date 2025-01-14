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
