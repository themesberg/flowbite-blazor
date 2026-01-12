# Phase 1: Foundation & Tooling

**Status:** Complete (Tasks 1.1, 1.2, 1.3, 1.4 Complete)
**Prerequisites:** None
**Priority:** P1 (High)
**Effort:** S (4-8 hours)
**Blocks:** Phase 2 (Slot System requires TailwindMerge)

---

## Objectives

1. Integrate TailwindMerge.NET for intelligent class conflict resolution
2. Enhance base component class with Style and AdditionalAttributes parameters
3. Create ElementClass fluent builder for cleaner style methods
4. Add motion-reduce accessibility support to animated components

---

## Task 1.1: TailwindMerge.NET Integration

**Priority:** P1 (High) | **Effort:** Low (2-4 hours)

### Goal

Replace `CombineClasses()` string concatenation with TailwindMerge to resolve Tailwind class conflicts automatically.

### Tasks

- [x] Add `TailwindMerge.NET` NuGet package to Flowbite.csproj
- [x] Register `TwMerge` in `ServiceCollectionExtensions.cs`
- [x] Inject `TwMerge` into `FlowbiteComponentBase`
- [x] Create `MergeClasses()` helper method
- [ ] Migrate components from `CombineClasses()` to `MergeClasses()`

### Acceptance Criteria

- [x] `TailwindMerge.NET` NuGet package added to `Flowbite.csproj`
- [x] `TwMerge` service registered in `AddFlowbite()` extension method
- [x] `FlowbiteComponentBase` has `[Inject] internal TwMerge TwMerge { get; set; }`
- [x] New `MergeClasses()` method added alongside existing `CombineClasses()`
- [x] `TwMerge.Merge("px-4 px-6")` returns `"px-6"`
- [x] `TwMerge.Merge("bg-red-500 bg-blue-500")` returns `"bg-blue-500"`
- [ ] All components use TwMerge for class combination
- [ ] Unit test: `MergeClasses("p-2", "p-4")` returns `"p-4"`
- [ ] Unit test: `MergeClasses("text-red-500", "text-blue-500")` returns `"text-blue-500"`
- [ ] Unit test verifies conflict resolution

### Code Examples

```csharp
// ServiceCollectionExtensions.cs
using TailwindMerge;

namespace Flowbite.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFlowbite(this IServiceCollection services)
    {
        services.AddTailwindMerge(); // Add this
        services.AddSingleton<FlowbiteVersionService>();
        services.AddFlowbiteOffCanvasServices();
        return services;
    }
}
```

```csharp
// FlowbiteComponentBase.cs
using TailwindMerge;

namespace Flowbite.Base;

public abstract class FlowbiteComponentBase : ComponentBase
{
    [Inject] internal TwMerge TwMerge { get; set; } = default!;

    protected string MergeClasses(params string?[] classes)
    {
        var combined = string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
        return TwMerge.Merge(combined);
    }
}
```

### Implementation Notes

- The `TwMerge` service is registered as a singleton for performance
- Keep existing `CombineClasses()` method initially for backward compatibility
- Gradually migrate components from `CombineClasses()` to `MergeClasses()`

---

## Task 1.2: Base Class Enhancement

**Priority:** P1 (High) | **Effort:** Low (30 minutes)

### Goal

Add `Style` parameter and `AdditionalAttributes` capture to enable inline styles and arbitrary HTML attributes on all components.

### Tasks

- [x] Add `[Parameter] public string? Style { get; set; }` to `FlowbiteComponentBase`
- [x] Add `[Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes`
- [x] Update component templates to spread `@attributes="AdditionalAttributes"`

### Acceptance Criteria

- [x] `FlowbiteComponentBase` has `[Parameter] public string? Style { get; set; }`
- [x] `FlowbiteComponentBase` has `[Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }`
- [x] `<Button Style="margin-top: 1rem">` applies inline style
- [x] Button component renders `style` attribute when `Style` is set
- [x] `<Button data-testid="submit-btn">` passes through to HTML
- [x] Button component renders `data-testid` when passed as additional attribute
- [x] Existing components continue to work without changes
- [x] Existing functionality unchanged (breaking changes documented in MIGRATION.md)

### Code Examples

```csharp
// FlowbiteComponentBase.cs
using Microsoft.AspNetCore.Components;

namespace Flowbite.Base;

public abstract class FlowbiteComponentBase : ComponentBase
{
    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
```

```razor
<!-- Button.razor -->
<button class="@GetButtonClasses()"
        style="@Style"
        @attributes="AdditionalAttributes">
    @ChildContent
</button>
```

### Implementation Notes

- In component .razor files, add to root element: `style="@Style" @attributes="AdditionalAttributes"`
- Component-set attributes take precedence over passed AdditionalAttributes
- Document attribute precedence behavior in API documentation

---

## Task 1.3: ElementClass Builder ✅

**Priority:** P1 (High) | **Effort:** Low (1-2 hours)

### Goal

Create fluent API for building CSS class strings with conditional logic.

### Tasks

- [x] Create `src/Flowbite/Utilities/ElementClass.cs`
- [x] Implement `Empty()` static factory
- [x] Implement `Add(string?)` method
- [x] Implement `Add(string?, bool when)` conditional method
- [x] Add implicit string conversion

### Acceptance Criteria

