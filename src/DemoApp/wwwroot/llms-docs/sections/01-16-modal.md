
#### Modal Dialog Examples


__Default Modal:__

```razor
<div class="space-y-4">
    <div class="flex items-center gap-4">
        <Button OnClick="@(() => showDefaultModal = true)">Open Modal</Button>

        @if (termsAccepted != null)
        {
            <div class="@GetChoiceAlertClass()" role="alert">
                <span class="font-medium">@(termsAccepted.Value ? "Accepted" : "Declined")</span> the Terms of Service
            </div>
        }
    </div>

    <Modal Show="showDefaultModal" ShowChanged="(value) => showDefaultModal = value">
        <ModalHeader>
            <h3>Terms of Service</h3>
        </ModalHeader>
        <ModalBody>
            <div class="space-y-6">
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    With less than a month to go before the European Union enacts new consumer privacy laws...
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => HandleTermsChoice(false))" Color="ButtonColor.Gray" Class="mr-2">Decline</Button>
                <Button OnClick="@(() => HandleTermsChoice(true))">Accept</Button>
            </div>
        </ModalFooter>
    </Modal>
</div>

@code {
    private bool showDefaultModal = false;
    private bool? termsAccepted = null;

    private void HandleTermsChoice(bool accepted)
    {
        termsAccepted = accepted;
        showDefaultModal = false;
    }
}
```

__Modal with Slots:__

```razor
<Modal Show="@show" Slots="@(new ModalSlots {
    Backdrop = "bg-gray-900/70",
    Content = "rounded-xl shadow-2xl",
    Header = "border-b-2 border-blue-500",
    Body = "p-8",
    Footer = "bg-gray-50 dark:bg-gray-800"
})">
    <ModalHeader>Custom Styled Header</ModalHeader>
    <ModalBody>
        Content with extra padding from slot customization.
    </ModalBody>
    <ModalFooter>
        <Button OnClick="@(() => show = false)">Close</Button>
    </ModalFooter>
</Modal>
```

__Modal Options:__

```csharp
public enum ModalSize
{
    Small,
    Medium,
    Large,
    ExtraLarge,
    TwoExtraLarge,
    ThreeExtraLarge,
    FourExtraLarge,
    FiveExtraLarge,
    SixExtraLarge,
    SevenExtraLarge,
    Default = TwoExtraLarge
}

public enum ModalPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    Center,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}
```

__Sized and Positioned Modal:__

```razor
<Modal Show="showModal"
       ShowChanged="(value) => showModal = value"
       Size="ModalSize.FourExtraLarge"
       Position="ModalPosition.TopCenter">
    <ModalHeader>
        <h3>Large Top-Center Modal</h3>
    </ModalHeader>
    <ModalBody>
        <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
            Content here...
        </p>
    </ModalBody>
    <ModalFooter>
        <div class="flex justify-end w-full">
            <Button OnClick="@(() => showModal = false)">Close</Button>
        </div>
    </ModalFooter>
</Modal>
```

### Modal Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Show` | `bool` | `false` | Visibility state |
| `ShowChanged` | `EventCallback<bool>` | - | State change callback |
| `OnClose` | `EventCallback` | - | Close callback |
| `Title` | `string?` | `null` | Modal title |
| `Size` | `ModalSize` | `Default` | Size variant |
| `Position` | `ModalPosition` | `Center` | Screen position |
| `Dismissible` | `bool` | `true` | Close on Escape/backdrop |
| `Slots` | `ModalSlots?` | `null` | Per-element classes |
| `BackdropClass` | `string?` | `null` | Backdrop CSS classes |
| `ModalClass` | `string?` | `null` | Modal container CSS |

### ModalSlots Properties

| Slot | Description |
|------|-------------|
| `Base` | The modal root (same as ModalClass) |
| `Backdrop` | The background overlay |
| `Content` | The modal dialog container |
| `Header` | Passed to ModalHeader components |
| `Body` | Passed to ModalBody components |
| `Footer` | Passed to ModalFooter components |

### Keyboard Support

- **Escape**: Close modal (when Dismissible="true")
- Focus trap: Tab cycles within modal
