# Carousel Component Migration Plan

## Overview

Migrate the Flowbite Svelte Carousel component to Flowbite Blazor, creating a fully functional image/content slider with navigation controls, indicators, and auto-advance capabilities.

## Analysis

### Svelte Approach

The Flowbite Svelte Carousel is a sophisticated component with the following structure:

**Core Components:**
1. **Carousel.svelte** - Main container with state management
   - Manages current slide index
   - Handles auto-advance timer
   - Provides drag/swipe gesture support
   - Controls transitions between slides
   - Exposes state via Svelte context

2. **Slide.svelte** - Individual slide component
   - Displays image with configurable fit mode
   - Handles transition animations (fly effect)
   - Supports custom transitions

3. **Controls.svelte** - Navigation control wrapper
   - Provides previous/next navigation
   - Customizable control buttons

4. **ControlButton.svelte** - Individual control button
   - SVG icons for navigation
   - Positioned absolutely on carousel edges
   - Hover and focus states

5. **CarouselIndicators.svelte** - Dot indicators
   - Shows all slides as clickable dots
   - Highlights active slide
   - Position variants (top/bottom)

6. **Thumbnails.svelte** - Thumbnail navigation (optional)

**Key Features:**
- Auto-advance with configurable interval
- Drag/swipe gesture support (touch and mouse)
- Smooth CSS transitions
- Multiple image fit modes (contain, cover, fill, none, scale-down)
- Bindable index for external control
- Change event callback
- Image preloading option
- Fully customizable styling
- Accessibility support

**Theme System:**
Uses tailwind-variants with slots for:
- `base` - Main carousel container
- `slide` - Individual slide styling
- `indicator` - Dot indicator styling
- `controlButton` - Navigation button styling

**Variants:**
- Slide fit: contain, cover, fill, none, scale-down
- Indicator position: top, bottom
- Indicator selected state: true/false
- Control direction: forward/backward

### Blazor Approach

**Component Architecture:**

1. **Carousel.razor/.razor.cs** - Main carousel component
   - Manages slide index state
   - Implements auto-advance timer using `System.Timers.Timer`
   - Provides navigation methods (Next, Previous, GoToSlide)
   - Supports both image array and RenderFragment children
   - Exposes `OnSlideChanged` event callback
   - Uses cascading value to share state with child components

2. **CarouselItem.razor/.razor.cs** - Individual slide wrapper
   - Receives cascading carousel state
   - Determines if currently active
   - Applies transition CSS classes
   - Supports both image src or custom content via RenderFragment

3. **CarouselControls.razor/.razor.cs** - Previous/Next buttons
   - Receives cascading carousel state
   - Calls parent navigation methods
   - Customizable button content
   - Default SVG icons (using Flowbite.Icons)

4. **CarouselIndicators.razor/.razor.cs** - Dot indicators
   - Receives cascading carousel state
   - Renders indicator for each slide
   - Handles click navigation
   - Position variants

**Differences from Svelte:**
- **No drag/swipe support** - Would require JavaScript interop, considered out of scope for v1
- **CSS-based transitions** - Use CSS classes instead of Svelte transitions
- **Simpler state management** - No context API, use cascading values
- **Timer-based auto-advance** - Use C# Timer instead of JS setInterval
- **Event callbacks** - Use EventCallback<T> instead of Svelte events

**Enumerations:**

```csharp
// CarouselEnums.cs
public enum CarouselImageFit
{
    Contain,    // object-contain
    Cover,      // object-cover
    Fill,       // object-fill
    None,       // object-none
    ScaleDown   // object-scale-down
}

public enum CarouselIndicatorPosition
{
    Top,        // top-5
    Bottom      // bottom-5
}
```

**Parameter Structure:**

**Carousel Parameters:**
- `Images` - List<CarouselImage> (optional, for image-based carousels)
- `ChildContent` - RenderFragment (optional, for custom content)
- `Index` - int (current slide index, bindable)
- `IndexChanged` - EventCallback<int> (for two-way binding)
- `AutoAdvanceInterval` - int? (milliseconds, null = no auto-advance)
- `TransitionDuration` - int (default 1000ms)
- `ShowControls` - bool (default false)
- `ShowIndicators` - bool (default false)
- `IndicatorPosition` - CarouselIndicatorPosition (default Bottom)
- `OnSlideChanged` - EventCallback<CarouselSlideChangedEventArgs>
- `Class` - string (additional CSS classes)
- `AdditionalAttributes` - capture unmatched

