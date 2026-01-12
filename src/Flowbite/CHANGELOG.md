# Changelog

## 0.2.1-beta

### Added
- Add `ElementClass` fluent builder utility for CSS class composition with conditional logic
- Refactor `Button` component to use `ElementClass` pattern as demonstration

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
