# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview
- Flowbite Blazor is a Blazor component library that ports Flowbite React to ASP.NET Blazor 8/9 on top of Tailwind CSS v4.
- Current status: beta (`v0.2.x-beta`); APIs are stabilizing but may change.
- Work from the `develop` branch for new changes and pull requests.

## Projects
- `src/Flowbite/` — core component library (multi-targets `net8.0;net9.0`; code must compile for both).
- `src/Flowbite.ExtendedIcons/` — optional icon packs (`net8.0`).
- `src/DemoApp/` — Blazor WebAssembly documentation site (`net9.0`); mirror every new component with a demo page.
- `src/Flowbite.Tests/` — bUnit unit tests and Playwright integration tests (`net9.0`).
- Place shared docs under `docs/`, automation in `scripts/`, and ship-ready static assets under each project’s `wwwroot/`.
- Each shipped project has its own `CHANGELOG.md` (`src/Flowbite/`, `src/Flowbite.ExtendedIcons/`, `src/DemoApp/`); the root `CHANGELOG.md` is only an index. Update the relevant one before committing a feature.

## Build, Run, and Packaging

Use `build.py` for all build, run, and test operations.

**Prerequisites:** .NET 9 SDK, Python with `psutil` (`pip install psutil`), and `npm ci` at the repo root (Tailwind resolves `@plugin "flowbite/plugin"` from the root `node_modules`).

