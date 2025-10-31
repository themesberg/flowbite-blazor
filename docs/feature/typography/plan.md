# Typography Components Implementation Plan

## Overview

This document outlines the plan to migrate Flowbite Svelte Typography components to Flowbite Blazor, following Blazor idiomatic patterns while maintaining visual and functional parity with the Svelte implementation.

## Analysis

### Svelte Approach

The Flowbite Svelte library uses a sophisticated approach for typography components:

#### Key Architectural Patterns

1. **Tailwind-Variants Library**
   - Uses `tv()` function from `tailwind-variants` package
   - Provides type-safe variant definitions
   - Automatic class merging and conflict resolution
   - Clean separation of styling logic from component logic

2. **Component Structure**
   ```svelte
   <script lang="ts">
     import { heading } from "./theme";
     let { children, tag = "h1", class: className, ...restProps } = $props();
     let headingCls = $derived(heading({ tag, class: clsx(theme, className) }));
   </script>
   
   <svelte:element this={tag} {...restProps} class={headingCls}>
     {@render children()}
   </svelte:element>
   ```

3. **Theme Organization**
   - Each component has a dedicated `theme.ts` file
   - Base classes applied to all variants
   - Variant definitions (size, weight, alignment, etc.)
   - Default variant values
   - Type exports for variant props

4. **Typography Component Suite**
   - **Heading** - H1-H6 with tag-based sizing
   - **P (Paragraph)** - Rich variants: size, weight, height, align, whitespace, italic, justify, firstUpper, space
   - **Span** - Inline text styling: italic, underline, linethrough, uppercase, gradient, highlight, decoration
   - **Blockquote** - Quote styling
   - **Hr** - Horizontal rules
   - **List** - Ordered/unordered lists
   - **Mark** - Text highlighting
   - **Secondary** - Secondary text styling
   - **A (Anchor)** - Link component

5. **Example: Paragraph Theme**
   ```typescript
   export const paragraph = tv({
     base: "text-gray-900 dark:text-white",
     variants: {
       size: {
         xs: "text-xs",
         sm: "text-sm",
         base: "text-base",
         lg: "text-lg",
         // ... up to 9xl
       },
       weight: {
         thin: "font-thin",
         normal: "font-normal",
         bold: "font-bold",
         // ... etc
       },
       // ... more variants
     }
   });
   ```

### Blazor Approach

For Blazor, we'll use C# strengths while maintaining flexibility:

#### Architectural Decisions

1. **Enum-Based Variant System**
   - Replace `tailwind-variants` with C# enums
   - Type-safe at compile time
   - IntelliSense support
   - Example:
     ```csharp
     public enum TextSize { XS, SM, Base, LG, XL, XXL, XXXL, ... }
     public enum FontWeight { Thin, Light, Normal, Medium, Bold, ... }
     ```

2. **Static Helper Classes**
   - Generate Tailwind classes from enums
   - Centralized CSS class management
   - Example:
     ```csharp
     public static class TypographyHelper
     {
         public static string GetTextSizeClass(TextSize size) => size switch
         {
             TextSize.XS => "text-xs",
             TextSize.SM => "text-sm",
             // ...
         };
     }
     ```

3. **Component Architecture**
   - Separate `.razor` and `.razor.cs` files (existing pattern)
   - Parameters for all variant options
   - Private methods to compute CSS classes
   - Inherit from `FlowbiteComponentBase`
   - Support `AdditionalAttributes` for flexibility

4. **Class Composition Strategy**
   - Use existing `CombineClasses()` method from `FlowbiteComponentBase`
   - Build class lists dynamically based on parameters
   - Example:
     ```csharp
     private string GetClasses()
     {
         var classes = new List<string>
         {
             "base-classes",
             TypographyHelper.GetTextSizeClass(Size),
             TypographyHelper.GetFontWeightClass(Weight),
         };
         
         if (Italic) classes.Add("italic");
         
         return CombineClasses(classes.ToArray());
     }
     ```

5. **Blazor Idioms to Follow**
   - `RenderFragment` for content (instead of children prop)
   - `[Parameter]` attributes for all component inputs
   - `[Parameter(CaptureUnmatchedValues = true)]` for additional attributes
   - Null-safety with nullable reference types
   - `DynamicComponent` for dynamic tag rendering
   - XML documentation for IntelliSense support

