# Migration Guide

This guide documents breaking changes and provides migration instructions for upgrading Flowbite Blazor.

---

## Version 0.2.0-beta (Upcoming)

### Breaking Changes

#### 1. Button.Style renamed to Button.Variant

The `Style` parameter on `Button` was renamed to `Variant` to free up `Style` for inline CSS styles.

**Before:**
```razor
<Button Style="ButtonStyle.Outline">Click me</Button>
<Button Style="ButtonStyle.Outline" Color="ButtonColor.Red">Delete</Button>
```

**After:**
```razor
<Button Variant="ButtonStyle.Outline">Click me</Button>
<Button Variant="ButtonStyle.Outline" Color="ButtonColor.Red">Delete</Button>
```

**Find & Replace:** `Style="ButtonStyle.` → `Variant="ButtonStyle.`

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

**Find & Replace:** On Tooltip elements, replace `Style="light"` → `Theme="light"`, `Style="dark"` → `Theme="dark"`, `Style="auto"` → `Theme="auto"`

---

#### 3. New Base Class Parameters (Non-Breaking Enhancement)

All components now inherit `Style` and `AdditionalAttributes` from `FlowbiteComponentBase`. This is an additive change that enables new capabilities:

- **`Style`** - Apply inline CSS styles to any component
- **`AdditionalAttributes`** - Pass arbitrary HTML attributes (data-*, aria-*, etc.)

**New capabilities:**
```razor
@* Inline styles *@
<Button Style="margin-top: 1rem; min-width: 200px">
    Styled Button
</Button>

@* Data attributes for testing *@
<Button data-testid="submit-btn" data-action="save">
    Submit
</Button>

@* ARIA attributes *@
<Button aria-label="Close dialog" aria-pressed="false">
    X
</Button>

@* Combined usage *@
<Card Style="max-width: 400px" data-section="profile" aria-labelledby="card-title">
    <Heading id="card-title">Profile</Heading>
</Card>
```

**Note for component authors:** If you had custom `AdditionalAttributes` parameters in component subclasses, they are now inherited from the base class and can be removed.

---

### Migration Steps

1. **Update Button.Style to Button.Variant**
   ```bash
   # In your IDE or using grep/sed:
   # Find: Style="ButtonStyle.
   # Replace: Variant="ButtonStyle.
   ```

2. **Update Tooltip.Style to Tooltip.Theme**
   ```bash
   # Find Tooltip elements with Style parameter and update:
   # Style="light" → Theme="light"
   # Style="dark" → Theme="dark"
   # Style="auto" → Theme="auto"
   ```

3. **Remove duplicate AdditionalAttributes** (component authors only)
   - If you created custom components inheriting from `FlowbiteComponentBase`
   - Remove any `[Parameter(CaptureUnmatchedValues = true)] AdditionalAttributes` declarations
   - They are now inherited from the base class

4. **Verify build passes**
   ```bash
   dotnet build
   ```

5. **Test your application** to ensure components render correctly

---

## Version 0.1.x-beta

No breaking changes.

---

## Version 0.0.x-alpha

Initial development releases. API stability not guaranteed.
