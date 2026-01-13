# Phase 2: Core Architecture

**Status:** Complete (Task 2.1 Complete, Task 2.2 Complete, Task 2.3 Complete)
**Prerequisites:** Phase 1 complete (TailwindMerge Integration)
**Priority:** P0 (Critical)
**Effort:** L (20-30 hours)

---

## Objectives

1. Implement Slot System for per-element customization
2. Integrate Floating UI for intelligent positioning

These are the two most impactful architectural improvements. Both are P0 because they significantly affect developer experience and end-user UX.

---

## 2.1 Slot System

### Goal
Enable per-element class customization within complex components while respecting Flowbite's design system.

### Problem Statement
```csharp
// Current: Only one Class parameter for entire component
<Accordion Class="my-custom-class">  // Applies to wrapper only
    <AccordionItem>...</AccordionItem>
</Accordion>

// Desired: Fine-grained control
<Accordion>
    <AccordionItem Slots="@(new AccordionItemSlots {
        Trigger = "hover:bg-gray-100",
        Title = "text-lg font-bold"
    })">
        ...
    </AccordionItem>
</Accordion>
```

### Tasks

#### 2.1.1 Create SlotBase ✅
- [x] Create `src/Flowbite/Common/SlotBase.cs`
- [x] Define abstract base with `Base` property

```csharp
// Common/SlotBase.cs
namespace Flowbite.Common;

public abstract class SlotBase
{
    /// <summary>
    /// Classes applied to the component's root element.
    /// </summary>
    public string? Base { get; set; }
}
```

#### 2.1.2 Create Component Slots ✅
- [x] Create `AccordionItemSlots.cs`
- [x] Create `DropdownSlots.cs`
- [x] Create `ModalSlots.cs`
- [x] Create `CardSlots.cs`

```csharp
// Components/Accordion/AccordionItemSlots.cs
public class AccordionItemSlots : SlotBase
{
    public string? Heading { get; set; }
    public string? Trigger { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Indicator { get; set; }
}
```

#### 2.1.3 Integrate with Components ✅
- [x] Add `[Parameter] public TSlots? Slots { get; set; }` to components
- [x] Update `Get*Classes()` methods to merge slot classes with TwMerge

```csharp
// AccordionItem.razor.cs
[Parameter] public AccordionItemSlots? Slots { get; set; }

private string GetTriggerClasses()
{
    return MergeClasses(
        "flex items-center justify-between w-full p-5 font-medium",
        "text-left text-gray-500 border border-gray-200",
        "hover:bg-gray-100 dark:hover:bg-gray-800",
        "motion-reduce:transition-none",
        Slots?.Trigger  // User customization
    );
}
```

### Acceptance Criteria ✅
- [x] `SlotBase` abstract class exists
- [x] At least 4 component slot classes created
- [x] Slot classes merge correctly with defaults via TwMerge
- [x] `Slots?.Trigger` overrides default trigger classes
- [x] Existing `Class` parameter still works (applied to root)

### Components to Update
| Component | Slots | Status |
|-----------|-------|--------|
| AccordionItem | Base, Heading, Trigger, Title, Content, Indicator | ✅ Slots class created |
| Dropdown | Base, Trigger, Menu, Item | ✅ Complete |
| Modal | Backdrop, Content, Header, Body, Footer | ✅ Complete |
| Card | Base, Image, Body | ✅ Complete |

---

## 2.2 Floating UI Integration

### Goal
Replace CSS-only positioning with Floating UI for viewport-aware popover positioning.

### Problem Statement
```csharp
// Current: Static CSS positioning (Dropdown.razor.cs:215-236)
var positionClass = Placement switch
{
    DropdownPlacement.Top => "bottom-full mb-2",
    _ => "top-full mt-2"
};
// Problem: Dropdown at viewport edge gets clipped
```

### Tasks

