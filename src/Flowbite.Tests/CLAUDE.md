# Flowbite.Tests - Testing Guidelines

> **Purpose:** This document provides AI guidance for developing and maintaining tests in the Flowbite Blazor test project.

## Overview

This project contains two categories of tests:
1. **Unit Tests (bUnit)** - Component and utility testing with mocked dependencies
2. **Integration Tests (Playwright)** - End-to-end browser testing against DemoApp

## Project Structure

```
src/Flowbite.Tests/
├── Flowbite.Tests.csproj          # Test project (net9.0 only)
├── CLAUDE.md                       # This file - AI guidance
├── GlobalUsings.cs                 # Common imports
├── TestSetup/
│   └── FlowbiteTestContext.cs     # Base test context with Flowbite services
├── Utilities/
│   ├── DebouncerTests.cs          # Debouncer class tests
│   └── ElementClassTests.cs       # ElementClass builder tests
├── Services/
│   └── TailwindMergeTests.cs      # TailwindMerge integration tests
├── Components/
│   ├── CollapseStateTests.cs      # CollapseState enum tests
│   └── Forms/
│       ├── TextInputTests.cs      # TextInput component tests
│       ├── TextAreaTests.cs       # Textarea component tests
│       └── SelectTests.cs         # Select component tests
└── Integration/
    ├── PlaywrightFixture.cs       # Shared Playwright setup/teardown
    └── DemoAppSmokeTests.cs       # Golden example E2E test
```

## Test Categories

### Unit Tests (bUnit)

**Location:** `Utilities/`, `Services/`, `Components/`

**Run with:**
```bash
python build.py test
# Or directly:
dotnet test src/Flowbite.Tests/ --filter "Category!=Integration"
```

**Base Class:** Use `FlowbiteTestContext` for component tests that need Flowbite services.

### Integration Tests (Playwright)

**Location:** `Integration/`

**Run with:**
```bash
python build.py test-integration
# Or manually:
python build.py start
dotnet test src/Flowbite.Tests/ --filter "Category=Integration"
python build.py stop
```

**Requirements:**
- DemoApp must be running at `http://localhost:5290`
- Playwright browsers must be installed (run `pwsh bin/Debug/net9.0/playwright.ps1 install` on first use)

## Test Patterns

### Component Test Pattern (bUnit)

```csharp
using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components;

public class MyComponentTests : FlowbiteTestContext
{
    [Fact]
    public void Component_Scenario_ExpectedResult()
    {
        // Arrange
        var cut = RenderComponent<MyComponent>(parameters => parameters
            .Add(p => p.SomeParam, "value")
            .Add(p => p.ChildContent, "<span>Child</span>"));

        // Act
        cut.Find("button").Click();

        // Assert
        cut.Find(".result").TextContent.Should().Be("expected");
    }

    [Fact]
    public void Component_WithBinding_UpdatesValue()
    {
        // Arrange
        var boundValue = "initial";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, boundValue)
            .Add(p => p.ValueChanged, newValue => boundValue = newValue));

        // Act
        cut.Find("input").Change("new value");

        // Assert
        boundValue.Should().Be("new value");
    }
}
```

### Utility Test Pattern (xUnit)

```csharp
using Flowbite.Utilities;

namespace Flowbite.Tests.Utilities;

public class MyUtilityTests
{
    [Fact]
    public void Method_Scenario_ExpectedResult()
    {
        // Arrange
        var utility = new MyUtility();

        // Act
        var result = utility.DoSomething("input");

        // Assert
        result.Should().Be("expected");
    }

    [Theory]
    [InlineData("input1", "expected1")]
    [InlineData("input2", "expected2")]
    public void Method_WithVariousInputs_ReturnsExpected(string input, string expected)
    {
        var utility = new MyUtility();
        utility.DoSomething(input).Should().Be(expected);
    }
}
```

### Playwright Integration Test Pattern

```csharp
using Microsoft.Playwright;
using Flowbite.Tests.Integration;

namespace Flowbite.Tests.Integration;

[Collection("Playwright")]
public class MyIntegrationTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public MyIntegrationTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Page_Action_ExpectedResult()
    {
        // Arrange
        var page = await _fixture.Browser.NewPageAsync();

        try
        {
            // Act
            await page.GotoAsync("http://localhost:5290/docs/components/button");

            // Assert
            await Assertions.Expect(page.Locator("h1")).ToHaveTextAsync("Button");

            // Optional: Screenshot for debugging
            await page.ScreenshotAsync(new() { Path = "screenshots/button-page.png" });
        }
        finally
        {
            await page.CloseAsync();
        }
    }
}
```

## Running Tests

### All Unit Tests
```bash
python build.py test
```

### Specific Test Class
```bash
python build.py test --filter "DebouncerTests"
```

### Specific Test Method
```bash
python build.py test --filter "DebouncerTests.Debounce_FiresOnceAfterDelay"
```

