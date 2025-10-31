<task name="Migrate Flowbite Svelte Component to Blazor">

<task_objective>
Guide the migration of a Flowbite Svelte component to Flowbite Blazor by analyzing the Svelte implementation, designing a Blazor-idiomatic approach, implementing the component with proper structure, creating demo pages, and verifying visual parity using Playwright MCP. The output will be a fully functional Blazor component with documentation, demo pages, and verification screenshots.
</task_objective>

<detailed_sequence_steps>

# Migrate Flowbite Svelte Component to Blazor - Detailed Sequence of Steps

## 1. Component Selection and Initial Analysis

1. Use the `ask_followup_question` tool to ask the user which Flowbite Svelte component they want to migrate.
   - Provide examples: "Button", "Modal", "Dropdown", "Accordion", "Tabs", etc.
   - Ask if they know the component category (e.g., Forms, Typography, Navigation)

2. Determine the Svelte source paths:
   - Component source: `C:\Users\tschavey\projects\themesberg\flowbite-svelte\src\lib\{category}\{component-name}\`
   - Example usage: `C:\Users\tschavey\projects\themesberg\flowbite-svelte\src\routes\docs-examples\{category}\{component-name}\`
   - Documentation URL: `https://flowbite-svelte.com/docs/{category}/{component-name}`

3. Use the `list_files` tool to explore the Svelte component directory structure.
   - List files in the component source directory
   - Identify key files: component file (.svelte), theme file (theme.ts), index file (index.ts)

4. Use the `read_file` tool to read the main Svelte component file.
   - Analyze component structure
   - Identify props/parameters
   - Note reactive statements and derived values
   - Document event handlers

5. Use the `read_file` tool to read the theme file if it exists.
   - Analyze variant definitions
   - Document base classes
   - Note default values
   - Identify all styling options

6. Use the `list_files` tool to list example files in the Svelte docs-examples directory.
   - Identify all example variations
   - Note which examples are most important

7. Use the `read_file` tool to read 2-3 key example files.
   - Understand typical usage patterns
   - Note common parameter combinations
   - Identify dependencies on other components

## 2. Design Blazor Architecture

1. Create a design document analyzing the Svelte approach:
   - Document in `<svelte_approach>` tags:
     * Component structure and organization
     * Props/parameters and their types
     * Variant system (if using tailwind-variants)
     * Event handling patterns
     * Child component relationships
     * Special features or behaviors

2. Design the Blazor implementation approach:
   - Document in `<blazor_approach>` tags:
     * How to translate Svelte props to Blazor parameters
     * Enum-based variant system design
     * Helper class structure for CSS generation
     * Component file organization (.razor and .razor.cs)
     * Event callback patterns
     * Child content handling with RenderFragment

3. Identify required enumerations:
   - List all enum types needed (e.g., Size, Color, Variant, Position)
   - Define enum values based on Svelte variants
   - Plan enum naming conventions (PascalCase, handle numbers like XXL)

4. Plan component parameter structure:
   - Required vs optional parameters
   - Default values
   - Parameter types (enums, bool, string, RenderFragment)
   - AdditionalAttributes support

5. Use the `ask_followup_question` tool to confirm the design approach with the user.
   - Present the high-level architecture
   - Ask if there are any specific requirements or constraints
   - Confirm breaking changes are acceptable if needed

## 3. Create Implementation Plan and Task Files

1. Determine the component category and create appropriate directory structure:
   - For general components: `src/Flowbite/Components/{ComponentName}/`
   - For specialized categories: `src/Flowbite/Components/{Category}/{ComponentName}/`
   - Enums: Follow C# conventions:
     * Component-specific enums: Define in component's `.razor.cs` file
     * Shared enums: Create separate files at `src/Flowbite/Components/{EnumName}.cs`
     * Multiple related enums: Group in `src/Flowbite/Components/{ComponentName}Enums.cs`

2. Create feature documentation directory:
   - Check if `docs/feature/{component-name}/` exists, create if needed
   - Plan to create `plan.md` and `tasks.md` files

3. Generate comprehensive implementation plan:
   - Use the `write_to_file` tool to create `docs/feature/{component-name}/plan.md`
   - Include sections:
     * Overview
     * Analysis (Svelte Approach, Blazor Approach)
     * Project Structure
     * Component Specifications
     * Enumeration Definitions
     * Helper Class Design
     * Verification Strategy with Playwright MCP
     * Implementation Phases
     * Success Criteria