**CarouselItem Parameters:**
- `ImageSrc` - string (optional)
- `ImageAlt` - string (optional)
- `ImageFit` - CarouselImageFit (default Cover)
- `ChildContent` - RenderFragment (optional custom content)
- `Class` - string
- `AdditionalAttributes` - capture unmatched

**CarouselControls Parameters:**
- `PreviousButtonContent` - RenderFragment (optional custom button)
- `NextButtonContent` - RenderFragment (optional custom button)
- `Class` - string
- `AdditionalAttributes` - capture unmatched

**CarouselIndicators Parameters:**
- `Position` - CarouselIndicatorPosition
- `ActiveClass` - string (custom active indicator class)
- `InactiveClass` - string (custom inactive indicator class)
- `Class` - string
- `AdditionalAttributes` - capture unmatched

**Helper Classes:**

Carousel will use inline CSS class generation rather than a separate helper class, keeping it simple.

**State Management:**

Use cascading values to share carousel state:

```csharp
public class CarouselState
{
    public int CurrentIndex { get; set; }
    public int TotalSlides { get; set; }
    public Action<int> GoToSlide { get; set; }
    public Action NextSlide { get; set; }
    public Action PreviousSlide { get; set; }
}
```

## Project Structure

```
src/Flowbite/Components/Carousel/
├── Carousel.razor
├── Carousel.razor.cs
├── CarouselItem.razor
├── CarouselItem.razor.cs
├── CarouselControls.razor
├── CarouselControls.razor.cs
├── CarouselIndicators.razor
├── CarouselIndicators.razor.cs
└── CarouselEnums.cs

src/DemoApp/Pages/Docs/components/
└── CarouselPage.razor

src/DemoApp/wwwroot/llms-docs/sections/
└── carousel.md

docs/feature/carousel/
├── plan.md (this file)
├── tasks.md
└── completion-summary.md (created at end)

verification/
├── svelte-carousel-default.png
├── blazor-carousel-default.png
├── svelte-carousel-controls.png
├── blazor-carousel-controls.png
├── svelte-carousel-indicators.png
├── blazor-carousel-indicators.png
└── summary.md
```

## Component Specifications

### Carousel (Main Component)

**Purpose:** Container component that manages slide state and auto-advance

**Rendering:**
- Outer div with grid layout and overflow hidden
- Renders either images or child content
- Cascades state to child components
- Manages timer for auto-advance

**Key Methods:**
- `NextSlide()` - Advance to next slide
- `PreviousSlide()` - Go to previous slide
- `GoToSlide(int index)` - Jump to specific slide
- `StartAutoAdvance()` - Start auto-advance timer
- `StopAutoAdvance()` - Stop auto-advance timer

**Lifecycle:**
- OnInitialized: Set up initial state
- OnParametersSet: Handle index changes, restart timer if needed
- Dispose: Clean up timer

### CarouselItem

**Purpose:** Individual slide wrapper with transition support

**Rendering:**
- Absolute positioned wrapper
- Conditionally renders image or custom content
- Applies CSS transition classes based on state
- Only visible when active

**CSS Classes:**
- Base: `absolute block w-full h-full`
- Transition: CSS transition for opacity/transform
- Fit mode: `object-{fit}` classes for images

### CarouselControls

**Purpose:** Previous/Next navigation buttons

**Rendering:**
- Two button elements (previous and next)
- Default: SVG arrow icons from Flowbite.Icons
- Optional: Custom RenderFragment for buttons

**Features:**
- Absolute positioning on left/right edges
- Hover and focus states
- Screen reader labels

### CarouselIndicators

**Purpose:** Dot indicators for all slides

**Rendering:**
- Container div with flex layout
- Button for each slide
- Visual indicator using `Indicator` component
- Positioned at top or bottom

**Features:**
- Click to navigate
- Visual active state
- Aria labels for accessibility

## Enumeration Definitions

### CarouselImageFit

Maps to CSS object-fit values:

