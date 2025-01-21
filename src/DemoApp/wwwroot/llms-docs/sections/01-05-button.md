
#### Button Examples

The available Button colors are:
- None specified is the same as ButtonColor.Default
- ButtonColor.Gray
- ButtonColor.Primary
- ButtonColor.Dark
- ButtonColor.Light
- ButtonColor.Green
- ButtonColor.Red
- ButtonColor.Yellow
- ButtonColor.Purple

The available Button sizes are:
- ButtonSize.Small,
- ButtonSize.Medium,
- ButtonSize.Large

```razor
<!-- Default button with color -->
<Button Color="ButtonColor.Green" Size="ButtonSize.Large">
    Click me
</Button>

<!-- Outline button with icon -->
<Button Style="ButtonStyle.Outline" Color="ButtonColor.Info" Icon="@(new InfoIcon())">
    Info with icon
</Button>

<!-- Loading state -->
<Button Loading="true" Color="ButtonColor.Success">
    Processing...
</Button>

<!-- Link button -->
<Button Href="https://flowbite.com" Target="_blank" Color="ButtonColor.Purple">
    Visit Website
</Button>

<!-- Full-width button -->
<Button Color="ButtonColor.Dark" class="w-full">
    Block Button
</Button>
```
