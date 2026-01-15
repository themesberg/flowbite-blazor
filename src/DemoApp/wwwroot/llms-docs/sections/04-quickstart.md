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
