# Flowbite Blazor Components

Flowbite component library for ASP.NET Blazor 8.0, providing a comprehensive set of UI components styled with TailwindCSS. This library is a port of the popular Flowbite React library, bringing its beautiful design system to the Blazor ecosystem.

## Installation

1. Install the NuGet package:

```powershell
dotnet add package Flowbite.Blazor --prerelease
```

2. Install TailwindCSS (if not already installed):

```bash
npm install -D tailwindcss
npx tailwindcss init
```

3. Add Flowbite plugin to your tailwind.config.js:

```js
module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        './node_modules/flowbite/**/*.js'
    ],
    plugins: [
        require('flowbite/plugin')
    ],
    darkMode: 'class',
    safelist: [
        "md:bg-transparent",
        "md:block",
        "md:border-0",
        "md:dark:hover:bg-transparent",
        "md:dark:hover:text-white",
        "md:flex-row",
        "md:font-medium",
        "md:hidden",
        "md:hover:bg-transparent",
        "md:hover:text-primary-700",
        "md:mt-0",
        "md:p-0",
        "md:space-x-8",
        "md:text-primary-700",
        "md:text-sm",
        "md:w-auto"
    ],
}
```

4. Add imports to your _Imports.razor:

```razor
@using Flowbite
@using Flowbite.Components
```

## Basic Usage

```razor
<Button Color="ButtonColor.Primary" Size="ButtonSize.Large">
    Click me!
</Button>

<Alert Color="AlertColor.Info">
    This is an info alert!
</Alert>

<Tooltip Text="This is a tooltip">
    <Button>Hover me</Button>
</Tooltip>
```

## Available Components

- Button - Customizable button component with various styles and sizes
- Alert - Contextual feedback messages
- Card - Flexible content container
- Tooltip - Add tooltips to any element
- Spinner - Loading indicators
- Badge - Small count and labeling components
- Avatar - User profile pictures or initials
- Breadcrumb - Navigation aid
- Sidebar - Responsive side navigation
- Navbar - Top navigation bar
- Dropdown - Interactive dropdown menus

## Features

- 🎨 TailwindCSS Integration
- 🌙 Dark Mode Support
- ♿ Built-in Accessibility
- 📱 Responsive Design
- 🚀 Native Blazor Events
- 🎯 Strong Typing
- 📖 XML Documentation

## Additional Icons

Looking for more icons? Check out our [Flowbite.Blazor.ExtendedIcons](https://www.nuget.org/packages/Flowbite.Blazor.ExtendedIcons) package!

## Documentation

For detailed documentation and examples, visit our [GitHub repository](https://github.com/peakflames/flowbite-blazor).

## License

This project is licensed under the MIT License.
