# Carousel Component Migration Tasks

## Phase 1: Foundation & Core Components

### Task 1.1: Create Directory Structure
**Acceptance Criteria:**
- [x] Create `src/Flowbite/Components/Carousel/` directory
- [x] Create `docs/feature/carousel/` directory

**Commands:**
```powershell
New-Item -ItemType Directory -Path "src/Flowbite/Components/Carousel" -Force
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/
git commit -m "feat(carousel): create component directory structure"
```

---

### Task 1.2: Create Enumeration Definitions
**Acceptance Criteria:**
- [x] Create `CarouselEnums.cs` with `CarouselImageFit` enum
- [x] Create `CarouselEnums.cs` with `CarouselIndicatorPosition` enum
- [x] All enums have XML documentation
- [x] Enums include Tailwind class comments
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselEnums.cs`

**Enum Values:**
- CarouselImageFit: Contain, Cover, Fill, None, ScaleDown
- CarouselIndicatorPosition: Top, Bottom

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselEnums.cs
git commit -m "feat(carousel): add enumeration definitions for ImageFit and IndicatorPosition"
```

---

### Task 1.3: Create CarouselImage Model Class
**Acceptance Criteria:**
- [x] Create `CarouselImage.cs` model class
- [x] Include `Src`, `Alt`, and `Title` properties
- [x] Add XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselImage.cs`

**Properties:**
```csharp
public string Src { get; set; } = string.Empty;
public string Alt { get; set; } = string.Empty;
public string? Title { get; set; }
```

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselImage.cs
git commit -m "feat(carousel): add CarouselImage model class"
```

---

### Task 1.4: Create CarouselState Class
**Acceptance Criteria:**
- [x] Create `CarouselState.cs` class for cascading state
- [x] Include CurrentIndex, TotalSlides properties
- [x] Include GoToSlide, NextSlide, PreviousSlide action delegates
- [x] Add XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselState.cs`

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselState.cs
git commit -m "feat(carousel): add CarouselState class for state management"
```

---

### Task 1.5: Implement Carousel.razor Component
**Acceptance Criteria:**
- [x] Create `Carousel.razor` file
- [x] Include proper using directives and namespace
- [x] Implement grid container layout
- [x] Add CascadingValue for CarouselState
- [x] Support ChildContent rendering
- [x] Include all required HTML attributes
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/Carousel.razor`

**Key Elements:**
- Grid layout with overflow hidden
- Rounded corners (rounded-lg)
- Responsive height classes (h-56 sm:h-64 xl:h-80 2xl:h-96)
- Relative positioning for absolute children
- CascadingValue wrapper for state

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

---

### Task 1.6: Implement Carousel.razor.cs Code-Behind
**Acceptance Criteria:**
- [x] Create `Carousel.razor.cs` file
- [x] Inherit from FlowbiteComponentBase
- [x] Implement all parameters (Index, IndexChanged, AutoAdvanceInterval, etc.)
- [x] Implement navigation methods (NextSlide, PreviousSlide, GoToSlide)
- [x] Implement timer for auto-advance
- [x] Implement IDisposable for timer cleanup
- [x] Implement OnSlideChanged event callback
- [x] Add comprehensive XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/Carousel.razor.cs`

**Key Parameters:**
- Index (int, bindable)
- IndexChanged (EventCallback<int>)
- AutoAdvanceInterval (int?, nullable)
- TransitionDuration (int, default 1000)
- ShowControls (bool, default false)
- ShowIndicators (bool, default false)
- IndicatorPosition (CarouselIndicatorPosition)
- OnSlideChanged (EventCallback<int>)

**Key Methods:**
- NextSlide() - Move to next slide
- PreviousSlide() - Move to previous slide
- GoToSlide(int index) - Jump to specific slide
- StartAutoAdvance() - Start timer
- StopAutoAdvance() - Stop timer
- Dispose() - Clean up timer

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/Carousel.razor
git add src/Flowbite/Components/Carousel/Carousel.razor.cs
git commit -m "feat(carousel): implement main Carousel component with state management and auto-advance"
```

---

### Task 1.7: Implement CarouselItem.razor Component
**Acceptance Criteria:**
- [x] Create `CarouselItem.razor` file
- [x] Include using directives and namespace
- [x] Receive CascadingParameter for CarouselState
- [x] Conditionally render based on active state
- [x] Support both image and custom content rendering
- [x] Apply transition CSS classes
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselItem.razor`

