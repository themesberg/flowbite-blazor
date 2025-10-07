# GitHub Issue Resolution Workflow

## Overview
This workflow guides the resolution of GitHub issues for the Flowbite Blazor project, from initial analysis to runtime verification.

## Prerequisites
- Access to the GitHub repository
- Local development environment set up
- Ability to create branches and commits
- DemoApp project can be built and run

## Workflow Steps

### 1. Issue Analysis and Validation

**Input Required:** GitHub issue number

**GitHub MCP Tools to Use:**
- `get_issue` - Retrieve detailed issue information including description, labels, assignees
- `get_issue_comments` - Get all comments to understand discussion and context

**Actions:**
1. Use `get_issue` with owner, repo, and issue_number to fetch complete issue details
2. Use `get_issue_comments` to review all discussion and additional context
3. Analyze the problem description, steps to reproduce, and expected behavior
4. Determine if this is indeed a bug or enhancement request
5. Identify the affected components/files using local file exploration
6. Assess the scope and complexity of the required changes

**Decision Point:** 
- If NOT a valid bug/issue → Use `add_issue_comment` to document findings
- If valid issue → Proceed to step 2

### 2. Planning Phase (PLAN MODE)

**Local Tools to Use:**
- `read_file` - Examine affected component files
- `search_files` - Find related code patterns and implementations
- `list_code_definition_names` - Understand component structure
- `list_files` - Explore directory structures

**Actions:**
1. Switch to PLAN MODE for detailed analysis
2. Use `read_file` to examine the affected component files
3. Use `search_files` to find similar implementations or patterns
4. Use `list_code_definition_names` to understand component architecture
5. Create a detailed implementation plan including:
   - Files that need modification
   - Specific changes required
   - Testing approach
   - Potential risks or dependencies
6. Present plan to user for approval
7. Get user confirmation to proceed to implementation

### 3. Branch Creation

**Actions:**
1. Ensure you're on the `develop` branch: `git checkout develop`
2. Pull latest changes: `git pull origin develop`
3. Create a new branch using naming convention:
   - For bugs: `fix/issue-{issue-number}-{brief-description}`
   - For features: `feature/issue-{issue-number}-{brief-description}`
   - For enhancements: `enhancement/issue-{issue-number}-{brief-description}`
   
**Example:** `git checkout -b fix/issue-123-button-hover-state`

### 4. Implementation Phase (ACT MODE)

**Local Tools to Use:**
- `read_file` - Examine existing component files
- `write_to_file` - Create new files or completely rewrite existing ones
- `replace_in_file` - Make targeted edits to existing files
- `search_files` - Find code patterns during implementation
- `execute_command` - Run git commands, builds, and tests

**Actions:**
1. Use local file tools to examine and modify source code
2. Make incremental changes following the project's coding standards
3. Test changes locally as you implement them
4. Follow Blazor component development guidelines from `.clinerules/developer_guidelines.md`

**Commit Strategy:**
- Make logical, incremental commits using `execute_command` with git
- Use descriptive commit messages: `fix(component): resolve hover state issue for Button component`
- Ask for user confirmation before each commit
- Commit frequently to track progress

**Commit Message Format:**
```
{type}({scope}): {description}

Fixes #{issue-number}
```

Types: fix, feat, docs, style, refactor, test, chore

### 5. Build and Initial Testing

**Actions:**
1. Build the DemoApp project: 
   ```powershell
   cd "src/DemoApp"
   dotnet build
   ```
2. Resolve any build errors
3. Run the application locally (have the user perform this manually):
4. Perform initial smoke testing

### 6. Runtime Verification

**Critical Step:** This workflow is only complete when runtime verification confirms the issue is resolved.

**Manual Testing Required:** Since the testing environment is not fully automated, this step requires manual interaction with the running application.

**Actions:**
1. Navigate to the affected component/page in the running application
2. Reproduce the original issue steps manually
3. Verify the fix works as expected through manual testing
4. Test edge cases and related functionality manually
5. Check for any regressions by testing related components

**User Involvement:**
- **Required:** User must manually interact with the application to verify the fix
- Ask user to perform the original issue reproduction steps
- Get confirmation that the behavior now matches expectations
- Have user test related functionality to ensure no regressions
- Document any additional findings or edge cases discovered during testing

### 7. Final Commit and Documentation

**Actions:**
1. Make final commit with any remaining changes using local git commands
2. Update CHANGELOG.md using `write_to_file` or `replace_in_file` if significant change
3. Update component documentation using local file tools if needed
4. Ensure all files are properly formatted

### 8. Completion Criteria

The workflow is considered complete when:
- [ ] Issue has been analyzed and confirmed as valid
- [ ] Branch created from develop with proper naming
- [ ] Implementation completed following coding standards
- [ ] All changes committed with descriptive messages
- [ ] DemoApp builds successfully
- [ ] Runtime verification confirms issue is resolved
- [ ] No regressions introduced
- [ ] User or developer has confirmed the fix works

## Follow-up Actions (Post-Workflow)

**GitHub MCP Tools to Use:**
- `create_pull_request` - Create PR from your branch to develop
- `add_issue_comment` - Update the original issue with PR link and status
- `update_pull_request` - Add reviewers or update PR description (if needed)

**Actions:**
1. Use `create_pull_request` to create PR from your branch to develop
2. Use `add_issue_comment` to link the PR to the original issue and provide status update
3. Use `update_pull_request` to add reviewers if needed
4. Monitor PR status through GitHub interface
5. Merge after approval using GitHub interface

## Troubleshooting

**Build Failures:**
- Check for missing dependencies
- Verify all using statements are correct
- Ensure proper namespace usage

**Runtime Issues:**
- Check browser console for JavaScript errors
- Verify Blazor component lifecycle methods
- Test in different browsers if UI-related

**Git Issues:**
- If branch naming conflicts, use: `fix/issue-{number}-{description}-v2`
- For merge conflicts, resolve carefully and test thoroughly

## Notes
- Always work from the `develop` branch as the base
- Follow the 4-space indentation standard
- Use existing Flowbite Blazor components when possible
- Prefer C# code in `.cs` files over `.razor` files
- Always use `@key` directive when rendering lists
- Use icons from Flowbite.Icons or Flowbite.ExtendedIcons only

## GitHub MCP Server Configuration

**Required MCP Tools:**
- `get_issue` - Fetch issue details
- `get_issue_comments` - Get issue discussion
- `add_issue_comment` - Add comments to issues
- `create_pull_request` - Create pull requests
- `update_pull_request` - Update PR details

**Repository Information:**
- Owner: `themesberg`
- Repo: `flowbite-blazor`
- Base Branch: `develop`

## Workflow Execution Command
To start this workflow, provide the GitHub issue number and follow each step sequentially, asking for confirmation at commit points and verification steps. The workflow leverages GitHub MCP tools for issue and PR management while using local tools for all source code analysis and modifications.
