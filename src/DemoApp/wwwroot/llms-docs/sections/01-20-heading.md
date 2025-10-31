
#### Heading Examples

The Heading component renders semantic HTML headings (h1-h6) with consistent styling.

**Available heading tags:**
- HeadingTag.H1 (default) - text-5xl, font-extrabold
- HeadingTag.H2 - text-4xl, font-bold
- HeadingTag.H3 - text-3xl, font-bold
- HeadingTag.H4 - text-2xl, font-bold
- HeadingTag.H5 - text-xl, font-bold
- HeadingTag.H6 - text-lg, font-bold

**Optional parameters:**
- `Size` (TextSize?) - Override default tag-based sizing
- `Weight` (FontWeight?) - Override default weight
- `Gradient` (GradientColor) - Apply gradient text effect
- `CustomColor` (string?) - Custom Tailwind color classes

**Default colors:**
- Light mode: text-gray-900
- Dark mode: text-white

```razor
<!-- Basic headings -->
<Heading Tag="HeadingTag.H1">Page Title</Heading>
<Heading Tag="HeadingTag.H2">Section Heading</Heading>
<Heading Tag="HeadingTag.H3">Subsection Heading</Heading>

<!-- Custom size (semantic HTML with visual control) -->
<Heading Tag="HeadingTag.H2" Size="TextSize.XXXXXXXXXL">
    Extra Large H2
</Heading>

<!-- Custom weight -->
<Heading Tag="HeadingTag.H2" Weight="FontWeight.Light">
    Light Heading
</Heading>

<!-- Gradient heading -->
<Heading Tag="HeadingTag.H1" Gradient="GradientColor.PurpleToBlue">
    Gradient Title
</Heading>

<!-- Custom color -->
<Heading Tag="HeadingTag.H2" CustomColor="text-blue-600 dark:text-blue-400">
    Blue Heading
</Heading>

<!-- Combined styling -->
<Heading Tag="HeadingTag.H1"
         Size="TextSize.XXXXXXL"
         Weight="FontWeight.Black"
         Gradient="GradientColor.PurpleToBlue">
    Large Gradient Title
</Heading>
```
