# Phase 5: Polish & Developer Experience

**Status:** In Progress (Tasks 5.1, 5.2, 5.3, 5.4, 5.5, 5.6 Complete; Tasks 5.7, 5.8 Pending)
**Prerequisites:** Phases 1-4 complete
**Priority:** P2
**Effort:** M (10-16 hours)

---

## Objectives

1. Add debounced input support for search/autocomplete scenarios
2. Convert JavaScript to lazy-loaded ES modules for faster initial loads
3. Create comprehensive documentation for all new features
4. Write migration guide and prepare for release

---

## 5.1 Debounced Input Support

### Problem Statement

```csharp
// Current: Every keystroke triggers ValueChanged immediately
<TextInput @bind-Value="SearchQuery" />  // Fires on every character

// Desired: Wait for user to stop typing
<TextInput @bind-Value="SearchQuery" DebounceDelay="300" />  // Fires after 300ms pause
```

This causes excessive API calls in search/autocomplete scenarios, degrading performance and user experience.

### Tasks

- [x] Create `InputBehavior` enum with XML documentation
- [x] Create `Debouncer` class with cancellation support (standalone utility in `Utilities/`)
- [x] Add `DebounceDelay` parameter to `TextInput`
- [x] Add `Behavior` parameter to `TextInput`
- [x] Implement debouncing in `TextInput.razor.cs`
- [x] Ensure proper disposal to prevent memory leaks

### Code Examples

#### InputBehavior Enum

```csharp
// Common/InputBehavior.cs
namespace Flowbite.Common;

/// <summary>
/// Specifies when input components fire their ValueChanged event.
/// </summary>
public enum InputBehavior
{
    /// <summary>
    /// Fire on every keystroke (subject to DebounceDelay if configured).
    /// Use this for search-as-you-type scenarios.
    /// </summary>
    OnInput,

    /// <summary>
    /// Fire only on blur or when Enter is pressed.
    /// This is the default behavior, matching standard HTML input behavior.
    /// </summary>
    OnChange
}
```

#### TextInput Implementation

```csharp
// TextInput.razor.cs
public partial class TextInput : FlowbiteComponentBase
{
    /// <summary>
    /// Gets or sets when the ValueChanged event fires.
    /// Default is OnChange (fires on blur or Enter).
    /// </summary>
    [Parameter] public InputBehavior Behavior { get; set; } = InputBehavior.OnChange;

    /// <summary>
    /// Gets or sets the debounce delay in milliseconds.
    /// Only applies when Behavior is OnInput. Set to 0 to disable debouncing.
    /// </summary>
    [Parameter] public int DebounceDelay { get; set; }

    private readonly Debouncer _debouncer = new();

    private async Task HandleInputAsync(ChangeEventArgs e)
    {
        if (Behavior is not InputBehavior.OnInput)
            return;

        var value = e.Value?.ToString();

        if (DebounceDelay > 0)
        {
            await _debouncer.DebounceAsync(
                () => UpdateValueAsync(value),
                DebounceDelay);
        }
        else
        {
            await UpdateValueAsync(value);
        }
    }

    public void Dispose()
    {
        _debouncer.Dispose();
    }
}
```

#### Debouncer Class

```csharp
// TextInput.razor.cs (nested class)
private sealed class Debouncer : IDisposable
{
    private CancellationTokenSource? _cts;

    public async Task DebounceAsync(Func<Task> action, int delayMs)
    {
        _cts?.Cancel();
        _cts?.Dispose();
        var cts = _cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(delayMs, cts.Token);
            await action();
        }
        catch (OperationCanceledException) { }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}
```

#### Usage Example

```razor
@* Search with debouncing *@
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search..." />

@code {
    private string SearchQuery { get; set; } = "";

    // ValueChanged fires 300ms after user stops typing
}
```

### Acceptance Criteria

