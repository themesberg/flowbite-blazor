# TailwindMerge Migration Guide

This guide helps migrate components from `CombineClasses()` to the new `ElementClass` + `MergeClasses()` pattern.

## Why Migrate?

**Before (CombineClasses):** Simply concatenates strings - duplicate/conflicting classes cause issues
```csharp
// User passes Class="p-8" but component has p-4 - BOTH are applied (conflict!)
CombineClasses("p-4 bg-white", Class)  // → "p-4 bg-white p-8" (BROKEN)
```

**After (MergeClasses):** Intelligently resolves Tailwind conflicts
```csharp
// User passes Class="p-8" and component has p-4 - User wins!
MergeClasses("p-4 bg-white", Class)  // → "bg-white p-8" (CORRECT)
```

---

## Quick Reference

| Pattern | Before | After |
|---------|--------|-------|
| Simple concatenation | `CombineClasses("a", "b")` | `MergeClasses("a", "b")` |
| With Class param | `CombineClasses(baseClasses, Class)` | `MergeClasses(baseClasses, Class)` |
| Conditional classes | `if (cond) classes.Add("x")` | `.Add("x", when: cond)` |
| Multiple conditionals | `List<string>` + joins | `ElementClass.Empty().Add(...).Add(...)` |

---

## Migration Patterns

### Pattern 1: Simple String Concatenation

**Before:**
```csharp
private string ComponentClasses => CombineClasses("flex gap-4", Class);
```

**After:**
```csharp
private string ComponentClasses => MergeClasses("flex gap-4", Class);
```

---

### Pattern 2: List-Based Class Building

**Before (Card.razor.cs style):**
```csharp
private string? ComponentClasses
{
    get
    {
        var classes = new List<string>
        {
            BaseClasses,
            Horizontal ? "flex-col md:flex-row" : "flex-col",
        };

        if (!string.IsNullOrEmpty(Href))
        {
            classes.Add("cursor-pointer hover:bg-gray-100");
        }

        return CombineClasses(string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c))));
    }
}
```

**After (Button.razor.cs style):**
```csharp
private string ComponentClasses => MergeClasses(
    ElementClass.Empty()
        .Add(BaseClasses)
        .Add(Horizontal ? "flex-col md:flex-row" : "flex-col")
        .Add("cursor-pointer hover:bg-gray-100", when: !string.IsNullOrEmpty(Href))
        .Add(Class)
);
```

---

### Pattern 3: Multiple Helper Methods

**Before:**
```csharp
private string GetClasses()
{
    var result = BaseClasses;
    result += " " + GetSizeClasses();
    result += " " + GetColorClasses();
    if (Disabled) result += " opacity-50";
    return CombineClasses(result, Class);
}
```

**After:**
```csharp
private string GetClasses() => MergeClasses(
    ElementClass.Empty()
        .Add(BaseClasses)
        .Add(GetSizeClasses())
        .Add(GetColorClasses())
        .Add("opacity-50", when: Disabled)
        .Add(Class)
);
```

---

### Pattern 4: Switch/Ternary Expressions

**Before:**
```csharp
private string GetSizeClasses() => Size switch
{
    Size.Small => "text-sm px-2",
    Size.Large => "text-lg px-6",
    _ => "text-base px-4"
};

private string Classes => CombineClasses("font-medium", GetSizeClasses(), Class);
```

**After (no change to switch, just final combination):**
```csharp
private string GetSizeClasses() => Size switch
{
    Size.Small => "text-sm px-2",
    Size.Large => "text-lg px-6",
    _ => "text-base px-4"
};

private string Classes => MergeClasses("font-medium", GetSizeClasses(), Class);
```

---

## Step-by-Step Migration

### 1. Identify the class-building method

Look for methods/properties containing:
- `CombineClasses(...)`
- `List<string>` with `.Add()` for classes
- String concatenation for CSS classes

### 2. Convert List patterns to ElementClass

```csharp
// Before
var classes = new List<string> { "base" };
if (condition1) classes.Add("extra1");
if (condition2) classes.Add("extra2");
return CombineClasses(string.Join(" ", classes), Class);

// After
return MergeClasses(
    ElementClass.Empty()
        .Add("base")
        .Add("extra1", when: condition1)
        .Add("extra2", when: condition2)
        .Add(Class)
);
```

### 3. Replace CombineClasses with MergeClasses

Simply swap the method call. `MergeClasses` accepts the same params.