## Project Structure

```
src/Flowbite/Components/Typography/
├── Heading.razor
├── Heading.razor.cs (includes HeadingTag enum)
├── Paragraph.razor
├── Paragraph.razor.cs
├── Span.razor
├── Span.razor.cs
├── TypographyEnums.cs (shared enums: TextSize, FontWeight, LineHeight, etc.)
├── Blockquote.razor (future)
├── Blockquote.razor.cs (future)
├── Hr.razor (future)
├── Hr.razor.cs (future)
├── Mark.razor (future)
└── Mark.razor.cs (future)

src/DemoApp/Pages/Docs/components/typography/
├── HeadingPage.razor
├── ParagraphPage.razor
└── SpanPage.razor

src/DemoApp/wwwroot/llms-docs/sections/
├── heading.md
├── paragraph.md
└── span.md
```

**Enum Organization Strategy:**
Following C#/Blazor conventions observed in the existing codebase:
- **Component-specific enums** (e.g., `HeadingTag`) are defined in the component's `.razor.cs` file
- **Shared typography enums** (e.g., `TextSize`, `FontWeight`, `LineHeight`, `TextAlign`, `LetterSpacing`, `Whitespace`, `GradientColor`) are grouped in `TypographyEnums.cs`
- This follows the pattern seen in existing components like `ModalEnums.cs`, `DropdownEnums.cs`, and `ToastEnums.cs`

## Component Specifications

### 1. Heading Component

**Purpose:** Render semantic HTML headings (h1-h6) with consistent styling.

**Parameters:**
```csharp
[Parameter] public HeadingTag Tag { get; set; } = HeadingTag.H1;
[Parameter] public TextSize? Size { get; set; }  // Overrides default tag size
[Parameter] public FontWeight? Weight { get; set; }
[Parameter] public GradientColor Gradient { get; set; } = GradientColor.None;
[Parameter] public string? CustomColor { get; set; }
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

**Default Styling:**
- Base: `font-bold text-gray-900 dark:text-white`
- H1: `text-5xl font-extrabold`
- H2: `text-4xl`
- H3: `text-3xl`
- H4: `text-2xl`
- H5: `text-xl`
- H6: `text-lg`

**Implementation Notes:**
- Use `DynamicComponent` for rendering different heading tags
- Size parameter overrides default tag-based sizing
- Support gradient text with background-clip technique
- Maintain backward compatibility where possible

### 2. Paragraph Component

**Purpose:** Render paragraph text with extensive styling options.

**Parameters:**
```csharp
[Parameter] public TextSize Size { get; set; } = TextSize.Base;
[Parameter] public FontWeight Weight { get; set; } = FontWeight.Normal;
[Parameter] public LineHeight Height { get; set; } = LineHeight.Normal;
[Parameter] public LetterSpacing Spacing { get; set; } = LetterSpacing.Normal;
[Parameter] public TextAlign Align { get; set; } = TextAlign.Left;
[Parameter] public Whitespace Whitespace { get; set; } = Whitespace.Normal;
[Parameter] public bool Italic { get; set; }
[Parameter] public bool Justify { get; set; }
[Parameter] public bool FirstUpper { get; set; }  // First letter styling
[Parameter] public string CustomColor { get; set; } = "text-gray-900 dark:text-white";
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

**Default Styling:**
- Base: `text-gray-900 dark:text-white`
- Size: `text-base`
- Weight: `font-normal`
- Height: `leading-normal`

**Special Features:**
- `FirstUpper`: Applies `first-line:uppercase first-line:tracking-widest first-letter:text-7xl first-letter:font-bold first-letter:text-gray-900 dark:first-letter:text-gray-100 first-letter:me-3 first-letter:float-left`

### 3. Span Component

**Purpose:** Inline text styling for emphasis and decoration.

