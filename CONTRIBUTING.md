# Contributing to Flowbite Blazor

Thank you for your interest in contributing to Flowbite Blazor! This guide will help you get started.

---

## Prerequisites

Before contributing, ensure you have these installed:

| Dependency | Version | Purpose |
|------------|---------|---------|
| **.NET SDK** | 9.0+ | Build and run the solution |
| **Python** | 3.8+ | Build automation (`build.py`) |
| **psutil** | any | Python package for process management |
| **Node.js** | 18+ | Tailwind CSS compilation (optional, standalone binary auto-downloaded) |

### Quick Setup

```bash
# Install Python dependency
pip install psutil

# Install Playwright browsers (for integration tests)
cd src/Flowbite.Tests
pwsh bin/Debug/net9.0/playwright.ps1 install chromium
```

> **Note:** Tailwind CSS standalone binary is auto-downloaded to `tools/` on first build.

---

## Development Setup

### 1. Clone and Build

```bash
git clone https://github.com/themesberg/flowbite-blazor.git
cd flowbite-blazor

# Build the solution
python build.py

# Start DemoApp
python build.py start
```

Open http://localhost:5290/ in your browser.

### 2. Development Commands

| Command | Description |
|---------|-------------|
| `python build.py` | Build the solution |
| `python build.py start` | Build and start DemoApp in background |
| `python build.py stop` | Stop background DemoApp |
| `python build.py watch` | Hot reload development |
| `python build.py test` | Run unit tests |
| `python build.py test-integration` | Run Playwright smoke tests |
| `python build.py test-all` | Run all tests (unit + integration) |
| `python build.py --help` | Show all commands |

---

## Contribution Workflow

### 1. Create a Branch

```bash
git checkout develop
git pull origin develop
git checkout -b <type>/issue-<id>-<description>
```

**Branch naming:**
- `feature/issue-42-add-datepicker`
- `fix/issue-17-button-disabled-state`
- `enhancement/issue-99-improve-tooltip`

### 2. Make Changes

Follow our coding standards in [`docs/developer_rules.md`](docs/developer_rules.md).

**Key requirements:**
- One component per file
- Use partial classes (`.razor` for markup, `.razor.cs` for logic)
- XML comments on all public APIs
- Support dark mode with `dark:` variants
- Include ARIA attributes for accessibility
- Add `motion-reduce:transition-none` to animated elements

### 3. Verify Your Changes

**This is mandatory before committing:**

```bash
# Build
python build.py

# Run all tests
python build.py test-all

# Start app and manually verify
python build.py start
# Open http://localhost:5290/ and test your changes
# Check both light and dark modes
```

> **Important:** A successful build does NOT equal working code. Always verify UI changes visually.

### 4. Commit with Proper Format

```bash
git add .
git commit -m "<type>(<scope>): <description>"
```

**Commit types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

**Examples:**
- `feat(datepicker): add DatePicker component`
- `fix(button): resolve disabled state styling`
- `docs(forms): update TextInput examples`
- `test(select): add ValueExpression tests`

Reference issues: `Fixes #42` or `Closes #17`

### 5. Push and Create PR

```bash
git push origin <branch-name>
```

Then create a Pull Request on GitHub targeting `develop`.

---

## Pull Request Requirements

Before your PR can be merged:

- [ ] All tests pass (`python build.py test-all`)
- [ ] No build warnings
- [ ] Changes verified visually in DemoApp
- [ ] Demo page added/updated for new components
- [ ] Unit tests added for new functionality
- [ ] Documentation updated
- [ ] Follows coding standards

---

## Testing

### Test Commands

```bash
# Unit tests only (fast, ~1s)
python build.py test

# Specific test class
python build.py test DebouncerTests

# Integration/smoke tests (auto-starts DemoApp)
python build.py test-integration

# All tests (recommended before PR)
python build.py test-all
```

### Writing Tests

Tests live in `src/Flowbite.Tests/`. See [`src/Flowbite.Tests/CLAUDE.md`](src/Flowbite.Tests/CLAUDE.md) for detailed patterns.

**Component test example:**

```csharp
using Flowbite.Components;
using Flowbite.Tests.TestSetup;

public class MyComponentTests : FlowbiteTestContext
{
    [Fact]
    public void MyComponent_RendersCorrectly()
    {
        var value = "";
        var cut = RenderComponent<MyComponent>(p => p
            .Add(x => x.Value, value)
            .Add(x => x.ValueExpression, () => value));

        cut.Find("input").Should().NotBeNull();
    }
}
```

---

## Project Structure

```
src/
├── Flowbite/              # Core component library
├── Flowbite.ExtendedIcons/# Optional icon packs
├── Flowbite.Tests/        # Unit and integration tests
└── DemoApp/               # Documentation playground
    └── Pages/Docs/        # Component demo pages

docs/
├── developer_rules.md     # Coding standards (READ THIS)
└── memory_aid/            # Lessons learned and gotchas
```

---

## Adding a New Component

1. **Create the component** in `src/Flowbite/Components/`
2. **Add a demo page** in `src/DemoApp/Pages/Docs/components/`
3. **Add sidebar entry** in `src/DemoApp/Layout/DocLayoutSidebarData.cs`
4. **Add unit tests** in `src/Flowbite.Tests/Components/`
5. **Add documentation** in `src/DemoApp/wwwroot/llms-docs/sections/`

---

## Git Workflow for Maintainers

When merging feature branches:

```bash
# ALWAYS use --no-ff to preserve feature history
git checkout develop
git merge --no-ff feature/issue-42-add-datepicker
```

> **Never fast-forward merge.** The `--no-ff` flag creates explicit merge commits that group related changes.

---

## Getting Help

- **Bugs:** [Create an issue](https://github.com/themesberg/flowbite-blazor/issues)
- **Questions:** [Start a discussion](https://github.com/themesberg/flowbite-blazor/discussions)
- **Discord:** [Join our community](https://discord.com/invite/4eeurUVvTy)

---

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