- [x] `DebounceDelay="300"` delays input by 300ms
- [x] Typing during delay cancels previous pending call
- [x] `Behavior="OnInput"` required for debouncing to work
- [x] `Behavior="OnChange"` (default) fires on blur only
- [x] Component disposes cleanly (no memory leaks)
- [x] Navigate away mid-debounce produces no errors

### Unit Test Example

```csharp
// DebouncerTests.cs
public class DebouncerTests
{
    [Fact]
    public async Task Debounce_FiresOnceAfterDelay()
    {
        var callCount = 0;
        var debouncer = new Debouncer();

        // Rapid calls
        await debouncer.DebounceAsync(_ => { callCount++; return Task.CompletedTask; }, "a", 50);
        await debouncer.DebounceAsync(_ => { callCount++; return Task.CompletedTask; }, "b", 50);
        await debouncer.DebounceAsync(_ => { callCount++; return Task.CompletedTask; }, "c", 50);

        await Task.Delay(100);

        callCount.Should().Be(1);
    }

    [Fact]
    public async Task Debounce_CancelsOnDispose()
    {
        var callCount = 0;
        var debouncer = new Debouncer();

        _ = debouncer.DebounceAsync(_ => { callCount++; return Task.CompletedTask; }, "a", 100);
        debouncer.Dispose();

        await Task.Delay(150);

        callCount.Should().Be(0);
    }
}
```

---

## 5.2 Lazy JavaScript Module Loading

### Problem Statement

```javascript
// Current: Global namespace loaded immediately
window.Flowbite = {
    copyToClipboard: async function(content) { ... },
    highlightCode: async function(element) { ... }
};
// All code loaded upfront, even if never used
```

This increases initial bundle size and slows down page load, even when features are never used.

### Tasks

- [x] Convert `flowbite.js` to ES module exports
- [x] Create wrapper services with `Lazy<Task<IJSObjectReference>>`
- [x] Update components to use lazy-loaded modules
- [x] Implement `IAsyncDisposable` for proper cleanup
- [x] Remove global `window.Flowbite` namespace (legacy kept for backward compatibility)

### Code Examples

#### ES Module (clipboard.js)

```javascript
// wwwroot/js/clipboard.js (ES module)
export async function copyToClipboard(content) {
    if (!navigator.clipboard) {
        return fallbackCopy(content);
    }
    await navigator.clipboard.writeText(content);
    return true;
}

function fallbackCopy(content) {
    const textArea = document.createElement('textarea');
    textArea.value = content;
    textArea.style.cssText = 'position:fixed;opacity:0';
    document.body.appendChild(textArea);
    textArea.select();
    const success = document.execCommand('copy');
    document.body.removeChild(textArea);
    return success;
}
```

#### ClipboardService with Lazy Loading

```csharp
// Services/ClipboardService.cs
public class ClipboardService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public ClipboardService(IJSRuntime js)
    {
        _moduleTask = new(() => js.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Flowbite/js/clipboard.js").AsTask());
    }

    public async Task<bool> CopyToClipboardAsync(string content)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("copyToClipboard", content);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
```

### Files to Create/Modify

| File | Action | Description |
|------|--------|-------------|
| `wwwroot/js/clipboard.js` | Create | ES module for clipboard operations |
| `wwwroot/js/element.js` | Create | Consolidated element utility functions |
| `Services/ClipboardService.cs` | Create | Lazy wrapper for clipboard module |
| `Services/SyntaxHighlightService.cs` | Create | Lazy wrapper for Prism (if kept) |
| `ServiceCollectionExtensions.cs` | Modify | Register new services |

### Acceptance Criteria

- [x] JS modules loaded only when first used
- [x] No global `window.Flowbite` namespace required (legacy kept for backward compatibility)
- [x] Services properly dispose modules on app shutdown
- [x] No regression in functionality
- [x] Network tab shows on-demand module loading

---

## 5.3 Feature Documentation

### Documentation Structure