**Parameters:**
```csharp
[Parameter] public bool Italic { get; set; }
[Parameter] public bool Underline { get; set; }
[Parameter] public bool LineThrough { get; set; }
[Parameter] public bool Uppercase { get; set; }
[Parameter] public GradientColor Gradient { get; set; } = GradientColor.None;
[Parameter] public string? HighlightColor { get; set; }  // e.g., "bg-blue-100"
[Parameter] public string? CustomColor { get; set; }
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

**Styling Options:**
- Italic: `italic`
- Underline: `underline`
- LineThrough: `line-through`
- Uppercase: `uppercase`
- Gradient: Background gradient with text clip

## Enumeration Definitions

### TextSize
```csharp
public enum TextSize
{
    XS,          // text-xs
    SM,          // text-sm
    Base,        // text-base (default)
    LG,          // text-lg
    XL,          // text-xl
    XXL,         // text-2xl
    XXXL,        // text-3xl
    XXXXL,       // text-4xl
    XXXXXL,      // text-5xl
    XXXXXXL,     // text-6xl
    XXXXXXXL,    // text-7xl
    XXXXXXXXL,   // text-8xl
    XXXXXXXXXL   // text-9xl
}
```

### FontWeight
```csharp
public enum FontWeight
{
    Thin,        // font-thin
    ExtraLight,  // font-extralight
    Light,       // font-light
    Normal,      // font-normal (default)
    Medium,      // font-medium
    SemiBold,    // font-semibold
    Bold,        // font-bold
    ExtraBold,   // font-extrabold
    Black        // font-black
}
```

### LineHeight
```csharp
public enum LineHeight
{
    None,     // leading-none
    Tight,    // leading-tight
    Snug,     // leading-snug
    Normal,   // leading-normal (default)
    Relaxed,  // leading-relaxed
    Loose,    // leading-loose
    Three,    // leading-3
    Four,     // leading-4
    Five,     // leading-5
    Six,      // leading-6
    Seven,    // leading-7
    Eight,    // leading-8
    Nine,     // leading-9
    Ten       // leading-10
}
```

### TextAlign
```csharp
public enum TextAlign
{
    Left,     // text-left
    Center,   // text-center
    Right,    // text-right
    Justify   // text-justify
}
```

### LetterSpacing
```csharp
public enum LetterSpacing
{
    Tighter,  // tracking-tighter
    Tight,    // tracking-tight
    Normal,   // tracking-normal (default)
    Wide,     // tracking-wide
    Wider,    // tracking-wider
    Widest    // tracking-widest
}
```

### Whitespace
```csharp
public enum Whitespace
{
    Normal,   // whitespace-normal (default)
    Nowrap,   // whitespace-nowrap
    Pre,      // whitespace-pre
    PreLine,  // whitespace-pre-line
    PreWrap   // whitespace-pre-wrap
}
```

### GradientColor
```csharp
public enum GradientColor
{
    None,
    PurpleToBlue,     // bg-gradient-to-r from-purple-600 to-blue-500
    CyanToBlue,       // bg-gradient-to-r from-cyan-500 to-blue-500
    GreenToBlue,      // bg-gradient-to-r from-green-400 to-blue-500
    PurpleToPink,     // bg-gradient-to-r from-purple-500 to-pink-500
    RedToYellow,      // bg-gradient-to-r from-red-500 to-yellow-500
    TealToLime        // bg-gradient-to-r from-teal-500 to-lime-500
}
```

## Helper Class (Optional)

**Note:** Based on existing Flowbite Blazor patterns, most components use inline switch expressions in their `GetClasses()` method rather than separate helper classes. A helper class is optional and should only be created if the CSS logic becomes complex enough to warrant extraction.

**TypographyHelper.cs** - Static methods for CSS class generation (if needed):

```csharp
public static class TypographyHelper
{
    public static string GetTextSizeClass(TextSize size) => size switch
    {
        TextSize.XS => "text-xs",
        TextSize.SM => "text-sm",
        TextSize.Base => "text-base",
        TextSize.LG => "text-lg",
        TextSize.XL => "text-xl",
        TextSize.XXL => "text-2xl",
        TextSize.XXXL => "text-3xl",
        TextSize.XXXXL => "text-4xl",
        TextSize.XXXXXL => "text-5xl",
        TextSize.XXXXXXL => "text-6xl",
        TextSize.XXXXXXXL => "text-7xl",
        TextSize.XXXXXXXXL => "text-8xl",
        TextSize.XXXXXXXXXL => "text-9xl",
        _ => "text-base"
    };
    
