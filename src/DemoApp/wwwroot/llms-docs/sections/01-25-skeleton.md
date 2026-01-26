
#### Skeleton Component

The Skeleton component provides loading placeholder animations while content is being fetched.

**Available Variants:**
- `SkeletonVariant.Text` - Text line placeholder (default)
- `SkeletonVariant.Avatar` - Circular avatar placeholder
- `SkeletonVariant.Thumbnail` - Thumbnail image placeholder
- `SkeletonVariant.Button` - Button placeholder
- `SkeletonVariant.Card` - Card placeholder
- `SkeletonVariant.Input` - Form input placeholder

**Parameters:**
- `Variant` - The skeleton variant type
- `Width` - Custom width class (e.g., "w-64")
- `Height` - Custom height class (e.g., "h-16")
- `Animated` - Whether to show pulse animation (default: true)
- `Lines` - Number of lines for Text variant (default: 1)
- `LineSpacing` - Custom spacing between lines (default: "space-y-2.5")
- `AriaLabel` - Screen reader label for accessibility

**Accessibility:**
- Uses `role="status"` and `aria-busy="true"`
- Respects `prefers-reduced-motion` via `motion-reduce:animate-none`
- Includes screen reader text "Loading..."

```razor
<!-- Basic text skeleton -->
<Skeleton />

<!-- Avatar skeleton -->
<Skeleton Variant="SkeletonVariant.Avatar" />

<!-- Multi-line text skeleton -->
<Skeleton Variant="SkeletonVariant.Text" Lines="3" />

<!-- Input skeleton -->
<Skeleton Variant="SkeletonVariant.Input" />

<!-- Custom dimensions -->
<Skeleton Width="w-64" Height="h-16" />

<!-- Without animation -->
<Skeleton Animated="false" />

<!-- Card skeleton -->
<Skeleton Variant="SkeletonVariant.Card" />
```
