# Phase 2: Core Architecture

**Status:** Not Started
**Prerequisites:** Phase 1 complete (TailwindMerge Integration)
**Priority:** P0 (Critical)
**Effort:** L (20-30 hours)

---

## Objectives

1. Implement Slot System for per-element customization
2. Integrate Floating UI for intelligent viewport-aware positioning

These are the two most impactful architectural improvements. Both are P0 because they significantly affect developer experience and end-user UX.

---

## 2.1 Slot System

### Problem Statement

Currently, components only expose a single `Class` parameter that applies to the wrapper element. This limits customization for complex components with multiple internal elements.

```csharp
// Current: Only one Class parameter for entire component
<LumexAccordion Class="my-custom-class">  // Applies to wrapper only
    <LumexAccordionItem>...</LumexAccordionItem>
</LumexAccordion>

// Desired: Fine-grained control over every internal element
<LumexAccordion>
    <LumexAccordionItem Slots="@(new AccordionItemSlots {
        Trigger = "hover:bg-gray-100",
        Title = "text-lg font-bold",
        Indicator = "text-primary-500"
    })">
        ...
    </LumexAccordionItem>
</LumexAccordion>
```

### Tasks

#### 2.1.1 Create SlotBase Abstract Class

- [ ] Create `src/LumexUI/Common/SlotBase.cs`
- [ ] Define abstract base with `Base` property for root element customization
- [ ] Add XML documentation

```csharp
// src/LumexUI/Common/SlotBase.cs
namespace LumexUI.Common;

/// <summary>
/// Base class for component slot customization.
/// Slots allow fine-grained CSS class customization of internal component elements.
/// </summary>
public abstract class SlotBase
{
    /// <summary>
    /// Classes applied to the component's root element.
    /// Merged with default styles via TwMerge.
    /// </summary>
    public string? Base { get; set; }
}
```

#### 2.1.2 Create AccordionItemSlots

- [ ] Create `src/LumexUI/Components/Accordion/AccordionItemSlots.cs`
- [ ] Define all 6 customizable slots

```csharp
// src/LumexUI/Components/Accordion/AccordionItemSlots.cs
namespace LumexUI.Components;

/// <summary>
/// Slot customization for AccordionItem component.
/// </summary>
public class AccordionItemSlots : SlotBase
{
    /// <summary>
    /// Classes for the heading wrapper element.
    /// </summary>
    public string? Heading { get; set; }

    /// <summary>
    /// Classes for the clickable trigger button.
    /// </summary>
    public string? Trigger { get; set; }

    /// <summary>
    /// Classes for the title text element.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Classes for the collapsible content container.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Classes for the expand/collapse indicator icon.
    /// </summary>
    public string? Indicator { get; set; }
}
```

#### 2.1.3 Create DropdownSlots

- [ ] Create `src/LumexUI/Components/Dropdown/DropdownSlots.cs`
- [ ] Define all 4 customizable slots

```csharp
// src/LumexUI/Components/Dropdown/DropdownSlots.cs
namespace LumexUI.Components;

/// <summary>
/// Slot customization for Dropdown component.
/// </summary>
public class DropdownSlots : SlotBase
{
    /// <summary>
    /// Classes for the trigger button element.
    /// </summary>
    public string? Trigger { get; set; }

    /// <summary>
    /// Classes for the dropdown menu container.
    /// </summary>
    public string? Menu { get; set; }

    /// <summary>
    /// Classes for individual menu items.
    /// </summary>
    public string? Item { get; set; }
}
```

#### 2.1.4 Create ModalSlots

- [ ] Create `src/LumexUI/Components/Modal/ModalSlots.cs`
- [ ] Define all 6 customizable slots

```csharp
// src/LumexUI/Components/Modal/ModalSlots.cs
namespace LumexUI.Components;

/// <summary>
/// Slot customization for Modal component.
/// </summary>
public class ModalSlots : SlotBase
{
    /// <summary>
    /// Classes for the backdrop overlay.
    /// </summary>
    public string? Backdrop { get; set; }

    /// <summary>
    /// Classes for the modal content wrapper.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Classes for the modal header section.
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Classes for the modal body section.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Classes for the modal footer section.
    /// </summary>
    public string? Footer { get; set; }

    /// <summary>
    /// Classes for the close button.
    /// </summary>
    public string? CloseButton { get; set; }
}
```