#### 2.2.1 Install Dependencies ✅
- [x] Add `@floating-ui/dom` via npm
- [x] Configure bundling (Rollup/Vite)

#### 2.2.2 Create JavaScript Module ✅
- [x] Create `src/Flowbite/js-src/positioning.js`
- [x] Create `src/Flowbite/js-src/dom.js` (portal, waitForElement)

```javascript
// positioning.js
import { computePosition, flip, shift, offset, autoUpdate } from '@floating-ui/dom';
import { portal } from './dom.js';

const instances = new Map();

export async function initialize(id, options) {
    const trigger = document.querySelector(`[data-floating-trigger="${id}"]`);
    const floating = document.querySelector(`[data-floating-element="${id}"]`);

    if (!trigger || !floating) return;

    portal(floating); // Move to document.body for z-index

    const update = async () => {
        const { x, y, placement } = await computePosition(trigger, floating, {
            placement: options.placement || 'bottom',
            middleware: [
                offset(options.offset ?? 8),
                flip(),
                shift({ padding: 8 })
            ]
        });

        Object.assign(floating.style, {
            left: `${x}px`,
            top: `${y}px`,
            position: 'absolute'
        });

        floating.dataset.placement = placement;
    };

    const cleanup = autoUpdate(trigger, floating, update);
    instances.set(id, { cleanup, floating, trigger });

    return { placement: options.placement };
}

export function destroy(id) {
    const instance = instances.get(id);
    if (instance) {
        instance.cleanup();
        instances.delete(id);
    }
}
```

```javascript
// dom.js
export function portal(element, target = document.body) {
    if (element.parentElement !== target) {
        target.appendChild(element);
    }
}

export function waitForElement(selector, timeout = 5000) {
    return new Promise((resolve, reject) => {
        const el = document.querySelector(selector);
        if (el) return resolve(el);

        const observer = new MutationObserver(() => {
            const el = document.querySelector(selector);
            if (el) {
                observer.disconnect();
                resolve(el);
            }
        });

        observer.observe(document.body, { childList: true, subtree: true });
        setTimeout(() => { observer.disconnect(); reject(); }, timeout);
    });
}
```

#### 2.2.3 Create C# Service ✅
- [x] Create `src/Flowbite/Services/FloatingService.cs`
- [x] Register in DI

```csharp
// Services/FloatingService.cs
public class FloatingService : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;
    private readonly List<string> _activeIds = [];

    public FloatingService(IJSRuntime js) => _js = js;

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        return _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Flowbite/js/utils/positioning.js");
    }

    public async Task InitializeAsync(string id, FloatingOptions options)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("initialize", id, options);
        _activeIds.Add(id);
    }

    public async Task DestroyAsync(string id)
    {
        if (_module is not null && _activeIds.Remove(id))
        {
            await _module.InvokeVoidAsync("destroy", id);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            foreach (var id in _activeIds)
            {
                await _module.InvokeVoidAsync("destroy", id);
            }
            await _module.DisposeAsync();
        }
    }
}

public record FloatingOptions(string Placement = "bottom", int Offset = 8);
```

#### 2.2.4 Update Components ✅
- [x] Refactor `Dropdown.razor.cs`
- [x] Refactor `Tooltip.razor.cs`
- [ ] Refactor `Popover.razor.cs` (if exists) - N/A, no Popover component

```csharp
// Dropdown.razor.cs
public partial class Dropdown : FlowbiteComponentBase, IAsyncDisposable
{
    [Inject] private FloatingService Floating { get; set; } = default!;

    private string _id = Guid.NewGuid().ToString("N")[..8];
    private bool _initialized;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender || (!_initialized && IsOpen))
        {
            await Floating.InitializeAsync(_id, new FloatingOptions(
                Placement: Placement.ToString().ToLowerInvariant()
            ));
            _initialized = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await Floating.DestroyAsync(_id);
    }
}
```

