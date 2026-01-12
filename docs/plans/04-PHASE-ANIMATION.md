# Phase 4: Animation State Machine

**Status:** Not Started
**Prerequisites:** Phase 1 (ElementReference extensions)
**Priority:** P1 | **Effort:** M (8-12 hours)

---

## Objectives

1. Create `CollapseState` enum for animation lifecycle management
2. Implement dynamic height calculation for smooth collapse/expand
3. Update Accordion and SidebarCollapse components

---

## Problem Statement

**Current Behavior:**
```csharp
// SidebarCollapse.razor - Binary toggle, no height animation
private bool _isOpen;

// Content visibility: instant show/hide
@if (_isOpen)
{
    <div>@ChildContent</div>
}
```

**Issues:**
- Content appears/disappears instantly (no smooth transition)
- No height animation for collapsible sections
- State can change mid-animation causing visual glitches

---

## 4.1 CollapseState Enum

### Goal
Define animation lifecycle states for predictable transitions.

### Tasks
- [ ] Create `src/Flowbite/Common/CollapseState.cs`
- [ ] Define 4 states: `Collapsed`, `Expanding`, `Expanded`, `Collapsing`

### Code Example
```csharp
namespace Flowbite.Common;

/// <summary>
/// Represents the animation state of a collapsible element.
/// </summary>
public enum CollapseState
{
    /// <summary>Height = 0, content hidden</summary>
    Collapsed,

    /// <summary>Height animating from 0 to scrollHeight</summary>
    Expanding,

    /// <summary>Height = auto, content visible</summary>
    Expanded,

    /// <summary>Height animating from scrollHeight to 0</summary>
    Collapsing
}
```

---

## 4.2 JavaScript Height Measurement

### Goal
Measure content height via JS interop for smooth animations.

### Tasks
- [ ] Create `src/Flowbite/wwwroot/js/utils/element.js`
- [ ] Implement `getScrollHeight(element)` function
- [ ] Create C# extension method `ElementReferenceExtensions.GetScrollHeightAsync()`

### JavaScript Implementation

**Option A: Module Export (Recommended)**
```javascript
// wwwroot/js/utils/element.js
export function getScrollHeight(element) {
    return element?.scrollHeight ?? 0;
}
```

**Option B: Global Function**
```javascript
// Add to flowbite.js
window.flowbiteBlazor = window.flowbiteBlazor || {};
window.flowbiteBlazor.getScrollHeight = function(element) {
    return element?.scrollHeight ?? 0;
};
```

### C# Extension Method
```csharp
// Extensions/ElementReferenceExtensions.cs
public static class ElementReferenceExtensions
{
    public static async ValueTask<int> GetScrollHeightAsync(
        this ElementReference elementRef,
        IJSRuntime js)
    {
        return await js.InvokeAsync<int>(
            "Flowbite.element.getScrollHeight",
            elementRef);
    }
}
```

---

## 4.3 Accordion Component Update

### Goal
Implement state machine in Accordion for smooth height transitions.

### Tasks
- [ ] Add `CollapseState _state` field
- [ ] Add `int _height` field for measured height
- [ ] Implement `ToggleAsync()` with state transitions
- [ ] Add `@ontransitionend` handler
- [ ] Apply dynamic height via inline style

### Code Example

```csharp
// AccordionItem.razor.cs
public partial class AccordionItem : FlowbiteComponentBase
{
    [Inject] private IJSRuntime JS { get; set; } = default!;

    private CollapseState _state = CollapseState.Collapsed;
    private int _height;
    private ElementReference _contentRef;

    public bool IsExpanded => _state is CollapseState.Expanded or CollapseState.Expanding;

    private async Task ToggleAsync()
    {
        switch (_state)
        {
            case CollapseState.Collapsed:
                _height = await _contentRef.GetScrollHeightAsync(JS);
                _state = CollapseState.Expanding;
                break;

            case CollapseState.Expanded:
                _height = await _contentRef.GetScrollHeightAsync(JS);
                await Task.Yield(); // Allow height to be set before collapsing
                _state = CollapseState.Collapsing;
                _height = 0;
                break;
        }
        StateHasChanged();
    }

    private void HandleTransitionEnd()
    {
        _state = _state switch
        {
            CollapseState.Expanding => CollapseState.Expanded,
            CollapseState.Collapsing => CollapseState.Collapsed,
            _ => _state
        };
        StateHasChanged();
    }

    private string GetContentStyle() => _state switch
    {
        CollapseState.Expanding => $"height: {_height}px",
        CollapseState.Collapsing => "height: 0",
        CollapseState.Expanded => "height: auto",
        CollapseState.Collapsed => "height: 0"
    };

    private string GetContentClasses() => MergeClasses(
        "overflow-hidden",
        "transition-[height] duration-300 ease-in-out",
        "motion-reduce:transition-none",
        Slots?.Content
    );
}
```

