
#### Spinner Examples

The available Button colors are:
- None specified is the same as SpinnerColor.Info
- SpinnerColor.Info,
- SpinnerColor.Success,
- SpinnerColor.Warning,
- SpinnerColor.Failure,
- SpinnerColor.Pink,
- SpinnerColor.Purple,
- SpinnerColor.Gray

The available Button sizes are:
- SpinnerSize.Xs,
- SpinnerSize.Sm,
- SpinnerSize.Md,
- SpinnerSize.Lg,
- SpinnerSize.Xl


```razor
<!-- Different sizes -->
<Spinner Size="SpinnerSize.Small" />
<Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Success" />
<Spinner Size="SpinnerSize.Large" Color="SpinnerColor.Warning" />

<!-- With text -->
<div class="flex items-center">
    <Spinner Size="SpinnerSize.Medium" Color="SpinnerColor.Primary" />
    <span class="ml-2">Loading...</span>
</div>
```