Update README and create supplementary documentation covering all new features.

### README Updates Checklist

- [x] Tailwind v4 requirements documented
- [x] Installation instructions updated
- [x] Primary color customization with `@theme` examples
- [x] Slot system documented with clear examples
- [x] Debounced input usage examples
- [x] @floating-ui behavior explained (auto-flip, shift)
- [x] Animation state machine behavior explained
- [x] Breaking changes highlighted

### Customization Guide Content

```markdown
## Customization Guide

### Primary Color (Recommended)

Flowbite Blazor uses the Flowbite design system. You can customize your brand's
primary color via Tailwind v4's `@theme` directive:

```css
/* Your app.css */
@import "tailwindcss";

@theme {
  --color-primary-50: #eff6ff;
  --color-primary-100: #dbeafe;
  --color-primary-200: #bfdbfe;
  --color-primary-300: #93c5fd;
  --color-primary-400: #60a5fa;
  --color-primary-500: #3b82f6;  /* Your brand blue */
  --color-primary-600: #2563eb;
  --color-primary-700: #1d4ed8;
  --color-primary-800: #1e40af;
  --color-primary-900: #1e3a8a;
  --color-primary-950: #172554;
}
```

### Layout Customization (Slots)

For structural changes (spacing, borders, rounded corners), use the `Slots` parameter:

```razor
<Card Slots="@(new SlotCollection { Root = "rounded-xl shadow-lg" })">
    Content here
</Card>

<AccordionItem Slots="@(new AccordionItemSlots { Trigger = "hover:bg-blue-100" })">
    Accordion content
</AccordionItem>
```

**Note:** Slots are for layout adjustments only. For color changes, customize
your primary color via `@theme` or use Tailwind utility classes.

### What You Cannot Customize (By Design)

Flowbite Blazor intentionally maintains the Flowbite design system. The following
are NOT customizable to ensure design consistency:

- Secondary, success, warning, danger colors (use Flowbite's built-in colors)
- Typography scales (use standard Tailwind)
- Component shadows (part of Flowbite design)
```

---

## 5.4 AI Documentation Updates (llms-docs)

### Problem Statement

The `llms-ctx.md` file (generated from files in `src/DemoApp/wwwroot/llms-docs/`) provides AI assistants with context about Flowbite Blazor. This context is outdated and doesn't reflect the new features from Phases 1-5:

- TailwindMerge integration and `MergeClasses()` pattern
- Slot system for component customization
- Floating UI positioning for Dropdown/Tooltip/Combobox
- Debounced input for TextInput
- Animation state machine for collapse components
- Lazy-loaded JavaScript services
- Tailwind v4 configuration

### Tasks

- [x] Update `project.md` with current version and new features overview
- [x] Update `04-quickstart.md` with Tailwind v4 setup and service registration
- [x] Update `03-patterns.md` with new patterns:
  - [x] TailwindMerge and `MergeClasses()` pattern
  - [x] Slot system usage pattern
  - [x] Debounced input pattern
  - [x] Lazy service injection pattern
- [x] Update component sections with new parameters:
  - [x] `01-07-dropdown.md` - Slots, Floating UI behavior, keyboard navigation
  - [x] `01-12-tooltip.md` - Floating UI, keyboard navigation, Theme parameter
  - [x] `01-14-forms.md` - TextInput debouncing (Behavior, DebounceDelay)
  - [x] `01-09-sidebar.md` - SidebarCollapse animation states
  - [x] `01-06-card.md` - CardSlots
  - [x] `01-16-modal.md` - ModalSlots
- [x] Update `01-05-button.md` with Variant parameter (renamed from Style)
- [x] Verify `llms-ctx.md` regenerates correctly on build

### Files to Modify