### Integration Tests Only
```bash
# Requires DemoApp to be running
python build.py start
python build.py test-integration
python build.py stop
```

### With Verbose Output
```bash
dotnet test src/Flowbite.Tests/ -v detailed
```

## Adding New Tests

### 1. Unit Tests for Utilities/Services

Add to the appropriate folder under `Utilities/` or `Services/`:

```csharp
namespace Flowbite.Tests.Utilities;

public class NewUtilityTests
{
    [Fact]
    public void Method_Scenario_ExpectedResult()
    {
        // Test implementation
    }
}
```

### 2. Unit Tests for Components

Add to `Components/` or create a subfolder for component categories:

```csharp
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components;

public class NewComponentTests : FlowbiteTestContext
{
    [Fact]
    public void Component_Scenario_ExpectedResult()
    {
        // Test implementation using RenderComponent<T>
    }
}
```

### 3. Integration Tests

Add to `Integration/` folder with the `[Trait("Category", "Integration")]` attribute:

```csharp
namespace Flowbite.Tests.Integration;

[Collection("Playwright")]
public class NewPageTests : IClassFixture<PlaywrightFixture>
{
    // Implementation
}
```

## JSInterop Mocking

bUnit mocks JSInterop by default. Configure based on your needs:

### Loose Mode (Ignore Unmocked Calls)
```csharp
public class MyTests : FlowbiteTestContext
{
    public MyTests()
    {
        // Already set in FlowbiteTestContext, but can be overridden
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
}
```

### Mock Specific Calls
```csharp
[Fact]
public void Component_WithJSInterop_MocksCorrectly()
{
    // Setup mock before rendering
    JSInterop.Setup<bool>("Flowbite.copyToClipboard", "test content")
        .SetResult(true);

    var cut = RenderComponent<CopyButton>(p => p
        .Add(c => c.Content, "test content"));

    cut.Find("button").Click();

    // Verify the call was made
    JSInterop.VerifyInvoke("Flowbite.copyToClipboard");
}
```

### Mock Module Imports
```csharp
[Fact]
public void Component_WithModuleImport_MocksCorrectly()
{
    var moduleInterop = JSInterop.SetupModule("./_content/Flowbite/js/clipboard.js");
    moduleInterop.Setup<bool>("copyToClipboard", _ => true).SetResult(true);

    // Render and test component
}
```

## Assertions

Use FluentAssertions for readable test assertions:

### Basic Assertions
```csharp
result.Should().Be("expected");
result.Should().NotBeNull();
result.Should().BeEmpty();
result.Should().Contain("substring");
```

### Collection Assertions
```csharp
list.Should().HaveCount(3);
list.Should().Contain("item");
list.Should().BeInAscendingOrder();
```

### Object Assertions
```csharp
component.Instance.IsVisible.Should().BeTrue();
component.Instance.Value.Should().Be("expected");
```

### Exception Assertions
```csharp
action.Should().Throw<ArgumentNullException>()
    .WithMessage("*parameter*");
```

### Markup Assertions (bUnit)
```csharp
cut.Markup.Should().Contain("expected-class");
cut.Find("input").GetAttribute("disabled").Should().Be("");
cut.FindAll("li").Should().HaveCount(3);
```

## Common Issues and Solutions

### Issue: Component Not Rendering

**Problem:** `RenderComponent<T>` throws or returns empty markup.

**Solution:** Ensure services are registered in `FlowbiteTestContext`:
```csharp
public FlowbiteTestContext()
{
    Services.AddFlowbite();
    JSInterop.Mode = JSRuntimeMode.Loose;
}
```

### Issue: JSInterop Not Found

**Problem:** Test fails with "JavaScript interop calls cannot be issued..."

**Solution:** Set loose mode or mock the specific call:
```csharp
JSInterop.Mode = JSRuntimeMode.Loose;
// or
JSInterop.Setup<ReturnType>("methodName", args).SetResult(value);
```

### Issue: Playwright Browsers Not Installed

**Problem:** Integration tests fail with browser not found error.

**Solution:** Install Playwright browsers:
```bash
pwsh bin/Debug/net9.0/playwright.ps1 install
```

### Issue: DemoApp Not Running

**Problem:** Integration tests timeout or can't connect.

**Solution:** Start DemoApp before running integration tests:
```bash
python build.py start
# Wait for startup
python build.py test-integration
```

## Test Naming Convention

Follow the pattern: `Method_Scenario_ExpectedResult`

Examples:
- `Debounce_FiresOnceAfterDelay`
- `TextInput_RendersWithDefaultAttributes`
- `MergeClasses_ResolvesConflicts`
- `HomePage_LoadsSuccessfully`

## Code Coverage

Generate coverage report:
```bash
dotnet test src/Flowbite.Tests/ --collect:"XPlat Code Coverage"
```

View coverage in Visual Studio or use ReportGenerator:
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:coverage.xml -targetdir:coveragereport
```
