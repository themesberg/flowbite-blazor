# Incremental Phase Implementation Workflow

<purpose>
This workflow provides a repeatable process for implementing Flowbite Blazor component library phases incrementally. It emphasizes small batches, validation through the DemoApp, and explicit user approval before any git push or merge operations.
</purpose>

---

## No Task Specified Protocol

<no_task_protocol>
**When the user does not specify a task** (e.g., empty message, "continue", "what's next?"), Claude must proactively assess the project state and propose next steps.

### Step 1: Assess Current State

1. **Check Git Status**
   ```bash
   git status && git branch -a && git --no-pager log --oneline -10
   ```

2. **Identify Current Phase and Version**
   - Read `CHANGELOG.md` for current version
   - Read `src/Flowbite/Flowbite.csproj` for `<Version>` tag
   - Identify what phase the project is in based on recent commits

3. **Check for Existing Implementation Plans**
   - Look for `docs/plans/00-INDEX.md` for phase overview
   - Read the phase documents (`docs/plans/0{N}-PHASE-*.md`)
   - Identify current task status

4. **Check for Incomplete Work**
   - Review `docs/verification/` for any incomplete checklists
   - Check for uncommitted changes or unmerged feature branches

### Step 2: Determine Next Action

Based on the assessment, recommend ONE of the following:

| State | Recommended Action |
|-------|-------------------|
| Uncommitted changes exist | Complete current work, commit, await approval |
| Feature branch exists but not merged | Complete task, await approval for merge |
| Phase plan exists with incomplete tasks | Propose starting next task |
| Current phase complete, no next phase plan | Propose creating next phase implementation plan |
| All phases complete | Propose maintenance, enhancements, or new phase planning |

### Step 3: Present Proposal to User

Format the proposal clearly:

```markdown
## Project State Assessment

**Current Version:** {version}
**Current Branch:** {branch}
**Last Commit:** {hash} - {message}

## Recommendation

{One of the following:}

**Option A: Continue Current Work**
- Task: {task description}
- Status: {status}
- Next Steps: {specific steps}

**Option B: Start New Task**
- Phase {N} Task {M}: {title}
- Dependencies: {met/unmet}
- Estimated effort: {S/M/L}

**Option C: Create Phase {N+1} Implementation Plan**
- Current phase status: Complete
- Suggested next phase focus: {area}
- Would you like me to draft the implementation plan?
```

### Step 4: Phase Plan Development (Simulated PLAN Mode)

If the next step is creating a new phase implementation plan, follow this process:

1. **Gather Context**
   - Read `docs/plans/00-INDEX.md` (master plan)
   - Read completed phase plans in `docs/plans/` for patterns
   - Review current codebase structure in `src/Flowbite/`

2. **Draft the Phase Plan**

   Create `docs/plans/0{N}-PHASE-{NAME}.md` with:
   - Overview and objectives
   - Tasks with acceptance criteria
   - Files to create/modify
   - Code examples
   - Testing approach
   - Risks and mitigations
   - Definition of done

3. **Present Draft for Review**

   Show the user:
   - Summary of proposed tasks
   - Key technical decisions
   - Estimated effort
   - Request: "Would you like me to create this implementation plan?"

4. **Await Approval Before Creating File**

   Only after user approves:
   - Create the `docs/plans/0{N}-PHASE-{NAME}.md` file
   - Update `docs/plans/00-INDEX.md` with link to new phase
   - Commit with message: `docs(planning): add phase {N} implementation plan`

### Example: No Task Response

```
I notice no specific task was provided. Let me assess the project state...

## Project State Assessment

**Current Version:** 0.1.3-beta
**Current Branch:** develop
**Last Commit:** af071ce - docs: remove detailed project documentation

**Phase 1 Status:** Not Started
- Foundation & Tooling tasks pending

## Recommendation

The Phase 1 foundation work has not yet started. I recommend beginning with Task 1.1: TailwindMerge.NET Integration.

**Phase 1 Focus Areas (from implementation plan):**
- TailwindMerge.NET integration
- Base class enhancements
- ElementClass builder
- Motion-reduce accessibility

Would you like me to:
1. **Start Task 1.1** - TailwindMerge.NET Integration
2. **Review the Phase 1 plan** - Discuss approach before starting
3. **Something else** - Please specify

Please confirm your preference.
```
</no_task_protocol>