| File | Changes |
|------|---------|
| `llms-docs/project.md` | Version, features list, Tailwind v4 requirement |
| `llms-docs/sections/03-patterns.md` | TailwindMerge, Slots, debouncing patterns |
| `llms-docs/sections/04-quickstart.md` | Tailwind v4 setup, AddFlowbite() services |
| `llms-docs/sections/01-05-button.md` | Variant parameter (was Style) |
| `llms-docs/sections/01-06-card.md` | CardSlots parameter |
| `llms-docs/sections/01-07-dropdown.md` | DropdownSlots, Floating UI, keyboard nav |
| `llms-docs/sections/01-09-sidebar.md` | CollapseState, animation behavior |
| `llms-docs/sections/01-12-tooltip.md` | Theme parameter, Floating UI |
| `llms-docs/sections/01-14-forms.md` | TextInput Behavior, DebounceDelay |
| `llms-docs/sections/01-16-modal.md` | ModalSlots parameter |

### Acceptance Criteria

- [x] All new Phase 1-5 features documented in llms-docs
- [x] Code examples use current API (not deprecated Style parameter)
- [x] Tailwind v4 configuration clearly explained
- [x] Service registration includes all new services
- [x] `llms-ctx.md` regenerates without errors
- [x] AI assistants can use new features from context alone

---

## 5.5 DemoApp Getting-Started Pages Update

### Problem Statement

The DemoApp getting-started pages (`IntroductionPage.razor` and `QuickstartPage.razor`) contain outdated information that doesn't reflect the current Flowbite Blazor setup:

**IntroductionPage.razor issues:**
- Shows Tailwind v3 `tailwind.config.js` configuration instead of v4 CSS-first approach
- Missing new features (Slots, debouncing, Floating UI, animation state machine, lazy services)
- Outdated feature descriptions

**QuickstartPage.razor issues:**
- Uses Tailwind v3.4.15 download URLs (should be v4.x)
- Missing `AddFlowbite()` service registration in Program.cs example
- Missing Floating UI script reference
- Tailwind configuration shows v3 JavaScript syntax instead of v4 CSS

### Tasks

#### IntroductionPage.razor
- [x] Update Tailwind configuration example from v3 JS to v4 CSS-first
- [x] Update features list to include new Phase 1-5 features:
  - [x] TailwindMerge class conflict resolution
  - [x] Slot system for component customization
  - [x] Floating UI positioning
  - [x] Debounced input for TextInput
  - [x] Animation state machine for collapse
  - [x] Lazy-loaded JavaScript services
- [x] Update any version references to current v0.2.x-beta
- [x] Ensure code examples use current API

#### QuickstartPage.razor
- [x] Update Tailwind CSS download URL to v4.x
- [x] Add `AddFlowbite()` service registration to Program.cs example
- [x] Add Floating UI CDN script reference
- [x] Update Tailwind configuration from v3 JS to v4 CSS-first approach:
  - [x] Show `@import "tailwindcss"` directive
  - [x] Show `@theme` block for primary color
  - [x] Show `@source` directive for content paths
  - [x] Show `@plugin` directive for flowbite
- [x] Update `tailwind.config.js` to minimal v4 version (just `darkMode: 'class'`)
- [x] Update any version references

### Files to Modify

| File | Changes |
|------|---------|
| `src/DemoApp/Pages/Docs/getting-started/IntroductionPage.razor` | Tailwind v4 config, new features list |
| `src/DemoApp/Pages/Docs/getting-started/QuickstartPage.razor` | Tailwind v4 URLs, AddFlowbite(), Floating UI |

### Acceptance Criteria

- [x] IntroductionPage shows Tailwind v4 CSS-first configuration
- [x] IntroductionPage lists all Phase 1-5 features
- [x] QuickstartPage uses Tailwind v4 download URLs
- [x] QuickstartPage includes `builder.Services.AddFlowbite()` in Program.cs example
- [x] QuickstartPage includes Floating UI script reference
- [x] QuickstartPage shows v4 CSS `@theme` configuration instead of v3 JS
- [x] Both pages render correctly in DemoApp
- [x] Code examples compile and match current API

