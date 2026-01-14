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
.validation-message {
    @apply text-red-600 dark:text-red-500;
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