---

## Prerequisites

Before starting any task:

1. **Verify Repository State**
   ```bash
   git status
   git branch -a
   git log --oneline -5
   ```

2. **Confirm Build Passes**
   ```bash
   python build.py
   ```

3. **Review Current Phase Plan**
   - Read `docs/plans/0{N}-PHASE-*.md` for the target phase
   - Identify the target task and its dependencies

---

## Phase Start Procedure (One-Time per Phase)

<phase_initialization>
1. Ensure `develop` branch is current:
   ```bash
   git checkout develop
   git pull origin develop
   ```

2. Read the implementation plan:
   - `read_file` on `docs/plans/00-INDEX.md`
   - `read_file` on `docs/plans/0{N}-PHASE-*.md` for current phase

3. Review project rules:
   - `read_file` on `.clinerules/AGENTS.md`

4. Create phase verification directory if not exists:
   - `docs/verification/phase{N}/`
</phase_initialization>

---

## Task Implementation Cycle

<task_cycle>
Repeat this cycle for each task within a phase. Complete the entire implementation and address 100% of the acceptance criteira without stopping. If you need to continue, do so automatically without waiting for confirmation.

### Step 1: Create Feature Branch

```bash
git checkout develop
git pull origin develop
git checkout -b feature/phase{N}-task{M}-{short-description}
```

**Example:** `feature/phase1-task1-tailwindmerge`

### Step 2: Context Recovery (CRITICAL for LLM Implementers)

<context_recovery>
**ASSUME NO PRIOR MEMORY.** Every conversation is a fresh start.

1. **Read Phase Plan**
   ```
   read_file docs/plans/0{N}-PHASE-*.md
   ```

2. **Read Component Reference**

   For component work, understand the existing patterns:
   - Read `src/Flowbite/Base/FlowbiteComponentBase.cs` for base patterns
   - Read existing similar components for API conventions
   - Check `src/DemoApp/Pages/Docs/components/` for demo patterns

3. **Read Relevant Source Files**
   - Use `list_files` to understand current structure
   - Use `read_file` on files you plan to modify
   - Use `search_files` to find existing patterns

4. **Verify Current State via Git**
   ```bash
   git status
   git log --oneline -10
   git diff develop --stat
   ```

5. **Run Build**
   ```bash
   python build.py
   ```
</context_recovery>

### Step 3: Implementation (Small Batches)

<implementation_steps>
For each file change:

1. **Read Current State**
   ```
   read_file {path/to/file}
   ```

2. **Make Focused Change**
   - Use `replace_in_file` for targeted edits
   - Use `write_to_file` for new files or complete rewrites

3. **Build Immediately**
   ```bash
   python build.py
   ```

4. **Verify Change**
   ```bash
   git diff {path/to/file}
   ```

5. **Stage If Successful**
   ```bash
   git add {path/to/file}
   ```

**RULE:** Never chain multiple file changes without building between each.
</implementation_steps>

### Step 4: Functional Verification

<verification>
1. **Start DemoApp**
   ```bash
   python build.py start
   ```

2. **Test Functionality**
   - Use Playwright MCP for browser-based verification
   - Navigate to relevant demo page: `http://localhost:5290/docs/components/{component}`
   - Use `browser_snapshot` to capture state
   - Check both light and dark themes

3. **Review Logs**
   ```bash
   python build.py log
   python build.py log --level error
   ```

4. **Stop DemoApp**
   ```bash
   python build.py stop
   ```
</verification>

### Step 5: Commit (Atomic, Descriptive)

<commit_protocol>
1. **Review All Changes**
   ```bash
   git status
   git diff --cached
   ```

