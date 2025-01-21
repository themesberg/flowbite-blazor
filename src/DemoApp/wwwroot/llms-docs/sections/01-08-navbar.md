
#### Navbar Examples

```razor

<!-- Add dropdown menus to navigation items and include a user profile dropdown for additional interactions. -- >
<Navbar>
    <NavbarBrand Href="/">
        <img src="/images/logo.svg" class="mr-3 h-6 sm:h-9" alt="Flowbite Logo" />
        <span class="self-center text-xl font-semibold whitespace-nowrap dark:text-white">Flowbite</span>
    </NavbarBrand>
    <div class="flex md:order-2">
        <Dropdown ArrowIcon="false" Inline="true">
            <Label>
                <Avatar Alt="User settings" ImageUrl="https://flowbite.com/docs/images/people/profile-picture-5.jpg" Rounded />
            </Label>
            <ChildContent>
                <DropdownHeader>
                    <span class="block text-sm">Bonnie Green</span>
                    <span class="block truncate text-sm font-medium">name@flowbite.com</span>
                </DropdownHeader>
                <DropdownItem>Dashboard</DropdownItem>
                <DropdownItem>Settings</DropdownItem>
                <DropdownItem>Earnings</DropdownItem>
                <DropdownDivider />
                <DropdownItem>Sign out</DropdownItem>
            </ChildContent>
        </Dropdown>
        <NavbarToggle />
    </div>
    <NavbarCollapse>
        <NavbarLink Href="/" Active>Home</NavbarLink>
        <NavbarLink HasDropdown>
            <ChildContent>Features</ChildContent>
            <DropdownContent>
                <DropdownItem>Analytics</DropdownItem>
                <DropdownItem>Automation</DropdownItem>
                <DropdownItem>Reports</DropdownItem>
            </DropdownContent>
        </NavbarLink>
        <NavbarLink Href="/pricing">Pricing</NavbarLink>
        <NavbarLink HasDropdown>
            <ChildContent>Resources</ChildContent>
            <DropdownContent>
                <DropdownHeader>Documentation</DropdownHeader>
                <DropdownItem>Getting Started</DropdownItem>
                <DropdownItem>Components</DropdownItem>
                <DropdownDivider />
                <DropdownHeader>Help</DropdownHeader>
                <DropdownItem>Support</DropdownItem>
                <DropdownItem>Contact</DropdownItem>
            </DropdownContent>
        </NavbarLink>
    </NavbarCollapse>
</Navbar>
```
