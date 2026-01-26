# Changelog

## 0.2.6-beta

### Added
- Add `Skeleton` component for loading placeholder animations
  - Six variants: Text, Avatar, Thumbnail, Button, Card, Input
  - Multi-line support with `Lines` parameter for Text variant
  - `prefers-reduced-motion` support via `motion-reduce:animate-none`
  - Customizable dimensions with `Width` and `Height` parameters
  - Accessibility features: `role="status"`, `aria-busy="true"`, screen reader text
- Add `EmptyState` component for displaying placeholder content when no data is available
  - Support for custom icons and images/illustrations
  - `Image` slot takes precedence over `Icon` for larger illustrations
  - Primary and secondary action button slots
  - Customizable title and description
- Add `Pagination` component for navigating through pages of data
  - First/Last page navigation buttons (`ShowFirstLast`)
  - Ellipsis display for large page ranges (`ShowEllipsis`)
  - Go-to-page input field (`ShowGoToPage`)
  - Items-per-page selector dropdown (`ShowPageSizeSelector`)
  - Size variants: Small, Default, Large
  - Full ARIA accessibility support with proper labels and roles

### Changed
- Component enhancements bringing parity with shadcn/ui, Mantine, Ant Design features

## 0.2.5-beta

### Added
- Initial implementation of Skeleton, EmptyState, and Pagination components

### Fixed
- N/A

### Changed
- N/A

## 0.2.4-beta

### Added
- `python build.py test-publish` command to catch pre-rendering errors during publish

### Fixed
- Fix pre-rendering errors in ChatAiPage and ToolbarPage caused by missing `@bind-Value` bindings
- Refactor ChatAiPage.AiProviderConfig to use mutable ApiKey property for proper binding support

## 0.2.3-beta

