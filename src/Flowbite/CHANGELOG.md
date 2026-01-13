# Changelog

## 0.2.1-beta

### Added
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

### Changed
- Migrate all 30 components from `CombineClasses()` to `MergeClasses()` + `ElementClass` pattern
  - Enables TailwindMerge.NET intelligent CSS class conflict resolution
  - User-provided `Class` parameter now properly overrides component defaults
  - Components: Card, Combobox, Modal, ModalBody, ModalFooter, ModalHeader, Drawer, DrawerHeader, DrawerItems, Navbar, NavbarLink, SidebarCTA, SidebarLogo, Dropdown, DropdownItem, Carousel, CarouselIndicators, CarouselItem, ChatMessage, ChatMessageContent, Activity, ActivityItem, Group, GroupItem, Timeline, TimelineItem, Heading, Paragraph, Span, TableContext
- Add `motion-reduce:transition-none` accessibility support to all animated components
  - Drawer, Modal, Sidebar, SidebarCollapse, Tooltip, Toast, Card, Combobox
  - CarouselIndicators, CarouselItem, PromptInput components, ToggleSwitch
  - Respects `prefers-reduced-motion: reduce` user preference
- Add smooth animations to Sidebar and SidebarCollapse components
  - SidebarCollapse expand/collapse with max-height + opacity transitions
  - Chevron icon rotation animation on toggle
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
