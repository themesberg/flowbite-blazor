# Contributing to Flowbite Blazor

Thank you for your interest in contributing to Flowbite Blazor! This document provides guidelines and instructions for contributing to the project.

## Development Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/peakflames/flowbite-blazor.git
   cd flowbite-blazor
   ```

1. Install standalone Tailwind CSS CLI executable:

   Mac/Linux:

   ```bash
   mkdir ./tools && cd ./tools && curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-macos-arm64  && chmod +x tailwindcss-macos-arm64 && mv tailwindcss-macos-arm64 tailwindcss
   ```

   Windows:

   ```pwsh
   mkdir ./tools -Force; `
   cd ./tools; `
   Invoke-WebRequest -Uri "https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-windows-x64.exe" `
      -OutFile "tailwindcss.exe" `
      -UseBasicParsing ; `
   cd ..

   ```

1. Build the solution

   ```bash
   dotnet build
   ```

1. Run the DemoApp

   ```bash
   cd src/DemoApp
   dotnet run
   ```

   Then open <http://localhost:5290/> in your browser.


## Development Workflow

### Local Development

The solution is configured for two development modes:

1. Debug/Development (default):
   - Use `dotnet watch`
   - F5 to run and debug

1. Release (package testing):
   - Uses NuGet package references
   - Tests the packaged components
   - To test changes with local NuGet packages:

      1. Pack the libraries and publish the DemoApp to the .\dist directory:

         ```powershell
         .\publish-local.ps1
         ```

      1. Serve the DemoApp as a static web site:

         ```powershell
         # Ensure you have the dotnet tool `dotnet-serve` installed.
         cd dist\wwwwroot; dotnet serve
         ```

## Coding Standards

### Component Development

1. File Organization:
   - One component per file
   - Place in appropriate feature folder under Components/
   - Use partial classes for complex components

1. Naming Conventions:
   - PascalCase for public members and types
   - _camelCase for private fields
   - Parameters must be public properties

1. Documentation:
   - XML comments for public APIs
   - Include parameter descriptions
   - Document event callbacks

1. Code Style:
   - Use 4 spaces for indentation
   - Prefer C# code in .cs file over .razor file
   - Prefer functions over lambda expressions for event handlers

### Component Requirements

1. Accessibility:
   - Include ARIA attributes
   - Support keyboard navigation
   - Maintain focus management

1. Styling:
   - TailwindCSS is integrated via MSBuild
   - Use TailwindCSS utilities
   - Support dark mode
   - Follow Flowbite design system

1. Testing:
   - Create demo page examples
   - Test all component variants
   - Verify accessibility
   - Test in both light and dark modes

## Pull Request Process

1. Create a feature branch:

   ```bash
   git checkout -b feature/your-feature-name
   ```

1. Make your changes:
   - Follow coding standards
   - Add/update documentation
   - Create/update demo pages

1. Test your changes:
   - Build in both Debug and Release modes
   - Test with local NuGet packages
   - Verify demo pages

1. Create Pull Request:
   - Provide clear description
   - Reference any related issues
   - Include screenshots for UI changes
   - List breaking changes if any

### PR Requirements

- All builds must pass
- Demo pages for new components
- Documentation updates
- No breaking changes without discussion
- Follows coding standards

## Release Process

1. Version Updates:
   - Update version numbers
   - Update CHANGELOG.md
   - Update documentation

1. Package Testing:
   - Test with local NuGet packages
   - Verify in DemoApp
   - Check documentation accuracy

1. Release:
   - Create release branch
   - Update version numbers
   - Create GitHub release
   - Publish to NuGet

## Getting Help

- Create an issue for bugs
- Start a discussion for features
- Tag maintainers for urgent issues

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