**Key Elements:**
- Absolute positioning
- Block display, full width/height
- Conditional rendering based on index match
- Transition support via CSS classes
- Image rendering with fit mode
- Custom content via ChildContent

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

---

### Task 1.8: Implement CarouselItem.razor.cs Code-Behind
**Acceptance Criteria:**
- [x] Create `CarouselItem.razor.cs` file
- [x] Inherit from FlowbiteComponentBase
- [x] Implement CascadingParameter for CarouselState
- [x] Implement ImageSrc, ImageAlt parameters
- [x] Implement ImageFit parameter with default Cover
- [x] Implement ChildContent parameter
- [x] Implement GetClasses() method for CSS generation
- [x] Add XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselItem.razor.cs`

**Key Parameters:**
- ImageSrc (string, optional)
- ImageAlt (string, optional)
- ImageFit (CarouselImageFit, default Cover)
- ChildContent (RenderFragment, optional)

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselItem.razor
git add src/Flowbite/Components/Carousel/CarouselItem.razor.cs
git commit -m "feat(carousel): implement CarouselItem component with image and content support"
```

---

## Phase 2: Navigation Components

### Task 2.1: Implement CarouselControls.razor Component
**Acceptance Criteria:**
- [x] Create `CarouselControls.razor` file
- [x] Include using directives and namespace
- [x] Receive CascadingParameter for CarouselState
- [x] Render previous button (absolute left)
- [x] Render next button (absolute right)
- [x] Use Flowbite.Icons for arrow icons
- [x] Support custom button content via RenderFragment
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselControls.razor`

**Key Elements:**
- Two button elements
- ChevronLeftIcon and ChevronRightIcon from Flowbite.Icons
- Absolute positioning (start-0 and end-0)
- Focus ring styling
- Background hover effects
- Screen reader labels

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

---

### Task 2.2: Implement CarouselControls.razor.cs Code-Behind
**Acceptance Criteria:**
- [x] Create `CarouselControls.razor.cs` file
- [x] Inherit from FlowbiteComponentBase
- [x] Implement CascadingParameter for CarouselState
- [x] Implement PreviousButtonContent parameter
- [x] Implement NextButtonContent parameter
- [x] Implement click handlers that call state methods
- [x] Add XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselControls.razor.cs`

**Key Parameters:**
- PreviousButtonContent (RenderFragment, optional)
- NextButtonContent (RenderFragment, optional)

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselControls.razor
git add src/Flowbite/Components/Carousel/CarouselControls.razor.cs
git commit -m "feat(carousel): implement CarouselControls component with navigation buttons"
```

---

### Task 2.3: Implement CarouselIndicators.razor Component
**Acceptance Criteria:**
- [x] Create `CarouselIndicators.razor` file
- [x] Include using directives and namespace
- [x] Receive CascadingParameter for CarouselState
- [x] Render indicator for each slide
- [x] Use Indicator component from Flowbite
- [x] Support position variants (top/bottom)
- [x] Apply active/inactive styling
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselIndicators.razor`

**Key Elements:**
- Container div with flex layout
- Absolute positioning
- Centered horizontally
- Button for each slide
- Indicator component
- Position-based classes (top-5 or bottom-5)

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

---

### Task 2.4: Implement CarouselIndicators.razor.cs Code-Behind
**Acceptance Criteria:**
- [x] Create `CarouselIndicators.razor.cs` file
- [x] Inherit from FlowbiteComponentBase
- [x] Implement CascadingParameter for CarouselState
- [x] Implement Position parameter with default Bottom
- [x] Implement ActiveClass and InactiveClass parameters
- [x] Implement click handler for navigation
- [x] Implement GetClasses() method for positioning
- [x] Add XML documentation
- [x] File compiles without errors

**File:** `src/Flowbite/Components/Carousel/CarouselIndicators.razor.cs`

**Key Parameters:**
- Position (CarouselIndicatorPosition, default Bottom)
- ActiveClass (string, optional)
- InactiveClass (string, optional)

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

**Git Commit:**
```bash
git add src/Flowbite/Components/Carousel/CarouselIndicators.razor
git add src/Flowbite/Components/Carousel/CarouselIndicators.razor.cs
git commit -m "feat(carousel): implement CarouselIndicators component with position variants"
```

---

### Task 2.5: Verify All Components Compile Together
**Acceptance Criteria:**
- [x] Build entire Flowbite project
- [x] No compilation errors
- [x] No warnings (or documented acceptable warnings)