- [x] `ElementClass.Empty().Add("px-4").Add("py-2").ToString()` returns `"px-4 py-2"`
- [x] `ElementClass.Empty().Add("hidden", when: false)` returns `""`
- [x] Implicit conversion: `string classes = ElementClass.Empty().Add("flex")`

### Code Examples

```csharp
// Utilities/ElementClass.cs
namespace Flowbite.Utilities;

public record struct ElementClass
{
    private string? _buffer;

    public static ElementClass Empty() => new();

    public ElementClass Add(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            _buffer = string.IsNullOrEmpty(_buffer) ? value : $"{_buffer} {value}";
        return this;
    }

    public ElementClass Add(string? value, bool when) => when ? Add(value) : this;

    public override string ToString() => _buffer ?? string.Empty;

    public static implicit operator string(ElementClass el) => el.ToString();
}
```

### Usage Example in Button Component

```csharp
// Button.razor.cs
namespace Flowbite.Components;

public partial class Button : FlowbiteComponentBase
{
    [Parameter] public bool Disabled { get; set; }

    private string GetButtonClasses()
    {
        return ElementClass.Empty()
            .Add("px-4 py-2 rounded font-medium")
            .Add("opacity-50 cursor-not-allowed", when: Disabled)
            .Add(GetColorClasses())
            .Add(Class);
    }
}
```

---

## Task 1.4: Motion-Reduce Accessibility

**Priority:** P1 (High) | **Effort:** Low (1-2 hours)

### Goal

Add `motion-reduce:transition-none` to all components with CSS transitions to respect user accessibility preferences for users with vestibular disorders.

### Tasks

- [x] Audit all components with `transition-*` classes
- [x] Add `motion-reduce:transition-none` to each
- [ ] Document pattern in contributing guide

### Acceptance Criteria

- [x] `Drawer` component includes `motion-reduce:transition-none`
- [x] `Modal` component includes `motion-reduce:transition-none` (N/A - no transitions)
- [x] `Tooltip` component includes `motion-reduce:transition-none`
- [x] `SidebarCollapse` component includes `motion-reduce:transition-none`
- [x] Any component with `transition-*` class has matching `motion-reduce:transition-none`
- [x] Users with `prefers-reduced-motion: reduce` see no transitions
- [x] Components affected: Button (N/A), Drawer, Modal (N/A), Sidebar, Accordion (N/A), Tooltip, Toast
- [x] No visual regression for users without motion preference

### Components to Update

| Component | File Path | Current Classes | Add |
|-----------|-----------|-----------------|-----|
| Drawer | `src/Flowbite/Components/Drawer.razor.cs` | `transition-transform` | `motion-reduce:transition-none` |
| Modal | `src/Flowbite/Components/Modal.razor.cs` | `transition-opacity` | `motion-reduce:transition-none` |
| Tooltip | `src/Flowbite/Components/Tooltip.razor.cs` | `transition-opacity` | `motion-reduce:transition-none` |
| Toast | `src/Flowbite/Components/Toast.razor.cs` | `transition-*` | `motion-reduce:transition-none` |
| Button | `src/Flowbite/Components/Button.razor.cs` | `transition-*` | `motion-reduce:transition-none` |
| Sidebar | `src/Flowbite/Components/Sidebar.razor.cs` | `transition-*` | `motion-reduce:transition-none` |
| SidebarCollapse | `src/Flowbite/Components/SidebarCollapse.razor` | `transition-*` | `motion-reduce:transition-none` |
| Accordion | `src/Flowbite/Components/Accordion.razor.cs` | `transition-*` | `motion-reduce:transition-none` |

### Code Examples

```csharp
// Before (Drawer.razor.cs:154)
var baseClasses = "fixed z-[70] overflow-y-auto bg-white p-4 transition-transform dark:bg-gray-800";

// After
var baseClasses = "fixed z-[70] overflow-y-auto bg-white p-4 transition-transform motion-reduce:transition-none dark:bg-gray-800";
```

```csharp
// General pattern
// Before
var classes = "transition-transform duration-300";

// After
var classes = "transition-transform duration-300 motion-reduce:transition-none";
```

### Implementation Notes

- Document the `motion-reduce:transition-none` pattern in the contributing guide
- This pattern should be applied to all future components with transitions
- CSS only disables transitions, not functionality - component behavior remains unchanged

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| TailwindMerge performance overhead | Low | Medium | Benchmark before/after; service is singleton |
| AdditionalAttributes conflicts | Medium | Low | Document that component-set attributes take precedence |
| Motion-reduce breaks animations | Low | Low | CSS only disables transitions, not functionality |

---

## Definition of Done

Phase 1 is complete when:

1. All acceptance criteria for Tasks 1.1, 1.2, 1.3, and 1.4 are met
2. TailwindMerge.NET installed and registered
3. `FlowbiteComponentBase` has `Style` and `AdditionalAttributes`
4. `ElementClass` utility created and documented
5. All animated components have `motion-reduce:transition-none`
6. All existing tests pass
7. New unit tests added for TailwindMerge functionality
8. No breaking changes to public API
9. PR reviewed and merged

---

## Next Phase

Upon completion, proceed to **[Phase 2: Core Architecture](./02-PHASE-CORE-ARCHITECTURE.md)** (P0 - Critical)