2. **Commit with Descriptive Message**
   ```bash
   git commit -m "{type}({scope}): {description}

   - {bullet point 1}
   - {bullet point 2}

   Task: Phase {N} Task {M}"
   ```

   **Types:** `feat`, `fix`, `docs`, `refactor`, `test`, `chore`
   **Scopes:** `component`, `base`, `service`, `demo`, `build`

3. **Present Commit to User for Review**
   - Show `git log --oneline -1`
   - Show `git diff develop --stat`
</commit_protocol>

### Step 6: Update Verification Checklist

<checklist_update>
Create or update the task-specific verification checklist:

**File:** `docs/verification/phase{N}/task{M}-checklist.md`

```markdown
# Phase {N} Task {M}: {Title}

**Branch:** `feature/phase{N}-task{M}-{desc}`
**Started:** {date}
**Status:** Not Started | In Progress | Complete | Blocked

## Task Description
{Brief description from the phase implementation plan}

## Acceptance Criteria

> Copy directly from the phase implementation plan

- [ ] Criterion 1
- [ ] Criterion 2
- [ ] Criterion 3

## Files Modified
- `path/to/file1.cs` - {what changed}
- `path/to/file2.razor` - {what changed}

## Tests Performed
- [ ] Build passes (`python build.py`)
- [ ] DemoApp starts (`python build.py start`)
- [ ] Functional test: {description}
- [ ] Light theme verified
- [ ] Dark theme verified
- [ ] No console errors in browser

## Commits
| Hash | Message |
|------|---------|
| | |

## Issues Encountered
| Issue | Resolution |
|-------|------------|
| | |

## Approvals
- [ ] Changes reviewed by user
- [ ] Flowbite and/or DemoApp csproj versions updated AND CHANGELOG.md file(s) updated
- [ ] Approved for push to origin
- [ ] DemoApp stopped before merge
- [ ] Merged to develop
- [ ] Feature branch deleted
```
</checklist_update>

Complete the entire implementation and address 100% of the acceptance criteira without stopping. If you need to continue, do so automatically without waiting for confirmation.

</task_cycle>

### Step 7: Await User Approval

<approval_gate>
**STOP AND WAIT FOR EXPLICIT USER APPROVAL**

**CRITICAL: Before asking for approval, verify checklist is complete:**
```
read_file docs/verification/phase{N}/task{M}-checklist.md
```
- **ALL** acceptance criteria must be `[x]`
- **ALL** verification steps must be `[x]`
- If ANY item shows `[ ]`, complete it FIRST before asking for approval
- The commit is NOT the completion milestone - the full checklist is
- If breaking changes occurred: Verify `docs/MIGRATION.md` is updated
- **REMINDER:** After merge completes, Step 9 (version bump + changelog) is REQUIRED

Present to user:
1. Summary of changes made
2. Verification checklist status (must show ALL items `[x]`)
3. Git diff summary
4. Request: "Ready to push to origin? Please confirm."

**DO NOT** execute `git push` or `git merge` without user confirmation.
</approval_gate>

### Step 8: Push and Merge (User-Initiated)

<push_merge>
Only after user approval:

0. **Stop the Running DemoApp**
   ```bash
   python build.py stop
   ```

1. **Push Feature Branch**
   ```bash
   git push origin feature/phase{N}-task{M}-{desc}
   ```

2. **Merge to Develop with --no-ff** (if approved)
   ```bash
   git checkout develop
   git merge --no-ff feature/phase{N}-task{M}-{desc}
   git push origin develop
   ```

   **CRITICAL: ALWAYS use `--no-ff` flag when merging feature branches!**
   - ❌ **NEVER:** `git merge feature/branch` (fast-forward merge loses feature context)
   - ✅ **ALWAYS:** `git merge --no-ff feature/branch` (creates merge commit)
   - **Why:** No-ff merges create explicit merge commits that:
     - Group related changes together
     - Make feature boundaries visible in history
     - Enable reverting entire features with single revert
     - Preserve who merged and when
   - **Non-negotiable:** This is a hard requirement for ALL feature branches

