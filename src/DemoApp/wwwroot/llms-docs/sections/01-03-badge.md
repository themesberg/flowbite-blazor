
#### Badge Examples

The available Badge colors are:
- BadgeColor.Primary,
- BadgeColor.Info,
- BadgeColor.Gray,
- BadgeColor.Success,
- BadgeColor.Warning,
- BadgeColor.Indigo,
- BadgeColor.Purple,
- BadgeColor.Pink

The available Badge sizes are:
- BadgeSize.ExtraSmall,
- BadgeSize.Small


```razor
<!-- Default badges with different colors -->
<div class="flex flex-wrap gap-2">
    <Badge Color="BadgeColor.Info">Info</Badge>
    ....
</div>

<!-- Badge as link -->
<div class="flex flex-wrap gap-2">
    <Badge Href="#">Primary</Badge>
    <Badge Color="BadgeColor.Gray" Href="#">Gray</Badge>
    ...
</div>

<!-- Badge with icon -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())">2 minutes ago</Badge>
    <Badge Color="BadgeColor.Gray" Icon="@(new ClockIcon())">In progress</Badge>
    ...
</div>

<!-- Badge with icon only -->
<div class="flex flex-wrap gap-2">
    <Badge Icon="@(new CheckIcon())" />
    <Badge Icon="@(new CheckIcon())" Color="BadgeColor.Gray" />
    ...
</div>

<!-- Different sizes -->
<div class="flex flex-wrap items-center gap-2">
    <Badge Size="BadgeSize.ExtraSmall">Extra small</Badge>
    <Badge Size="BadgeSize.Small">Small</Badge>
</div>
```