4. Generate detailed task checklist:
   - Use the `write_to_file` tool to create `docs/feature/{component-name}/tasks.md`
   - Organize tasks by phases:
     * Phase 1: Foundation & Core Components
     * Phase 2: Demo Pages
     * Phase 3: Documentation
     * Phase 4: Verification with Playwright MCP
     * Phase 5: Final Review
   - Each task should include:
     * Task number and description
     * Acceptance criteria
     * Dependencies
     * Commands or code snippets where applicable

5. Use the `ask_followup_question` tool to get user approval of the plan.
   - Present summary of phases and major tasks
   - Ask if they want to proceed with implementation

## 4. Git Branch Management and Setup

### 4.1 Verify Current Branch and Create Feature Branch

1. Use the `execute_command` tool to check current Git branch:
   ```bash
   git branch --show-current
   ```

2. If on `main` or `develop` branch, create a feature branch:
   - Use the `execute_command` tool to create and checkout feature branch:
   ```bash
   git checkout -b feature/migrate-{component-name}-component
   ```
   - Branch naming convention: `feature/migrate-{component-name}-component`
   - Example: `feature/migrate-accordion-component`
   - **Do not request approval** - proceed automatically if in autonomous mode

3. If already on a feature branch, verify it's appropriate:
   - Check branch name matches the component being migrated
   - If not, ask user if they want to create a new feature branch or continue on current branch

4. Document the feature branch name for reference throughout the workflow.

### 4.2 Commit Strategy

**Important Git Guidelines:**
- **Commit incrementally** after each major step or logical unit of work
- **Never push** to remote repository unless explicitly approved by user
- Commits should be atomic and have clear, descriptive messages
- Push will only occur at the end of workflow with user approval (see Section 9)

**Commit Message Format:**
```
feat({component-name}): [brief description]

- Detailed change 1
- Detailed change 2
```

**Example commit messages:**
- `feat(accordion): create component directory structure`
- `feat(accordion): add AccordionEnums with Size and Color enums`
- `feat(accordion): implement Accordion.razor component`
- `feat(accordion): add demo page with examples`

**When to commit:**
- After creating directory structure
- After creating enum files
- After implementing each component file
- After creating demo pages
- After adding documentation
- After verification adjustments

## 5. Implement Foundation Components

### 5.1 Create Directory Structure

1. Use the `execute_command` tool to create component directory if needed:
   ```powershell
   New-Item -ItemType Directory -Path "src/Flowbite/Components/{ComponentName}" -Force
   ```
   Note: Only create subdirectory if component has multiple related files (e.g., Tabs, Sidebar)

2. If moving existing component, use `execute_command` to move files:
   ```powershell
   Move-Item -Path "src/Flowbite/Components/{OldFile}.razor" -Destination "src/Flowbite/Components/{ComponentName}/"
   ```

3. Commit directory structure changes:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/Flowbite/Components/{ComponentName}
   git commit -m "feat({component-name}): create component directory structure"
   ```

### 5.2 Create Enumeration Definitions

1. Determine enum organization strategy based on C# conventions:
   - **Option A - Component-specific enum (single enum):**
     * Define enum inside component's `.razor.cs` file
     * Place after the component class definition
     * Example: `HeadingTag` enum in `Heading.razor.cs`
   
   - **Option B - Shared enum (used by multiple components):**
     * Create separate file: `src/Flowbite/Components/{EnumName}.cs`
     * Examples: `CheckboxColor.cs`, `TextInputSize.cs`, `SelectColor.cs`
   
   - **Option C - Multiple related enums:**
     * Group in single file: `src/Flowbite/Components/{ComponentName}Enums.cs`
     * Examples: `ModalEnums.cs`, `DropdownEnums.cs`, `ToastEnums.cs`

2. For each enum, include:
   - Namespace: `Flowbite.Components`
   - XML documentation for enum and each value
   - Comment showing corresponding Tailwind class
   - Example structure:
     ```csharp
     namespace Flowbite.Components;
     
     /// <summary>
     /// Defines the available {description}.
     /// </summary>
     public enum {EnumName}
     {
         /// <summary>
         /// {Description} (tailwind-class).
         /// </summary>
         Value1,
         
         /// <summary>
         /// {Description} (tailwind-class).
         /// </summary>
         Value2,
         // ... more values
     }
     ```

3. Use the `write_to_file` tool to create enum file(s) based on chosen strategy.

4. Verify enums compile:
   - Use `execute_command` to build: `dotnet build src/Flowbite/Flowbite.csproj`

5. Commit enum files:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/Flowbite/Components/
   git commit -m "feat({component-name}): add enumeration definitions"
   ```