    public static string GetFontWeightClass(FontWeight weight) => weight switch
    {
        FontWeight.Thin => "font-thin",
        FontWeight.ExtraLight => "font-extralight",
        FontWeight.Light => "font-light",
        FontWeight.Normal => "font-normal",
        FontWeight.Medium => "font-medium",
        FontWeight.SemiBold => "font-semibold",
        FontWeight.Bold => "font-bold",
        FontWeight.ExtraBold => "font-extrabold",
        FontWeight.Black => "font-black",
        _ => "font-normal"
    };
    
    public static string GetLineHeightClass(LineHeight height) => height switch
    {
        LineHeight.None => "leading-none",
        LineHeight.Tight => "leading-tight",
        LineHeight.Snug => "leading-snug",
        LineHeight.Normal => "leading-normal",
        LineHeight.Relaxed => "leading-relaxed",
        LineHeight.Loose => "leading-loose",
        LineHeight.Three => "leading-3",
        LineHeight.Four => "leading-4",
        LineHeight.Five => "leading-5",
        LineHeight.Six => "leading-6",
        LineHeight.Seven => "leading-7",
        LineHeight.Eight => "leading-8",
        LineHeight.Nine => "leading-9",
        LineHeight.Ten => "leading-10",
        _ => "leading-normal"
    };
    
    public static string GetTextAlignClass(TextAlign align) => align switch
    {
        TextAlign.Left => "text-left",
        TextAlign.Center => "text-center",
        TextAlign.Right => "text-right",
        TextAlign.Justify => "text-justify",
        _ => "text-left"
    };
    
    public static string GetLetterSpacingClass(LetterSpacing spacing) => spacing switch
    {
        LetterSpacing.Tighter => "tracking-tighter",
        LetterSpacing.Tight => "tracking-tight",
        LetterSpacing.Normal => "tracking-normal",
        LetterSpacing.Wide => "tracking-wide",
        LetterSpacing.Wider => "tracking-wider",
        LetterSpacing.Widest => "tracking-widest",
        _ => "tracking-normal"
    };
    
    public static string GetWhitespaceClass(Whitespace whitespace) => whitespace switch
    {
        Whitespace.Normal => "whitespace-normal",
        Whitespace.Nowrap => "whitespace-nowrap",
        Whitespace.Pre => "whitespace-pre",
        Whitespace.PreLine => "whitespace-pre-line",
        Whitespace.PreWrap => "whitespace-pre-wrap",
        _ => "whitespace-normal"
    };
    
    public static string GetGradientClasses(GradientColor gradient) => gradient switch
    {
        GradientColor.PurpleToBlue => "bg-gradient-to-r from-purple-600 to-blue-500 bg-clip-text text-transparent",
        GradientColor.CyanToBlue => "bg-gradient-to-r from-cyan-500 to-blue-500 bg-clip-text text-transparent",
        GradientColor.GreenToBlue => "bg-gradient-to-r from-green-400 to-blue-500 bg-clip-text text-transparent",
        GradientColor.PurpleToPink => "bg-gradient-to-r from-purple-500 to-pink-500 bg-clip-text text-transparent",
        GradientColor.RedToYellow => "bg-gradient-to-r from-red-500 to-yellow-500 bg-clip-text text-transparent",
        GradientColor.TealToLime => "bg-gradient-to-r from-teal-500 to-lime-500 bg-clip-text text-transparent",
        _ => ""
    };
}
```

## Verification Strategy with Playwright MCP

### Reference URLs
- **Base:** `https://flowbite-svelte.com`
- **Heading:** `https://flowbite-svelte.com/docs/typography/heading`
- **Paragraph:** `https://flowbite-svelte.com/docs/typography/paragraph`
- **Text/Span:** `https://flowbite-svelte.com/docs/typography/text`
- **Blockquote:** `https://flowbite-svelte.com/docs/typography/blockquote`

### Verification Process

For each component:

1. **Capture Svelte Reference**
   - Navigate to Flowbite Svelte docs
   - Take screenshots of each example
   - Save as: `verification/svelte-{component}-{example}.png`

2. **Build and Run Blazor Demo**
   - Build DemoApp: `dotnet build`
   - Run: `dotnet run --project src/DemoApp`
   - Note local URL (typically `https://localhost:5001`)

3. **Capture Blazor Implementation**
   - Navigate to local demo page
   - Take matching screenshots
   - Save as: `verification/blazor-{component}-{example}.png`

