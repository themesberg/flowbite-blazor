# Contributing to Flowbite Blazor

Thank you for your interest in contributing to Flowbite Blazor! This document provides guidelines and instructions for contributing to the project.

## Development Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/peakflames/flowbite-blazor.git
   cd flowbite-blazor
   ```

1. Build and run the DemoApp:

   ```bash
   # Build the solution (auto-downloads Tailwind CSS on first run)
   python build.py

   # Start DemoApp in background
   python build.py start
   ```

   Then open <http://localhost:5290/> in your browser.

   ```bash
   # Stop when done
   python build.py stop
   ```

   See `python build.py --help` for all available commands.


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
   - Add `motion-reduce:transition-none` to all elements with `transition-*` classes (respects `prefers-reduced-motion` user preference)

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
   - Add unit tests for new components (see Testing section below)

## Testing

The project uses [bUnit](https://bunit.dev/) for component unit tests and [Playwright](https://playwright.dev/dotnet/) for end-to-end integration tests.

### Running Tests

```bash
# Run all unit tests
python build.py test

# Run specific test class
python build.py test --filter "TextInputTests"

# Run integration tests (requires DemoApp running)
python build.py test-integration
```

### Test Project Structure

Tests are located in `src/Flowbite.Tests/`:

```
src/Flowbite.Tests/
├── TestSetup/           # Base test context and fixtures
├── Utilities/           # Utility class tests (Debouncer, ElementClass)
├── Services/            # Service tests (TailwindMerge)
├── Components/          # Component unit tests
│   └── Forms/           # Form component tests
└── Integration/         # Playwright E2E tests
```

### Writing Component Tests

Inherit from `FlowbiteTestContext` for component tests:

```csharp
using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components;

public class MyComponentTests : FlowbiteTestContext
{
    [Fact]
    public void MyComponent_RendersCorrectly()
    {
        // Arrange & Act
        var cut = RenderComponent<MyComponent>(parameters => parameters
            .Add(p => p.Label, "Test"));

        // Assert
        cut.Find("label").TextContent.Should().Contain("Test");
    }
}
```

See `src/Flowbite.Tests/CLAUDE.md` for detailed testing patterns and examples.

### First-Time Setup for Integration Tests

Integration tests use Playwright and require browser installation:

```bash
# After building the test project
cd src/Flowbite.Tests
pwsh bin/Debug/net9.0/playwright.ps1 install chromium
```

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
   - Run `python build.py test` to verify all tests pass
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
- All tests must pass (`python build.py test`)
- Demo pages for new components
- Unit tests for new components
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
