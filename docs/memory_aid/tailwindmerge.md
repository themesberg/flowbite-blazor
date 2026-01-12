# TailwindMerge.NET Usage

## Package Info

- **Package:** `TailwindMerge.NET` (NOT `TailwindMerge`)
- **Version:** 1.0.0
- **NuGet:** https://www.nuget.org/packages/TailwindMerge.NET

## Service Registration

TailwindMerge.NET uses dependency injection, not a static API.

```csharp
// In ServiceCollectionExtensions.cs
using TailwindMerge.Extensions;

services.AddTailwindMerge();
```

## Usage in Components

```csharp
// In FlowbiteComponentBase.cs
using TailwindMerge;

[Inject]
internal TwMerge TwMerge { get; set; } = default!;

protected string MergeClasses(params string?[] classes)
{
    var combined = string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    return TwMerge.Merge(combined) ?? string.Empty;
}
```

## When to Use

| Scenario | Use |
|----------|-----|
| Class conflicts possible (user override) | `MergeClasses()` |
| Simple concatenation, no conflicts | `CombineClasses()` |
| Building component classes with `Class` param | `MergeClasses(baseClasses, Class)` |

## Examples

```csharp
// Later classes win when conflicts occur
MergeClasses("px-4", "px-6")           // Returns "px-6"
MergeClasses("bg-red-500", "bg-blue-500") // Returns "bg-blue-500"
MergeClasses("p-2 m-4", "p-4")         // Returns "m-4 p-4"
```

## Gotchas

1. **Wrong package name:** Use `TailwindMerge.NET`, not `TailwindMerge`
2. **Not static:** Must inject `TwMerge`, can't use `TW.Merge()` static call
3. **Nullable return:** `TwMerge.Merge()` can return null, add `?? string.Empty`
4. **WebAssembly compatible:** Works in Blazor WebAssembly after clean rebuild