---

## 5.6 Integration Tests

### Overview

This task introduces automated testing to the Flowbite Blazor project, establishing the test infrastructure for both unit tests (bUnit) and E2E integration tests (Playwright).

### Test Project Structure

```
src/Flowbite.Tests/
├── Flowbite.Tests.csproj          # Test project (net9.0 only)
├── CLAUDE.md                       # AI guidance for test development
├── GlobalUsings.cs
├── TestSetup/
│   └── FlowbiteTestContext.cs     # Base test context with service setup
├── Utilities/
│   ├── DebouncerTests.cs          # Debouncer class tests
│   └── ElementClassTests.cs       # ElementClass builder tests
├── Services/
│   └── TailwindMergeTests.cs      # TailwindMerge integration tests
├── Components/
│   ├── CollapseStateTests.cs      # Collapse animation state tests
│   └── Forms/
│       ├── TextInputTests.cs      # TextInput happy-path tests
│       ├── TextAreaTests.cs       # TextArea happy-path tests
│       └── SelectTests.cs         # Select happy-path tests
└── Integration/
    ├── PlaywrightFixture.cs       # Shared Playwright setup/teardown
    └── DemoAppSmokeTests.cs       # Golden example E2E test
```

### Dependencies

| Package | Purpose | Version |
|---------|---------|---------|
| `Microsoft.NET.Test.Sdk` | Test SDK runner | 17.11.1 |
| `xunit` | Testing framework | 2.9.2 |
| `xunit.runner.visualstudio` | VS Test Explorer | 2.8.2 |
| `bunit` | Blazor component testing | 1.35.10 |
| `FluentAssertions` | Readable assertions | 6.12.2 |
| `coverlet.collector` | Code coverage | 6.0.2 |
| `Microsoft.Playwright` | E2E browser testing | 1.49.0 |

### Tasks

- [x] Create `src/Flowbite.Tests/Flowbite.Tests.csproj` targeting net9.0
- [x] Add test project to `FlowbiteBlazor.sln`
- [x] Create `CLAUDE.md` with AI guidance for future test development
- [x] Create `GlobalUsings.cs` with common imports
- [x] Create `FlowbiteTestContext.cs` base class with Flowbite services pre-configured
- [x] Create `PlaywrightFixture.cs` for E2E test infrastructure

#### Unit Tests - Utilities
- [x] Implement `DebouncerTests.cs` (4 test cases)
- [x] Implement `ElementClassTests.cs` (8 test cases - expanded)

#### Unit Tests - Services
- [x] Implement `TailwindMergeTests.cs` (4 test cases - expanded)

#### Unit Tests - Components
- [x] Implement `CollapseStateTests.cs` (3 test cases - expanded)

#### Unit Tests - Form Components (Happy Path)
- [x] Implement `TextInputTests.cs` (9 test cases - expanded)
- [x] Implement `TextAreaTests.cs` (8 test cases - expanded)
- [x] Implement `SelectTests.cs` (6 test cases - expanded)

#### Integration Tests - Playwright
- [x] Implement `DemoAppSmokeTests.cs` - Golden example E2E test (2 tests)

#### Build Integration
- [x] Add `python build.py test` command for unit tests
- [x] Add `python build.py test-integration` command for E2E tests

### Test Case Details

#### DebouncerTests
| Test | Description |
|------|-------------|
| `Debounce_FiresOnceAfterDelay` | Rapid calls → only 1 callback after delay |
| `Debounce_CancelsOnDispose` | Dispose mid-delay → no callback fires |
| `Debounce_CancelsPreviousCallOnRapidCalls` | New call cancels pending call |
| `Debounce_DoesNotFireWhenDisposedBeforeDelay` | Early dispose prevents execution |