4. **Visual Comparison**
   - Compare screenshots side-by-side
   - Check: font sizes, spacing, colors, alignment
   - Document discrepancies

5. **Refinement**
   - Adjust components based on findings
   - Re-verify with new screenshots
   - Iterate until match is achieved

### Playwright MCP Workflow Example

```
# Capture Svelte reference
1. browser_action: launch → https://flowbite-svelte.com/docs/typography/heading
2. browser_action: browser_take_screenshot → verification/svelte-heading-default.png
3. browser_action: close

# Capture Blazor implementation
1. browser_action: launch → https://localhost:5001/docs/components/typography/heading
2. browser_action: browser_take_screenshot → verification/blazor-heading-default.png
3. browser_action: close

# Compare and iterate
```

### Verification Checklist

For each component:
- [ ] Default styling matches
- [ ] All size variants render correctly
- [ ] Color schemes (light/dark mode) match
- [ ] Spacing and padding align
- [ ] Typography hierarchy is consistent
- [ ] Gradient effects work (if applicable)
- [ ] Responsive behavior matches
- [ ] All example variations render correctly

## Git Branch Management

### Branch Strategy

1. **Verify current branch** before starting implementation:
   ```bash
   git branch --show-current
   ```

2. **Create feature branch** if on `main` or `develop`:
   ```bash
   git checkout -b feature/typography-components
   ```

3. **Branch naming**: `feature/typography-components`

### Commit Strategy

**Commit incrementally** after each major step:
- After creating directory structure
- After creating TypographyEnums.cs
- After implementing each component (Heading, Paragraph, Span)
- After creating each demo page
- After adding documentation
- After verification adjustments

**Commit message format**:
```
feat(typography): [brief description]

- Detailed change 1
- Detailed change 2
```

**Example commits**:
- `feat(typography): create Typography directory structure`
- `feat(typography): add TypographyEnums with shared typography enums`
- `feat(typography): implement enhanced Heading component`
- `feat(typography): add Paragraph component with styling options`
- `feat(typography): add Span component for inline text styling`
- `feat(typography): add HeadingPage with examples`
- `feat(typography): add component documentation`
- `feat(typography): refine components based on visual verification`

**Important**: Never push to remote without explicit user approval. Push will occur at end of implementation after user review.

## Implementation Phases

### Phase 1: Foundation & Core Components
1. Create Typography folder structure
2. Create TypographyEnums.cs with shared enums (TextSize, FontWeight, LineHeight, TextAlign, LetterSpacing, Whitespace, GradientColor)
3. Move and enhance Heading component (include HeadingTag enum in Heading.razor.cs)
4. Create Paragraph component
5. Create Span component
6. Optionally create TypographyHelper class if CSS logic warrants it

### Phase 2: Demo Pages
1. Create HeadingPage with placeholder examples
2. Create ParagraphPage with placeholder examples
3. Create SpanPage with placeholder examples
4. Update DemoAppSidebar navigation

### Phase 3: Documentation
1. Create LLMS documentation files
2. Update Build-LlmsContext.ps1 script

### Phase 4: Verification
1. Capture Svelte reference screenshots
2. Build and run Blazor demo
3. Capture Blazor screenshots
4. Compare and document findings
5. Refine components as needed
6. Final verification pass

## Breaking Changes

The enhanced Heading component may introduce breaking changes:
- New optional parameters added
- Potential changes to default behavior
- Migration path: All new parameters are optional, existing usage should continue to work

## Success Criteria

1. All three core components (Heading, Paragraph, Span) implemented
2. Demo pages created with comprehensive examples
3. Visual parity with Flowbite Svelte verified via screenshots
4. Documentation complete and integrated
5. All components follow Blazor idiomatic patterns
6. Type-safe with full IntelliSense support
7. Extensible through AdditionalAttributes

## Future Enhancements

After Phase 1 completion:
- Blockquote component
- Hr component
- Mark component
- List components
- Link/Anchor component
- Additional typography utilities

## References

- Flowbite Svelte Source: `C:\Users\tschavey\projects\themesberg\flowbite-svelte\src\lib\typography`
- Flowbite Svelte Docs: `https://flowbite-svelte.com/docs/typography`
- Tailwind CSS Typography: `https://tailwindcss.com/docs/font-size`
