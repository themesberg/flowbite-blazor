# Migration Guide

This guide documents breaking changes and provides migration instructions for upgrading Flowbite Blazor.

---

## Version 0.2.3-beta

### Breaking Changes

#### 1. Form Components Require @bind-Value

TextInput, Textarea, and Select components now inherit from `InputBase<TValue>` (via `FlowbiteInputBase<TValue>`) and **require** the `@bind-Value` directive instead of separate `Value`/`ValueChanged` parameters.

**Before:**
```razor
<TextInput Value="@email" ValueChanged="@OnEmailChanged" />
<Textarea Value="@message" ValueChanged="@OnMessageChanged" />
<Select Value="@country" ValueChanged="@OnCountryChanged">
    <option value="US">United States</option>
</Select>

@code {
    private string email = "";
    private string message = "";
    private string country = "";

    private void OnEmailChanged(string value) => email = value;
    private void OnMessageChanged(string value) => message = value;
    private void OnCountryChanged(string value) => country = value;
}
```

**After:**
```razor
<TextInput @bind-Value="email" />
<Textarea @bind-Value="message" />
<Select @bind-Value="country">
    <option value="US">United States</option>
</Select>

@code {
    private string email = "";
    private string message = "";
    private string country = "";
}
```

**For EditForm contexts (recommended):**
```razor
<EditForm Model="@model" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />

    <TextInput TValue="string" @bind-Value="model.Email" />
    <ValidationMessage For="@(() => model.Email)" />

    <Textarea @bind-Value="model.Message" />
    <ValidationMessage For="@(() => model.Message)" />

    <Select @bind-Value="model.Country">
        <option value="">Select country</option>
        <option value="US">United States</option>
    </Select>
    <ValidationMessage For="@(() => model.Country)" />

    <Button Type="submit">Submit</Button>
</EditForm>
```

**Migration steps:**
1. Replace `Value="@variable"` with `@bind-Value="variable"`
2. Remove `ValueChanged` parameter and handler methods
3. For generic types (e.g., `int`), specify `TValue`: `<TextInput TValue="int" @bind-Value="model.Age" />`

---

#### 2. Color Parameters Are Now Nullable

The `Color` parameter on TextInput, Textarea, and Select is now nullable (`TextInputColor?` / `SelectColor?`).

**What changed:**
- When `Color` is `null` (the new default), components **automatically** display red/Failure color when validation errors occur
- When `Color` is explicitly set, the component uses that color regardless of validation state

**Before:**
```razor
@* Color defaulted to Gray, no automatic validation styling *@
<TextInput @bind-Value="model.Email" />
```

**After:**
```razor
@* Color is null by default - automatically shows red on validation errors *@
<TextInput @bind-Value="model.Email" />

@* To force a specific color regardless of validation state: *@
<TextInput @bind-Value="model.Email" Color="TextInputColor.Gray" />
```

**Impact:** Forms inside `<EditForm>` now automatically show visual validation feedback without additional code. If you relied on inputs staying gray during validation errors, explicitly set `Color="TextInputColor.Gray"`.

---

### New Features (Non-Breaking)

#### Automatic Validation Color Changes

Form components now automatically integrate with Blazor's EditForm validation:

- **Red/Failure color** appears when field has validation errors
- **Default color** returns when validation passes
- No additional code required - just use `@bind-Value` inside an `EditForm`

```razor
<EditForm Model="@model" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />

    @* Input automatically turns red when validation fails *@
    <TextInput TValue="string" @bind-Value="model.Email" Type="email" />
    <ValidationMessage For="@(() => model.Email)" />

    <Button Type="submit">Submit</Button>
</EditForm>

@code {
    private MyModel model = new();

    public class MyModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
    }
}
```

---

### Migration Steps for 0.2.3-beta

1. **Update form component bindings:**
   ```bash
   # Find patterns like:
   # Value="@variable" ValueChanged="@handler"
   # Replace with:
   # @bind-Value="variable"
   ```

2. **Remove obsolete ValueChanged handlers** - no longer needed with `@bind-Value`

3. **Add TValue for non-string types:**
   ```razor
   <TextInput TValue="int" @bind-Value="model.Age" Type="number" />
   <TextInput TValue="decimal" @bind-Value="model.Price" />
   ```

4. **Test forms inside EditForm** - validation colors now appear automatically

5. **Explicitly set Color if needed** - if you don't want automatic validation colors:
   ```razor
   <TextInput @bind-Value="model.Email" Color="TextInputColor.Gray" />
   ```