### 5.3 Create Helper Class (Optional)

1. Determine if a separate helper class is needed:
   - **Use helper class if:** Component has complex CSS logic or multiple enum-to-class mappings
   - **Skip helper class if:** CSS logic is simple enough to include in component's GetClasses() method
   - Review existing components: Most use inline switch expressions in GetClasses()

2. If creating helper class, use the `write_to_file` tool:
   - Path: `src/Flowbite/Components/{ComponentName}Helper.cs` (at component level, not in subfolder)
   - Include static methods for each enum type
   - Each method returns appropriate Tailwind CSS class string
   - Add XML documentation for all public methods
   - Example structure:
     ```csharp
     namespace Flowbite.Components;
     
     /// <summary>
     /// Helper class for generating CSS classes for {ComponentName} component.
     /// </summary>
     public static class {ComponentName}Helper
     {
         /// <summary>
         /// Gets the CSS class for the specified {property}.
         /// </summary>
         public static string Get{Property}Class({EnumType} value) => value switch
         {
             {EnumType}.Value1 => "tailwind-class-1",
             {EnumType}.Value2 => "tailwind-class-2",
             // ... more cases
             _ => "default-class"
         };
     }
     ```

3. Verify helper class compiles:
   - Use `execute_command` to build: `dotnet build src/Flowbite/Flowbite.csproj`

4. Commit helper class (if created):
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/Flowbite/Components/{ComponentName}Helper.cs
   git commit -m "feat({component-name}): add helper class for CSS generation"
   ```

### 5.4 Implement Main Component

1. Use the `write_to_file` tool to create component Razor file:
   - Path: `src/Flowbite/Components/{ComponentName}/{ComponentName}.razor`
   - Include:
     * `@using Flowbite.Base`
     * `@inherits FlowbiteComponentBase`
     * `@namespace Flowbite.Components`
     * Component markup with class binding
     * Support for ChildContent via RenderFragment

2. Use the `write_to_file` tool to create component code-behind:
   - Path: `src/Flowbite/Components/{ComponentName}/{ComponentName}.razor.cs`
   - Include:
     * Namespace: `Flowbite.Components`
     * Partial class inheriting from FlowbiteComponentBase (if applicable)
     * All [Parameter] properties with XML documentation
     * Private GetClasses() method for CSS generation
     * Event callback parameters if needed
     * AdditionalAttributes support

3. Implement GetClasses() method logic:
   - Combine base classes
   - Add variant-based classes using helper methods
   - Handle conditional classes (bool parameters)
   - Use CombineClasses() from base class

4. Verify component compiles:
   - Use `execute_command` to build: `dotnet build src/Flowbite/Flowbite.csproj`
   - Fix any compilation errors before proceeding

5. Commit component files:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/Flowbite/Components/{ComponentName}/
   git commit -m "feat({component-name}): implement main component with parameters and styling"
   ```

## 6. Create Demo Pages

### 5.1 Setup Demo Structure

1. Use the `execute_command` tool to create demo directory:
   ```powershell
   New-Item -ItemType Directory -Path "src/DemoApp/Pages/Docs/components/{category}" -Force
   ```

2. Determine the demo page route:
   - Route: `/docs/components/{category}/{component-name}`

### 5.2 Create Demo Page

1. Use the `write_to_file` tool to create demo page:
   - Path: `src/DemoApp/Pages/Docs/components/{category}/{ComponentName}Page.razor`
   - Include:
     * `@page` directive with route
     * `<PageTitle>` element
     * Main container with proper spacing
     * Section heading

2. Add example sections using ComponentExample wrapper:
   - For each major example from Svelte docs:
     * Use `<ComponentExample>` component
     * Set Title, Description, RazorCode parameters
     * Include PreviewContent with actual component usage
     * Reference Svelte example URL in comments