### 4. IMPORTANT: Always add `Class` parameter last

The `Class` parameter should be the LAST thing added so user overrides win:
```csharp
// CORRECT - user's Class overrides component defaults
MergeClasses(componentClasses, Class)

// WRONG - component defaults would override user's Class
MergeClasses(Class, componentClasses)
```

---

## Full Example: Card Component Migration

### Before:
```csharp
public partial class Card
{
    private string BaseClasses => "flex rounded-lg border bg-white shadow-md";

    private string? ComponentClasses
    {
        get
        {
            var classes = new List<string>
            {
                BaseClasses,
                Horizontal ? "flex-col md:flex-row" : "flex-col",
            };

            if (!string.IsNullOrEmpty(Href))
            {
                classes.Add("cursor-pointer hover:bg-gray-100");
            }

            return CombineClasses(string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c))));
        }
    }
}
```

### After:
```csharp
public partial class Card
{
    private const string BaseClasses = "flex rounded-lg border bg-white shadow-md";

    private string ComponentClasses => MergeClasses(
        ElementClass.Empty()
            .Add(BaseClasses)
            .Add(Horizontal ? "flex-col md:flex-row" : "flex-col")
            .Add("cursor-pointer hover:bg-gray-100", when: !string.IsNullOrEmpty(Href))
            .Add(Class)
    );
}
```

---

## Components to Migrate

Use this command to find components still using `CombineClasses`:

```bash
grep -r "CombineClasses" src/Flowbite/Components --include="*.cs" -l
```

### Current Status (30 files need migration)

#### Simple Components (start here)
- [ ] `Card.razor.cs` - Good first example
- [ ] `Combobox.razor.cs`

#### Modal/Drawer Family
- [ ] `Drawer.razor.cs`
- [ ] `DrawerHeader.razor.cs`
- [ ] `DrawerItems.razor.cs`
- [ ] `Modal.razor.cs`
- [ ] `ModalBody.razor.cs`
- [ ] `ModalFooter.razor.cs`
- [ ] `ModalHeader.razor.cs`

#### Navbar Family
- [ ] `Navbar.razor.cs`
- [ ] `NavbarLink.razor.cs`

#### Sidebar Family
- [ ] `SidebarCTA.razor.cs`
- [ ] `SidebarLogo.razor.cs`

#### Dropdown
- [ ] `Dropdown.razor.cs`
- [ ] `DropdownItem.razor.cs`

#### Carousel Family
- [ ] `Carousel/Carousel.razor.cs`
- [ ] `Carousel/CarouselIndicators.razor.cs`
- [ ] `Carousel/CarouselItem.razor.cs`

#### Chat Components
- [ ] `Chat/ChatMessage.razor.cs`
- [ ] `Chat/ChatMessageContent.razor.cs`

#### Timeline Family
- [ ] `Timeline/Activity.razor.cs`
- [ ] `Timeline/ActivityItem.razor.cs`
- [ ] `Timeline/Group.razor.cs`
- [ ] `Timeline/GroupItem.razor.cs`
- [ ] `Timeline/Timeline.razor.cs`
- [ ] `Timeline/TimelineItem.razor.cs`

#### Typography
- [ ] `Typography/Heading.razor.cs`
- [ ] `Typography/Paragraph.razor.cs`
- [ ] `Typography/Span.razor.cs`

#### Other
- [ ] `Table/TableContext.cs`

---

## Testing After Migration

1. **Visual regression**: Component should look identical
2. **Class override**: Test with `Class="p-8"` - should override component padding
3. **Conditional classes**: Test all boolean parameters that affect styling
4. **Dark mode**: Ensure dark: variants still apply correctly

---

## Common Mistakes

### ❌ Forgetting to add `Class` parameter
```csharp
// WRONG - user cannot customize
MergeClasses(ElementClass.Empty().Add("flex gap-4"))
```

### ✅ Always include `Class` at the end
```csharp
// CORRECT
MergeClasses(ElementClass.Empty().Add("flex gap-4").Add(Class))
```

### ❌ Using ElementClass without MergeClasses
```csharp
// WRONG - no conflict resolution
return ElementClass.Empty().Add("p-4").Add(Class).ToString();
```

### ✅ Always pass through MergeClasses
```csharp
// CORRECT - TailwindMerge resolves conflicts
return MergeClasses(ElementClass.Empty().Add("p-4").Add(Class));
