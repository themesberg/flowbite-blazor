
#### Tooltip Examples

Tooltip uses Floating UI for smart viewport-aware positioning with automatic flip and shift behavior.

```razor
<!-- Basic tooltip -->
<Tooltip Content="This is a basic tooltip">
    <Button>Hover me</Button>
</Tooltip>

<!-- Different placements -->
<div class="flex items-center gap-4">
    <Tooltip Content="Top tooltip" Placement="TooltipPlacement.Top">
        <Button>Top</Button>
    </Tooltip>
    <Tooltip Content="Bottom tooltip" Placement="TooltipPlacement.Bottom">
        <Button>Bottom</Button>
    </Tooltip>
    <Tooltip Content="Left tooltip" Placement="TooltipPlacement.Left">
        <Button>Left</Button>
    </Tooltip>
    <Tooltip Content="Right tooltip" Placement="TooltipPlacement.Right">
        <Button>Right</Button>
    </Tooltip>
</div>

<!-- Theme variations -->
<div class="flex items-center gap-4">
    <Tooltip Content="Dark theme" Theme="dark">
        <Button>Dark</Button>
    </Tooltip>
    <Tooltip Content="Light theme" Theme="light">
        <Button Color="ButtonColor.Light">Light</Button>
    </Tooltip>
</div>

<!-- Different triggers -->
<div class="flex items-center gap-4">
    <Tooltip Content="Hover to show" Trigger="hover">
        <Button>Hover Trigger</Button>
    </Tooltip>
    <Tooltip Content="Click to show" Trigger="click">
        <Button>Click Trigger</Button>
    </Tooltip>
</div>

<!-- Without arrow -->
<Tooltip Content="No arrow tooltip" Arrow="false">
    <Button>No Arrow</Button>
</Tooltip>

<!-- Animation options -->
<div class="flex flex-wrap gap-4">
    <Tooltip Content="Tooltip content" Animation="@null">
        <Button>Not animated</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-150">
        <Button>Fast animation</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-300">
        <Button>Normal speed</Button>
    </Tooltip>
    <Tooltip Content="Tooltip content" Animation="duration-500">
        <Button>Slow animation</Button>
    </Tooltip>
</div>
```

### Tooltip Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Content` | `string` | **Required** | Tooltip text |
| `Placement` | `TooltipPlacement` | `Top` | Position relative to trigger |
| `Theme` | `string` | `"dark"` | Visual theme (dark, light) |
| `Trigger` | `string` | `"hover"` | Trigger mode (hover, click) |
| `Arrow` | `bool` | `true` | Show arrow pointer |
| `Animation` | `string?` | `"duration-300"` | CSS transition duration |
| `Class` | `string?` | `null` | Additional CSS classes |

### TooltipPlacement Options

| Value | Description |
|-------|-------------|
| `Auto` | Auto-select best placement |
| `Top` | Above the trigger |
| `Bottom` | Below the trigger |
| `Left` | To the left of trigger |
| `Right` | To the right of trigger |

### Floating UI Behavior

The tooltip automatically:
- **Flips** to opposite side when near viewport edge
- **Shifts** along axis to stay visible
- Updates position on scroll/resize

### Keyboard Support

- **Escape**: Close tooltip (when visible)
- Shows on focus for keyboard accessibility
