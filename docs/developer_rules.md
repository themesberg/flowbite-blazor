# Developer Rules and Guidelines

> **Related Document:** See [`memory_aid.md`](./memory_aid.md) for lessons learned and gotchas discovered through development.

## Project Structure Guidelines

### Solution Structure
```
FlowbiteBlazor.sln
├── src/
│   └── TBD # need help filing in
```

## Coding Standards

### C# Conventions
- Use C# 12 features (collection expressions, primary constructors, etc.)
- Follow Microsoft's C# Coding Conventions
- Prefer Component.razor and Component.razor.cs over Component.razor (all in one file)
- Use `nullable` reference types throughout
- Prefer `async`/`await` for all I/O operations
- Use dependency injection for all services

### Naming Conventions
- **Services**: `I{Name}Service` interface, `{Name}Service` implementation
- **Models**: PascalCase, singular (e.g., `RiddleSession`, `Character`)
- **SignalR Hubs**: Suffix with `Hub` (e.g., `GameHub`)
- **Tools**: Suffix with `Tool` (e.g., `GetGameStateTool`)
- **Razor Components**: PascalCase matching filename
- **Methods**: Async methods should end with `Async`


## Documentation Requirements

- Add XML comments to public APIs
- Maintain README.md with setup instructions

## Testing Guidelines

- No automated test project ships yet. Before submitting, smoke-test DemoApp using `python build.py start` and playwright MCP.

## Git Workflow

- Branch from `develop`: `git checkout develop && git pull origin develop`.
- Naming: `fix/issue-{id}-description`, `feature/issue-{id}-description`, or `enhancement/issue-{id}-description`.
- Commit format: `{type}({scope}): {description}` (types: fix, feat, docs, style, refactor, test, chore). Reference issues with `Fixes #{number}` when applicable.
- Ensure `src/WebApp/wwwroot/css/app.min.css` is committed if it has changed. It auto-generate by the WebApp.csproj build instructions.


## Verification Checklist Discipline

- **NEVER** ask for push/merge approval until ALL verification checklist items are `[x]`
- Before asking "Ready to push?", first `read_file` the verification checklist
- If any item shows `[ ]`, complete that step first (e.g., runtime testing, UI verification)
- The commit is NOT the completion milestone - the full checklist is