#### 2.1.5 Create CardSlots

- [ ] Create `src/LumexUI/Components/Card/CardSlots.cs`
- [ ] Define all 5 customizable slots

```csharp
// src/LumexUI/Components/Card/CardSlots.cs
namespace LumexUI.Components;

/// <summary>
/// Slot customization for Card component.
/// </summary>
public class CardSlots : SlotBase
{
    /// <summary>
    /// Classes for the card header section.
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Classes for the card body section.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Classes for the card footer section.
    /// </summary>
    public string? Footer { get; set; }

    /// <summary>
    /// Classes for the card image element.
    /// </summary>
    public string? Image { get; set; }
}
```

#### 2.1.6 Integrate Slots with Components

- [ ] Add `[Parameter] public TSlots? Slots { get; set; }` to each component
- [ ] Update `Get*Classes()` methods to merge slot classes with TwMerge
- [ ] Ensure existing `Class` parameter still applies to root element

**Example Integration Pattern:**

```csharp
// src/LumexUI/Components/Accordion/LumexAccordionItem.razor.cs
namespace LumexUI.Components;

public partial class LumexAccordionItem : LumexComponentBase
{
    /// <summary>
    /// Slot classes for customizing internal elements.
    /// </summary>
    [Parameter] public AccordionItemSlots? Slots { get; set; }

    private string GetTriggerClasses()
    {
        return TwMerge.Merge(
            "flex items-center justify-between w-full p-5 font-medium",
            "text-left text-gray-500 border border-gray-200",
            "hover:bg-gray-100 dark:hover:bg-gray-800",
            "focus:ring-4 focus:ring-gray-200 dark:focus:ring-gray-800",
            "motion-reduce:transition-none",
            Slots?.Trigger  // User customization - wins due to TwMerge
        );
    }

    private string GetHeadingClasses()
    {
        return TwMerge.Merge(
            "text-gray-900 dark:text-white",
            Slots?.Heading
        );
    }

    private string GetTitleClasses()
    {
        return TwMerge.Merge(
            "font-medium",
            Slots?.Title
        );
    }

    private string GetContentClasses()
    {
        return TwMerge.Merge(
            "p-5 border border-t-0 border-gray-200 dark:border-gray-700",
            "dark:bg-gray-900",
            Slots?.Content
        );
    }

    private string GetIndicatorClasses()
    {
        return TwMerge.Merge(
            "w-3 h-3 shrink-0 transition-transform duration-200",
            "motion-reduce:transition-none",
            Slots?.Indicator
        );
    }

    // Existing Class parameter applies to root via base class
    private string GetRootClasses()
    {
        return TwMerge.Merge(
            "border-b border-gray-200 dark:border-gray-700",
            Slots?.Base,
            Class  // From LumexComponentBase
        );
    }
}
```

### Acceptance Criteria

- [ ] `SlotBase` abstract class exists in `src/LumexUI/Common/`
- [ ] `AccordionItemSlots` class created with 6 slots (Base, Heading, Trigger, Title, Content, Indicator)
- [ ] `DropdownSlots` class created with 4 slots (Base, Trigger, Menu, Item)
- [ ] `ModalSlots` class created with 6 slots (Base, Backdrop, Content, Header, Body, Footer, CloseButton)
- [ ] `CardSlots` class created with 5 slots (Base, Header, Body, Footer, Image)
- [ ] Slot classes merge correctly with defaults via TwMerge
- [ ] `Slots?.Trigger` overrides default trigger classes when set
- [ ] Existing `Class` parameter still works (applied to root element)
- [ ] Unit tests verify slot merging behavior
- [ ] XML documentation present on all public members

### Components to Update

| Component | Slots |
|-----------|-------|
| LumexAccordionItem | Base, Heading, Trigger, Title, Content, Indicator |
| LumexDropdown | Base, Trigger, Menu, Item |
| LumexModal | Base, Backdrop, Content, Header, Body, Footer, CloseButton |
| LumexCard | Base, Header, Body, Footer, Image |

---

## 2.2 Floating UI Integration

### Problem Statement

Current CSS-only positioning uses static classes that cannot adapt to viewport constraints, causing dropdowns and tooltips to be clipped at viewport edges.

```csharp
// Current: Static CSS positioning
var positionClass = Placement switch
{
    DropdownPlacement.Top => "bottom-full mb-2",
    _ => "top-full mt-2"
};
// Problem: Dropdown at viewport edge gets clipped - no auto-flip or shift
```