3. Create placeholder examples for remaining variations:
   - Add ComponentExample sections with TODO comments
   - Note which Svelte examples they correspond to
   - Mark for future implementation

4. Verify demo page compiles:
   - Use `execute_command` to build: `dotnet build src/DemoApp/DemoApp.csproj`

5. Commit demo page:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/DemoApp/Pages/Docs/components/{category}/{ComponentName}Page.razor
   git commit -m "feat({component-name}): add demo page with examples"
   ```

### 6.3 Update Navigation

1. Use the `read_file` tool to read the sidebar component:
   - Path: `src/DemoApp/Layout/DemoAppSidebar.razor`

2. Use the `replace_in_file` tool to add navigation link:
   - Add link in appropriate category section
   - Follow existing link format
   - Maintain alphabetical order if applicable

3. Verify navigation works:
   - Use `execute_command` to build and run: `dotnet run --project src/DemoApp/DemoApp.csproj`
   - Note the local URL for testing

4. Commit navigation changes:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/DemoApp/Layout/DemoAppSidebar.razor
   git commit -m "feat({component-name}): add navigation link to sidebar"
   ```

## 7. Create Documentation

### 6.1 Create LLMS Documentation

1. Use the `write_to_file` tool to create component documentation:
   - Path: `src/DemoApp/wwwroot/llms-docs/sections/{component-name}.md`
   - Include:
     * Component description and purpose
     * Parameter documentation with types and defaults
     * Usage examples
     * Common patterns
     * Related components

### 6.2 Update Build Script

1. Use the `read_file` tool to read the build script:
   - Path: `src/DemoApp/Build-LlmsContext.ps1`

2. Use the `replace_in_file` tool to add new documentation file:
   - Add to the file list in appropriate section
   - Follow existing pattern

3. Use the `execute_command` tool to run the build script:
   ```powershell
   .\src\DemoApp\Build-LlmsContext.ps1
   ```

4. Verify the llms-ctx.md file was updated:
   - Use `read_file` to check `src/DemoApp/wwwroot/llms-ctx.md`
   - Confirm new component documentation is included

5. Commit documentation:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/DemoApp/wwwroot/llms-docs/sections/{component-name}.md
   git add src/DemoApp/Build-LlmsContext.ps1
   git add src/DemoApp/wwwroot/llms-ctx.md
   git commit -m "feat({component-name}): add component documentation"
   ```

## 8. Verification with Playwright MCP

### 7.1 Setup Verification Environment

1. Use the `execute_command` tool to create verification directory:
   ```powershell
   New-Item -ItemType Directory -Path "verification" -Force
   ```

2. Ensure DemoApp is running:
   - Use `execute_command` to run: `dotnet run --project src/DemoApp/DemoApp.csproj`
   - Note the local URL (typically https://localhost:5001 or http://localhost:5000)
   - Keep this running in background for verification steps

### 7.2 Capture Reference Screenshots

1. For each major example, capture Svelte reference:
   - Use `use_mcp_tool` with Playwright MCP server
   - Tool: `browser_navigate` to launch browser at Svelte docs URL
   - Example: `https://flowbite-svelte.com/docs/{category}/{component-name}`
   - Use `browser_take_screenshot` to capture
   - Filename: `verification/svelte-{component}-{example-name}.png`
   - Use `browser_close` when done

2. Repeat for each key example section:
   - Default example
   - Size variations
   - Color variations
   - Special features
   - Interactive states (if applicable)

### 7.3 Capture Blazor Implementation Screenshots

1. For each example, capture Blazor implementation:
   - Use `use_mcp_tool` with Playwright MCP server
   - Tool: `browser_navigate` to local demo page
   - URL: `https://localhost:5001/docs/components/{category}/{component-name}`
   - Use `browser_take_screenshot` to capture
   - Filename: `verification/blazor-{component}-{example-name}.png`
   - Use `browser_close` when done

2. Capture matching examples in same order as Svelte screenshots

### 7.4 Compare and Document Findings

1. Create verification summary document:
   - Use `write_to_file` to create `verification/summary.md`
   - For each example comparison:
     * List Svelte screenshot filename
     * List Blazor screenshot filename
     * Document visual differences found
     * Note discrepancies in: sizing, spacing, colors, alignment
     * Mark as "Match" or "Needs Adjustment"

