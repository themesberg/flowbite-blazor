<div align="center">
  <h1>Flowbite Blazor</h1>
  <p>
    Build websites even faster with components on top of Blazor and Tailwind CSS
  </p>
  <p>
    <a href="https://discord.com/invite/4eeurUVvTy">
      <img src="https://img.shields.io/discord/902911619032576090?color=%237289da&label=Discord" alt="Discord">
    </a>
    <a href="https://flowbite.com/getting-started/license/">
      <img src="https://img.shields.io/badge/license-MIT-blue" alt="License">
    </a>
    <a href="https://www.nuget.org/packages/Flowbite">
      <img src="https://img.shields.io/nuget/v/Flowbite" alt="NuGet">
    </a>
  </p>
</div>

---

<div align="center">
  <p />
  <p>
    <img alt="Flowbite Blazor Demo" src="docs/assests/screenshot.png" />
  </p>
</div>

---

## Overview

Flowbite Blazor is a Blazor component library that ports [Flowbite](https://flowbite.com) to ASP.NET Blazor 8/9 on top of Tailwind CSS. It provides 30+ UI components styled with the Flowbite design system, including forms, navigation, modals, and more.

**Current Status:** Beta (`v0.2.x-beta`) - APIs are stabilizing but may change.

### Live Demos

- **Production:** https://flowbite-blazor.org/
- **Development:** https://develop.flowbite-blazor-8s8.pages.dev/

---

## Installation

### Prerequisites

- .NET 8.0 or .NET 9.0 SDK
- Node.js 18+ (for Tailwind CSS)
- Tailwind CSS v4.x

### 1. Install the NuGet Package

```bash
dotnet add package Flowbite
```

Or add to your `.csproj`:

```xml
<PackageReference Include="Flowbite" Version="0.2.1-beta" />
```

### 2. Register Flowbite Services

In your `Program.cs`:

```csharp
using Flowbite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Flowbite services (includes TailwindMerge, FloatingUI, lazy-loaded JS services)
builder.Services.AddFlowbite();
```

### 3. Add CSS References

In your `App.razor` or `_Host.cshtml`:

```html
<head>
    <!-- Flowbite component styles -->
    <link href="_content/Flowbite/flowbite.min.css" rel="stylesheet" />

    <!-- Your app styles (includes Tailwind) -->
    <link href="css/app.min.css" rel="stylesheet" />
</head>
```

### 4. Add JavaScript References

Before the closing `</body>` tag:

```html
<!-- Floating UI for viewport-aware dropdowns/tooltips -->
<script src="https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.6.3/dist/floating-ui.dom.umd.min.js"></script>

<!-- Flowbite JavaScript -->
<script src="_content/Flowbite/flowbite.js"></script>
```

### 5. Configure Tailwind CSS v4

Flowbite Blazor requires **Tailwind CSS v4.x** with CSS-first configuration.

Create or update your `app.css`:

```css
/* Tailwind CSS v4 - CSS-first configuration */
@import "tailwindcss";

/* Dark mode configuration (required for proper class-based dark mode) */
@config "./tailwind.config.js";

/* Flowbite plugin for component styles */
@plugin "flowbite/plugin";

/* Source paths for class scanning */
@source "./**/*.{razor,html,cshtml,cs}";

/* Theme configuration - customize your primary color */
@theme {
    --color-primary-50: #eff6ff;
    --color-primary-100: #dbeafe;
    --color-primary-200: #bfdbfe;
    --color-primary-300: #93c5fd;
    --color-primary-400: #60a5fa;
    --color-primary-500: #3b82f6;
    --color-primary-600: #2563eb;
    --color-primary-700: #1d4ed8;
    --color-primary-800: #1e40af;
    --color-primary-900: #1e3a8a;
    --color-primary-950: #172554;
}
```

Create `tailwind.config.js` for dark mode support:

```javascript
// tailwind.config.js (minimal - just for dark mode)
module.exports = {
    darkMode: 'class'
};
```

### 6. Add Imports

In your `_Imports.razor`:

```razor
@using Flowbite.Components
@using Flowbite.Icons
```

---

## Features

### Component Library

| Category | Components |
|----------|------------|
| **Forms** | TextInput, TextArea, Select, Checkbox, Radio, FileInput, ToggleSwitch, RangeSlider, Combobox |
| **Navigation** | Navbar, Sidebar, Breadcrumb, Dropdown |
| **Feedback** | Alert, Badge, Toast, Spinner, Modal, Drawer |
| **Data Display** | Card, Avatar, Table, Timeline, Carousel |
| **Layout** | Button, ButtonGroup |
| **Typography** | Heading, Paragraph, Span |

### New in v0.2.x

- **TailwindMerge Integration** - Intelligent CSS class conflict resolution
- **Slot System** - Per-element CSS customization for complex components
- **Floating UI** - Viewport-aware positioning for dropdowns and tooltips
- **Debounced Input** - Search-as-you-type with configurable delay
- **Animation State Machine** - Smooth height-animated expand/collapse
- **Lazy JavaScript Modules** - On-demand loading for reduced bundle size
- **Motion-Reduce Support** - Respects `prefers-reduced-motion` preference

---

## Customization Guide

### Primary Color (Recommended)

Flowbite Blazor uses the Flowbite design system. Customize your brand's primary color via Tailwind v4's `@theme` directive:

```css
/* Your app.css */
@import "tailwindcss";

@theme {
    /* Example: Green brand color */
    --color-primary-50: #f0fdf4;
    --color-primary-100: #dcfce7;
    --color-primary-200: #bbf7d0;
    --color-primary-300: #86efac;
    --color-primary-400: #4ade80;
    --color-primary-500: #22c55e;  /* Your brand green */
    --color-primary-600: #16a34a;
    --color-primary-700: #15803d;
    --color-primary-800: #166534;
    --color-primary-900: #14532d;
    --color-primary-950: #052e16;
}
```

### Layout Customization (Slots)

For structural changes (spacing, borders, shadows), use the `Slots` parameter:

```razor
@* Card with custom styling *@
<Card Slots="@(new CardSlots {
    Base = "shadow-xl rounded-2xl",
    Body = "p-8"
})">
    Custom styled card content
</Card>

@* Dropdown with custom menu width *@
<Dropdown Slots="@(new DropdownSlots {
    Menu = "w-72 shadow-lg",
    Item = "hover:bg-primary-100"
})">
    <Label>Options</Label>
    <ChildContent>
        <DropdownItem>Wide menu item</DropdownItem>
    </ChildContent>
</Dropdown>

@* Modal with custom styling *@
<Modal Slots="@(new ModalSlots {
    Content = "rounded-2xl max-w-2xl",
    Header = "border-b-2 border-primary-500"
})">
    Modal content here
</Modal>
```

**Available Slot Classes:**

| Component | Slots |
|-----------|-------|
| `Card` | `Base`, `Image`, `Body` |
| `Dropdown` | `Base`, `Trigger`, `Menu`, `Item` |
| `Modal` | `Backdrop`, `Content`, `Header`, `Body`, `Footer` |
| `AccordionItem` | `Base`, `Trigger`, `Content` |

### Component Class Override

All components accept a `Class` parameter that merges with default styles:

```razor
<Button Class="px-8 py-4">Large padded button</Button>

<Card Class="border-2 border-primary-500">Highlighted card</Card>
```

TailwindMerge intelligently resolves conflicts, so your classes override defaults.

---

## Feature Guide

### Debounced Input

For search-as-you-type scenarios, use the `Behavior` and `DebounceDelay` parameters:

```razor
@* Search with 300ms debounce *@
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search users..." />

@code {
    private string SearchQuery { get; set; } = "";

    // ValueChanged fires 300ms after user stops typing
    // Typing during delay cancels the previous pending call
}
```

**Behavior Options:**

| Value | Description |
|-------|-------------|
| `InputBehavior.OnChange` | Fire on blur or Enter (default, standard HTML behavior) |
| `InputBehavior.OnInput` | Fire on every keystroke (subject to `DebounceDelay`) |

### Floating UI Positioning

Dropdowns, Tooltips, and Combobox components automatically use Floating UI for smart positioning:

```razor
@* Dropdown that auto-flips when near viewport edge *@
<Dropdown>
    <Label>Options</Label>
    <ChildContent>
        <DropdownItem>Item 1</DropdownItem>
        <DropdownItem>Item 2</DropdownItem>
    </ChildContent>
</Dropdown>

@* Tooltip with auto-positioning *@
<Tooltip Content="This tooltip will flip to stay visible">
    <Button>Hover me</Button>
</Tooltip>
```

**Floating UI Behaviors:**

- **Flip**: Automatically changes placement (e.g., bottom → top) when constrained
- **Shift**: Slides along the axis to stay within viewport bounds
- **Offset**: Maintains 8px gap between trigger and floating element

### Smooth Collapse Animations

Accordion and Sidebar collapse animations use a state machine for smooth height transitions:

```razor
<Sidebar>
    <SidebarItemGroup>
        @* Smooth animated expand/collapse *@
        <SidebarCollapse Label="E-commerce">
            <SidebarItem Href="/products">Products</SidebarItem>
            <SidebarItem Href="/billing">Billing</SidebarItem>

            @* Nested collapses supported *@
            <SidebarCollapse Label="Settings">
                <SidebarItem Href="/general">General</SidebarItem>
                <SidebarItem Href="/advanced">Advanced</SidebarItem>
            </SidebarCollapse>
        </SidebarCollapse>
    </SidebarItemGroup>
</Sidebar>
```

Animation respects `prefers-reduced-motion` - users with this preference get instant transitions.

### Lazy-Loaded Services

JavaScript modules load on-demand for faster initial page load:

```csharp
// Services are automatically registered via AddFlowbite()
// Or register individually:
services.AddFlowbiteClipboardService();    // IClipboardService
services.AddFlowbiteElementService();       // IElementService
services.AddFlowbiteFocusManagementService(); // IFocusManagementService
```

```razor
@inject IClipboardService Clipboard

<Button @onclick="CopyText">Copy to Clipboard</Button>

@code {
    private async Task CopyText()
    {
        // clipboard.js module loads only on first use
        await Clipboard.CopyToClipboardAsync("Hello, World!");
    }
}
```

### Accessibility

All components support:

- **Keyboard Navigation**: Arrow keys, Tab, Enter, Escape
- **ARIA Attributes**: Proper roles, labels, and descriptions
- **Motion-Reduce**: Respects `prefers-reduced-motion` preference

```razor
@* Focus ring styling on keyboard navigation *@
<Dropdown>
    @* ArrowUp/Down: navigate items *@
    @* Home/End: jump to first/last *@
    @* Enter/Space: select item *@
    @* Escape: close dropdown *@
    @* Type-ahead: jump to matching item *@
</Dropdown>
```

---

## Component List

### Core Components

- **Alert** - Contextual feedback messages
- **Avatar** - User profile images with fallback
- **Badge** - Status indicators and counts
- **Breadcrumb** - Navigation hierarchy
- **Button** - Actions with variants and sizes
- **Card** - Content containers
- **Drawer** - Slide-out panels
- **Dropdown** - Action menus with keyboard support
- **Modal** - Dialog overlays
- **Navbar** - Top navigation bars
- **Sidebar** - Vertical navigation with collapsible sections
- **Spinner** - Loading indicators
- **Toast** - Notification messages
- **Tooltip** - Hover information

### Form Components

- **TextInput** - Text entry with debouncing support
- **TextArea** - Multi-line text
- **Select** - Option selection
- **Checkbox** - Binary selection
- **Radio** - Single selection from group
- **FileInput** - File upload
- **ToggleSwitch** - On/off toggle
- **RangeSlider** - Numeric range
- **Combobox** - Searchable dropdown

### Data Display

- **Carousel** - Image/content slides
- **Timeline** - Event sequences
- **Table** - Data grids (QuickGrid integration)

### Typography

- **Heading** - H1-H6 with gradient support
- **Paragraph** - Body text
- **Span** - Inline text styling

### Icons

- **90+ core icons** in `Flowbite.Icons`
- **200+ extended icons** in `Flowbite.ExtendedIcons` package

---

## Additional Resources

### AI-Supported Development

Provide this URL to your AI code assistant for Flowbite Blazor context:

**https://flowbite-blazor.peakflames.org/llms-ctx.md**

### Documentation

- [Component Documentation](https://flowbite-blazor.org/docs)
- [Migration Guide](./docs/MIGRATION.md)
- [Changelog](./src/Flowbite/CHANGELOG.md)

### Related Packages

| Package | Description |
|---------|-------------|
| `Flowbite` | Core component library |
| `Flowbite.ExtendedIcons` | 200+ additional icons |
| `Flowbite.Templates.Wasm` | Project templates for Blazor WebAssembly |

---

## Breaking Changes

See [MIGRATION.md](./docs/MIGRATION.md) for upgrade instructions.

### v0.2.0-beta

- `Button.Style` renamed to `Button.Variant`
- `ButtonStyle` enum renamed to `ButtonVariant`
- `Tooltip.Style` renamed to `Tooltip.Theme`

---

## Contributing

We welcome contributions! Please see [CONTRIBUTING.md](./CONTRIBUTING.md) for guidelines.

### Development Setup

```bash
# Clone the repository
git clone https://github.com/themesberg/flowbite-blazor.git
cd flowbite-blazor

# Build the solution
python build.py

# Run the demo app with hot reload
python build.py watch
```

---

## License

MIT License - see [LICENSE](./LICENSE) for details.

---

<div align="center">
  <p>
    <a href="https://flowbite.com">
      <img src="https://flowbite.com/docs/images/logo.svg" alt="Flowbite" width="100">
    </a>
  </p>
  <p>
    Built on the <a href="https://flowbite.com">Flowbite</a> design system
  </p>
</div>
