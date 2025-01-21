
#### Alert Examples

The available Alert colors are:
- AlertColor.Info
- AlertColor.Gray
- AlertColor.Failure
- AlertColor.Success
- AlertColor.Warning
- AlertColor.Red
- AlertColor.Green
- AlertColor.Yellow
- AlertColor.Blue
- AlertColor.Primary
- AlertColor.Pink
- AlertColor.Lime
- AlertColor.Dark
- AlertColor.Indigo
- AlertColor.Purple
- AlertColor.Teal
- AlertColor.Light

```razor
<!-- Default alert with emphasis -->
<Alert Color="AlertColor.Info"
       TextEmphasis="Info alert!"
       Text="More info about this info alert goes here." />

<!-- Alert with icon and border accent -->
<Alert Color="AlertColor.Warning"
       Icon="@(new EyeIcon())"
       WithBorderAccent="true"
       TextEmphasis="Warning alert!"
       Text="More info about this warning alert goes here." />

<!-- Alert with custom content -->
<Alert Color="AlertColor.Info">
    <CustomContent>
        <div class="flex items-center">
            <span class="text-lg font-medium">Requirements:</span>
        </div>
    </CustomContent>
    <AdditionalContent>
        <div class="mt-2 mb-4 text-sm">
            <ul class="list-disc list-inside">
                <li>Minimum 10 characters</li>
                <li>At least one special character</li>
            </ul>
        </div>
    </AdditionalContent>
</Alert>
```