#### ElementClassTests
| Test | Description |
|------|-------------|
| `Empty_ReturnsEmptyString` | Fresh instance → empty string |
| `Add_SingleClass_ReturnsClass` | Single add works |
| `Add_MultipleClasses_CombinesClasses` | Chained adds combine |
| `Add_ConditionalTrue_IncludesClass` | `when: true` includes class |
| `Add_ConditionalFalse_ExcludesClass` | `when: false` excludes class |
| `ImplicitConversion_WorksCorrectly` | Assigns to string variable |

#### TailwindMergeTests
| Test | Description |
|------|-------------|
| `MergeClasses_ResolvesConflicts` | `p-2 p-4` → `p-4` |
| `MergeClasses_PreservesNonConflicting` | `p-2 m-4` → both preserved |
| `MergeClasses_HandlesNullAndEmpty` | Graceful handling of edge cases |

#### CollapseStateTests
| Test | Description |
|------|-------------|
| `CollapseState_HasAllRequiredValues` | Enum has Collapsed, Expanding, Expanded, Collapsing |

#### TextInputTests (Happy Path)
| Test | Description |
|------|-------------|
| `TextInput_RendersWithDefaultAttributes` | Basic rendering works |
| `TextInput_BindsValueCorrectly` | Two-way binding works |
| `TextInput_AppliesCustomClass` | Class parameter merges |
| `TextInput_SupportsPlaceholder` | Placeholder renders |
| `TextInput_SupportsDisabledState` | Disabled attribute renders |

#### TextAreaTests (Happy Path)
| Test | Description |
|------|-------------|
| `TextArea_RendersWithDefaultAttributes` | Basic rendering works |
| `TextArea_BindsValueCorrectly` | Two-way binding works |
| `TextArea_AppliesCustomClass` | Class parameter merges |
| `TextArea_SupportsRows` | Rows attribute works |

#### SelectTests (Happy Path)
| Test | Description |
|------|-------------|
| `Select_RendersWithOptions` | Options render correctly |
| `Select_BindsSelectedValue` | Selection binding works |
| `Select_AppliesCustomClass` | Class parameter merges |

#### DemoAppSmokeTests (Golden Example)
| Test | Description |
|------|-------------|
| `HomePage_LoadsSuccessfully` | Navigate to `/`, verify title and key elements |

This test serves as the **template** for future Playwright tests, demonstrating:
- Fixture-based DemoApp lifecycle management
- Page navigation pattern
- Element assertion pattern
- Screenshot capture for debugging

### Acceptance Criteria

- [x] Test project compiles without warnings
- [x] Test project added to FlowbiteBlazor.sln
- [x] `python build.py test` runs unit tests successfully
- [x] All DebouncerTests pass (4 tests)
- [x] All ElementClassTests pass (8 tests)
- [x] All TailwindMergeTests pass (4 tests)
- [x] CollapseStateTests pass (3 tests)
- [x] All TextInputTests pass (9 tests)
- [x] All TextAreaTests pass (8 tests)
- [x] All SelectTests pass (6 tests)
- [x] DemoAppSmokeTests passes when DemoApp is running (2 tests)
- [x] `CLAUDE.md` provides comprehensive guidance for future test development
- [x] `python build.py test-integration` command works correctly

### Test Scenarios Summary

| Category | Scenario | Test | Expected Result |
|----------|----------|------|-----------------|
| Utilities | Class conflict | `p-2 p-4` merge | `p-4` only |
| Utilities | Debounce rapid | 3 calls in 50ms | 1 callback |
| Utilities | Debounce dispose | Dispose mid-delay | No callback |
| Utilities | ElementClass conditional | `Add("x", false)` | Class excluded |
| Forms | TextInput binding | Set value | Value reflected in input |
| Forms | TextArea rows | `Rows="5"` | textarea has rows="5" |
| Forms | Select binding | Select option | Value bound correctly |
| E2E | DemoApp smoke | Navigate to `/` | Page loads, title correct |

---

