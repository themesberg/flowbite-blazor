#### Form Component Examples

The available form components are:
- Label - For form field labels with required/disabled states
- TextInput - Text input with sizes, states, addons, and debouncing
- Textarea - Multi-line text input
- Select - Dropdown selection with sizes and states
- Checkbox - Single checkbox with label
- Radio - Radio button groups
- FileInput - File upload input
- ToggleSwitch - Toggle switch with label
- RangeSlider - Range slider with sizes

```razor
<!-- Label Component
     Parameters:
     - For: string - Associates label with form control
     - Value: string - Label text content
     - Required: bool - Shows required indicator
     - Disabled: bool - Grays out the label
     - Class: string - Additional CSS classes -->
<Label For="email" Value="Your email" Required="true" />
<Label For="disabled" Value="Disabled label" Disabled="true" />

<!-- TextInput Component
     Parameters:
     - Id: string - Input identifier
     - Type: string - Input type (text, email, password, etc)
     - Size: TextInputSize - Small, Medium, Large
     - Color: TextInputColor - Gray, Success, Failure, Warning, Info
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the input
     - HelperText: string - Help text below input
     - AddonLeft/Right: string - Text addons
     - Icon/RightIcon: IconBase - Icon components
     - Shadow: bool - Adds shadow effect
     - Behavior: InputBehavior - OnChange (default) or OnInput
     - DebounceDelay: int - Debounce delay in milliseconds -->
<TextInput Id="email" Type="email" Placeholder="name@flowbite.com" Required="true" />
<TextInput Size="TextInputSize.Small" Placeholder="Small input" />
<TextInput Color="TextInputColor.Success" Value="Success input" HelperText="Success message" />
<TextInput AddonLeft="https://" AddonRight=".com" Placeholder="flowbite" />
```

### TextInput Debouncing

For search-as-you-type scenarios, use the `Behavior` and `DebounceDelay` parameters:

```razor
<!-- Standard form input (fires on blur/Enter) -->
<TextInput @bind-Value="Username" Placeholder="Username" />

<!-- Search with debouncing (fires 300ms after typing stops) -->
<TextInput
    @bind-Value="SearchQuery"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="300"
    Placeholder="Search products..."
    Icon="@(new SearchIcon())" />

@code {
    private string SearchQuery { get; set; } = "";

    // ValueChanged fires 300ms after user stops typing
    // Previous pending calls are automatically cancelled
}

<!-- Instant validation without debounce -->
<TextInput
    @bind-Value="Email"
    Behavior="InputBehavior.OnInput"
    DebounceDelay="0"
    Type="email"
    Color="@GetEmailValidationColor()" />
```

### InputBehavior Options

| Value | Description |
|-------|-------------|
| `OnChange` (default) | Fire on blur or Enter key - standard form behavior |
| `OnInput` | Fire on every keystroke (subject to DebounceDelay) |

```razor
<!-- Textarea Component -->
<Textarea Id="comment"
          Rows="4"
          Placeholder="Write your thoughts here..."
          Required="true" />

<!-- Select Component -->
<Select Id="countries" @bind-Value="selectedCountry">
    <option value="">Choose a country</option>
    <option value="US">United States</option>
    <option value="CA">Canada</option>
</Select>

<!-- Checkbox Component -->
<div class="flex items-center gap-2">
    <Checkbox Id="remember" />
    <Label For="remember">Remember me</Label>
</div>

<!-- Radio Component -->
<div class="flex items-center gap-2">
    <Radio Id="option1" Name="group" Value="true" />
    <Label For="option1">Option 1</Label>
</div>

<!-- FileInput Component -->
<FileInput Id="file"
           HelperText="Upload your profile picture" />

<!-- ToggleSwitch Component -->
<ToggleSwitch @bind-Checked="isEnabled"
              Label="Enable notifications" />

<!-- RangeSlider Component -->
<RangeSlider Id="default-range"
             Size="RangeSliderSize.Medium" />
```

#### Form Validation Examples

The Flowbite Blazor library supports both built-in DataAnnotations validation and custom validation scenarios:

##### Basic Form Validation with DataAnnotations

```razor
<!-- Basic form with DataAnnotations validation -->
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="email" Value="Email address" />
            </div>
            <TextInput TValue="string"
                      Id="email"
                      @bind-Value="model.Email"
                      Type="email" />
            <ValidationMessage For="@(() => model.Email)" />
        </div>
        <div>
            <div class="mb-2 block">
                <Label For="password" Value="Password" />
            </div>
            <TextInput TValue="string"
                      Id="password"
                      @bind-Value="model.Password"
                      Type="password" />
            <ValidationMessage For="@(() => model.Password)" />
        </div>
        <Button Type="submit">Submit</Button>
    </div>
</EditForm>

@code {
    private class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = "";
    }
}
```

##### Form State Management

```razor
<!-- Form state management example using @bind-Value with :after callback -->
<EditForm EditContext="@editContext" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="name" Value="Name" />
            </div>
            <TextInput TValue="string"
                      Id="name"
                      @bind-Value="model.Name"
                      @bind-Value:after="OnNameChanged" />
            <ValidationMessage For="@(() => model.Name)" />
        </div>
        <div class="flex gap-2">
            <Button Type="submit"
                    Disabled="@(!editContext?.IsModified() ?? true)">
                Submit
            </Button>
            <Button Color="ButtonColor.Light"
                    OnClick="@ResetForm">
                Reset
            </Button>
        </div>
        <div class="text-sm text-gray-500">
            Form Status: @(editContext?.IsModified() ?? false ? "Modified" : "Unmodified")
        </div>
    </div>
</EditForm>

@code {
    private EditContext? editContext;
    private Model model = new();

    protected override void OnInitialized()
    {
        editContext = new EditContext(model);
    }

    private void OnNameChanged()
    {
        // Notify EditContext that field changed (for IsModified tracking)
        editContext?.NotifyFieldChanged(editContext.Field(nameof(model.Name)));
    }

    private void ResetForm()
    {
        model = new Model();
        editContext = new EditContext(model);
    }
}
```

### TextInput Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `@bind-Value` | `TValue` | - | Two-way data binding (recommended) |
| `Type` | `string` | `"text"` | HTML input type |
| `Size` | `TextInputSize` | `Medium` | Size variant |
| `Color` | `TextInputColor?` | `null` | Color variant (null = auto-validation colors) |
| `Placeholder` | `string?` | `null` | Placeholder text |
| `Disabled` | `bool` | `false` | Disabled state |
| `Required` | `bool` | `false` | Required field |
| `Shadow` | `bool` | `false` | Shadow effect |
| `HelperText` | `string?` | `null` | Help text below input |
| `Icon` | `IconBase?` | `null` | Left icon |
| `RightIcon` | `IconBase?` | `null` | Right icon |
| `AddonLeft` | `string?` | `null` | Left text addon |
| `AddonRight` | `string?` | `null` | Right text addon |
| `Behavior` | `InputBehavior` | `OnChange` | When to fire ValueChanged |
| `DebounceDelay` | `int` | `0` | Debounce delay (ms) |
| `Pattern` | `string?` | `null` | Regex validation pattern |
| `InputMode` | `string?` | `null` | Mobile keyboard hint |
| `Class` | `string?` | `null` | Additional CSS classes |

The form validation system integrates seamlessly with Blazor's built-in form handling while providing additional features for debouncing, custom validation scenarios, and state management. TextInput, Textarea, and Select components automatically display red/Failure color when validation errors occur (when Color is null). Set an explicit Color to override this automatic behavior. All form components integrate with ValidationMessage components.
