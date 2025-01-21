

#### Dropdown Examples

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
```
