
#### Button Examples

The available Button colors are:
- ButtonColor.Default (blue)
- ButtonColor.Primary (customizable via @theme)
- ButtonColor.Gray
- ButtonColor.Dark
- ButtonColor.Light
- ButtonColor.Green
- ButtonColor.Red
- ButtonColor.Yellow
- ButtonColor.Purple

The available Button sizes are:
- ButtonSize.Small
- ButtonSize.Medium
- ButtonSize.Large

The available Button variants are:
- ButtonVariant.Default (filled)
- ButtonVariant.Outline (bordered)

```razor
<!-- Default button with color -->
<Button Color="ButtonColor.Green" Size="ButtonSize.Large">
    Click me
</Button>

<!-- Outline button with icon -->
<Button Variant="ButtonVariant.Outline" Color="ButtonColor.Primary" Icon="@(new InfoIcon())">
    Info with icon
</Button>

<!-- Loading state -->
<Button Loading="true" Color="ButtonColor.Green">
    Processing...
</Button>

<!-- Link button -->
<Button Href="https://flowbite.com" Target="_blank" Color="ButtonColor.Purple">
    Visit Website
</Button>

<!-- Full-width button -->
<Button Color="ButtonColor.Dark" Class="w-full">
    Block Button
</Button>

<!-- Pill-shaped button -->
<Button Color="ButtonColor.Primary" Pill="true">
    Rounded Button
</Button>

<!-- Primary color (uses @theme customization) -->
<Button Color="ButtonColor.Primary">
    Brand Color
</Button>
```

### Button Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Color` | `ButtonColor` | `Default` | Color variant |
| `Size` | `ButtonSize` | `Medium` | Size variant |
| `Variant` | `ButtonVariant` | `Default` | Visual style (Default or Outline) |
| `Disabled` | `bool` | `false` | Disables the button |
| `Loading` | `bool` | `false` | Shows loading state |
| `Pill` | `bool` | `false` | Fully rounded corners |
| `Icon` | `IconBase?` | `null` | Icon to display |
| `Href` | `string?` | `null` | Renders as link when set |
| `Target` | `string?` | `null` | Link target (_blank, _self) |
| `Type` | `string` | `"button"` | HTML button type |
| `Class` | `string?` | `null` | Additional CSS classes |
| `OnClick` | `EventCallback<MouseEventArgs>` | - | Click handler |
