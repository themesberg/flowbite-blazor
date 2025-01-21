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

