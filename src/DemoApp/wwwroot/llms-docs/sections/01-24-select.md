#### Select Component

Use the `Select` component for native single-select inputs with Flowbite styling, addon support, icons, helper text, and validation states. Pass the available options through standard `<option>` elements inside the component and bind to `Value`/`ValueChanged` (or `@bind-Value`) for data binding.

- `Color`: switch validation colors (`SelectColor.Gray`, `Success`, `Failure`, etc.)
- `Size`: `TextInputSize.Small`, `Medium`, or `Large`
- `Icon`/`Addon`: render icons or addon content inside the field
- `Shadow`, `Disabled`, `HelperText`: match the API used by `TextInput`
- For inline filtering, use the `Combobox` component which renders a custom dropdown with a dedicated search field.

```razor
<Select Id="countries" @bind-Value="selectedCountry">
    <option value="">Choose a country</option>
    <option value="US">United States</option>
    <option value="CA">Canada</option>
    <option value="DE">Germany</option>
    <option value="FR">France</option>
</Select>

@code {
    private string? selectedCountry = "US";
}
```
