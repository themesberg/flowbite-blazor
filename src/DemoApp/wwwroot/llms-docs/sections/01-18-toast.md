#### Toast Examples

__Default Toast:__

```razor
<Button OnClick="@(() => ToastService.Show("Hi there 👋!"))">Show Default Toast</Button>
```

__Toast Types:__

The `ToastService` provides helper methods to show different types of toasts.

```csharp
public enum ToastType
{
    Default,
    Info,
    Success,
    Warning,
    Error
}
```

```razor
<div class="flex flex-wrap gap-2">
    <Button OnClick="@(() => ToastService.ShowSuccess("Item successfully created."))">Show Success Toast</Button>
    <Button OnClick="@(() => ToastService.ShowError("Something went wrong!"))">Show Error Toast</Button>
    <Button OnClick="@(() => ToastService.ShowWarning("Warning: Low disk space."))">Show Warning Toast</Button>
    <Button OnClick="@(() => ToastService.ShowInfo("New version available."))">Show Info Toast</Button>
</div>
```

__Toast Positioning:__

Positioning is controlled by the `Position` parameter on the `<ToastHost />` component.

```csharp
public enum ToastPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}
```

```razor
<!-- This dedicated host is for the positioning demo only. -->
<ToastHost Position="@currentPosition" HostId="position-demo" />

<div class="flex flex-wrap gap-2">
    <Button OnClick="@(() => SetPosition(ToastPosition.TopLeft))" Color="Button.ButtonColor.Primary">Top Left</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.TopCenter))" Color="Button.ButtonColor.Primary">Top Center</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.TopRight))" Color="Button.ButtonColor.Primary">Top Right</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomLeft))" Color="Button.ButtonColor.Primary">Bottom Left</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomCenter))" Color="Button.ButtonColor.Primary">Bottom Center</Button>
    <Button OnClick="@(() => SetPosition(ToastPosition.BottomRight))" Color="Button.ButtonColor.Primary">Bottom Right</Button>
</div>

@code {
    private ToastPosition currentPosition = ToastPosition.TopRight;

    private void SetPosition(ToastPosition newPosition)
    {
        currentPosition = newPosition;
        ToastService.Show($"Position set to {newPosition}", hostId: "position-demo");
    }
}
