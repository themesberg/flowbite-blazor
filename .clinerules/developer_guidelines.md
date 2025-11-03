# Agent Guidelines

## Overview
- Flowbite Blazor is a Blazor component library that ports Flowbite React to ASP.NET Blazor 8/9 on top of Tailwind CSS.
- Current status: early development (`v0.0.x-alpha`); expect API and package changes.
- Work from the `develop` branch for new changes and pull requests.

## Projects
- `src/Flowbite/` — core component library.
- `src/Flowbite.ExtendedIcons/` — optional icon packs.
- `src/DemoApp/` — documentation playground; mirror every new component with a demo page.
- Place shared docs under `docs/`, automation in `scripts/`, and ship-ready static assets under each project’s `wwwroot/`.

## Build, Run, and Packaging
- `dotnet build` or `dotnet build FlowbiteBlazor.sln` compiles the full solution; `dotnet build -c Release` tests the packaged flow.
- `dotnet watch --project src/DemoApp/DemoApp.csproj run` (or `dotnet run` from `src/DemoApp/`) starts the demo with hot reload at http://localhost:5290/.
- `./cf-build.sh` (Linux/macOS) or `./publish-local.ps1` (Windows) packs libraries and publishes the demo to `dist/` for NuGet-style validation.
- Test packaged components by running `publish-local.ps1` then serving `dist/wwwroot` (e.g., `dotnet serve`).
- Tailwind builds automatically via MSBuild; install the CLI into `tools/` before the first build. Manual run: `tools/tailwindcss -i src/DemoApp/wwwroot/css/app.css -o src/DemoApp/wwwroot/css/app.min.css --minify --postcss`.
- Documentation context can be regenerated with `powershell -ExecutionPolicy Bypass -File Build-LlmsContext.ps1` inside `src/DemoApp/`.

## Architecture and Component Patterns
- Base classes live in `src/Flowbite/Base/`:
  - `FlowbiteComponentBase` provides `CombineClasses()` and a `Class` parameter.
  - `IconBase` extends the base for SVG icons with aria and stroke control.
  - `OffCanvasComponentBase` manages visibility for drawers, modals, and toasts.
- Components use a two-file pattern: `Component.razor` for markup and `Component.razor.cs` for logic.
- Services for programmatic control (`AddFlowbite*`) reside in `src/Flowbite/Services/`; register them in `Program.cs`.
- DemoApp structure:
  - Pages under `src/DemoApp/Pages/Docs/components/`.
  - Sidebar data in `src/DemoApp/Layout/DocLayoutSidebarData.cs`.
  - AI documentation snippets in `src/DemoApp/wwwroot/llms-docs/sections/`.
- Debug builds link directly to projects; Release builds use locally packed NuGet packages.

## Development Conventions
- Follow `.editorconfig`: 4-space indentation, file-scoped namespaces, PascalCase public APIs, `_camelCase` private fields.
- Keep C# logic in `.cs` files via partial classes; parameters are public properties with `[Parameter]`.
- Use `[CaptureUnmatchedValues]` for additional HTML attributes, `RenderFragment? ChildContent` for slots, and prefer enums for style variations.
- Always apply `@key` when looping components with `@foreach`.
- Use Tailwind utility classes exclusively; ensure dark mode coverage with `dark:` variants and accept a `Class` parameter for custom styling.
- Only use icons from `Flowbite.Icons` or `Flowbite.ExtendedIcons`; add missing glyphs internally.
- Document all public APIs with XML comments.

## React-to-Blazor Migration Checklist
1. Review the Flowbite React source in `C:/Users/tschavey/projects/javascript_projects/flowbite-react/packages/ui/src`.
2. Cross-reference the React docs in `.../flowbite-react/apps/web/content/docs/components`.
3. Implement the component in `src/Flowbite/Components/` following the two-file pattern.
4. Add a demo page at `src/DemoApp/Pages/Docs/components/{ComponentName}Page.razor`.
5. Update navigation in `src/DemoApp/Layout/DocLayoutSidebarData.cs`.
6. Add documentation in `src/DemoApp/wwwroot/llms-docs/sections/` and update `Build-LlmsContext.ps1` if needed.

## Testing and Validation
- No automated test suite yet; rely on `dotnet build` and manual verification in the DemoApp.
- Exercise both light and dark themes, keyboard navigation, and key scenarios on the demo pages.
- When fixing bugs, reproduce them in the demo first, then validate the fix there.
- Ensure `src/Flowbite/wwwroot/flowbite.min.css` is regenerated as part of builds and committed whenever component styles change.
- **Non‑negotiable:** drive every meaningful UI verification through the Playwright MCP server (`mcp__playwright__browser_*`). Treat these scripted runs as mandatory—launch the DemoApp, navigate to the affected surface, and capture evidence (screenshots or DOM state) before calling a change “done.”

## Problem-Solving Approach
1. Analyze and form a hypothesis before modifying code.
2. Implement a focused fix and verify it.
3. If the hypothesis fails, stop and surface findings instead of pivoting blindly.

## Git Workflow
- Branch from `develop`: `git checkout develop && git pull origin develop`.
- Naming: `fix/issue-{id}-description`, `feature/issue-{id}-description`, or `enhancement/issue-{id}-description`.
- Commit format: `{type}({scope}): {description}` (types: fix, feat, docs, style, refactor, test, chore). Reference issues with `Fixes #{number}` when applicable.

## Key References
- `.clinerules/developer_guidelines.md` — detailed contributor expectations.
- `.clinerules/workflows/github-issue-resolution.md` — issue handling process.
- `CONTRIBUTING.md` — community guidelines.
- `src/DemoApp/wwwroot/llms-ctx.md` — shareable AI documentation context.
