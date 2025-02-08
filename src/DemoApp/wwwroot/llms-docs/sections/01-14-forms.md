#### Form Component Examples

The available form components are:
- Label - For form field labels with required/disabled states
- TextInput - Text input with sizes, states, and addons
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
     - Color: TextInputColor - Success, Failure, etc for validation
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the input
     - HelperText: string - Help text below input
     - AddonLeft/Right: string - Text addons
     - Icon/RightIcon: Component - Icon components
     - Shadow: bool - Adds shadow effect -->
<TextInput Id="email" Type="email" Placeholder="name@flowbite.com" Required="true" />
<TextInput Size="TextInputSize.Small" Placeholder="Small input" />
<TextInput Color="TextInputColor.Success" Value="Success input" HelperText="Success message" />
<TextInput AddonLeft="https://" AddonRight=".com" Placeholder="flowbite" />

<!-- Textarea Component
     Parameters:
     - Id: string - Textarea identifier
     - Rows: int - Number of visible text rows
     - Placeholder: string - Placeholder text
     - Required: bool - Makes field required
     - Disabled: bool - Disables the textarea
     - HelperText: string - Help text below textarea
     - Shadow: bool - Adds shadow effect -->
<Textarea Id="comment" 
          Rows="4" 
          Placeholder="Write your thoughts here..." 
          Required="true" />

<!-- Select Component
     Parameters:
     - Id: string - Select identifier
     - Size: TextInputSize - Small, Medium, Large
     - Color: SelectColor - Success, Failure, etc
     - Disabled: bool - Disables the select
     - HelperText: string - Help text below select
     - Icon: Type - Icon component type
     - Shadow: bool - Adds shadow effect -->
<Select Id="countries">
    <option value="">Choose a country</option>
    <option value="US">United States</option>
    <option value="CA">Canada</option>
</Select>

<!-- Checkbox Component
     Parameters:
     - Id: string - Checkbox identifier
     - Checked: bool - Checked state
     - Disabled: bool - Disables the checkbox
     - Required: bool - Makes field required -->
<div class="flex items-center gap-2">
    <Checkbox Id="remember" />
    <Label For="remember">Remember me</Label>
</div>

<!-- Radio Component
     Parameters:
     - Id: string - Radio identifier
     - Name: string - Groups radio buttons
     - Value: bool - Selected state
     - Disabled: bool - Disables the radio
     - Required: bool - Makes field required -->
<div class="flex items-center gap-2">
    <Radio Id="option1" Name="group" Value="true" />
    <Label For="option1">Option 1</Label>
</div>

<!-- FileInput Component
     Parameters:
     - Id: string - Input identifier
     - HelperText: string - Help text below input
     - Color: FileInputColor - Success, Failure, etc
     - Disabled: bool - Disables the input
     - Shadow: bool - Adds shadow effect -->
<FileInput Id="file" 
           HelperText="Upload your profile picture" />

<!-- ToggleSwitch Component
     Parameters:
     - Checked: bool - Toggle state
     - Label: string - Label text
     - Disabled: bool - Disables the toggle
     - Name: string - Form field name -->
<ToggleSwitch @bind-Checked="isEnabled" 
              Label="Enable notifications" />

<!-- RangeSlider Component
     Parameters:
     - Id: string - Slider identifier
     - Size: RangeSliderSize - Small, Medium, Large
     - Value: double - Current value
     - Min: double - Minimum value (default: 0)
     - Max: double - Maximum value (default: 100)
     - Step: double - Step increment (default: 1)
     - Disabled: bool - Disables the slider -->
<RangeSlider Id="default-range"
             Size="RangeSliderSize.Medium" />

#### Form Validation Examples

The Flowbite Blazor library supports both built-in DataAnnotations validation and custom validation scenarios:

##### Basic Form Validation with DataAnnotations

```razor
<!-- Basic form with DataAnnotations validation
     Features:
     - Uses EditForm for form handling
     - DataAnnotationsValidator for validation
     - ValidationMessage for error display
     - Real-time validation feedback -->
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

##### Custom Validation

```razor
<!-- Custom validation example
     Features:
     - Custom validator component
     - Field-level validation
     - Custom validation messages -->
<EditForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <CustomValidator @ref="customValidator" />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="username" Value="Username" />
            </div>
            <TextInput TValue="string" 
                      Id="username" 
                      @bind-Value="model.Username" />
            <ValidationMessage For="@(() => model.Username)" />
        </div>
        <Button Type="submit">Submit</Button>
    </div>
</EditForm>

@code {
    private CustomValidator? customValidator;

    private async Task HandleValidSubmit()
    {
        // Example of custom validation logic
        if (customModel.Username.Contains(" "))
        {
            customValidator?.DisplayError("Username", "Username cannot contain spaces");
            return;
        }
        await SubmitForm();
    }
}
```

##### Form State Management

```razor
<!-- Form state management example
     Features:
     - Dirty state tracking
     - Change tracking
     - Reset functionality
     - EditContext integration -->
<EditForm EditContext="@editContext" OnValidSubmit="@HandleSubmit">
    <DataAnnotationsValidator />
    <div class="flex max-w-md flex-col gap-4">
        <div>
            <div class="mb-2 block">
                <Label For="name" Value="Name" />
            </div>
            <TextInput TValue="string" 
                      Id="name" 
                      Value="@name" 
                      ValueChanged="@OnNameChanged" />
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
    private string? name;

    protected override void OnInitialized()
    {
        editContext = new EditContext(model);
        name = model.Name;
    }

    private void OnNameChanged(string? value)
    {
        name = value;
        model.Name = value ?? "";
        editContext?.NotifyFieldChanged(editContext.Field(nameof(model.Name)));
    }

    private void ResetForm()
    {
        model = new Model();
        editContext = new EditContext(model);
        name = model.Name;
    }
}
```

The form validation system integrates seamlessly with Blazor's built-in form handling while providing additional features for custom validation scenarios and state management. All form components support validation states through their Color properties and automatically integrate with ValidationMessage components.