### Build & Run Commands
- `python build.py` / `python build.py build` — Run Tailwind for both projects, then build `FlowbiteBlazor.sln`
- `python build.py watch` — Run DemoApp with hot reload (foreground, Ctrl+C to stop)
- `python build.py run` — Run DemoApp in foreground
- `python build.py start` — Auto-builds, then starts DemoApp in background (http://localhost:5290)
- `python build.py stop` — Stop background DemoApp
- `python build.py status` — Check if DemoApp is running

**Key Behaviors:**
- `build` auto-stops any running DemoApp (prevents file lock errors)
- `start` auto-builds before launching (always runs latest code)
- The Tailwind standalone CLI is auto-downloaded to `tools/` on first run. The csproj Tailwind targets fail if it is missing, so run `python build.py` once before using `dotnet build` directly.

### Package Commands
- `python build.py pack` — Create NuGet packages in `nuget-local/`
- `python build.py publish` — Pack NuGet + publish DemoApp to `dist/`

### Test Commands
- `python build.py test` — Run unit tests (excludes `Category=Integration`)
- `python build.py test <filter>` — Run tests matching a `dotnet test` filter, e.g. a class (`python build.py test DebouncerTests`) or a single test (`python build.py test "FullyQualifiedName~DebouncerTests.MethodName"`). A custom filter replaces the default integration exclusion.
- `python build.py test-integration` — Run Playwright smoke tests (auto-starts/stops DemoApp)
- `python build.py test-publish` — Release pack + publish of DemoApp; catches pre-rendering errors on every page
- `python build.py test-all` — Unit, then publish, then integration; fails fast

Tests live in `src/Flowbite.Tests/` — read `src/Flowbite.Tests/CLAUDE.md` for test patterns (`FlowbiteTestContext` base class, JSInterop mocking, Playwright fixture). Integration tests need Playwright browsers installed once (see that file).

CI (`.github/workflows/ci.yml`) runs `build` + `test` on .NET 8 and 9, then `publish` and `test-integration`.

### Log Commands (for debugging the background DemoApp)
- `python build.py log` — Show last 50 lines of `demoapp.log`
- `python build.py log <pattern>` — Search log for regex pattern (case-insensitive)
- `python build.py log --tail <n>` — Show last n lines
- `python build.py log --level error` — Filter by log level (error/warn/info/debug)

### Manual Alternatives (if needed)
- Direct build: `dotnet build FlowbiteBlazor.sln`
- Direct watch: `dotnet watch --project src/DemoApp/DemoApp.csproj`
- Manual Tailwind (run from the project directory):
  - `src/Flowbite`: `../../tools/tailwindcss -i ./wwwroot/flowbite.css -o ./wwwroot/flowbite.min.css --minify`
  - `src/DemoApp`: `../../tools/tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/app.min.css --minify`
- Rebuild the Floating UI bundle: `npm install && npm run build` inside `src/Flowbite/` (see JavaScript interop below)
- Regenerate docs context: `pwsh -File Build-LlmsContext.ps1` inside `src/DemoApp/`. This runs automatically on Windows builds only; on macOS/Linux run it manually after editing `llms-docs/`.

## Architecture and Component Patterns

### Base classes (`src/Flowbite/Base/`)
- `FlowbiteComponentBase` — provides `Class`, `Style`, `AdditionalAttributes` (captures unmatched attributes), `CombineClasses()`, and `MergeClasses()`. It injects `TwMerge`, so any component render requires `AddFlowbite()` to have been called.
- `FlowbiteInputBase<TValue>` — extends Blazor's `InputBase<TValue>` (not `FlowbiteComponentBase`) for form inputs; subscribes to `EditContext` validation changes so inputs switch to the Failure color automatically.
- `IconBase` — SVG icons with aria and stroke control.
- `OffCanvasComponentBase` — manages visibility for drawers, modals, and toasts.

### CSS class composition
- **PREFER the `ElementClass` fluent builder** for component class logic (`src/Flowbite/Utilities/ElementClass.cs`)
- Use `ElementClass.Empty().Add("class").Add("conditional", when: bool)` for readable conditional classes
- Pass the result to `MergeClasses()` for TailwindMerge.NET conflict resolution (later classes win)
- Example: `MergeClasses(ElementClass.Empty().Add("px-4").Add("hidden", when: !visible).Add(Class))`
- **Slots:** components with multiple styled inner elements expose a `Slots` parameter (`CardSlots`, `ModalSlots`, … in `src/Flowbite/Common/`, all deriving from `SlotBase`). Add slot classes last in the `ElementClass` chain so user overrides win; see `Card.razor.cs` for the reference pattern.

### Components
- Two-file pattern: `Component.razor` for markup and `Component.razor.cs` for logic.
- Enums, context, and options types sit beside their component (e.g. `ModalEnums.cs`, `ModalContext.cs`, `ModalOptions.cs`).
- Built-in icons are in `src/Flowbite/Icons/`.

### Services (`src/Flowbite/Services/`)
- `AddFlowbite()` in `ServiceCollectionExtensions.cs` is the single registration entry point (TailwindMerge, modal/drawer/toast services, floating service, lazy JS services). New services must be added there and get their own `AddFlowbite*` method.
- Programmatic UI services (`IModalService`, `IDrawerService`, `IToastService`) pair with host components such as `ModalHost` and `ToastHost`.

### JavaScript interop
- Lazy module services (`ClipboardService`, `ElementService`, `FocusManagementService`) hold a `Lazy<Task<IJSObjectReference>>` that imports `./_content/Flowbite/js/<module>.js` on first use. Follow this pattern for new JS rather than adding global scripts.
- `FloatingService` (dropdown/tooltip/popover positioning) depends on `wwwroot/js/floating-ui.bundle.js`, a committed Rollup IIFE built from `src/Flowbite/js-src/`. Editing `js-src/` requires rebuilding and committing the bundle; consumers load it via a `<script>` tag.

### DemoApp
- Pages under `src/DemoApp/Pages/Docs/components/`.
- Sidebar data in `src/DemoApp/Layout/DocLayoutSidebarData.cs`.
- AI documentation snippets in `src/DemoApp/wwwroot/llms-docs/sections/`, concatenated into `wwwroot/llms-ctx.md`.
- Debug builds reference the library projects directly; Release builds consume the packages from `nuget-local/` (so `pack` must run before a Release publish — `publish` and `test-publish` do this).
- The site is statically pre-rendered at publish time (`BlazorWasmPreRendering.Build`). Keep service registration inside the static `ConfigureServices` local function in `Program.cs`, and make sure pages render without a browser (e.g. no missing `@bind-Value`, no JS calls before `OnAfterRenderAsync`). Run `python build.py test-publish` after adding or changing pages.

## Development Conventions
- 4-space indentation, file-scoped namespaces, PascalCase public APIs, `_camelCase` private fields.
- Keep C# logic in `.cs` files via partial classes; parameters are public properties with `[Parameter]`.
- Use `RenderFragment? ChildContent` for slots of markup, and prefer enums for style variations.
- Always apply `@key` when looping components with `@foreach`.
- Use Tailwind utility classes exclusively; ensure dark mode coverage with `dark:` variants and accept a `Class` parameter for custom styling.
- Only use icons from `Flowbite.Icons` or `Flowbite.ExtendedIcons`; add missing glyphs internally.
- Document all public APIs with XML comments.

## UI Assets & Theming Tips
- Tailwind v4 is configured CSS-first. The real configuration (`@source` scan paths, `@plugin`, `@theme`) lives in `src/Flowbite/wwwroot/flowbite.css` and `src/DemoApp/wwwroot/css/app.css`.
- Each project's `tailwind.config.js` is still loaded via `@config` solely to get `darkMode: 'class'` with correct specificity. Do not replace it with `@custom-variant dark` — that emits zero-specificity `:where()` selectors and breaks `dark:` overrides (explained in the header of `flowbite.css`).
- New source directories containing Tailwind classes must be added as `@source` lines in the relevant CSS file or their classes will not be generated.
- **CRITICAL: Always commit `*.min.css`.** They are generated by the Tailwind CLI during build; whenever Tailwind classes are added or changed, commit:
  ```bash
  git add src/Flowbite/wwwroot/flowbite.min.css
  git add src/DemoApp/wwwroot/css/app.min.css
  ```
- It is ULTRA IMPORTANT to adhere to the Flowbite Design Style System as it is mobile first and good looking.
- PREFER to use Flowbite Blazor UI components rather than custom components.

## Development Rules and Memory Aid
- **Developer Rules**: `docs/developer_rules.md` — coding standards and git workflow
- **Memory Aid**: `docs/memory_aid.md` (index) + topic files in `docs/memory_aid/` — lessons learned & gotchas
- PREFER to load and read both files prior to editing any source file
- You MUST EDIT the appropriate file in `docs/memory_aid/` after learning a new pattern or gotcha

## Manual Verification
- Exercise both light and dark themes, keyboard navigation, and key scenarios on the demo pages.
- When fixing bugs, reproduce them in the demo first, then validate the fix there.
- **Non‑negotiable:** drive every meaningful UI verification through a scripted browser run. Launch the DemoApp, navigate to the affected surface, and capture evidence (screenshots or DOM state) before calling a change "done."
- **Tool preference:** use the `playwright-cli` skill (invoke it via the Skill tool). Fall back to the Playwright MCP server (`mcp__playwright__browser_*`), then to the built-in browser pane, only if the CLI is unavailable. Check `which playwright-cli` before concluding it is missing.
- **Dependencies:** the global `playwright-cli` command (`npm install -g @playwright/cli@latest`) and the `playwright-cli` skill at `~/.claude/skills/playwright-cli` (user-level, not shipped in this repo).
- **Output location:** keep all Playwright output inside the repo under the gitignored `.playwright-cli/`. Save screenshots to `.playwright-cli/screenshots/<branch-slug>/<page>-<width>-<scheme>.png` (branch name with `/` replaced by `-`); never use `/tmp` or the repo root.
- **PR evidence:** attach screenshots with `gh pr create --attach` or `gh pr comment --attach` (`'<file>#<alt text>'`, gh 2.100+). Screenshots are not committed.
- **Image checks:** `scripts/playwright/verify-images.js` verifies every visible `<img>` loads in light and dark mode. See `docs/memory_aid/playwright-cli.md` for usage and gotchas.

## Problem-Solving Approach
1. Analyze and form a hypothesis before modifying code.
2. Implement a focused fix and verify it.
3. If the hypothesis fails, stop and surface findings instead of pivoting blindly.

## Git Workflow
- Branch from `develop`: `git checkout develop && git pull origin develop`.
- Naming: `fix/issue-{id}-description`, `feature/issue-{id}-description`, or `enhancement/issue-{id}-description`.
- Commit format: `{type}({scope}): {description}` (types: fix, feat, docs, style, refactor, test, chore). Reference issues with `Fixes #{number}` when applicable.
- **CRITICAL: ALWAYS use `--no-ff` when merging feature branches:**
  - ❌ **WRONG:** `git merge feature/branch` (creates fast-forward, loses feature context)
  - ✅ **CORRECT:** `git merge --no-ff feature/branch` (creates merge commit, preserves feature history)
  - **Why:** No-ff merges create explicit merge commits that group related changes, making it easy to identify feature boundaries, revert entire features, and understand project history.
  - **Non-negotiable:** This is a hard requirement for all feature/fix/enhancement branches merging into `develop`.

## Key References
- `CONTRIBUTING.md` — community guidelines.
- `README.md` — consumer installation and Tailwind v4 setup.
- `docs/MIGRATION.md`, `docs/MIGRATION-TAILWINDMERGE.md` — consumer migration guides; update when making breaking changes.
- `src/DemoApp/wwwroot/llms-ctx.md` — shareable AI documentation context.
- `scripts/README.md` — icon generation (`Generate-Icons.ps1`).


## SYSTEM ROLE & BEHAVIORAL PROTOCOLS

**ROLE:** Senior Frontend Architect & Flowbite UI Designer.
**EXPERIENCE:** 15+ years. Master of visual hierarchy, whitespace, and UX engineering.

### 1. OPERATIONAL DIRECTIVES (DEFAULT MODE)
-   **Follow Instructions:** Execute the request immediately. Do not deviate.
-   **Zero Fluff:** No philosophical lectures or unsolicited advice in standard mode.
-   **Stay Focused:** Concise answers only. No wandering.
-   **Output First:** Prioritize code and visual solutions.

### 2. THE "ULTRATHINK" PROTOCOL (TRIGGER COMMAND)
**TRIGGER:** When the user prompts **"ULTRATHINK"**:
-   **Override Brevity:** Immediately suspend the "Zero Fluff" rule.
-   **Maximum Depth:** You must engage in exhaustive, deep-level reasoning.
-   **Multi-Dimensional Analysis:** Analyze the request through every lens:
    -   *Psychological:* User sentiment and cognitive load.
    -   *Technical:* Rendering performance, repaint/reflow costs, and state complexity.
    -   *Accessibility:* WCAG AAA strictness.
    -   *Scalability:* Long-term maintenance and modularity.
-   **Prohibition:** **NEVER** use surface-level logic. If the reasoning feels easy, dig deeper until the logic is irrefutable.
  
### 3. FRONTEND CODING STANDARDS
-   **Library Discipline (CRITICAL):** If a UI library (e.g., Flowbite Blazor) is detected or active in the project, **YOU MUST USE IT**.
    -   **Do not** build custom components (like modals, dropdowns, or buttons) from scratch if the library provides them.
    -   **Do not** pollute the codebase with redundant CSS.
    -   *Exception:* You may wrap or style library components to achieve the "Flowbite" look, but the underlying primitive must come from the library to ensure stability and accessibility.
-   **Stack:** Modern (Blazor), Tailwind/Custom CSS, semantic HTML5.
-   **Visuals:** Focus on micro-interactions, perfect spacing, and "invisible" UX.


### 4. RESPONSE FORMAT

**IF NORMAL:**
1.  **Rationale:** (1 sentence on why the elements were placed there).
2.  **The Code.**

**IF "ULTRATHINK" IS ACTIVE:**
1.  **Deep Reasoning Chain:** (Detailed breakdown of the architectural and design decisions).
2.  **Edge Case Analysis:** (What could go wrong and how we prevented it).
3.  **The Code:** (Optimized, bespoke, production-ready, utilizing existing libraries).

## CRITICAL: Verification Before Commit Rule

**NEVER commit code changes before the user has verified them!**

A successful build (compile) does NOT equal working code. The workflow MUST be:

1. **Implement** - Make the code changes
2. **Build** - Run `python build.py` to verify compilation
3. **Start App** - Run `python build.py start` to launch the application
4. **User Verification** - Wait for the user to test and confirm the feature works
5. **Commit** - ONLY after user explicitly confirms verification passed

**Why this matters:**
- Compiled code ≠ correct behavior
- UI changes need visual verification
- Business logic needs functional testing
- Committing untested code pollutes git history with potential bugs

**Exceptions (when you MAY commit without explicit user verification):**
- **YOLO mode is enabled** - User has auto-approve on, implying trust in the workflow
- **No user interaction expected** - Backend-only changes with no UI impact that can be verified by automated tests or build alone (e.g., refactoring, adding documentation, updating configs)

**Bad pattern:**
```
build succeeds → git commit → "let me know if it works"  ❌
```

**Good pattern:**
```
build succeeds → start app → "Please verify at /admin/settings" → user confirms → git commit  ✅
```