**Build Command:**
```bash
dotnet build src/Flowbite/Flowbite.csproj
```

---

## Phase 3: Demo Pages

### Task 3.1: Create Demo Page Structure
**Acceptance Criteria:**
- [x] Create `CarouselPage.razor` in correct location
- [x] Add @page directive with route
- [x] Add PageTitle element
- [x] Create main container with proper spacing
- [x] Add section heading
- [x] File compiles without errors

**File:** `src/DemoApp/Pages/Docs/components/CarouselPage.razor`

**Route:** `/docs/components/carousel`

**Build Command:**
```bash
dotnet build src/DemoApp/DemoApp.csproj
```

---

### Task 3.2: Add Default Carousel Example
**Acceptance Criteria:**
- [x] Create ComponentExample for default carousel
- [x] Include auto-advance demo
- [x] Add description and Razor code
- [x] Use sample images
- [x] Example renders correctly

**Svelte Reference:** https://flowbite-svelte.com/docs/components/carousel#default-carousel

---

### Task 3.3: Add Carousel with Controls Example
**Acceptance Criteria:**
- [x] Create ComponentExample for carousel with controls
- [x] Include CarouselControls component
- [x] Add description and Razor code
- [x] Example renders correctly

**Svelte Reference:** https://flowbite-svelte.com/docs/components/carousel#controls

---

### Task 3.4: Add Carousel with Indicators Example
**Acceptance Criteria:**
- [x] Create ComponentExample for carousel with indicators
- [x] Include CarouselIndicators component
- [x] Add description and Razor code
- [x] Example renders correctly

**Svelte Reference:** https://flowbite-svelte.com/docs/components/carousel#indicators

---

### Task 3.5: Add Carousel with Top Indicators Example
**Acceptance Criteria:**
- [x] Create ComponentExample for carousel with top indicators
- [x] Set Position="CarouselIndicatorPosition.Top"
- [x] Add description and Razor code
- [x] Example renders correctly

---

### Task 3.6: Add Custom Styling Example
**Acceptance Criteria:**
- [x] Create ComponentExample for custom styling
- [x] Demonstrate Class parameter usage
- [x] Show different image fit modes
- [x] Add description and Razor code
- [x] Example renders correctly

**Git Commit:**
```bash
git add src/DemoApp/Pages/Docs/components/CarouselPage.razor
git commit -m "feat(carousel): add demo page with multiple examples"
```

---

### Task 3.7: Update Navigation Sidebar
**Acceptance Criteria:**
- [ ] Open DemoAppSidebar.razor
- [ ] Add Carousel link in Components section
- [ ] Maintain alphabetical order
- [ ] Link points to correct route
- [ ] File compiles without errors

**File:** `src/DemoApp/Layout/DemoAppSidebar.razor`

**Build Command:**
```bash
dotnet build src/DemoApp/DemoApp.csproj
```

**Git Commit:**
```bash
git add src/DemoApp/Layout/DemoAppSidebar.razor
git commit -m "feat(carousel): add navigation link to sidebar"
```

---

## Phase 4: Documentation

### Task 4.1: Create LLMS Documentation
**Acceptance Criteria:**
- [ ] Create `carousel.md` in llms-docs/sections/
- [ ] Document component purpose and features
- [ ] Document all parameters with types and defaults
- [ ] Include usage examples (simple and complex)
- [ ] Document all sub-components
- [ ] Document enums and their values
- [ ] Include common patterns
- [ ] List related components

**File:** `src/DemoApp/wwwroot/llms-docs/sections/carousel.md`

**Sections:**
- Component Overview
- Parameters (Carousel, CarouselItem, CarouselControls, CarouselIndicators)
- Enumerations (CarouselImageFit, CarouselIndicatorPosition)
- Usage Examples
- Common Patterns
- Related Components

---

### Task 4.2: Update Build Script
**Acceptance Criteria:**
- [ ] Open Build-LlmsContext.ps1
- [ ] Add carousel.md to file list
- [ ] Maintain proper ordering
- [ ] File is valid PowerShell

**File:** `src/DemoApp/Build-LlmsContext.ps1`

---

### Task 4.3: Generate Updated LLMS Context
**Acceptance Criteria:**
- [ ] Run Build-LlmsContext.ps1 script
- [ ] Verify carousel documentation is included in llms-ctx.md
- [ ] No errors during generation

**Command:**
```powershell
.\src\DemoApp\Build-LlmsContext.ps1
```