```razor
<!-- Dropdown.razor -->
<div class="relative inline-block">
    <button data-floating-trigger="@_id" @onclick="Toggle">
        @Trigger
    </button>

    @if (IsOpen)
    {
        <div data-floating-element="@_id"
             class="@GetMenuClasses()">
            @ChildContent
        </div>
    }
</div>
```

### Acceptance Criteria ✅
- [x] `@floating-ui/dom` bundled in wwwroot (`floating-ui.bundle.js`)
- [x] `FloatingService` registered and injectable
- [x] Dropdown at right viewport edge shifts left
- [x] Tooltip near bottom flips to top
- [x] Position updates on scroll/resize
- [x] No memory leaks (cleanup on dispose)

### Testing Checklist ✅
- [x] **Edge Test:** Open dropdown at right edge → shifts left
- [x] **Flip Test:** Tooltip on button at bottom → flips to top
- [x] **Scroll Test:** Open dropdown, scroll page → stays attached
- [x] **Dispose Test:** Navigate away → no console errors

---

## Migration Notes

### Slot System (Non-Breaking)
- Existing `Class` parameter continues to work
- `Slots` is additive - opt-in for fine-grained control

### Floating UI (Minor Breaking)
- Dropdowns may appear in slightly different positions due to flip/shift
- Provide `DisableFlip` and `DisableShift` parameters for legacy behavior if needed

---

## Definition of Done

### Task 2.1: Slot System ✅
- [x] `SlotBase` and 4+ component slot classes created
- [x] Slot classes merge via TwMerge in style methods
- [x] Demo examples for Card, Dropdown, Modal

### Task 2.2: Floating UI ✅
- [x] Floating UI JavaScript module bundled
- [x] `FloatingService` created and registered
- [x] Dropdown and Tooltip use Floating UI
- [x] All acceptance criteria met
- [x] All testing checklist items pass

---

---

## 2.3 Keyboard Navigation & Focus Management

### Goal
Add full keyboard navigation support to interactive components for WCAG 2.1 compliance.

### Tasks

#### 2.3.1 Dropdown Keyboard Navigation ✅
- [x] ArrowUp/ArrowDown to navigate menu items
- [x] Home/End to jump to first/last item
- [x] Enter/Space to select focused item
- [x] Escape to close menu
- [x] Type-ahead search
- [x] Focus ring styling (`ring-2 ring-primary-500`)
- [x] Proper ARIA roles (`role="menu"`, `role="menuitem"`)
- [x] `tabindex` management (0 on focused, -1 on others)
- [x] `ShouldPreventDefault` for scroll prevention

#### 2.3.2 Tooltip Focus Management ✅
- [x] Show on focus (`onfocusin`)
- [x] Hide on blur (`onfocusout`, `onblur`)
- [x] Escape key dismissal
- [x] ARIA linkage (`aria-describedby`, `role="tooltip"`, `id`)

### Acceptance Criteria ✅
- [x] Dropdown navigable via keyboard only
- [x] Tooltip shows/hides on focus events
- [x] Escape closes both Dropdown and Tooltip
- [x] ARIA roles correctly applied
- [x] Focus rings visible on keyboard navigation
- [x] Build passes with no errors

### Testing Checklist ✅
- [x] **Open:** Click opens dropdown
- [x] **Navigate:** ArrowDown/Up moves focus
- [x] **Close:** Escape closes dropdown
- [x] **ARIA:** `role="menu"` and `role="menuitem"` present
- [x] **Tooltip Focus:** Tab to button shows tooltip
- [x] **Tooltip Dismiss:** Escape or blur hides tooltip

### QC Reports
- `docs/verification/phase2/task2-qc-report-v1.md` - Implementation verification
- `docs/verification/phase2/task2-qc-report-v2.md` - Independent QC verification

---

## Next Phase

Once complete, proceed to **[Phase 3: Animation](./03-PHASE-ANIMATION.md)** (P1)
