# System Patterns

## Component Styling Patterns

### Base Component Structure
- Components inherit from FlowbiteComponentBase
- Use CascadingValue for context sharing
- Parameters follow C# naming conventions (PascalCase)

### CSS Class Management
1. Context-Based Styling
```csharp
public class ComponentContext
{
    // Base classes that are always applied
    public string BaseClasses => "...";
    
    // Wrapper classes for container elements
    public string WrapperClasses => CombineClasses(...);
    
    // Additional classes based on state
    public string ShadowClasses => "...";
}
```

2. Class Combination Pattern
```csharp
private string CombineClasses(params string[] classes)
{
    return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
}
```

## Form Validation Patterns

### EditForm Integration
```csharp
// Base form component pattern
public class FlowbiteFormComponentBase : FlowbiteComponentBase
{
    [CascadingParameter] private EditContext EditContext { get; set; }
    
    // Track field state
    private FieldIdentifier _fieldIdentifier;
    private ValidationMessageStore _messageStore;
    
    // Handle validation state changes
    protected override void OnParametersSet()
    {
        _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
        _messageStore = new ValidationMessageStore(EditContext);
    }
}
```

### Validation State Management
```csharp
protected virtual string GetValidationClasses()
{
    if (!EditContext?.IsModified(_fieldIdentifier) ?? true)
        return "";
        
    return EditContext.GetValidationMessages(_fieldIdentifier).Any()
        ? "invalid:border-red-500 invalid:focus:border-red-500"
        : "valid:border-green-500 valid:focus:border-green-500";
}
```

### Custom Validation Support
```csharp
public interface IFlowbiteValidator
{
    bool Validate(object value, out string[] messages);
    bool ValidateField(FieldIdentifier field, out string[] messages);
}

// Usage in components
public class FlowbiteInput : FlowbiteFormComponentBase
{
    [Parameter] public IFlowbiteValidator Validator { get; set; }
    
    private async Task ValidateValue()
    {
        if (Validator?.Validate(Value, out var messages) == false)
        {
            _messageStore.Add(_fieldIdentifier, messages);
        }
    }
}
```

## Form State Management Patterns

### Form Context Provider
```csharp
public class FlowbiteFormContext
{
    // Track form-level state
    public bool IsDirty { get; private set; }
    public bool IsSubmitting { get; private set; }
    
    // Track field changes
    private Dictionary<string, object> _initialValues = new();
    private Dictionary<string, object> _currentValues = new();
    
    // State management methods
    public void TrackField(string name, object value) { }
    public void Reset() { }
    public bool IsFieldDirty(string name) { }
}
```

### Form State Tracking
```csharp
public class FlowbiteForm : ComponentBase
{
    private FlowbiteFormContext _formContext = new();
    
    [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }
    [Parameter] public EventCallback OnReset { get; set; }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<FlowbiteFormContext>>(0);
        builder.AddAttribute(1, "Value", _formContext);
        builder.AddAttribute(2, "ChildContent", (RenderFragment)(builder => {
            builder.OpenComponent<EditForm>(3);
            // Add form attributes
            builder.CloseComponent();
        }));
        builder.CloseComponent();
    }
}
```

### Form Submission Pattern
```csharp
public class FlowbiteSubmitButton : FlowbiteComponentBase
{
    [CascadingParameter] private FlowbiteFormContext FormContext { get; set; }
    
    private async Task HandleSubmit()
    {
        try
        {
            FormContext.IsSubmitting = true;
            await OnClick.InvokeAsync();
        }
        finally
        {
            FormContext.IsSubmitting = false;
        }
    }
}
```

## Off-Canvas Component Patterns

### Base Off-Canvas Component
```csharp
public abstract class OffCanvasComponentBase : FlowbiteComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; } = default!;
    
    protected string Id { get; } = $"flowbite-offcanvas-{Guid.NewGuid()}";
    protected bool IsVisible { get; set; }
    
    protected async Task ShowAsync()
    {
        IsVisible = true;
        await InvokeAsync(StateHasChanged);
        await JSRuntime.InvokeVoidAsync("flowbiteBlazor.trapFocus", Id);
    }
    
    protected async Task HideAsync()
    {
        IsVisible = false;
        await InvokeAsync(StateHasChanged);
        await JSRuntime.InvokeVoidAsync("flowbiteBlazor.restoreFocus");
    }
}
```

### Service-Based Architecture
```csharp
// Service interface
public interface IModalService
{
    Task<ModalResult<TResult>> ShowAsync<TResult>(string title, RenderFragment content, ModalOptions options = null);
    Task<ModalResult<TResult>> ShowAsync<TComponent, TResult>(ModalParameters parameters = null) where TComponent : ComponentBase;
    void Close(object result = null);
}

// Service implementation
public class ModalService : IModalService
{
    private readonly List<ModalInstance> _modals = new();
    
    public event Action<ModalInstance> OnModalInstanceAdded;
    public event Action<string> OnModalCloseRequested;
    
    // Implementation details...
}

// Host component
public class ModalHost : IDisposable
{
    [Inject] private IModalService ModalService { get; set; } = default!;
    
    private List<ModalInstance> _modals = new();
    
    protected override void OnInitialized()
    {
        ModalService.OnModalInstanceAdded += AddModal;
        ModalService.OnModalCloseRequested += CloseModal;
    }
    
    // Implementation details...
}
```

### CSS Transition Pattern
```csharp
// Modal position classes
private string GetPositionClasses() => Position switch
{
    ModalPosition.TopLeft => "items-start justify-start",
    ModalPosition.TopCenter => "items-start justify-center",
    ModalPosition.TopRight => "items-start justify-end",
    ModalPosition.CenterLeft => "items-center justify-start",
    ModalPosition.Center => "items-center justify-center",
    ModalPosition.CenterRight => "items-center justify-end",
    ModalPosition.BottomLeft => "items-end justify-start",
    ModalPosition.BottomCenter => "items-end justify-center",
    ModalPosition.BottomRight => "items-end justify-end",
    _ => "items-center justify-center" // Default
};

// Drawer transition classes
private string GetPositionClasses()
{
    return Position switch
    {
        DrawerPosition.Top when IsVisible => "left-0 right-0 top-0 w-full transform-none",
        DrawerPosition.Top => "left-0 right-0 top-0 w-full -translate-y-full",
        
        DrawerPosition.Right when IsVisible => "right-0 top-0 h-screen w-80 transform-none",
        DrawerPosition.Right => "right-0 top-0 h-screen w-80 translate-x-full",
        
        DrawerPosition.Bottom when IsVisible => "bottom-0 left-0 right-0 w-full transform-none",
        DrawerPosition.Bottom => "bottom-0 left-0 right-0 w-full translate-y-full",
        
        DrawerPosition.Left when IsVisible => "left-0 top-0 h-screen w-80 transform-none",
        DrawerPosition.Left => "left-0 top-0 h-screen w-80 -translate-x-full",
        
        _ => "left-0 top-0 h-screen w-80 -translate-x-full" // Default
    };
}
```

## Testing Support
1. Data Attributes:
   - Add data-testid for component testing
   - Match React testing attributes
   - Consider making configurable via parameters

## Documentation Requirements
1. XML Comments:
   - Document all public members
   - Explain parameter purposes
   - Note validation behavior

2. Class Documentation:
   - Document validation patterns
   - Explain state management
   - Note form submission handling

3. Usage Examples:
   - Provide validation examples
   - Show state management
   - Include form submission
