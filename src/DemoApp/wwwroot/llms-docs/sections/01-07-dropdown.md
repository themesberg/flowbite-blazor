

#### Dropdown Examples

Dropdown uses Floating UI for smart viewport-aware positioning with automatic flip and shift behavior.

```razor
<!-- Default dropdown -->
<Dropdown>
    <Label>Dropdown menu</Label>
    <ChildContent>
        <DropdownHeader>
            <span class="block text-sm">Bonnie Green</span>
            <span class="block truncate text-sm font-medium">name@flowbite.com</span>
        </DropdownHeader>
        <DropdownItem Icon="@(new UserIcon())" OnClick="@HandleProfileClick">Profile</DropdownItem>
        <DropdownItem Icon="@(new GearIcon())" OnClick="@HandleSettingsClick">Settings</DropdownItem>
        <DropdownDivider />
        <DropdownItem Icon="@(new ArrowRightToBracketIcon())" OnClick="@HandleSignOutClick">
            Sign out
        </DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with custom trigger -->
<Dropdown>
    <CustomTrigger>
        <Avatar Alt="User settings"
                ImageUrl="https://flowbite.com/docs/images/people/profile-picture-5.jpg"
                Rounded="true" />
    </CustomTrigger>
    <ChildContent>
        <DropdownItem>Profile</DropdownItem>
        <DropdownItem>Settings</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with Slots for custom styling -->
<Dropdown Slots="@(new DropdownSlots {
    Trigger = "bg-blue-600 hover:bg-blue-700",
    Menu = "w-64 shadow-xl",
    Item = "hover:bg-blue-50"
})">
    <Label>Styled Dropdown</Label>
    <ChildContent>
        <DropdownItem>Option 1</DropdownItem>
        <DropdownItem>Option 2</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with placement and offset -->
<Dropdown Placement="DropdownPlacement.TopEnd" Offset="12">
    <Label>Top End</Label>
    <ChildContent>
        <DropdownItem>Item 1</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Dropdown with disabled auto-positioning -->
<Dropdown
    Placement="DropdownPlacement.Bottom"
    DisableFlip="true"
    DisableShift="true">
    <Label>Fixed Position</Label>
    <ChildContent>
        <DropdownItem>Fixed at bottom</DropdownItem>
    </ChildContent>
</Dropdown>
```

### Dropdown Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Size` | `DropdownSize` | `Medium` | Trigger and menu size |
| `Placement` | `DropdownPlacement` | `Bottom` | Menu placement |
| `Inline` | `bool` | `false` | Inline trigger styling |
| `ArrowIcon` | `bool` | `true` | Show dropdown arrow |
| `DismissOnClick` | `bool` | `true` | Close on item click |
| `DisableFlip` | `bool` | `false` | Disable viewport flip |
| `DisableShift` | `bool` | `false` | Disable viewport shift |
| `Offset` | `int` | `8` | Distance from trigger |
| `Slots` | `DropdownSlots?` | `null` | Per-element classes |
| `MenuClass` | `string?` | `null` | Menu CSS classes |
| `IsOpenChanged` | `EventCallback<bool>` | - | Open state changed |
| `OnTriggerClick` | `EventCallback<MouseEventArgs>` | - | Trigger click |

### DropdownSlots Properties

| Slot | Description |
|------|-------------|
| `Base` | The dropdown container |
| `Trigger` | The trigger button |
| `Menu` | The dropdown menu panel |
| `Item` | Classes passed to DropdownItem components |

### Keyboard Navigation

- **Enter/Space**: Open dropdown, select item
- **ArrowDown/ArrowUp**: Navigate items
- **Home/End**: Jump to first/last item
- **Escape**: Close dropdown
- **Tab**: Close and move focus
- **Type-ahead**: Type to jump to matching item

### Floating UI Behavior

The dropdown automatically:
- **Flips** to opposite side when near viewport edge
- **Shifts** along axis to stay visible
- Updates position on scroll/resize
