# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Flowbite Blazor is a Blazor component library built on top of Tailwind CSS and the Flowbite design system. It's a port of the Flowbite React library to ASP.NET Blazor 8.0/9.0.

**Status:** Early development (v0.0.x-alpha). APIs and packages are likely to change.

**Repository:** themesberg/flowbite-blazor
**Base Branch:** `develop` (use this for PRs, not `main`)

## Solution Structure

The solution contains three main projects:

1. **Flowbite** (`src/Flowbite/`) - Core component library (v0.0.12-alpha)
2. **Flowbite.ExtendedIcons** (`src/Flowbite.ExtendedIcons/`) - Extended icon set
3. **DemoApp** (`src/DemoApp/`) - Documentation and demo application (v0.0.31-alpha)

## Common Commands

### Building

```powershell
# Build entire solution
dotnet build

# Build DemoApp specifically (from project root)
cd src/DemoApp; dotnet build

# Build with Release configuration (uses NuGet packages instead of project references)
dotnet build -c Release
```

### Running

```powershell
# Run DemoApp with hot reload
cd src/DemoApp
dotnet watch

# Or just run it
dotnet run
# Then open http://localhost:5290/
```

### Testing Packaged Components

When you need to test how the components work as NuGet packages:

```powershell
# Pack libraries and publish DemoApp to ./dist directory
.\publish-local.ps1

# Serve the static DemoApp
cd dist/wwwroot
dotnet serve  # Requires dotnet-serve tool
```

### Tailwind CSS

Tailwind CSS is integrated via MSBuild targets and runs automatically on build. The standalone Tailwind CLI executable is located in `./tools/tailwindcss.exe`.

**Manual compilation (if needed):**
```powershell
# From DemoApp directory
..\..\tools\tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/app.min.css --minify --postcss
```

### Documentation Generation

```powershell
# Build LLMS context documentation (runs automatically on DemoApp build)
cd src/DemoApp
powershell -ExecutionPolicy Bypass -File Build-LlmsContext.ps1
```

## Architecture

### Component Base Classes

All components inherit from base classes in `src/Flowbite/Base/`:

- **`FlowbiteComponentBase`** - Base for all components
  - Inherits from `ComponentBase`
  - Provides `CombineClasses()` utility for merging CSS classes
  - Exposes `Class` parameter for user-provided CSS classes

- **`IconBase`** - Base for icon components
  - Extends `FlowbiteComponentBase`
  - Handles SVG icon rendering with configurable stroke width and aria attributes

- **`OffCanvasComponentBase`** - Base for modal/drawer/toast components
  - Manages visibility state and cascading values

### Component Structure Pattern

Components follow a consistent two-file pattern:

```
Button.razor        # Template markup
Button.razor.cs     # Code-behind with properties and logic
```

**Key conventions:**
- Parameters are public properties with `[Parameter]` attribute
- Use `[CaptureUnmatchedValues]` for additional HTML attributes
- Child content via `[Parameter] public RenderFragment? ChildContent { get; set; }`
- Prefer enums for style variations (e.g., `ButtonColor`, `ButtonSize`)

### Services and Dependency Injection

The library provides services for programmatic component control:

```csharp
// In Program.cs
builder.Services.AddFlowbite();  // Adds all services

// Or individually:
builder.Services.AddFlowbiteModalService();
builder.Services.AddFlowbiteDrawerService();
builder.Services.AddFlowbiteToastService();
```

Service interfaces are in `src/Flowbite/Services/`:
- `IModalService` - Open/close modals programmatically
- `IDrawerService` - Control drawer components
- `IToastService` - Show toast notifications

### DemoApp Structure

- **Pages:** `src/DemoApp/Pages/Docs/components/` - Component demo pages
- **Layout:** `src/DemoApp/Layout/` - Documentation layout components
  - `DocLayoutSidebarData.cs` - Navigation structure configuration
- **Documentation:** `src/DemoApp/wwwroot/llms-docs/sections/` - AI-friendly documentation

### Build Configuration

**DemoApp uses conditional references:**
- **Debug:** Project references to Flowbite libraries (for active development)
- **Release:** NuGet package references (for testing packaged versions)

This allows testing the library both as source and as packages without changing configuration.

## Coding Standards

### General C# Conventions

- **Indentation:** 4 spaces
- **Naming:**
  - `PascalCase` for public members and types
  - `_camelCase` for private fields
  - Parameters must be public properties
