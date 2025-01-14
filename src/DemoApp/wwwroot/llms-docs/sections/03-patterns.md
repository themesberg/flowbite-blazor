<doc title="Common Patterns" desc="Common patterns and best practices">

## Best Practices

### Event Handling

Components use standard Blazor event handling:

```razor
<Button OnClick="@HandleClick">
    Click me
</Button>

@code {
    private void HandleClick()
    {
        // Handle the click event
    }
}

<!-- With parameters -->
<Dropdown>
    <ChildContent>
        <DropdownItem OnClick="@(() => HandleItemClick(item.Id))">
            @item.Name
        </DropdownItem>
    </ChildContent>
</Dropdown>
```

### Dark Mode

Enable dark mode by adding the 'dark' class to any parent element:

```razor
<!-- Dark mode container -->
<div class="dark">
    <!-- Components will use dark mode styles -->
    <Alert Color="AlertColor.Info">
        This alert uses dark mode styles
    </Alert>
    
    <Card>
        <h5 class="text-gray-900 dark:text-white">
            Dark mode card
        </h5>
        <p class="text-gray-700 dark:text-gray-400">
            Content adapts automatically
        </p>
    </Card>
</div>
```

### Accessibility

Components include built-in accessibility features:

```razor
<!-- Automatic ARIA attributes -->
<Alert Color="AlertColor.Warning" 
       IsDismissible="true"
       Role="alert">
    <p>Warning: This action cannot be undone.</p>
</Alert>

<!-- Keyboard navigation -->
<Dropdown>
    <Label>Accessible Menu</Label>
    <ChildContent>
        <!-- Tab and arrow key navigation supported -->
        <DropdownItem>Profile</DropdownItem>
        <DropdownItem>Settings</DropdownItem>
    </ChildContent>
</Dropdown>

<!-- Screen reader text -->
<Button>
    <span class="sr-only">Close menu</span>
    <XMarkIcon class="w-5 h-5" />
</Button>
```

</doc>
