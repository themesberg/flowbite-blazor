
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
                    With less than a month to go before the European Union enacts new consumer privacy laws for its citizens,
                    companies around the world are updating their terms of service agreements to comply.
                </p>
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    The European Union's General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                    to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                    soon as possible of high-risk data breaches that could personally affect them.
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => HandleTermsChoice(false))" Color="ButtonColor.Gray" class="mr-2">Decline</Button>
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
    
    private string GetChoiceAlertClass()
    {
        return termsAccepted == true
            ? "p-4 text-sm text-green-800 rounded-lg bg-green-50 dark:bg-gray-800 dark:text-green-400"
            : "p-4 text-sm text-red-800 rounded-lg bg-red-50 dark:bg-gray-800 dark:text-red-400";
    }
}

```

__Model Options:__

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

```razor
<div class="space-y-4">
        
    <Modal Show="showDefaultModal" ShowChanged="(value) => showDefaultModal = value" Size="ModalSize.FourExtraLarge" Position="ModalPosition.TopCenter">
        <ModalHeader>
            <h3>Terms of Service</h3>
        </ModalHeader>
        <ModalBody>
            <div class="space-y-6">
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    With less than a month to go before the European Union enacts new consumer privacy laws for its citizens,
                    companies around the world are updating their terms of service agreements to comply.
                </p>
                <p class="text-base leading-relaxed text-gray-500 dark:text-gray-400">
                    The European Union's General Data Protection Regulation (G.D.P.R.) goes into effect on May 25 and is meant
                    to ensure a common set of data rights in the European Union. It requires organizations to notify users as
                    soon as possible of high-risk data breaches that could personally affect them.
                </p>
            </div>
        </ModalBody>
        <ModalFooter>
            <div class="flex justify-end w-full">
                <Button OnClick="@(() => showSizedModal = false)" Color="ButtonColor.Gray" class="mr-2">Decline</Button>
                <Button OnClick="@(() => showSizedModal = false)">Accept</Button>
            </div>
        </ModalFooter>
    </Modal>
</div>

@code {
    private bool showDefaultModal = false;
}
```