### Added
- Add automatic validation color changes for form components (#17)
  - TextInput, Textarea, and Select components now automatically change to Failure color when validation errors occur
  - New `FlowbiteInputBase<TValue>` base class integrating with Blazor's EditForm validation
  - Components subscribe to `EditContext.OnValidationStateChanged` to update styling when validation state changes
  - `EffectiveColor` property automatically returns Failure when `HasValidationErrors` is true
  - TailwindMerge integration ensures proper CSS class conflict resolution (e.g., bg-gray-50 vs bg-red-50)

### Fixed
- Fix build warnings CS8604 in TextInput.razor by using conditional rendering instead of null ternary
- Fix memory leak in Select and Textarea components by adding `Dispose()` override to call `base.Dispose()`

### Changed
- **BREAKING:** Form components now require `@bind-Value` instead of `Value`/`ValueChanged` parameters (#17)
  - TextInput, Textarea, and Select components now inherit from `InputBase<TValue>` (via `FlowbiteInputBase<TValue>`)
  - Use `@bind-Value="model.Property"` instead of `Value="@value" ValueChanged="@OnValueChanged"`
  - `ValueExpression` is automatically provided by the `@bind-Value` directive
  - Migration: Replace all `Value` and `ValueChanged` usages with `@bind-Value` in EditForm contexts
- **BREAKING:** `TextInput.Color`, `Textarea.Color`, and `Select.Color` parameters are now nullable (#17)
  - Changed from `TextInputColor`/`SelectColor` to `TextInputColor?`/`SelectColor?`
  - When `Color` is `null` (default), components automatically use Failure color when validation errors occur
  - Migration: Explicit `Color` assignments continue to work; `null` enables automatic validation colors

## 0.2.2-beta

### Added
- Add `Flowbite.Tests` project with automated testing infrastructure (Phase 5.6)
  - bUnit for Blazor component unit tests
  - Playwright for end-to-end integration tests
  - 45 unit tests covering Debouncer, ElementClass, TailwindMerge, CollapseState, TextInput, TextArea, Select
  - 2 Playwright smoke tests as golden examples for future E2E tests
  - `python build.py test` command for running unit tests
  - `python build.py test-integration` command for running E2E tests
  - `FlowbiteTestContext` base class for component tests with pre-configured services
  - `PlaywrightFixture` for browser lifecycle management
  - Comprehensive `CLAUDE.md` AI guidance for test development

### Changed
- Update `CONTRIBUTING.md` with Testing section and test requirements
- Simplify development setup in `CONTRIBUTING.md` to use `build.py` commands

## 0.2.1-beta

### Added
- Add lazy-loaded JavaScript module services for improved initial load performance
  - `IClipboardService` - lazy-loaded clipboard operations via `clipboard.js` ES module
  - `IElementService` - lazy-loaded element utilities (scroll height, focus, etc.) via `element.module.js`
  - `IFocusManagementService` - lazy-loaded focus trap and body scroll management via `focus-management.module.js`
  - All services use `Lazy<Task<IJSObjectReference>>` pattern for on-demand module loading
  - Register via `AddFlowbiteLazyServices()` or individually
  - `CopyToClipboardButton` now uses `IClipboardService` for clipboard operations
- Add `CollapseState` enum and animation state machine for SidebarCollapse component
  - Four states: Collapsed, Expanding, Expanded, Collapsing
  - Smooth height-based animations using CSS transitions
  - Mid-animation toggle support (reverse direction on click during animation)
  - Timer fallback ensures state transitions complete reliably
  - Full support for deeply nested collapses (multi-level sidebars)
- Add `ElementReferenceExtensions.GetScrollHeightAsync()` extension method for JS interop height measurement
- Add `flowbiteBlazor.getScrollHeight()` JavaScript function for element height measurement
- Add keyboard navigation and focus management for Dropdown and Tooltip components
  - Dropdown: ArrowUp/Down navigation, Home/End, Enter/Space selection, Escape to close, type-ahead search
  - Dropdown: Focus ring styling with `ring-2 ring-primary-500` classes
  - Dropdown: Proper ARIA roles (`role="menu"`, `role="menuitem"`) and keyboard semantics
  - Tooltip: Focus/blur handlers for show/hide on focus
  - Tooltip: Escape key dismissal support
  - Tooltip: ARIA linkage via `aria-describedby` and `role="tooltip"`
- Add `ElementClass` fluent builder utility for CSS class composition with conditional logic
- Add Slot System for per-element CSS class customization within complex components
- Add Floating UI integration for viewport-aware positioning of Dropdown, Tooltip, and Combobox components
  - `FloatingService` - C# service for JavaScript interop with @floating-ui/dom
  - Automatic flip/shift middleware ensures elements stay within viewport boundaries
  - Arrow positioning for Tooltip component
  - Combobox dropdown now uses Floating UI for viewport-aware positioning
  - `SlotBase` abstract base class with `Base` property
  - `CardSlots` for Card component (Base, Image, Body)
  - `DropdownSlots` for Dropdown component (Base, Trigger, Menu, Item)
  - `ModalSlots` for Modal component (Backdrop, Content, Header, Body, Footer)
  - `AccordionItemSlots` for future Accordion component
  - All slot classes use TailwindMerge for intelligent conflict resolution

### Fixed
- Fix Tooltip positioning flash on show by using `invisible` + `absolute` positioning until Floating UI calculates position
- Fix dark mode specificity issue in Tailwind v4 by using `@config` directive instead of `@custom-variant`
  - `@custom-variant dark (&:where(.dark, .dark *))` generates zero-specificity selectors that get overridden
  - `@config` with `darkMode: 'class'` generates proper `:is(.dark *)` selectors with correct specificity
- Fix Tooltip width being constrained to trigger element width by adding `w-max` class
- Fix nested SidebarCollapse components not growing parent container
  - Added timer fallback (350ms) to ensure animation state transitions complete even when CSS `transitionend` event doesn't fire
  - Parent collapses now properly expand to accommodate nested child content
  - Component implements `IDisposable` for proper timer cleanup

### Changed
- **BREAKING:** TextInput styling updated to match Flowbite React v2
  - Info color changed from primary/blue to cyan colors
  - Size Small changed from `p-2 text-sm` to `p-2 sm:text-xs`
  - Size Large changed from `sm:text-md p-4` to `p-4 sm:text-base`
  - Base classes now include `focus:outline-none focus:ring-1` for better focus styling
  - Gray color classes moved from base to color switch for proper TailwindMerge conflict resolution
- **Migrate to Tailwind CSS v4.1.18** - major infrastructure upgrade
  - Build system now uses Tailwind v4 standalone CLI with built-in PostCSS
  - CSS files converted to v4 syntax: `@import "tailwindcss"` replaces `@tailwind` directives
  - Content paths now configured via `@source` directive in CSS
  - Theme values defined in `@theme` blocks as CSS custom properties
  - Flowbite plugin loaded via `@plugin "flowbite/plugin"` directive
  - No breaking changes to component APIs - styling continues to work identically
- Migrate all 30 components from `CombineClasses()` to `MergeClasses()` + `ElementClass` pattern
  - Enables TailwindMerge.NET intelligent CSS class conflict resolution
  - User-provided `Class` parameter now properly overrides component defaults
  - Components: Card, Combobox, Modal, ModalBody, ModalFooter, ModalHeader, Drawer, DrawerHeader, DrawerItems, Navbar, NavbarLink, SidebarCTA, SidebarLogo, Dropdown, DropdownItem, Carousel, CarouselIndicators, CarouselItem, ChatMessage, ChatMessageContent, Activity, ActivityItem, Group, GroupItem, Timeline, TimelineItem, Heading, Paragraph, Span, TableContext
- Add `motion-reduce:transition-none` accessibility support to all animated components
  - Drawer, Modal, Sidebar, SidebarCollapse, Tooltip, Toast, Card, Combobox
  - CarouselIndicators, CarouselItem, PromptInput components, ToggleSwitch
  - Respects `prefers-reduced-motion: reduce` user preference
- Rewrite SidebarCollapse animations with state machine architecture
  - Replace max-height approach with explicit height-based transitions
  - Use `transition-[height]` CSS property for smooth expand/collapse
  - Chevron icon rotation animation on toggle (180° rotation)
- Improve Carousel slide transitions with scale + opacity effect (500ms duration)

## 0.2.0-beta

### Added
- Add `Style` parameter to `FlowbiteComponentBase` for inline CSS styles on all components
- Add `AdditionalAttributes` parameter to `FlowbiteComponentBase` for arbitrary HTML attributes (data-*, aria-*, etc.)
- Add `docs/MIGRATION.md` with breaking change documentation and migration instructions

### Changed
- **BREAKING:** Rename `Button.Style` parameter to `Button.Variant`
- **BREAKING:** Rename `ButtonStyle` enum to `ButtonVariant`
- **BREAKING:** Rename `Tooltip.Style` parameter to `Tooltip.Theme`
- Remove duplicate `AdditionalAttributes` declarations from 56+ components (now inherited from base)

## 0.1.4-beta

- Add TailwindMerge.NET integration for intelligent CSS class conflict resolution
- Add `MergeClasses()` helper method to `FlowbiteComponentBase`

## 0.1.3-beta

- Fix Blazor Server input lag and character loss in TextInput and PromptInputTextarea components (#15)

## 0.1.2-beta

- Fix ToggleSwitch backing color css classes

## 0.1.1-beta

- Update Timeline component styling to simplify user-styling and bring consistency.

## 0.1.0-beta

- Added AI Chat primitives:
    - PromptInput + footer/header/actions infrastructure
    - Conversation host/content with auto-scroll behaviours
    - Reasoning, Sources, and PromptInput model select building blocks
    - Toast/Drawer interop helpers shipped via `flowbite.js`
- Added headless `Combobox` + `ComboboxItem` components with keyboard support, inline search/filtering, and external value binding hooks
- Introduced timeline component suite:
    - `Timeline` with `TimelineOrder` enum for Default, Vertical, Horizontal, Activity layouts
    - `TimelineItem` supporting color variants, date formatting (`TimelineDateFormat`), custom orientation content, and connector management
    - `Activity`/`ActivityItem` with `ActivityTimelineItem` model for activity feeds
    - `Group`/`GroupItem` with `GroupTimelineItem` model for grouped timelines
- Added `TimelineColor` enum to control indicator and connector accents
- Updated timeline layout spacing to keep indicators aligned within the new gutter

## 0.0.12-alpha

- Add Carousel component with support for:
    - Image and custom content slides
    - Navigation controls (previous/next buttons)
    - Slide indicators with customizable position
    - Auto-advance functionality with configurable interval
    - Multiple image fit options (Cover, Contain, Fill, ScaleDown, None)
    - Two-way data binding with @bind-Index
    - Smooth transitions with configurable duration
    - OnSlideChanged event callback
- Add CarouselEnums (CarouselImageFit, CarouselIndicatorPosition)
- Add CarouselImage model class
- Add CarouselState for cascading state management
- Add initial implementation of Toolbar component
- Add Typography components:
    - Heading component with gradient support, custom sizes, weights, and colors
    - Paragraph component with comprehensive text styling options
    - Span component for inline text styling
- Add TypographyEnums with shared typography enumerations (TextSize, FontWeight, LineHeight, TextAlign, LetterSpacing, Whitespace, GradientColor)

## 0.0.11-alpha

- Fixed Select component data binding issue [#1]
- Add initial implementation of Modal component
- Add initial implemetation of Drawer component
- Add initial implementation of Toast component
- Add `UserIcon` component

## 0.0.10-alpha

- Fixed Dropdown onclick event handling
- Replaced all cyan with primary colors
- Add CSS classes and Theme for Microsoft QuickGrid component
- Navbar: Add support for responsive menus

## 0.0.9-alpha

- Added Form controls:
    - TextInport
    - TextArea
    - Select
    - Checkbox
    - Radio
    - FileInput
    - ToggeSwitch
    - RangeSlider
    - Vaidation examples.

## 0.0.8-alpha

- Added Primary as color enum to Button and Alert components.
- Added Tree component

## 0.0.7-alpha

- update flowbite.min.css

## 0.0.6-alpha

- enhancements to CodeBlock and CopyToClipboardButton

## 0.0.5-alpha

- simply the icon base class

## 0.0.4-alpha

- CodeBlock: add CopyToClipboard

## 0.0.3-alpha

- Namespace fixes

## 0.0.2-alpha

- Rolled all assemblies to v0.0.2-alpha

## 0.0.1-alpha.5

- Added ClipboardArrowIcon component

## 0.0.1-alpha.4

- Initial public release
