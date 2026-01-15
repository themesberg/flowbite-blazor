<project title="Flowbite Blazor" summary="A comprehensive Blazor component library that ports the Flowbite React component library to ASP.NET Blazor 8.0/9.0. Built on Tailwind CSS v4, it provides strongly-typed Blazor components that implement Flowbite design patterns while maintaining consistency with the React implementation. The library offers a rich set of accessible, dark-mode compatible components with built-in ARIA support, smart positioning via Floating UI, and TailwindMerge-powered class conflict resolution.">

## Project Links

- [Github Repository](https://github.com/peakflames/flowbite-blazor)
- [Documentation Site](https://flowbite-blazor.peakflames.org/docs/components/{{COMPONENT_NAME}})

## Current Version

**v0.2.1-beta** - Requires Tailwind CSS v4 and .NET 8 or .NET 9

## Features

### Core Features
- **Tailwind CSS v4 Integration** - CSS-first configuration with `@theme` directive
- **Dark Mode Support** - Automatic dark mode through Tailwind CSS classes
- **Built-in Accessibility** - ARIA attributes and full keyboard navigation support
- **Responsive Design** - Mobile-first components that work everywhere
- **Native Blazor Events** - Seamless integration with Blazor's event system
- **Strong Typing** - Full type safety and IntelliSense support
- **No Node.js Required** - Simple MSBuild integration for Tailwind CSS
- **Extended Icons** - Optional package for additional icon components

### New in v0.2.x

- **TailwindMerge Integration** - Automatic class conflict resolution via `MergeClasses()`
- **Slot System** - Fine-grained component customization with typed slot classes
- **Floating UI Positioning** - Smart viewport-aware positioning for Dropdown, Tooltip, and Popover
- **Debounced Input** - Built-in debouncing for TextInput search scenarios
- **Animation State Machine** - Smooth height-based animations for SidebarCollapse
- **Lazy JavaScript Modules** - On-demand loading of JS modules for better performance
- **motion-reduce Support** - Respects user's reduced motion preferences

## Service Registration

Register Flowbite services in your `Program.cs`:

```csharp
using Flowbite.Services;

builder.Services.AddFlowbite();
```

This registers:
- `TwMerge` - TailwindMerge service for class conflict resolution
- `IFloatingService` - Floating UI positioning for dropdowns/tooltips
- `IClipboardService` - Lazy-loaded clipboard operations
- `IElementService` - Lazy-loaded DOM element utilities
- `IFocusManagementService` - Lazy-loaded focus trap and management
- `IModalService` - Programmatic modal control
- `IDrawerService` - Programmatic drawer control
- `IToastService` - Programmatic toast notifications

</project>