2. If discrepancies found, document required changes:
   - List specific CSS class adjustments needed
   - Note any parameter changes required
   - Identify any missing features

### 7.5 Refine Implementation

1. For each identified discrepancy:
   - Use `replace_in_file` to adjust component code
   - Update CSS classes in GetClasses() method
   - Adjust default parameter values if needed
   - Fix any styling issues

2. Rebuild and re-verify:
   - Use `execute_command` to rebuild: `dotnet build`
   - Restart DemoApp if needed
   - Recapture Blazor screenshots for adjusted examples
   - Update verification summary

3. Iterate until visual parity achieved:
   - Repeat capture and comparison process
   - Document final state in verification summary
   - Mark all examples as verified

4. Commit verification adjustments:
   - Use `execute_command` to stage and commit any component refinements:
   ```bash
   git add src/Flowbite/Components/{ComponentName}/
   git add verification/
   git commit -m "feat({component-name}): refine component based on visual verification"
   ```

## 9. Final Review and Testing

### 8.1 Code Quality Review

1. Review all component code:
   - Use `read_file` to review each component file
   - Verify XML documentation is complete
   - Check parameter naming follows conventions
   - Ensure proper use of nullable types
   - Confirm AdditionalAttributes support

2. Review demo pages:
   - Use `read_file` to review demo page
   - Verify all examples render correctly
   - Check that placeholder examples are clearly marked
   - Ensure ComponentExample usage is consistent

### 8.2 Build and Test

1. Build entire solution:
   - Use `execute_command`: `dotnet build`
   - Verify no compilation errors or warnings

2. Test in light mode:
   - Run DemoApp
   - Navigate to component demo page
   - Verify all examples render correctly
   - Check colors and styling

3. Test in dark mode:
   - Toggle dark mode in demo app
   - Verify dark mode classes work correctly
   - Check contrast and readability

4. Test responsive behavior:
   - Use Playwright MCP to resize browser window
   - Verify component adapts to different screen sizes
   - Check mobile, tablet, and desktop views

### 8.3 Update Project Documentation

1. Update CHANGELOG files:
   - Use `replace_in_file` to update `src/Flowbite/CHANGELOG.md`
   - Use `replace_in_file` to update `src/DemoApp/CHANGELOG.md`
   - Document new component and features

2. Update README if needed:
   - Use `read_file` to check `README.md`
   - Add component to list if appropriate
   - Use `replace_in_file` to update

3. Commit documentation updates:
   - Use `execute_command` to stage and commit:
   ```bash
   git add src/Flowbite/CHANGELOG.md
   git add src/DemoApp/CHANGELOG.md
   git add README.md
   git commit -m "docs({component-name}): update project documentation and changelogs"
   ```

## 10. Completion and Push Approval

1. Create final summary document:
   - Use `write_to_file` to create `docs/feature/{component-name}/completion-summary.md`
   - Include:
     * Component name and category
     * Implementation date
     * Key features implemented
     * Known limitations or placeholder examples
     * Verification results summary
     * Links to demo page and documentation

2. Review all commits made:
   - Use `execute_command` to view commit history:
   ```bash
   git log --oneline feature/migrate-{component-name}-component ^develop
   ```
   - Verify all commits are present and properly formatted

3. Use the `ask_followup_question` tool to request push approval:
   - Present summary of all commits
   - Ask: "All changes have been committed to the feature branch. Would you like to push these changes to the remote repository?"
   - Options: ["Yes, push to remote", "No, keep local only"]
   - **Important:** Only push if user explicitly approves

4. If user approves push:
   - Use `execute_command` to push feature branch:
   ```bash
   git push -u origin feature/migrate-{component-name}-component
   ```
   - Confirm push was successful

5. Use the `attempt_completion` tool to present final results:
   - Summarize what was accomplished
   - List all files created/modified
   - Show feature branch name
   - Indicate if changes were pushed to remote
   - Provide demo page URL
   - Note verification results
   - Mention any follow-up work needed (e.g., create pull request)
   - Include command to run demo: `dotnet run --project src/DemoApp/DemoApp.csproj`

</detailed_sequence_steps>

</task>
