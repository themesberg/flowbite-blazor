
#### Paragraph Examples

The Paragraph component renders paragraph text with flexible styling options.

**Parameters:**
- `Size` (TextSize) - Text size (default: Base)
- `Weight` (FontWeight?) - Font weight
- `Leading` (LineHeight?) - Line height spacing
- `Align` (TextAlign?) - Text alignment
- `Tracking` (LetterSpacing?) - Letter spacing
- `Space` (Whitespace?) - Whitespace handling
- `Gradient` (GradientColor) - Gradient text effect
- `CustomColor` (string?) - Custom color classes
- `FirstLetterUpper` (bool) - Drop cap styling
- `Opacity` (int?) - Opacity level (0-100)
- `Italic` (bool) - Italic styling
- `Underline` (bool) - Underline decoration

**Default colors:**
- Light mode: text-gray-700
- Dark mode: text-gray-400

```razor
<!-- Basic paragraph -->
<Paragraph>
    This is a paragraph with default styling.
</Paragraph>

<!-- Custom size and weight -->
<Paragraph Size="TextSize.LG" Weight="FontWeight.Medium">
    Larger, medium weight paragraph text.
</Paragraph>

<!-- Text alignment -->
<Paragraph Align="TextAlign.Center">
    Center-aligned paragraph text.
</Paragraph>

<!-- Line height (leading) -->
<Paragraph Leading="LineHeight.Relaxed">
    Paragraph with relaxed line spacing for improved readability.
</Paragraph>

<!-- Italic and underline -->
<Paragraph Italic="true">
    Italic paragraph text for emphasis.
</Paragraph>

<!-- First letter uppercase (drop cap) -->
<Paragraph FirstLetterUpper="true" Size="TextSize.LG" Leading="LineHeight.Relaxed">
    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
</Paragraph>

<!-- Gradient paragraph -->
<Paragraph Gradient="GradientColor.PurpleToBlue"
           Size="TextSize.XL"
           Weight="FontWeight.Bold">
    Gradient paragraph text
</Paragraph>

<!-- Custom color -->
<Paragraph CustomColor="text-blue-600 dark:text-blue-400">
    Custom colored paragraph text.
</Paragraph>

<!-- Letter spacing -->
<Paragraph Tracking="LetterSpacing.Wide">
    Paragraph with wider letter spacing.
</Paragraph>
```