### Tasks

#### 2.2.1 Install Dependencies

- [ ] Add `@floating-ui/dom` via npm: `npm install @floating-ui/dom`
- [ ] Configure bundling to include in wwwroot
- [ ] Verify tree-shaking is working (~15KB gzipped acceptable)

```json
// package.json addition
{
  "dependencies": {
    "@floating-ui/dom": "^1.6.0"
  }
}
```

#### 2.2.2 Create JavaScript Modules

##### positioning.js

- [ ] Create `src/LumexUI/wwwroot/js/utils/positioning.js`
- [ ] Implement `initialize()` function with flip/shift middleware
- [ ] Implement `destroy()` function for cleanup
- [ ] Track active instances for cleanup on dispose

```javascript
// src/LumexUI/wwwroot/js/utils/positioning.js
import { computePosition, flip, shift, offset, autoUpdate } from '@floating-ui/dom';
import { portal, waitForElement } from './dom.js';

const instances = new Map();

/**
 * Initialize floating positioning for a trigger/floating element pair.
 * @param {string} id - Unique identifier for this floating instance
 * @param {Object} options - Positioning options
 * @param {string} options.placement - Initial placement (top, bottom, left, right)
 * @param {number} options.offset - Offset from trigger in pixels
 */
export async function initialize(id, options) {
    const trigger = document.querySelector(`[data-floating-trigger="${id}"]`);
    const floating = await waitForElement(`[data-floating-element="${id}"]`);

    if (!trigger || !floating) {
        console.warn(`[LumexUI] Floating elements not found for id: ${id}`);
        return;
    }

    // Move to document.body for proper z-index stacking
    portal(floating);

    const { placement = 'bottom', offset: offsetVal = 8 } = options;

    const middleware = [
        offset(offsetVal),
        flip(),                    // Auto-flip when hitting viewport edge
        shift({ padding: 8 })      // Shift to stay within viewport bounds
    ];

    const update = async () => {
        const { x, y, placement: finalPlacement } = await computePosition(
            trigger,
            floating,
            { placement, middleware }
        );

        Object.assign(floating.style, {
            left: `${x}px`,
            top: `${y}px`,
            position: 'absolute'
        });

        // Store actual placement for CSS styling hooks
        floating.dataset.placement = finalPlacement;
    };

    // autoUpdate handles scroll, resize, and reference element changes
    const cleanup = autoUpdate(trigger, floating, update);
    instances.set(id, { cleanup, floating, trigger });

    return { placement: options.placement };
}

/**
 * Destroy a floating instance and clean up resources.
 * @param {string} id - The instance identifier to destroy
 */
export function destroy(id) {
    const instance = instances.get(id);
    if (instance) {
        instance.cleanup();
        instances.delete(id);
    }
}

/**
 * Destroy all active floating instances.
 * Called on page unload or service disposal.
 */
export function destroyAll() {
    instances.forEach((instance, id) => {
        instance.cleanup();
    });
    instances.clear();
}
```

##### dom.js

- [ ] Create `src/LumexUI/wwwroot/js/utils/dom.js`
- [ ] Implement `portal()` function for element relocation
- [ ] Implement `waitForElement()` for async element detection

```javascript
// src/LumexUI/wwwroot/js/utils/dom.js

/**
 * Move an element to document.body for proper z-index stacking.
 * @param {HTMLElement} element - Element to portal
 * @param {HTMLElement} target - Target container (default: document.body)
 */
export function portal(element, target = document.body) {
    if (element && element.parentElement !== target) {
        target.appendChild(element);
    }
}

/**
 * Wait for an element to appear in the DOM.
 * Uses MutationObserver for efficient detection.
 * @param {string} selector - CSS selector for the element
 * @param {number} timeout - Maximum wait time in ms (default: 5000)
 * @returns {Promise<HTMLElement|null>} The element or null if timeout
 */
export function waitForElement(selector, timeout = 5000) {
    return new Promise((resolve) => {
        // Check if element already exists
        const element = document.querySelector(selector);
        if (element) {
            resolve(element);
            return;
        }

        // Set up observer for dynamic elements
        const observer = new MutationObserver(() => {
            const el = document.querySelector(selector);
            if (el) {
                observer.disconnect();
                resolve(el);
            }
        });

        observer.observe(document.body, { childList: true, subtree: true });

        // Timeout to prevent indefinite waiting
        setTimeout(() => {
            observer.disconnect();
            resolve(null);
        }, timeout);
    });
}
```