**Git Commit:**
```bash
git add src/DemoApp/wwwroot/llms-docs/sections/carousel.md
git add src/DemoApp/Build-LlmsContext.ps1
git add src/DemoApp/wwwroot/llms-ctx.md
git commit -m "feat(carousel): add component documentation and update LLMS context"
```

---

## Phase 5: Verification with Playwright MCP

### Task 5.1: Setup Verification Environment
**Acceptance Criteria:**
- [ ] Verification directory exists
- [ ] DemoApp is running locally
- [ ] Can access http://localhost:5290
- [ ] Can access Svelte docs at https://flowbite-svelte.com

**Command:**
```bash
dotnet run --project src/DemoApp/DemoApp.csproj
```

---

### Task 5.2: Capture Svelte Reference Screenshots
**Acceptance Criteria:**
- [ ] Capture default carousel screenshot
- [ ] Capture carousel with controls screenshot
- [ ] Capture carousel with indicators screenshot
- [ ] Capture carousel with top indicators screenshot
- [ ] All screenshots saved to verification/ directory

**URL:** https://flowbite-svelte.com/docs/components/carousel

**Screenshots to capture:**
1. verification/svelte-carousel-default.png
2. verification/svelte-carousel-controls.png
3. verification/svelte-carousel-indicators.png
4. verification/svelte-carousel-indicators-top.png

**Use Playwright MCP tools:**
- browser_navigate
- browser_take_screenshot
- browser_close

---

### Task 5.3: Capture Blazor Implementation Screenshots
**Acceptance Criteria:**
- [ ] Capture matching Blazor default carousel screenshot
- [ ] Capture matching Blazor carousel with controls screenshot
- [ ] Capture matching Blazor carousel with indicators screenshot
- [ ] Capture matching Blazor carousel with top indicators screenshot
- [ ] All screenshots saved to verification/ directory

**URL:** http://localhost:5290/docs/components/carousel

**Screenshots to capture:**
1. verification/blazor-carousel-default.png
2. verification/blazor-carousel-controls.png
3. verification/blazor-carousel-indicators.png
4. verification/blazor-carousel-indicators-top.png

**Use Playwright MCP tools:**
- browser_navigate
- browser_take_screenshot
- browser_close

---

### Task 5.4: Visual Comparison and Refinement
**Acceptance Criteria:**
- [ ] Visually compare screenshot pairs
- [ ] Identify any discrepancies in sizing, spacing, colors
- [ ] Address all identified discrepancies
- [ ] Update component CSS classes as needed
- [ ] Rebuild project and re-test visually

**Commands:**
```bash
dotnet build
dotnet run --project src/DemoApp/DemoApp.csproj
```

**Git Commit (if changes made):**
```bash
git add src/Flowbite/Components/Carousel/
git commit -m "feat(carousel): refine component styling based on visual verification"
```

---

## Phase 6: Final Review and Testing

### Task 6.1: Code Quality Review
**Acceptance Criteria:**
- [ ] Review all component files for completeness
- [ ] Verify XML documentation is comprehensive
- [ ] Check parameter naming follows conventions
- [ ] Verify nullable types are used correctly
- [ ] Confirm AdditionalAttributes support
- [ ] Check that CSS classes match Flowbite design
- [ ] Verify no hardcoded values that should be parameters