```razor
<!-- AccordionItem.razor -->
<div class="@GetItemClasses()">
    <h2>
        <button class="@GetTriggerClasses()"
                @onclick="ToggleAsync"
                aria-expanded="@IsExpanded">
            <span>@Title</span>
            <svg class="@GetIndicatorClasses()">...</svg>
        </button>
    </h2>

    <div @ref="_contentRef"
         class="@GetContentClasses()"
         style="@GetContentStyle()"
         @ontransitionend="HandleTransitionEnd">
        <div class="p-5">
            @ChildContent
        </div>
    </div>
</div>
```

**Important:** Use `await Task.Yield()` instead of `Task.Delay(10)` to allow the render cycle to complete before triggering the collapse animation. This is more reliable and doesn't introduce artificial delays.

---

## 4.4 SidebarCollapse Update

### Goal
Apply same pattern to SidebarCollapse component.

### Tasks
- [ ] Add `CollapseState _state` field
- [ ] Add height measurement
- [ ] Update toggle logic
- [ ] Add transition end handler

### Files to Modify
- `src/Flowbite/Components/SidebarCollapse.razor`
- `src/Flowbite/Components/SidebarCollapse.razor.cs`

---

## Testing Checklist

### Expand/Collapse Behavior
- [ ] **Expand Test:** Click closed accordion -> animates smoothly to content height
- [ ] **Collapse Test:** Click open accordion -> animates smoothly to zero
- [ ] **Rapid Toggle:** Click multiple times quickly -> no visual glitches
- [ ] **Dynamic Content:** Content changes while expanded -> height adjusts
- [ ] **State Consistency:** `_state` matches visual state after animation

### Accessibility
- [ ] **Motion Reduce:** `motion-reduce` enabled -> instant show/hide (no animation)
- [ ] **ARIA Attributes:** `aria-expanded` correctly reflects expanded state
- [ ] **Keyboard Navigation:** Can toggle with Enter/Space keys

### State Machine
- [ ] **State Transitions:** Collapsed -> Expanding -> Expanded works correctly
- [ ] **State Transitions:** Expanded -> Collapsing -> Collapsed works correctly
- [ ] **Mid-Animation Toggle:** Toggling during animation doesn't cause glitches
- [ ] **TransitionEnd Event:** `ontransitionend` fires and updates state correctly

### Component Coverage
- [ ] **Accordion:** Height animation works
- [ ] **SidebarCollapse:** Height animation works
- [ ] **Nested Collapse:** Parent/child collapse components work correctly

---

## Acceptance Criteria

- [ ] `CollapseState` enum created with 4 states
- [ ] `GetScrollHeightAsync()` extension method works
- [ ] Accordion expands/collapses with smooth height animation
- [ ] SidebarCollapse uses same pattern
- [ ] `ontransitionend` finalizes state correctly
- [ ] `motion-reduce:transition-none` supported
- [ ] No visual regressions in existing components

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| Animation timing issues | Medium | Low | Use `transitionend` event, not timers. Use `Task.Yield()` instead of `Task.Delay()` for render synchronization |
| State stuck mid-animation | Low | Medium | Add timeout fallback if `transitionend` doesn't fire (e.g., when `motion-reduce` is enabled) |
| Height calculation incorrect | Low | Low | Measure height before animation starts; handle zero-height edge case |
| Browser compatibility | Low | Low | `transitionend` widely supported; fallback for older browsers if needed |

---

## Definition of Done

- [ ] All acceptance criteria met
- [ ] All testing checklist items pass
- [ ] No visual regressions
- [ ] Existing tests pass
- [ ] Code follows project conventions

---

## Next Phase

Once complete, proceed to **[Phase 5: Floating UI Integration](./05-PHASE-FLOATING-UI.md)** (P0)