```csharp
/// <summary>
/// Defines how carousel images fit within their container.
/// </summary>
public enum CarouselImageFit
{
    /// <summary>
    /// Image is scaled to maintain aspect ratio while fitting within the container (object-contain).
    /// </summary>
    Contain,

    /// <summary>
    /// Image is scaled to maintain aspect ratio while filling the container (object-cover).
    /// </summary>
    Cover,

    /// <summary>
    /// Image is stretched to fill the container (object-fill).
    /// </summary>
    Fill,

    /// <summary>
    /// Image is displayed at its original size (object-none).
    /// </summary>
    None,

    /// <summary>
    /// Image is scaled down to the smaller of contain or none (object-scale-down).
    /// </summary>
    ScaleDown
}
```

### CarouselIndicatorPosition

Positioning for indicators:

```csharp
/// <summary>
/// Defines the position of carousel indicators.
/// </summary>
public enum CarouselIndicatorPosition
{
    /// <summary>
    /// Indicators positioned at the top (top-5).
    /// </summary>
    Top,

    /// <summary>
    /// Indicators positioned at the bottom (bottom-5).
    /// </summary>
    Bottom
}
```

## Verification Strategy with Playwright MCP

### 1. Reference Capture
Capture screenshots from Flowbite Svelte documentation:
- https://flowbite-svelte.com/docs/components/carousel

Examples to capture:
1. Default carousel (auto-advance)
2. Carousel with controls
3. Carousel with indicators
4. Carousel with indicators at top position
5. Custom styling examples

### 2. Implementation Capture
After implementing Blazor version, capture matching examples from local demo:
- http://localhost:5290/docs/components/carousel

### 3. Comparison Points
- Container dimensions (h-56 sm:h-64 xl:h-80 2xl:h-96)
- Image scaling/fit behavior
- Control button positioning and styling
- Indicator positioning and spacing
- Transition smoothness (visual review)
- Dark mode appearance

### 4. Refinement
- Adjust CSS classes to match Svelte appearance
- Fine-tune spacing and sizing
- Verify responsive breakpoints
- Test all fit modes

## Implementation Phases

### Phase 1: Foundation & Core Components
1. Create directory structure
2. Create CarouselEnums.cs
3. Implement Carousel.razor and Carousel.razor.cs
4. Implement CarouselItem.razor and CarouselItem.razor.cs
5. Verify compilation

### Phase 2: Navigation Components
1. Implement CarouselControls.razor and CarouselControls.razor.cs
2. Implement CarouselIndicators.razor and CarouselIndicators.razor.cs
3. Verify all components compile together

### Phase 3: Demo Pages
1. Create CarouselPage.razor
2. Add default carousel example
3. Add controls example
4. Add indicators example
5. Add custom styling examples
6. Update navigation sidebar

### Phase 4: Documentation
1. Create carousel.md in llms-docs/sections
2. Update Build-LlmsContext.ps1
3. Generate llms-ctx.md

### Phase 5: Verification with Playwright MCP
1. Capture Svelte reference screenshots
2. Run DemoApp locally
3. Capture Blazor implementation screenshots
4. Compare and document findings
5. Refine implementation based on comparison
6. Re-verify until visual parity achieved

### Phase 6: Final Review
1. Code quality review
2. Build and test entire solution
3. Test dark mode
4. Test responsive behavior
5. Update CHANGELOG files
6. Create completion summary

## Success Criteria

- [ ] All components compile without errors
- [ ] Demo page shows multiple working examples
- [ ] Auto-advance functionality works correctly
- [ ] Navigation controls work (previous/next)
- [ ] Indicators work (click to navigate, visual active state)
- [ ] All image fit modes render correctly
- [ ] Dark mode styling works
- [ ] Responsive behavior matches Svelte version
- [ ] Documentation is complete and accurate
- [ ] Visual appearance matches Svelte version (verified with screenshots)
- [ ] No console errors or warnings
- [ ] Code follows Flowbite Blazor conventions
- [ ] Git commits are incremental and well-documented

## Notes

- Drag/swipe support is **out of scope** for initial implementation (would require JS interop)
- Thumbnail navigation is **out of scope** for initial implementation (can be added later)
- Custom transitions beyond CSS are **out of scope** for initial implementation
- Focus on core functionality: display, navigation, indicators, auto-advance