- **Prefer C# code in `.cs` files** rather than `@code` blocks in `.razor` files
- **Prefer functions over lambda expressions** for event handlers

### Component Development

1. **One component per file** - Place in `src/Flowbite/Components/`
2. **Use partial classes** for complex components
3. **XML comments** - Document all public APIs, parameters, and event callbacks
4. **Always use `@key` directive** when rendering lists with `@foreach`:
   ```razor
   @foreach (var item in MyItems)
   {
       <MyComponent @key="item.Id" Data="@item" />
   }
   ```

### Icon Usage

- **Always use icons from `Flowbite.Icons` or `Flowbite.ExtendedIcons`**
- Never use external icon libraries (Font Awesome, etc.)
- If a suitable icon doesn't exist, create one in the internal libraries

### Styling

- Use **Tailwind CSS utilities** exclusively
- Support **dark mode** via `dark:` variants
- Follow the **Flowbite design system**
- Components should accept `Class` parameter for additional styling

## Migrating React Components to Blazor

When porting Flowbite React components:

1. **Study the React source** in `C:/Users/tschavey/projects/javascript_projects/flowbite-react/packages/ui/src`
2. **Review React documentation** in `C:/Users/tschavey/projects/javascript_projects/flowbite-react/apps/web/content/docs/components`
3. **Create demo page** at `src/DemoApp/Pages/Docs/components/{ComponentName}Page.razor`
4. **Update sidebar navigation** in `src/DemoApp/Layout/DocLayoutSidebarData.cs`
5. **Create documentation file** in `src/DemoApp/wwwroot/llms-docs/sections/` and update `Build-LlmsContext.ps1`

**Demo page template:**
```razor
@page "/docs/components/{component-name}"

<PageTitle>{ComponentName} - Flowbite Blazor</PageTitle>

<main class="p-6 space-y-4 max-w-4xl">
    <h2>{ComponentName} Examples</h2>

    <div class="space-y-8">
        <ComponentExample
            Title="Example Title"
            Description="Example description"
            RazorCode="@(@"<Button>Click me</Button>")"
            SupportedLanguages="@(new[] { "razor" })">
            <PreviewContent>
                <Button>Click me</Button>
            </PreviewContent>
        </ComponentExample>
    </div>
</main>
```

**Component implementation template:**
```razor
@using Flowbite.Base
@inherits FlowbiteComponentBase
```

```csharp
namespace Flowbite.Components;

public partial class ComponentName
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    // Additional parameters and logic
}
```

## Problem-Solving Strategy

When encountering bugs or unexpected behavior:

1. **Analyze and Hypothesize** - Form a clear hypothesis about the root cause
2. **Propose and Verify** - Implement a specific, targeted fix
3. **Stop and Ask** - If the fix is incorrect, STOP. Do not attempt a second fix based on new assumptions. Present findings and ask for clarification.

This prevents wasting time on incorrect paths and ensures alignment with the user's understanding.

## Git Workflow

### Branch Naming

- Bugs: `fix/issue-{number}-{brief-description}`
- Features: `feature/issue-{number}-{brief-description}`
- Enhancements: `enhancement/issue-{number}-{brief-description}`

**Example:** `fix/issue-123-button-hover-state`

### Commit Message Format

```
{type}({scope}): {description}

Fixes #{issue-number}
```

**Types:** fix, feat, docs, style, refactor, test, chore

### Always Work from Develop

```bash
git checkout develop
git pull origin develop
git checkout -b fix/issue-123-description
```

## Important Files

- **Developer Guidelines:** `.clinerules/developer_guidelines.md`
- **Issue Workflow:** `.clinerules/workflows/github-issue-resolution.md`
- **Contributing Guide:** `CONTRIBUTING.md`
- **AI Context:** `src/DemoApp/wwwroot/llms-ctx.md` - Provide this URL to AI assistants for library documentation

## External References

- **Flowbite React Source:** `C:/Users/tschavey/projects/javascript_projects/flowbite-react/`
- **Preview Deployments:**
  - Main: https://flowbite-blazor.org/
  - Develop: https://develop.flowbite-blazor-8s8.pages.dev/
- The `src\Flowbite\wwwroot\flowbite.min.css` is the crucial CSS file that is actually used at runtime. When updating .html, .razor, or .cs the flowbite.min.css file is updated when running the project is rebuild. It is CRUCIAL that the flowbite.min.css be commited to git