## 5.7 Migration Guide Review

### Purpose

Review and finalize the MIGRATION.md file created during Task 5.3 to ensure it is complete and accurate.

### Checklist

- [ ] All breaking changes documented
- [ ] Migration steps are clear and actionable
- [ ] Code examples compile and work
- [ ] FAQ covers common questions
- [ ] Version numbers are correct

### Content Reference

The MIGRATION.md file should include:

```markdown
# Migration Guide: v0.1.x to v1.0.0

## Prerequisites

- Tailwind CSS v4.x (upgraded from v3)
- .NET 8 or .NET 9

## Step 1: Update Tailwind CSS

### Before (Tailwind v3)

```js
// tailwind.config.js
module.exports = {
  content: ['./**/*.razor'],
  theme: { extend: { colors: { primary: { /* ... */ } } } },
  plugins: [require('flowbite/plugin')]
}
```

### After (Tailwind v4)

```css
/* app.css */
@import "tailwindcss";
@plugin "flowbite/plugin";

@theme {
  --color-primary-500: #3b82f6;
  /* ... full 50-950 scale */
}
```

## Step 2: Update Package References

```xml
<PackageReference Include="Flowbite" Version="1.0.0" />
```

## Breaking Changes

### 1. TailwindMerge Class Conflict Resolution

**What changed:** TailwindMerge now resolves conflicting Tailwind classes.

**Before:** `Class="p-4"` + component's `p-2` rendered both `p-2 p-4`
**After:** `Class="p-4"` + component's `p-2` rendered `p-4` only

**Impact:** User classes now properly override component defaults.

**Action:** Review any custom classes that relied on both appearing in the output.

### 2. Floating Element Positioning (@floating-ui)

**What changed:** Dropdowns, tooltips, and popovers now use @floating-ui/dom.

**Impact:** Elements auto-flip when near viewport edges and shift to stay visible.

**Before (CSS-only):** No repositioning; elements could overflow viewport
**After (@floating-ui):** Smart positioning with automatic flip and shift

**Action:** Test floating elements; behavior may differ near screen edges.

**To restore legacy behavior (not recommended):**
```razor
<Dropdown DisableFlip="true" DisableShift="true">
```

### 3. Collapse Animation State Machine

**What changed:** Accordions and sidebars now animate height smoothly.

**Impact:** Visual improvement; functionality unchanged.

**Action:** None required unless you had custom collapse styling.

### 4. Slot System (New Feature - Non-Breaking)

**What changed:** Components now support customization via Slots parameter.

**Impact:** None (additive feature).

**Usage:**
```razor
<AccordionItem Slots="@(new AccordionItemSlots { Trigger = "hover:bg-blue-100" })">
```

### 5. Debounced Input (New Feature - Non-Breaking)

**What changed:** TextInput now supports debounced input for search scenarios.

**Impact:** None (additive feature).

**Usage:**
```razor
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300" />
```

## FAQ

**Q: My dropdown appears in a different position than before.**
A: @floating-ui now auto-flips to stay in viewport. This is expected behavior and improves UX on smaller screens.

**Q: Can I customize secondary/success/danger colors?**
A: No, these are part of the Flowbite design system. Customize your primary brand color via `@theme`, and use the built-in semantic colors for consistency.

**Q: The collapse animation is instant, not smooth.**
A: Check if you or your users have "Reduce motion" enabled in OS settings. The library respects this accessibility preference via `prefers-reduced-motion`.

**Q: TailwindMerge removed a class I needed.**
A: TailwindMerge resolves conflicts by keeping the last class. If you need both (rare), use arbitrary values: `p-[8px]` instead of `p-2`.

**Q: How do I migrate my custom Tailwind config?**
A: See the Tailwind v4 migration guide at https://tailwindcss.com/docs/upgrade-guide. Key changes:
- `tailwind.config.js` replaced by CSS `@theme` directive
- `require()` plugins replaced by `@plugin`
- Theme extensions now use CSS custom properties

**Q: Will my existing component classes break?**
A: Standard Tailwind classes work the same. Only conflicting classes (like `p-2 p-4`) are now resolved instead of both being applied.
```