#### 2.2.3 Create C# FloatingService

- [ ] Create `src/LumexUI/Services/FloatingService.cs`
- [ ] Create `src/LumexUI/Services/FloatingOptions.cs`
- [ ] Register in DI via `AddLumexServices()`
- [ ] Implement proper cleanup on dispose

```csharp
// src/LumexUI/Services/FloatingOptions.cs
namespace LumexUI.Services;

/// <summary>
/// Options for floating element positioning.
/// </summary>
/// <param name="Placement">Initial placement relative to trigger (top, bottom, left, right)</param>
/// <param name="Offset">Offset from trigger in pixels</param>
public record FloatingOptions(
    string Placement = "bottom",
    int Offset = 8
);
```

```csharp
// src/LumexUI/Services/FloatingService.cs
namespace LumexUI.Services;

using Microsoft.JSInterop;

/// <summary>
/// Service for managing floating UI element positioning.
/// Handles initialization, cleanup, and proper disposal of floating instances.
/// </summary>
public class FloatingService : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private IJSObjectReference? _module;
    private readonly List<string> _activeIds = [];
    private bool _disposed;

    public FloatingService(IJSRuntime js) => _js = js;

    private async Task<IJSObjectReference> GetModuleAsync()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(FloatingService));
        }

        return _module ??= await _js.InvokeAsync<IJSObjectReference>(
            "import", "./_content/LumexUI/js/utils/positioning.js");
    }

    /// <summary>
    /// Initialize floating positioning for a trigger/element pair.
    /// </summary>
    /// <param name="id">Unique identifier for this instance</param>
    /// <param name="options">Positioning options</param>
    public async Task InitializeAsync(string id, FloatingOptions options)
    {
        var module = await GetModuleAsync();
        await module.InvokeVoidAsync("initialize", id, options);
        _activeIds.Add(id);
    }

    /// <summary>
    /// Destroy a specific floating instance.
    /// </summary>
    /// <param name="id">The instance identifier to destroy</param>
    public async Task DestroyAsync(string id)
    {
        if (_module is not null && _activeIds.Remove(id))
        {
            try
            {
                await _module.InvokeVoidAsync("destroy", id);
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected, cleanup already handled
            }
        }
    }

    /// <summary>
    /// Dispose of all floating instances and the JS module.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        if (_module is not null)
        {
            try
            {
                // Destroy all active instances
                foreach (var id in _activeIds.ToList())
                {
                    await _module.InvokeVoidAsync("destroy", id);
                }
                _activeIds.Clear();

                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected, cleanup already handled
            }
        }
    }
}
```

#### 2.2.4 Update Component Integration

- [ ] Refactor `LumexDropdown.razor.cs` to use FloatingService
- [ ] Refactor `LumexTooltip.razor.cs` to use FloatingService
- [ ] Refactor `LumexPopover.razor.cs` to use FloatingService
- [ ] Add data attributes for JS targeting

**Dropdown Component Example:**

```csharp
// src/LumexUI/Components/Dropdown/LumexDropdown.razor.cs
namespace LumexUI.Components;

using LumexUI.Services;
using Microsoft.AspNetCore.Components;

public partial class LumexDropdown : LumexComponentBase, IAsyncDisposable
{
    [Inject] private FloatingService Floating { get; set; } = default!;

    /// <summary>
    /// Initial placement of the dropdown menu.
    /// </summary>
    [Parameter] public DropdownPlacement Placement { get; set; } = DropdownPlacement.Bottom;

    /// <summary>
    /// Offset from the trigger in pixels.
    /// </summary>
    [Parameter] public int Offset { get; set; } = 8;

    /// <summary>
    /// Slot classes for customizing internal elements.
    /// </summary>
    [Parameter] public DropdownSlots? Slots { get; set; }

    private readonly string _id = Guid.NewGuid().ToString("N")[..8];
    private bool _initialized;
    private bool _isOpen;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (_isOpen && !_initialized)
        {
            await Floating.InitializeAsync(_id, new FloatingOptions(
                Placement: Placement.ToString().ToLowerInvariant(),
                Offset: Offset
            ));
            _initialized = true;
        }
    }

    private async Task Toggle()
    {
        _isOpen = !_isOpen;
        if (!_isOpen && _initialized)
        {
            await Floating.DestroyAsync(_id);
            _initialized = false;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_initialized)
        {
            await Floating.DestroyAsync(_id);
        }
    }
}
```

