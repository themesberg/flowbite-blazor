# Phase 5: Polish & Developer Experience

**Status:** Not Started
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

- [ ] Create `InputBehavior` enum with XML documentation
- [ ] Create `Debouncer` nested class with cancellation support
- [ ] Add `DebounceDelay` parameter to `TextInput`
- [ ] Add `Behavior` parameter to `TextInput`
- [ ] Implement debouncing in `TextInput.razor.cs`
- [ ] Ensure proper disposal to prevent memory leaks

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

- [ ] `DebounceDelay="300"` delays input by 300ms
- [ ] Typing during delay cancels previous pending call
- [ ] `Behavior="OnInput"` required for debouncing to work
- [ ] `Behavior="OnChange"` (default) fires on blur only
- [ ] Component disposes cleanly (no memory leaks)
- [ ] Navigate away mid-debounce produces no errors

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

- [ ] Convert `flowbite.js` to ES module exports
- [ ] Create wrapper services with `Lazy<Task<IJSObjectReference>>`
- [ ] Update components to use lazy-loaded modules
- [ ] Implement `IAsyncDisposable` for proper cleanup
- [ ] Remove global `window.Flowbite` namespace

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

- [ ] JS modules loaded only when first used
- [ ] No global `window.Flowbite` namespace required
- [ ] Services properly dispose modules on app shutdown
- [ ] No regression in functionality
- [ ] Network tab shows on-demand module loading

---

## 5.3 Feature Documentation

### Documentation Structure

Update README and create supplementary documentation covering all new features.

### README Updates Checklist

- [ ] Tailwind v4 requirements documented
- [ ] Installation instructions updated
- [ ] Primary color customization with `@theme` examples
- [ ] Slot system documented with clear examples
- [ ] Debounced input usage examples
- [ ] @floating-ui behavior explained (auto-flip, shift)
- [ ] Animation state machine behavior explained
- [ ] Breaking changes highlighted

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

## 5.4 Integration Tests

### Test Examples

#### TailwindMergeTests

```csharp
// TailwindMergeTests.cs
public class TailwindMergeTests : TestContext
{
    public TailwindMergeTests()
    {
        Services.AddTailwindMerge();
    }

    [Fact]
    public void MergeClasses_ResolvesConflicts()
    {
        var twMerge = Services.GetRequiredService<TwMerge>();

        var result = twMerge.Merge("p-2 p-4 text-red-500 text-blue-500");

        result.Should().Be("p-4 text-blue-500");
    }

    [Fact]
    public void MergeClasses_PreservesNonConflicting()
    {
        var twMerge = Services.GetRequiredService<TwMerge>();

        var result = twMerge.Merge("p-2 m-4 text-red-500");

        result.Should().Contain("p-2");
        result.Should().Contain("m-4");
        result.Should().Contain("text-red-500");
    }
}
```

#### DebouncerTests

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
}
```

#### CollapseStateTests

```csharp
// CollapseStateTests.cs
public class CollapseStateTests : TestContext
{
    [Fact]
    public void Collapse_TransitionsToExpanding_WhenExpandedTrue()
    {
        var cut = RenderComponent<Collapse>(p => p
            .Add(c => c.Expanded, false));

        cut.SetParametersAndRender(p => p.Add(c => c.Expanded, true));

        cut.Instance.State.Should().Be(CollapseState.Expanding);
    }

    [Fact]
    public void Collapse_TransitionsToCollapsing_WhenExpandedFalse()
    {
        var cut = RenderComponent<Collapse>(p => p
            .Add(c => c.Expanded, true));

        cut.SetParametersAndRender(p => p.Add(c => c.Expanded, false));

        cut.Instance.State.Should().Be(CollapseState.Collapsing);
    }
}
```

### Test Scenarios

| Scenario | Test | Expected Result |
|----------|------|-----------------|
| Class conflict | `p-2 p-4` merge | `p-4` only |
| Debounce rapid | 3 calls in 50ms | 1 callback |
| Debounce dispose | Dispose mid-delay | No callback |
| Collapse expand | Set Expanded=true | State = Expanding |
| motion-reduce | prefers-reduced-motion | No animation |

---

## 5.5 Migration Guide

Create `MIGRATION.md` in repository root with the following content:

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

## 5.6 Release Preparation

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

1. [ ] Debounced input working with cancellation
2. [ ] JS modules lazy-loaded
3. [ ] All services implement `IAsyncDisposable`
4. [ ] MIGRATION.md complete and accurate
5. [ ] README updated with all new features
6. [ ] All tests passing (unit and integration)
7. [ ] Release checklist completed
8. [ ] Sample application demonstrates all features

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