3. **Delete Feature Branch**
   ```bash
   git branch -d feature/phase{N}-task{M}-{desc}
   git push origin --delete feature/phase{N}-task{M}-{desc}
   ```
</push_merge>

### Step 9: Update Version and Changelog (After Task Completion)

<versioning>
After completing a task or phase:

1. **Increment Version in csproj**

   Edit `src/Flowbite/Flowbite.csproj`:
   ```xml
   <Version>0.2.0-beta</Version>
   ```

   Version increment rules:
   - **MINOR** (0.1.x → 0.2.0): New feature or phase completed
   - **PATCH** (0.1.0 → 0.1.1): Bug fix
   - **MAJOR** (0.x.x → 1.0.0): Breaking changes or stable release

2. **Update CHANGELOG.md**

   Move items from `[Unreleased]` to new version section:
   ```markdown
   ## [Unreleased]

   ## [0.2.0-beta] - YYYY-MM-DD
   ### Added
   - TailwindMerge.NET integration for class conflict resolution

   ### Changed
   - Base class now includes Style parameter
   ```

   Categories: Added, Changed, Deprecated, Removed, Fixed, Security

3. **Commit Version Bump**
   ```bash
   git add src/Flowbite/Flowbite.csproj CHANGELOG.md
   git commit -m "chore(release): bump version to 0.2.0-beta"
   git push origin develop
   ```

4. **Create Git Tag (Optional, for releases)**
   ```bash
   git tag -a v0.2.0-beta -m "Version 0.2.0-beta"
   git push origin v0.2.0-beta
   ```
</versioning>

---

## Existing Code Recognition Protocol

<existing_code_protocol>
When beginning work on a task, you may discover that code in the repository already implements some or all of the acceptance criteria, even though the progress tracking doesn't reflect completion.

### Step 1: Detect Code-vs-Progress Discrepancy

Signs of existing implementation:
- Files exist that match the task's "Files to Create/Modify" list
- Code patterns match what the task describes
- Components already contain the required functionality
- Feature works when tested, despite checklist showing incomplete

### Step 2: Validate Existing Implementation

Before assuming existing code is correct:

1. **Read the acceptance criteria** from the phase implementation plan
2. **Read the existing code** using `read_file`
3. **Compare functionality** - does the code satisfy each criterion?
4. **Test the component** using `python build.py start` and Playwright MCP
5. **Check both themes** - light and dark mode verification

### Step 3: Honor Valid Implementations

**IF the existing code satisfies acceptance criteria:**

1. **DO NOT re-implement** - trust the existing code
2. **Update the verification checklist** to reflect actual state:
   - Mark completed items as `[x]`
   - Document files that were already modified
   - Note "Implementation found in existing codebase" in commits section
3. **Run validation steps** to confirm (build, start, test)
4. **Proceed to approval gate** as if you completed the work

**IF the existing code is incomplete or incorrect:**

1. **Document what exists** in the verification checklist
2. **Identify gaps** between current state and acceptance criteria
3. **Make targeted fixes** rather than full re-implementation
4. **Preserve working code** - use surgical `replace_in_file` edits

### Rationale

- Previous sessions may have implemented features before checklist updates
- Manual development outside Claude may have occurred
- Avoiding redundant work saves time and prevents regression bugs
- Existing patterns should be preserved for consistency
</existing_code_protocol>

---

## LLM Implementer Guidelines

<llm_guidelines>
### Memory Rules
- **NEVER** assume you know file contents from previous conversations
- **ALWAYS** `read_file` before using `replace_in_file`
- **ALWAYS** verify changes with `git diff` after editing
- **ALWAYS** read component files before referencing properties in Razor/UI code

### Build Discipline
- Run `python build.py` after EVERY file modification
- If build fails, fix immediately before proceeding
- Never claim completion without successful build
- Build errors often reveal API mismatches - read error messages carefully

### Component Development Patterns
- Follow the two-file pattern: `.razor` for markup, `.razor.cs` for logic
- Use `[Parameter]` for public properties
- Use `[CaptureUnmatchedValues]` for AdditionalAttributes
- Prefer enums for style variations (Size, Color, Variant)
- Always support `Class` parameter for consumer customization
- Add `dark:` variants for all color classes

