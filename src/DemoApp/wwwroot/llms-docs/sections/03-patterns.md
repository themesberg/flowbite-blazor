<doc title="Common Patterns" description="Common patterns and best practices for Flowbite Blazor">

## TailwindMerge Integration

Flowbite Blazor uses TailwindMerge.NET to intelligently resolve conflicting Tailwind CSS classes. User-provided classes override component defaults.

### How It Works

Components use `MergeClasses()` in their code-behind to combine base classes with user overrides:

```csharp
// Component internal pattern
private string GetButtonClasses() =>
    MergeClasses(
        ElementClass.Empty()
            .Add("px-4 py-2 rounded-lg")  // Component defaults
            .Add("bg-blue-500", when: Color == ButtonColor.Primary)
            .Add(Class)  // User's Class parameter wins
    );
```

### Usage in Your Code

When you add a `Class` parameter, TailwindMerge resolves conflicts:

```razor
<!-- Component default: p-4 -->
<!-- Your override: p-8 -->
<!-- Result: p-8 (user wins, no duplicate padding) -->
<Card Class="p-8">
    Content with larger padding
</Card>

<!-- Spacing conflict resolved -->
<Button Class="px-6 py-3">
    Custom padded button
</Button>
```

### ElementClass Fluent Builder

For custom components, use `ElementClass` for readable conditional classes:

```csharp
// In your component's code-behind
private string GetClasses() => MergeClasses(
    ElementClass.Empty()
        .Add("base-class")
        .Add("conditional-class", when: SomeCondition)
        .Add("another-class", when: !SomeCondition)
        .Add(Class)
);
```

---

## Slot System

Slots provide fine-grained control over component styling without modifying component source code.

### Available Slot Types

| Component | Slot Class | Available Slots |
|-----------|-----------|-----------------|
| Card | `CardSlots` | Base, Image, Body |
| Dropdown | `DropdownSlots` | Base, Trigger, Menu, Item |
| Modal | `ModalSlots` | Base, Backdrop, Content, Header, Body, Footer |
| AccordionItem | `AccordionItemSlots` | Trigger, Content |

### Usage Pattern

```razor
<!-- Card with custom slots -->
<Card Slots="@(new CardSlots {
    Base = "shadow-xl rounded-xl",
    Image = "object-top",
    Body = "p-8"
})">
    <h5>Card Title</h5>
    <p>Card content with extra padding</p>
</Card>

<!-- Dropdown with custom trigger and menu -->
<Dropdown Slots="@(new DropdownSlots {
    Trigger = "bg-blue-600 hover:bg-blue-700",
    Menu = "w-64 shadow-xl",
    Item = "hover:bg-blue-50"
})">
    <Label>Actions</Label>
    <ChildContent>
        <DropdownItem>Edit</DropdownItem>
        <DropdownItem>Delete</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Modal with custom styling -->
<Modal Show="@show" Slots="@(new ModalSlots {
    Backdrop = "bg-gray-900/70",
    Content = "rounded-xl shadow-2xl",
    Header = "border-b-2 border-blue-500",
    Body = "p-8"
})">
    <ModalHeader>Custom Header</ModalHeader>
    <ModalBody>Content here</ModalBody>
    <ModalFooter>Footer content</ModalFooter>
</Modal>
```

---

## Debounced Input Pattern

For search-as-you-type scenarios, use the `Behavior` and `DebounceDelay` parameters to reduce API calls.

### Basic Usage

```razor
<!-- Standard input (fires on blur/Enter) -->
<TextInput @bind-Value="username" />

<!-- Search input with debouncing (fires 300ms after typing stops) -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search..." />

@code {
    private string SearchQuery { get; set; } = "";

    // Called 300ms after user stops typing
    // Previous pending calls are cancelled automatically
}
```

### InputBehavior Options

| Value | Description |
|-------|-------------|
| `OnChange` (default) | Fire on blur or Enter key - standard form behavior |
| `OnInput` | Fire on every keystroke (subject to DebounceDelay) |

### Best Practices

```razor
<!-- Search with debouncing -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search products..." />

<!-- Instant validation (no debounce needed) -->
<TextInput
    @bind-Value="Email"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="0"
    Color="@GetValidationColor()" />

<!-- Form field (default - fires on blur) -->
<TextInput @bind-Value="Username" />
```

---

## Lazy Service Injection

Flowbite services are lazy-loaded for better performance. Inject them as needed:

```csharp
@inject ClipboardService Clipboard
@inject IFloatingService Floating

@code {
    private async Task CopyToClipboard(string text)
    {
        // Module loads on first use
        var success = await Clipboard.CopyToClipboardAsync(text);
    }
}
```

---

## Event Handling

Components use standard Blazor event handling:

```razor
<Button OnClick="@HandleClick">
    Click me
</Button>

@code {
    private void HandleClick()
    {
        // Handle the click event
    }
}

<!-- With parameters -->
<Dropdown>
    <Label>Actions</Label>
    <ChildContent>
        <DropdownItem OnClick="@(() => HandleItemClick(item.Id))">
            @item.Name
        </DropdownItem>
    </ChildContent>
</Dropdown>
```

---

## Dark Mode

Enable dark mode by adding the 'dark' class to any parent element:

```razor
<!-- Dark mode container -->
<div class="dark">
    <!-- Components will use dark mode styles -->
    <Alert Color="AlertColor.Info">
        This alert uses dark mode styles
    </Alert>

    <Card>
        <h5 class="text-gray-900 dark:text-white">
            Dark mode card
        </h5>
        <p class="text-gray-700 dark:text-gray-400">
            Content adapts automatically
        </p>
    </Card>
</div>
```

---

## Floating UI Positioning

Dropdown, Tooltip, and Popover components use Floating UI for smart positioning:

```razor
<!-- Dropdown auto-flips when near viewport edge -->
<Dropdown Placement="DropdownPlacement.Bottom">
    <Label>Menu</Label>
    <ChildContent>
        <DropdownItem>Option 1</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Disable auto-positioning for fixed placement -->
<Dropdown
    Placement="DropdownPlacement.Top"
    DisableFlip="true"
    DisableShift="true">
    ...
</Dropdown>

<!-- Tooltip with offset -->
<Tooltip Content="Help text" Placement="TooltipPlacement.Right">
    <Button>Hover me</Button>
</Tooltip>
```

### Floating UI Features

| Feature | Description |
|---------|-------------|
| Flip | Auto-flips placement when near viewport edge |
| Shift | Shifts along axis to stay in viewport |
| Offset | Configurable distance from trigger |
| Auto-update | Recalculates position on scroll/resize |

---

## Keyboard Navigation

Interactive components support full keyboard navigation:

### Dropdown
- `Enter`/`Space`: Open dropdown, select item
- `ArrowDown`/`ArrowUp`: Navigate items
- `Home`/`End`: Jump to first/last item
- `Escape`: Close dropdown
- `Tab`: Close and move focus
- Type-ahead: Type to jump to matching item

### Tooltip
- `Escape`: Close tooltip

### Modal
- `Escape`: Close modal (when Dismissible="true")
- Focus trap: Tab cycles within modal

---

## motion-reduce Support

Components respect the user's reduced motion preference:

```css
/* Applied automatically via Tailwind */
.motion-reduce:transition-none { }
```

Animations are disabled for users with `prefers-reduced-motion: reduce` in their OS settings.

</doc>
