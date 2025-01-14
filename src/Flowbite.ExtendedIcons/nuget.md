# Flowbite Blazor Extended Icons

Additional icon components package for Flowbite.Blazor, providing a comprehensive set of SVG icons as Blazor components. This package extends the core Flowbite.Blazor library with a rich collection of commonly used icons.

## Installation

1. Install the NuGet package:

```powershell
dotnet add package Flowbite.Blazor.ExtendedIcons --prerelease
```

2. Add imports to your _Imports.razor:

```razor
@using Flowbite.ExtendedIcons
```

## Basic Usage

Icons can be used directly as components:

```razor
<AngleDownIcon class="w-6 h-6" />
<BellActiveIcon class="w-6 h-6 text-blue-500" />
<BookmarkIcon class="w-6 h-6 dark:text-white" />
```

## Features

- 🎨 Color Inheritance - Icons inherit text color by default
- 📏 Flexible Sizing - Control size via CSS classes
- 🌙 Dark Mode Support - Works seamlessly with dark mode
- ♿ Accessibility - Built-in ARIA attributes
- 📦 Optimized - Individual components for tree-shaking
- 🎯 Strong Typing - Full IntelliSense support

## Available Icons

The package includes over 100 commonly used icons, including:

### UI Elements

- Angle (Down, Up, Left, Right)
- Caret (Down, Up, Left, Right)
- Chevron (Double variants)
- Arrow variants
- Sort indicators

### Actions

- Add/Delete
- Edit/Pen
- Copy/Paste
- Search
- Download/Upload
- Refresh/Sync

### Objects

- Files (Various types)
- Folders
- Documents
- Images
- Media

### Communication

- Bell/Notifications
- Chat/Messages
- Email
- Share

### Status

- Check/Success
- Warning
- Error
- Information
- Loading

## Customization

Icons can be customized using standard CSS classes:

```razor
<!-- Size -->
<BookIcon class="w-4 h-4" />  <!-- Small -->
<BookIcon class="w-6 h-6" />  <!-- Medium -->
<BookIcon class="w-8 h-8" />  <!-- Large -->

<!-- Color -->
<BookIcon class="text-blue-500" />
<BookIcon class="text-gray-700 dark:text-gray-200" />

<!-- Transitions -->
<BookIcon class="transition-colors hover:text-blue-600" />
```

## Requirements

- Flowbite.Blazor (core package)
- ASP.NET Core 8.0 or later
- TailwindCSS

## Documentation

For detailed documentation and examples, visit our [GitHub repository](https://github.com/peakflames/flowbite-blazor).

## License

This project is licensed under the MIT License.