---

## 5.8 Release Preparation

### Pre-Release Checklist

#### Code Quality
- [ ] All unit tests pass
- [ ] All integration tests pass
- [ ] No compiler warnings
- [ ] Code reviewed by team member

#### Documentation
- [ ] README updated with new features
- [ ] MIGRATION.md complete and reviewed
- [ ] CHANGELOG.md updated with all changes
- [ ] API documentation generated

#### Package
- [ ] Version bumped to 1.0.0
- [ ] NuGet description updated
- [ ] Package dependencies correct
- [ ] License file included
- [ ] Package icon updated (if applicable)

#### Testing
- [ ] Sample app runs successfully
- [ ] Tailwind v4 integration tested
- [ ] Dark mode works correctly
- [ ] Floating elements position correctly
- [ ] Animations smooth (and respect motion-reduce)
- [ ] Debouncing works as expected
- [ ] JS modules lazy-load correctly

#### Release
- [ ] Git tag created (v1.0.0)
- [ ] GitHub release drafted with release notes
- [ ] NuGet package pushed
- [ ] Announcement prepared

---

## Testing Checklist

### Debouncing
- [ ] Type "hello" quickly -> only one ValueChanged after delay
- [ ] Type, pause, type more -> two ValueChanged calls
- [ ] Navigate away mid-debounce -> no errors
- [ ] Dispose component mid-debounce -> no memory leaks

### Lazy Loading
- [ ] Initial page load -> no clipboard.js request
- [ ] Click copy button -> clipboard.js loaded
- [ ] Navigate away -> module disposed
- [ ] Multiple clicks -> module loaded only once

### Documentation
- [ ] New user can implement Slots from docs alone
- [ ] Migration guide covers all breaking changes
- [ ] Demo app runs without errors
- [ ] All code examples compile and work

### Integration
- [ ] Full app works with Tailwind v4
- [ ] All components render correctly
- [ ] Accessibility features intact
- [ ] Performance acceptable

---

## Definition of Done

Phase 5 is complete when:

1. [x] Debounced input working with cancellation
2. [x] JS modules lazy-loaded
3. [x] All services implement `IAsyncDisposable`
4. [x] MIGRATION.md complete and accurate
5. [x] README updated with all new features
6. [x] AI documentation (llms-docs) updated with Phase 1-5 features
7. [x] DemoApp getting-started pages updated with Tailwind v4 and new features
8. [x] All tests passing (unit and integration) - 45 unit tests, 2 integration tests
9. [ ] Migration guide reviewed and finalized
10. [ ] Release checklist completed
11. [ ] Sample application demonstrates all features

---

## Success Metrics

| Metric | Target | Measurement |
|--------|--------|-------------|
| Class conflicts | 0 reports | GitHub issues |
| Viewport overflow | 0 reports | GitHub issues |
| Tailwind v4 upgrade | Documented | Migration guide exists |
| Accessibility | motion-reduce respected | Manual testing |
| Collapse animations | Smooth | Visual inspection |
| Initial bundle size | Reduced | Lighthouse audit |
| Migration success | <30 min upgrade | User feedback |

---

## Post-Release

After v1.0.0 release:

1. **Monitor** - Watch GitHub issues for upgrade problems
2. **Respond** - Answer migration questions promptly
3. **Hotfix** - Address critical bugs with 1.0.x releases
4. **Plan** - Identify v1.1.0 features based on feedback
5. **Celebrate** - Acknowledge team effort and contributors

### Potential v1.1.0 Features
- Additional lazy-loaded modules (if not all completed)
- Virtual scrolling for large lists
- Additional input debounce options
- Performance optimizations based on real-world usage