**Files to review:**
- All Carousel/*.razor files
- All Carousel/*.razor.cs files
- CarouselEnums.cs
- CarouselImage.cs
- CarouselState.cs

---

### Task 6.2: Build Entire Solution
**Acceptance Criteria:**
- [x] Build completes without errors
- [x] No compilation warnings (or all warnings documented)
- [x] Both Flowbite and DemoApp build successfully

**Command:**
```bash
dotnet build
```

---

### Task 6.3: Test Light Mode
**Acceptance Criteria:**
- [ ] Run DemoApp
- [ ] Navigate to carousel demo page
- [ ] Verify all examples render correctly
- [ ] Check colors and styling
- [ ] Test auto-advance functionality
- [ ] Test control button navigation
- [ ] Test indicator navigation
- [ ] Test all image fit modes

**URL:** http://localhost:5290/docs/components/carousel

---

### Task 6.4: Test Dark Mode
**Acceptance Criteria:**
- [ ] Toggle dark mode in demo app
- [ ] Verify all examples still render correctly
- [ ] Check dark mode colors and contrast
- [ ] Verify control buttons are visible
- [ ] Verify indicators are visible
- [ ] Test all interactions work

---

### Task 6.5: Test Responsive Behavior
**Acceptance Criteria:**
- [ ] Test mobile view (320px width)
- [ ] Test tablet view (768px width)
- [ ] Test desktop view (1024px width)
- [ ] Test large desktop view (1440px width)
- [ ] Verify height breakpoints work (h-56 sm:h-64 xl:h-80 2xl:h-96)
- [ ] Verify controls remain accessible at all sizes
- [ ] Verify indicators remain visible at all sizes

**Use Playwright MCP:**
- browser_resize tool

---

### Task 6.6: Update CHANGELOG Files
**Acceptance Criteria:**
- [ ] Update src/Flowbite/CHANGELOG.md
- [ ] Update src/DemoApp/CHANGELOG.md
- [ ] Document new Carousel component
- [ ] List all sub-components
- [ ] Note version number (likely 0.0.x-alpha)
- [ ] Follow existing CHANGELOG format

**Files:**
- src/Flowbite/CHANGELOG.md
- src/DemoApp/CHANGELOG.md

**Git Commit:**
```bash
git add src/Flowbite/CHANGELOG.md
git add src/DemoApp/CHANGELOG.md
git commit -m "docs(carousel): update project changelogs"
```

---

## Phase 7: Completion

### Task 7.1: Review All Commits
**Acceptance Criteria:**
- [ ] View commit history on feature branch
- [ ] Verify all commits are present
- [ ] Check commit messages follow format
- [ ] Ensure commits are atomic and logical

**Command:**
```bash
git log --oneline feature/migrate-carousel-component ^develop
```

---

### Task 7.2: Request Push Approval
**Acceptance Criteria:**
- [ ] Present summary of all commits to user
- [ ] Ask for explicit push approval
- [ ] Wait for user response

**Note:** Do NOT push without explicit user approval

---

### Task 7.3: Push to Remote (If Approved)
**Acceptance Criteria:**
- [ ] User has approved push
- [ ] Push feature branch to remote
- [ ] Verify push was successful

**Command:**
```bash
git push -u origin feature/migrate-carousel-component
```

---

### Task 7.4: Final Completion
**Acceptance Criteria:**
- [ ] Use attempt_completion tool
- [ ] Summarize what was accomplished
- [ ] List all files created/modified
- [ ] Show feature branch name
- [ ] Indicate if changes were pushed
- [ ] Provide demo page URL
- [ ] Note verification results
- [ ] Mention next steps (create PR)

---

---

## Phase 8: Optional Enhancements (User Decision Required)

### Task 8.1: Prompt User for Enhanced Features
**Acceptance Criteria:**
- [ ] Present completed carousel component to user
- [ ] Ask if user wants to implement previously out-of-scope features
- [ ] Present list of optional enhancements available
- [ ] Wait for user decision on which enhancements to implement

**Optional Enhancements Available:**
1. **Drag/swipe gesture support** - Touch and mouse drag navigation (requires JavaScript interop)
2. **Thumbnail navigation component** - CarouselThumbnails component for thumbnail-based navigation
3. **Custom transition effects** - Additional transition options beyond CSS (fade, slide, zoom)
4. **Image lazy loading** - Performance optimization for large image sets
5. **Video content support** - Support for video slides in addition to images
6. **Keyboard navigation** - Arrow key support for navigation
7. **Custom indicator styles** - Support for numbered indicators, progress bars, etc.

**User Prompt:**
"The core Carousel component is now complete and functional. Would you like to implement any of the following optional enhancements that were initially marked as out of scope?"

**Options:**
- Implement all enhancements
- Implement specific enhancements (specify which ones)
- Skip enhancements and complete the migration

---

## Summary Statistics

**Total Tasks:** 47 (46 core + 1 optional decision point)
**Total Phases:** 8 (7 core + 1 optional)
**Estimated Components:** 9 files minimum (4 component pairs + 1 enum file)
**Estimated Time:** 4-6 hours core implementation (additional time for optional enhancements)

## Dependencies

- Flowbite.Icons (for navigation arrows)
- Flowbite.Components.Indicator (for dot indicators)
- System.Timers.Timer (for auto-advance)

## Initially Out of Scope (Available as Optional Enhancements in Phase 8)

- Drag/swipe gesture support (requires JavaScript interop)
- Thumbnail navigation component
- Custom transition effects beyond CSS
- Image lazy loading
- Video content support
- Keyboard navigation
- Custom indicator styles