```razor
@* src/LumexUI/Components/Dropdown/LumexDropdown.razor *@
<div class="@GetRootClasses()">
    <button data-floating-trigger="@_id"
            class="@GetTriggerClasses()"
            @onclick="Toggle">
        @Trigger
    </button>

    @if (_isOpen)
    {
        <div data-floating-element="@_id"
             class="@GetMenuClasses()"
             role="menu">
            @ChildContent
        </div>
    }
</div>
```

### Acceptance Criteria

- [ ] `@floating-ui/dom` installed and bundled in wwwroot
- [ ] `positioning.js` module created with initialize/destroy functions
- [ ] `dom.js` module created with portal/waitForElement helpers
- [ ] `FloatingService` created and registered in DI
- [ ] `FloatingOptions` record created
- [ ] LumexDropdown uses FloatingService for positioning
- [ ] LumexTooltip uses FloatingService for positioning
- [ ] LumexPopover uses FloatingService for positioning
- [ ] Dropdown at right viewport edge automatically shifts left
- [ ] Tooltip near bottom viewport automatically flips to top
- [ ] Position updates correctly on scroll/resize
- [ ] No memory leaks (cleanup called on component dispose)
- [ ] No console errors when navigating away from page with open dropdown

### Testing Checklist

- [ ] **Edge Test:** Open dropdown at right viewport edge - verify it shifts left to stay visible
- [ ] **Flip Test:** Place tooltip trigger at bottom of viewport - verify tooltip flips to top
- [ ] **Scroll Test:** Open dropdown, scroll page - verify dropdown stays attached to trigger
- [ ] **Resize Test:** Open dropdown, resize window - verify position updates correctly
- [ ] **Dispose Test:** Open dropdown, navigate to different page - verify no console errors
- [ ] **Rapid Toggle Test:** Rapidly open/close dropdown - verify no orphaned instances
- [ ] **Portal Test:** Verify floating element is moved to document.body
- [ ] **Z-Index Test:** Verify floating element appears above other content

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| @floating-ui bundle size | Medium | Medium | Tree-shake unused exports; ~15KB gzipped is acceptable |
| Portal breaks event bubbling | Medium | High | Test click-outside handling thoroughly; may need custom event handler |
| JS interop overhead | Low | Low | Lazy-load module; only initialize when needed |
| Memory leaks from orphaned instances | Medium | Medium | Track all IDs in FloatingService; cleanup on dispose |
| Race conditions during rapid toggle | Medium | Low | Use cancellation tokens; debounce rapid operations |
| SSR/prerendering issues | Medium | Medium | Guard JS calls with `OnAfterRenderAsync`; use `firstRender` check |

---

## Migration Notes

### Slot System (Non-Breaking)

- Existing `Class` parameter continues to work unchanged
- `Slots` is purely additive - opt-in for fine-grained control
- No changes required for existing component usage
- Slots apply after base styles, allowing full override via TwMerge

### Floating UI (Minor Breaking)

- Dropdowns may appear in slightly different positions due to auto-flip/shift behavior
- This is generally an improvement (no more clipped dropdowns)
- If exact legacy positioning is required, provide optional parameters:
  - `DisableFlip` - Prevent automatic flipping
  - `DisableShift` - Prevent automatic shifting
- Consider these parameters for edge cases only; default behavior should be preferred

---

## Definition of Done

Phase 2 is complete when:

- [ ] `SlotBase` abstract class created
- [ ] All 4 component slot classes created (AccordionItemSlots, DropdownSlots, ModalSlots, CardSlots)
- [ ] Slot classes have full documentation
- [ ] Components updated to accept and merge Slots parameter
- [ ] Floating UI JavaScript modules created and bundled
- [ ] `FloatingService` created and registered in DI
- [ ] Dropdown, Tooltip, and Popover components use FloatingService
- [ ] All acceptance criteria met
- [ ] All testing checklist items pass
- [ ] No console errors in browser
- [ ] Unit tests pass
- [ ] PR reviewed and merged

---

## Next Phase

Upon completion, proceed to **[Phase 3: Animation System](./03-PHASE-ANIMATION.md)** (P1)