6. **Verify build passes:**
   ```bash
   dotnet build
   ```

---

## Version 0.2.1-beta

### New Features (Non-Breaking)

#### Debounced Input for TextInput

TextInput now supports debounced input for search-as-you-type scenarios:

```razor
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search..." />

@code {
    private string SearchQuery { get; set; } = "";
    // ValueChanged fires 300ms after user stops typing
}
```

#### Lazy-Loaded JavaScript Services

New services with on-demand module loading for reduced initial bundle size:

```csharp
// Automatically registered via AddFlowbite(), or add individually:
services.AddFlowbiteClipboardService();    // IClipboardService
services.AddFlowbiteElementService();       // IElementService
services.AddFlowbiteFocusManagementService(); // IFocusManagementService
```

#### Animation State Machine for Collapse

SidebarCollapse now uses smooth height-based animations with a state machine:

- Four states: `Collapsed`, `Expanding`, `Expanded`, `Collapsing`
- Supports mid-animation toggle (reverse direction on click)
- Full nested collapse support
- Respects `prefers-reduced-motion` preference

#### Floating UI Integration

Dropdown, Tooltip, and Combobox components now use @floating-ui/dom for viewport-aware positioning:

- **Flip**: Automatically changes placement when constrained by viewport
- **Shift**: Slides along the axis to stay within bounds
- **Arrow positioning**: Tooltip arrows adjust to actual placement

#### Keyboard Navigation

Enhanced keyboard support for Dropdown and Tooltip:

- **Dropdown**: ArrowUp/Down, Home/End, Enter/Space, Escape, type-ahead search
- **Tooltip**: Focus/blur handlers, Escape dismissal, ARIA linkage

---

## Version 0.2.0-beta

### Breaking Changes

#### 1. Button.Style renamed to Button.Variant

The `Style` parameter on `Button` was renamed to `Variant` to free up `Style` for inline CSS styles. The `ButtonStyle` enum was also renamed to `ButtonVariant`.

**Before:**
```razor
<Button Style="ButtonStyle.Outline">Click me</Button>
<Button Style="ButtonStyle.Outline" Color="ButtonColor.Red">Delete</Button>
```

**After:**
```razor
<Button Variant="ButtonVariant.Outline">Click me</Button>
<Button Variant="ButtonVariant.Outline" Color="ButtonColor.Red">Delete</Button>
```

**Find & Replace (two-step):**
1. `Style="ButtonStyle.` → `Variant="ButtonStyle.`
2. `ButtonStyle.` → `ButtonVariant.`

---

#### 2. Tooltip.Style renamed to Tooltip.Theme

The `Style` parameter on `Tooltip` was renamed to `Theme` for the same reason.

**Before:**
```razor
<Tooltip Content="Hello" Style="light">
    <Button>Light tooltip</Button>
</Tooltip>

<Tooltip Content="Hello" Style="dark">
    <Button>Dark tooltip</Button>
</Tooltip>
```

**After:**
```razor
<Tooltip Content="Hello" Theme="light">
    <Button>Light tooltip</Button>
</Tooltip>

<Tooltip Content="Hello" Theme="dark">
    <Button>Dark tooltip</Button>
</Tooltip>
```

**Find & Replace:**
- `Style="light"` → `Theme="light"` (on Tooltip elements)
- `Style="dark"` → `Theme="dark"` (on Tooltip elements)
- `Style="auto"` → `Theme="auto"` (on Tooltip elements)

---

#### 3. Tailwind CSS v4 Required

Flowbite Blazor now requires **Tailwind CSS v4.x** with CSS-first configuration.

**Before (Tailwind v3):**
```javascript
// tailwind.config.js
module.exports = {
  content: ['./**/*.razor'],
  theme: {
    extend: {
      colors: {
        primary: { /* ... */ }
      }
    }
  },
  plugins: [require('flowbite/plugin')]
}
```

