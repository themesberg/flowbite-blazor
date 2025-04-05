
#### QuickGrid Examples

Leverage the Microsoft's QuickGrid Blazor componet and achieve Flowbite styling by applying the flowbite-grid CSS class and the Theme="flowbite".

```razor
@* the parent div must have both the grid and flowbite-grid classes *@
<div class="grid flowbite-grid">
    @* Set the Theme property to flowbite *@
    <QuickGrid Theme="flowbite">
    ...
    </QuickGrid>
</div>
```

Example:

```razor
<!-- Basic QuickGrid-->
<div class="grid flowbite-grid text-xs">
    <QuickGrid Items="@_pokemon" Theme="flowbite">
        <PropertyColumn Property="@(p => p.Id)" Title="#" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Name)" Title="Name" />
        <PropertyColumn Property="@(p => p.Type1)" Title="Type" />
        <PropertyColumn Property="@(p => p.HP)" Title="HP" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Attack)" Title="Attack" Align="Align.Center" />
        <PropertyColumn Property="@(p => p.Defense)" Title="Defense" Align="Align.Center" />
    </QuickGrid>
</div>


```