### Testing Requirements
- Create demo pages for new components in `src/DemoApp/Pages/Docs/components/`
- Use Playwright MCP for UI verification:
  - `browser_navigate` to load pages
  - `browser_snapshot` to capture state and get element refs
  - `browser_click` with refs from snapshot
  - **NOTE:** Refs change after page updates - always take new snapshot before clicking
- Check both light and dark themes
- Check `demoapp.log` for runtime errors

### Git Usage
- Use `git status` frequently to understand state
- Use `git diff` to verify changes match intent
- Use `git log` to understand recent history
- Never `git push` without user approval

### Documentation
- Update `.clinerules/AGENTS.md` with lessons learned after each task
- Keep verification checklists current
- Record blockers immediately when encountered
- Add XML comments to all public APIs
- **When APIs change:** Update `src/DemoApp/wwwroot/llms-docs/sections/` files
- **After llms-docs changes:** Build regenerates `llms-ctx.md` automatically

### When Stuck
1. Use `git log` and `git diff` to understand what changed
2. Use `search_files` to find similar patterns in codebase
3. Read error messages carefully - they often contain the solution
4. Check component files for actual property names (don't assume)
5. Surface findings to user rather than guessing blindly
</llm_guidelines>

---

## Git Commands Reference

<git_reference>
### Understanding State
```bash
# Current branch and status
git status

# Recent commits
git log --oneline -10

# Changes since develop
git diff develop

# Changes in staging
git diff --cached

# File history
git log --oneline -10 -- {path/to/file}
```

### Debugging Issues
```bash
# What changed in a specific commit
git show {commit-hash}

# Find commits that touched a file
git log --oneline -- {path/to/file}

# Compare branches
git diff develop..feature/branch-name

# Find when a line was added
git blame {path/to/file}
```

### Recovery
```bash
# Discard unstaged changes to a file
git checkout -- {path/to/file}

# Unstage a file (keep changes)
git reset HEAD {path/to/file}

# Reset to last commit (careful!)
git reset --hard HEAD

# Reset to develop (lose all branch changes!)
git reset --hard develop
```
</git_reference>

---

## Verification Checklist Template

<template>
Copy this template when starting a new task:

```markdown
# Phase {N} Task {M}: {Title}

**Branch:** `feature/phase{N}-task{M}-{short-desc}`
**Started:** {date}
**Status:** Not Started | In Progress | Complete | Blocked

## Task Description
{Brief description of what this task accomplishes}

## Acceptance Criteria
- [ ] {Criterion 1}
- [ ] {Criterion 2}
- [ ] {Criterion 3}

## Dependencies
- {Task X must be complete}
- {NuGet package required}

## Implementation Steps
- [ ] Step 1: {description}
- [ ] Step 2: {description}
- [ ] Step 3: {description}

## Files to Modify
| File | Change Type | Description |
|------|-------------|-------------|
| `path/to/file` | New/Modify | {what} |

## Verification Steps
- [ ] `python build.py` passes
- [ ] `python build.py start` runs without errors
- [ ] Component renders correctly in DemoApp
- [ ] Light theme verified
- [ ] Dark theme verified
- [ ] No console errors in browser
- [ ] `demoapp.log` shows no errors

## Commits
| Hash | Message |
|------|---------|
| | |

## Issues Encountered
| Issue | Resolution |
|-------|------------|
| | |

## Approvals
- [ ] Changes reviewed by user
- [ ] Approved for push to origin
- [ ] DemoApp stopped
- [ ] Merged to develop
- [ ] Feature branch deleted
```
</template>

---

## Quick Reference

<quick_reference>
### Start Task
```bash
git checkout develop && git pull origin develop
git checkout -b feature/phase{N}-task{M}-{desc}
```

### Implementation Loop
```
read_file -> edit -> python build.py -> git diff -> git add
```

### Verify
```bash
python build.py start
# Test with Playwright MCP at http://localhost:5290
python build.py stop
```

### Commit
```bash
git status
git diff --cached
git commit -m "type(scope): description"
```

### Await Approval -> Push
```bash
# Only after user says "approved"
git push origin feature/phase{N}-task{M}-{desc}
```
</quick_reference>

---

## Common Pitfalls and Solutions

<common_pitfalls>
### Flowbite Blazor Component Patterns

| Issue | Wrong | Correct |
|-------|-------|---------|
| Missing dark mode | `bg-white` | `bg-white dark:bg-gray-800` |
| String concatenation | `Class + " px-4"` | `MergeClasses(baseClasses, Class)` |
| Missing accessibility | No motion-reduce | Add `motion-reduce:transition-none` |

### Blazor Context Conflicts
When EditForm is inside AuthorizeView, both use "context" parameter:
```razor
<!-- WRONG: context name collision -->
<AuthorizeView>
    <Authorized>
        <EditForm Model="@model">

<!-- CORRECT: disambiguate context -->
<AuthorizeView>
    <Authorized>
        <EditForm Model="@model" Context="editContext">
```

### Component Parameter Patterns
```csharp
// Standard component setup
public partial class MyComponent : FlowbiteComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;
    [Parameter] public ButtonColor Color { get; set; } = ButtonColor.Primary;

    // Already inherited from FlowbiteComponentBase:
    // [Parameter] public string? Class { get; set; }
    // [Parameter(CaptureUnmatchedValues = true)]
    // public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
```

### Playwright MCP Ref Invalidation
Element refs change after page updates:
```
# After clicking, page state changes
# Old refs become invalid
# ALWAYS take new snapshot before next click
browser_snapshot -> browser_click -> browser_snapshot -> browser_click
```

### Service Registration
```csharp
// Register Flowbite services in Program.cs
builder.Services.AddFlowbite();
```

### CSS Commits
**CRITICAL:** When Tailwind classes change, commit the generated CSS:
```bash
git add src/Flowbite/wwwroot/flowbite.min.css
git add src/DemoApp/wwwroot/css/app.min.css
```

### Base Class Parameter Conflicts
When adding parameters to `FlowbiteComponentBase`, check for conflicts:
```csharp
// If adding Style to base class, search for existing Style properties:
// grep -r "public.*Style" src/Flowbite/Components/

// Common conflicts:
// - Button.Style (enum) → rename to Button.Variant
// - Tooltip.Style (string) → rename to Tooltip.Theme

// Also remove duplicate AdditionalAttributes from derived classes:
// grep -r "AdditionalAttributes" src/Flowbite/Components/
```

### Parameter/Enum Naming Consistency
When renaming parameters, also rename associated enums for consistency:

| Parameter Rename | Also Rename Enum |
|------------------|------------------|
| `Style` → `Variant` | `ButtonStyle` → `ButtonVariant` |
| `Style` → `Theme` | (string, no enum change needed) |

This ensures API consistency: `Variant="ButtonVariant.Outline"` instead of `Variant="ButtonStyle.Outline"`.
</common_pitfalls>

---

## Breaking Changes Protocol

<breaking_changes>
When a task requires breaking changes to existing APIs:

### Step 1: Document in MIGRATION.md
Update `docs/MIGRATION.md` with:
- Version number
- What changed (old → new)
- Before/after code examples
- Find & replace instructions

### Step 2: Update All Affected Files
- Component source files
- DemoApp pages in `src/DemoApp/Pages/Docs/components/`
- AI docs in `src/DemoApp/wwwroot/llms-docs/sections/`
- Regenerated `llms-ctx.md` (automatic on build)

### Step 3: Changelog Entry
Mark breaking changes with `**BREAKING:**` prefix:
```markdown
### Changed
- **BREAKING:** Rename `Button.Style` to `Button.Variant`
- **BREAKING:** Rename `ButtonStyle` enum to `ButtonVariant`
```

### Step 4: Consider Version Impact
Breaking changes typically warrant a MINOR version bump (0.1.x → 0.2.0).
</breaking_changes>