**After (Tailwind v4):**
```css
/* app.css */
@import "tailwindcss";
@config "./tailwind.config.js";
@plugin "flowbite/plugin";
@source "./**/*.{razor,html,cshtml,cs}";

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

```javascript
// tailwind.config.js (minimal - just for dark mode)
module.exports = {
    darkMode: 'class'
}
```

---

#### 4. TailwindMerge Class Conflict Resolution

**What changed:** TailwindMerge.NET now resolves conflicting Tailwind classes.

**Before:** `Class="p-4"` + component's `p-2` rendered both `p-2 p-4`
**After:** `Class="p-4"` + component's `p-2` renders `p-4` only

**Impact:** User classes now properly override component defaults. Review any custom classes that relied on both appearing in the output.

**If you need both classes (rare):** Use arbitrary values: `p-[8px]` instead of `p-2`.

---

#### 5. Floating Element Positioning (@floating-ui)

**What changed:** Dropdowns, tooltips, and popovers now use @floating-ui/dom for positioning.

**Before (CSS-only):** No repositioning; elements could overflow viewport
**After (@floating-ui):** Smart positioning with automatic flip and shift

**Impact:** Elements auto-flip when near viewport edges and shift to stay visible. Test floating elements; behavior may differ near screen edges.

---

### New Base Class Parameters (Non-Breaking Enhancement)

All components now inherit `Style` and `AdditionalAttributes` from `FlowbiteComponentBase`:

```razor
@* Inline styles *@
<Button Style="margin-top: 1rem; min-width: 200px">
    Styled Button
</Button>

@* Data attributes *@
<Button data-testid="submit-btn" data-action="save">
    Submit
</Button>

@* ARIA attributes *@
<Button aria-label="Close dialog" aria-pressed="false">
    X
</Button>
```

**For component authors:** Remove any duplicate `AdditionalAttributes` parameters from component subclasses - they're now inherited.

---

### Migration Steps

1. **Update Button.Style to Button.Variant and ButtonStyle to ButtonVariant**
   ```bash
   # Step 1 - Fix property name:
   # Find: Style="ButtonStyle.
   # Replace: Variant="ButtonStyle.

   # Step 2 - Fix enum name:
   # Find: ButtonStyle.
   # Replace: ButtonVariant.
   ```

2. **Update Tooltip.Style to Tooltip.Theme**
   ```bash
   # Find Tooltip elements with Style parameter and update:
   # Style="light" → Theme="light"
   # Style="dark" → Theme="dark"
   # Style="auto" → Theme="auto"
   ```

3. **Migrate Tailwind CSS to v4**
   - Replace `tailwind.config.js` content paths with `@source` in CSS
   - Move theme colors to `@theme` block in CSS
   - Keep minimal `tailwind.config.js` for `darkMode: 'class'`

4. **Add Floating UI Script**
   ```html
   <script src="https://cdn.jsdelivr.net/npm/@floating-ui/dom@1.6.3/dist/floating-ui.dom.umd.min.js"></script>
   ```

5. **Remove duplicate AdditionalAttributes** (component authors only)
   - If you created custom components inheriting from `FlowbiteComponentBase`
   - Remove any `[Parameter(CaptureUnmatchedValues = true)] AdditionalAttributes` declarations

6. **Verify build passes**
   ```bash
   dotnet build
   ```

7. **Test your application** to ensure components render correctly

---

## FAQ

**Q: My dropdown appears in a different position than before.**
A: @floating-ui now auto-flips to stay in viewport. This is expected behavior and improves UX on smaller screens.

**Q: Can I customize secondary/success/danger colors?**
A: No, these are part of the Flowbite design system. Customize your primary brand color via `@theme`, and use the built-in semantic colors for consistency.

**Q: The collapse animation is instant, not smooth.**
A: Check if you or your users have "Reduce motion" enabled in OS settings. The library respects this accessibility preference via `prefers-reduced-motion`.

**Q: TailwindMerge removed a class I needed.**
A: TailwindMerge resolves conflicts by keeping the last class. If you need both (rare), use arbitrary values: `p-[8px]` instead of `p-2`.

**Q: How do I migrate my custom Tailwind config?**
A: See the Tailwind v4 migration guide at https://tailwindcss.com/docs/upgrade-guide. Key changes:
- `tailwind.config.js` theme is replaced by CSS `@theme` directive
- `require()` plugins replaced by `@plugin`
- Content paths replaced by `@source` directive

**Q: Will my existing component classes break?**
A: Standard Tailwind classes work the same. Only conflicting classes (like `p-2 p-4`) are now resolved instead of both being applied.

**Q: Do I need to add Floating UI for all components?**
A: Only if you use Dropdown, Tooltip, or Combobox components. If not, the script can be omitted.

---

## Version 0.1.x-beta

No breaking changes.

---

## Version 0.0.x-alpha

Initial development releases. API stability not guaranteed.
