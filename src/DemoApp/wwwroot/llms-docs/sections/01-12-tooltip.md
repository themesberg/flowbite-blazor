
#### Tooltip Examples

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

<!-- Style variations -->
<div class="flex items-center gap-4">
    <Tooltip Content="Dark style" Style="dark">
        <Button>Dark</Button>
    </Tooltip>
    <Tooltip Content="Light style" Style="light">
        <Button Color="ButtonColor.Light">Light</Button>
    </Tooltip>
    <Tooltip Content="Auto style (adapts to dark mode)" Style="auto">
        <Button>Auto</Button>
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
