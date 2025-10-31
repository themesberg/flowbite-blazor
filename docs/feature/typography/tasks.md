# Typography Components Implementation Tasks

This document contains the actionable task checklist for implementing Typography components in Flowbite Blazor.

## Task Status Legend
- [ ] Not started
- [x] Completed
- [~] In progress
- [!] Blocked/Issue

---

## Phase 0: Git Branch Setup

### 0.1 Verify and Create Feature Branch

- [ ] **Task 0.1.1:** Check current Git branch
  - **Command:** `git branch --show-current`
  - **Acceptance:** Current branch identified
  - **Dependencies:** None

- [ ] **Task 0.1.2:** Create feature branch if on main or develop
  - **Command:** `git checkout -b feature/typography-components`
  - **Acceptance:** Feature branch created and checked out
  - **Dependencies:** Task 0.1.1
  - **Note:** Skip if already on appropriate feature branch

- [ ] **Task 0.1.3:** Confirm feature branch is active
  - **Command:** `git branch --show-current`
  - **Acceptance:** Shows `feature/typography-components`
  - **Dependencies:** Task 0.1.2

## Phase 1: Foundation & Core Components

### 1.1 Project Structure Setup

- [ ] **Task 1.1.1:** Create Typography folder structure
  - **Path:** `src/Flowbite/Components/Typography/`
  - **Acceptance:** Folder exists
  - **Dependencies:** None
  - **Commands:**
    ```powershell
    New-Item -ItemType Directory -Path "src/Flowbite/Components/Typography" -Force
    ```
  - **Note:** No Enums/ subfolder needed - following C#/Blazor conventions

- [ ] **Task 1.1.2:** Move existing Heading component to Typography folder
  - **Files to move:**
    - `src/Flowbite/Components/Heading.razor` → `src/Flowbite/Components/Typography/Heading.razor`
    - `src/Flowbite/Components/Heading.razor.cs` → `src/Flowbite/Components/Typography/Heading.razor.cs`
  - **Acceptance:** Files moved, namespace updated to `Flowbite.Components`
  - **Dependencies:** Task 1.1.1
  - **Note:** Update namespace if needed, ensure backward compatibility

- [ ] **Task 1.1.3:** Commit directory structure
  - **Command:** 
    ```bash
    git add src/Flowbite/Components/Typography/
    git commit -m "feat(typography): create Typography directory structure"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 1.1.2

### 1.2 Create Shared Enumeration File

- [ ] **Task 1.2.1:** Create TypographyEnums.cs with all shared enums
  - **Path:** `src/Flowbite/Components/Typography/TypographyEnums.cs`
  - **Acceptance:** File compiles, includes all shared typography enums
  - **Dependencies:** Task 1.1.1
  - **Enums to include:**
    * `TextSize`: XS, SM, Base, LG, XL, XXL, XXXL, XXXXL, XXXXXL, XXXXXXL, XXXXXXXL, XXXXXXXXL, XXXXXXXXXL
    * `FontWeight`: Thin, ExtraLight, Light, Normal, Medium, SemiBold, Bold, ExtraBold, Black
    * `LineHeight`: None, Tight, Snug, Normal, Relaxed, Loose, Three, Four, Five, Six, Seven, Eight, Nine, Ten
    * `TextAlign`: Left, Center, Right, Justify
    * `LetterSpacing`: Tighter, Tight, Normal, Wide, Wider, Widest
    * `Whitespace`: Normal, Nowrap, Pre, PreLine, PreWrap
    * `GradientColor`: None, PurpleToBlue, CyanToBlue, GreenToBlue, PurpleToPink, RedToYellow, TealToLime
  - **Note:** Following pattern from ModalEnums.cs, DropdownEnums.cs, ToastEnums.cs

- [ ] **Task 1.2.2:** Commit enum file
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/TypographyEnums.cs
    git commit -m "feat(typography): add TypographyEnums with shared typography enums"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 1.2.1

### 1.3 Create Helper Class (Optional)

- [ ] **Task 1.3.1:** Evaluate need for TypographyHelper class
  - **Decision:** Determine if CSS logic is complex enough to warrant separate helper class
  - **Dependencies:** Task 1.2.1
  - **Note:** Most existing components use inline switch expressions in GetClasses() method
  - **Recommendation:** Start without helper class, add later if needed

- [ ] **Task 1.3.2:** Create TypographyHelper class (if needed)
  - **Path:** `src/Flowbite/Components/Typography/TypographyHelper.cs`
  - **Acceptance:** Class compiles, all helper methods implemented with XML documentation
  - **Dependencies:** Task 1.3.1 (only if helper class deemed necessary)
  - **Methods to implement:**
    - `GetTextSizeClass(TextSize size)`
    - `GetFontWeightClass(FontWeight weight)`
    - `GetLineHeightClass(LineHeight height)`
    - `GetTextAlignClass(TextAlign align)`
    - `GetLetterSpacingClass(LetterSpacing spacing)`
    - `GetWhitespaceClass(Whitespace whitespace)`
    - `GetGradientClasses(GradientColor gradient)`

### 1.4 Enhanced Heading Component

- [ ] **Task 1.4.1:** Update Heading.razor to use DynamicComponent
  - **Path:** `src/Flowbite/Components/Typography/Heading.razor`
  - **Acceptance:** Component uses DynamicComponent instead of switch statement
  - **Dependencies:** Task 1.1.2

- [ ] **Task 1.4.2:** Enhance Heading.razor.cs with new parameters and HeadingTag enum
  - **Path:** `src/Flowbite/Components/Typography/Heading.razor.cs`
  - **Acceptance:** All new parameters added, HeadingTag enum defined in same file, backward compatible
  - **Dependencies:** Task 1.4.1, Task 1.2.1
  - **New parameters:**
    - `TextSize? Size` (optional override)
    - `FontWeight? Weight` (optional override)
    - `GradientColor Gradient`
    - `string? CustomColor`
  - **Enum to add:** `HeadingTag` (H1, H2, H3, H4, H5, H6) - place after component class
  - **Note:** Component-specific enum follows pattern from existing Heading.razor.cs

- [ ] **Task 1.4.3:** Implement GetClasses() method in Heading
  - **Acceptance:** Method generates correct CSS classes based on parameters
  - **Dependencies:** Task 1.4.2
  - **Logic:** Combine base classes, tag-based size (or override), weight, gradient, custom color

- [ ] **Task 1.4.4:** Add XML documentation to Heading component
  - **Acceptance:** All public members have XML comments
  - **Dependencies:** Task 1.4.2

- [ ] **Task 1.4.5:** Test Heading component compiles
  - **Command:** `dotnet build src/Flowbite/Flowbite.csproj`
  - **Acceptance:** No compilation errors
  - **Dependencies:** Task 1.4.3

- [ ] **Task 1.4.6:** Commit Heading component
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Heading.*
    git commit -m "feat(typography): implement enhanced Heading component"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 1.4.5

### 1.5 Paragraph Component

- [ ] **Task 1.5.1:** Create Paragraph.razor
  - **Path:** `src/Flowbite/Components/Typography/Paragraph.razor`
  - **Acceptance:** Basic component structure created
  - **Dependencies:** Task 1.1.1
  - **Template:** Simple `<p>` element with class binding

- [ ] **Task 1.5.2:** Create Paragraph.razor.cs with all parameters
  - **Path:** `src/Flowbite/Components/Typography/Paragraph.razor.cs`
  - **Acceptance:** All parameters defined with defaults
  - **Dependencies:** Task 1.5.1, Task 1.2.1
  - **Parameters:**
    - `TextSize Size = TextSize.Base`
    - `FontWeight Weight = FontWeight.Normal`
    - `LineHeight Height = LineHeight.Normal`
    - `LetterSpacing Spacing = LetterSpacing.Normal`
    - `TextAlign Align = TextAlign.Left`
    - `Whitespace Whitespace = Whitespace.Normal`
    - `bool Italic`
    - `bool Justify`
    - `bool FirstUpper`
    - `string CustomColor = "text-gray-900 dark:text-white"`
    - `RenderFragment? ChildContent`

- [ ] **Task 1.5.3:** Implement GetClasses() method in Paragraph
  - **Acceptance:** Method generates correct CSS classes based on all parameters
  - **Dependencies:** Task 1.5.2
  - **Logic:** Combine base color, size, weight, height, spacing, align, whitespace, italic, justify, firstUpper

- [ ] **Task 1.5.4:** Add XML documentation to Paragraph component
  - **Acceptance:** All public members have XML comments
  - **Dependencies:** Task 1.5.2

- [ ] **Task 1.5.5:** Test Paragraph component compiles
  - **Command:** `dotnet build src/Flowbite/Flowbite.csproj`
  - **Acceptance:** No compilation errors
  - **Dependencies:** Task 1.5.3

- [ ] **Task 1.5.6:** Commit Paragraph component
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Paragraph.*
    git commit -m "feat(typography): add Paragraph component with styling options"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 1.5.5

### 1.6 Span Component

- [ ] **Task 1.6.1:** Create Span.razor
  - **Path:** `src/Flowbite/Components/Typography/Span.razor`
  - **Acceptance:** Basic component structure created
  - **Dependencies:** Task 1.1.1
  - **Template:** Simple `<span>` element with class binding

- [ ] **Task 1.6.2:** Create Span.razor.cs with all parameters
  - **Path:** `src/Flowbite/Components/Typography/Span.razor.cs`
  - **Acceptance:** All parameters defined
  - **Dependencies:** Task 1.6.1, Task 1.2.1
  - **Parameters:**
    - `bool Italic`
    - `bool Underline`
    - `bool LineThrough`
    - `bool Uppercase`
    - `GradientColor Gradient = GradientColor.None`
    - `string? HighlightColor`
    - `string? CustomColor`
    - `RenderFragment? ChildContent`

- [ ] **Task 1.6.3:** Implement GetClasses() method in Span
  - **Acceptance:** Method generates correct CSS classes based on all parameters
  - **Dependencies:** Task 1.6.2
  - **Logic:** Combine italic, underline, linethrough, uppercase, gradient, highlight, custom color

- [ ] **Task 1.6.4:** Add XML documentation to Span component
  - **Acceptance:** All public members have XML comments
  - **Dependencies:** Task 1.6.2

- [ ] **Task 1.6.5:** Test Span component compiles
  - **Command:** `dotnet build src/Flowbite/Flowbite.csproj`
  - **Acceptance:** No compilation errors
  - **Dependencies:** Task 1.6.3

- [ ] **Task 1.6.6:** Commit Span component
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Span.*
    git commit -m "feat(typography): add Span component for inline text styling"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 1.6.5

### 1.7 Build and Verify Phase 1

- [ ] **Task 1.7.1:** Build entire Flowbite project
  - **Command:** `dotnet build src/Flowbite/Flowbite.csproj`
  - **Acceptance:** No compilation errors or warnings
  - **Dependencies:** All Phase 1 tasks completed

---

## Phase 2: Demo Pages

### 2.1 Create Demo Page Structure

- [ ] **Task 2.1.1:** Create typography demo folder
  - **Path:** `src/DemoApp/Pages/Docs/components/typography/`
  - **Acceptance:** Folder exists
  - **Dependencies:** None
  - **Command:**
    ```powershell
    New-Item -ItemType Directory -Path "src/DemoApp/Pages/Docs/components/typography" -Force
    ```

### 2.2 Heading Demo Page

- [ ] **Task 2.2.1:** Create HeadingPage.razor
  - **Path:** `src/DemoApp/Pages/Docs/components/typography/HeadingPage.razor`
  - **Route:** `/docs/components/typography/heading`
  - **Acceptance:** Page created with basic structure
  - **Dependencies:** Task 2.1.1, Task 1.4.5

- [ ] **Task 2.2.2:** Add "Default" example to HeadingPage
  - **Acceptance:** Example shows basic heading usage
  - **Dependencies:** Task 2.2.1
  - **Reference:** `https://flowbite-svelte.com/docs/typography/heading` - Default section

- [ ] **Task 2.2.3:** Add "Sizes" example to HeadingPage
  - **Acceptance:** Example shows H1-H6 variations
  - **Dependencies:** Task 2.2.1
  - **Reference:** `https://flowbite-svelte.com/docs/typography/heading` - Sizes section

- [ ] **Task 2.2.4:** Add "Gradient" example to HeadingPage
  - **Acceptance:** Example shows gradient text
  - **Dependencies:** Task 2.2.1
  - **Reference:** `https://flowbite-svelte.com/docs/typography/heading` - Gradient section

- [ ] **Task 2.2.5:** Add placeholder examples for remaining variations
  - **Examples:** Badge, Breadcrumb, Highlighted, Mark, Secondary, Underline
  - **Acceptance:** Placeholder ComponentExample sections created
  - **Dependencies:** Task 2.2.1
  - **Note:** Use placeholder content, will be filled in later

- [ ] **Task 2.2.6:** Commit HeadingPage
  - **Command:**
    ```bash
    git add src/DemoApp/Pages/Docs/components/typography/HeadingPage.razor
    git commit -m "feat(typography): add HeadingPage with examples"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 2.2.5

### 2.3 Paragraph Demo Page

- [ ] **Task 2.3.1:** Create ParagraphPage.razor
  - **Path:** `src/DemoApp/Pages/Docs/components/typography/ParagraphPage.razor`
  - **Route:** `/docs/components/typography/paragraph`
  - **Acceptance:** Page created with basic structure
  - **Dependencies:** Task 2.1.1, Task 1.5.5

- [ ] **Task 2.3.2:** Add "Default" example to ParagraphPage
  - **Acceptance:** Example shows basic paragraph usage
  - **Dependencies:** Task 2.3.1
  - **Reference:** `https://flowbite-svelte.com/docs/typography/paragraph` - Default section

- [ ] **Task 2.3.3:** Add "Sizes" example to ParagraphPage
  - **Acceptance:** Example shows size variations
  - **Dependencies:** Task 2.3.1

- [ ] **Task 2.3.4:** Add "Alignments" example to ParagraphPage
  - **Acceptance:** Example shows left, center, right, justify
  - **Dependencies:** Task 2.3.1

- [ ] **Task 2.3.5:** Add placeholder examples for remaining variations
  - **Examples:** Font weights, Line heights, Leading, First letter, Italic, With links, Layouts
  - **Acceptance:** Placeholder ComponentExample sections created
  - **Dependencies:** Task 2.3.1

- [ ] **Task 2.3.6:** Commit ParagraphPage
  - **Command:**
    ```bash
    git add src/DemoApp/Pages/Docs/components/typography/ParagraphPage.razor
    git commit -m "feat(typography): add ParagraphPage with examples"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 2.3.5

### 2.4 Span Demo Page

- [ ] **Task 2.4.1:** Create SpanPage.razor
  - **Path:** `src/DemoApp/Pages/Docs/components/typography/SpanPage.razor`
  - **Route:** `/docs/components/typography/span`
  - **Acceptance:** Page created with basic structure
  - **Dependencies:** Task 2.1.1, Task 1.6.5

- [ ] **Task 2.4.2:** Add "Default" example to SpanPage
  - **Acceptance:** Example shows basic inline text
  - **Dependencies:** Task 2.4.1
  - **Reference:** `https://flowbite-svelte.com/docs/typography/text` - Default section

- [ ] **Task 2.4.3:** Add "Text Decoration" example to SpanPage
  - **Acceptance:** Example shows italic, underline, line-through
  - **Dependencies:** Task 2.4.1

- [ ] **Task 2.4.4:** Add "Gradient" example to SpanPage
  - **Acceptance:** Example shows gradient text
  - **Dependencies:** Task 2.4.1

- [ ] **Task 2.4.5:** Add placeholder examples for remaining variations
  - **Examples:** Uppercase, Highlighted, Custom colors
  - **Acceptance:** Placeholder ComponentExample sections created
  - **Dependencies:** Task 2.4.1

- [ ] **Task 2.4.6:** Commit SpanPage
  - **Command:**
    ```bash
    git add src/DemoApp/Pages/Docs/components/typography/SpanPage.razor
    git commit -m "feat(typography): add SpanPage with examples"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 2.4.5

### 2.5 Update Navigation

- [ ] **Task 2.5.1:** Update DemoAppSidebar.razor
  - **Path:** `src/DemoApp/Layout/DemoAppSidebar.razor`
  - **Acceptance:** Typography section added with links to Heading, Paragraph, Span pages
  - **Dependencies:** Tasks 2.2.1, 2.3.1, 2.4.1
  - **Location:** Add under Components section

- [ ] **Task 2.5.2:** Commit navigation changes
  - **Command:**
    ```bash
    git add src/DemoApp/Layout/DemoAppSidebar.razor
    git commit -m "feat(typography): add navigation links to sidebar"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 2.5.1

### 2.6 Build and Test Demo Pages

- [ ] **Task 2.6.1:** Build DemoApp project
  - **Command:** `dotnet build src/DemoApp/DemoApp.csproj`
  - **Acceptance:** No compilation errors
  - **Dependencies:** All Phase 2 tasks completed

- [ ] **Task 2.6.2:** Run DemoApp locally
  - **Command:** `dotnet run --project src/DemoApp/DemoApp.csproj`
  - **Acceptance:** App runs without errors, note the URL
  - **Dependencies:** Task 2.6.1

- [ ] **Task 2.6.3:** Manually verify demo pages load
  - **URLs to check:**
    - `/docs/components/typography/heading`
    - `/docs/components/typography/paragraph`
    - `/docs/components/typography/span`
  - **Acceptance:** All pages load without errors
  - **Dependencies:** Task 2.6.2

---

## Phase 3: Documentation

### 3.1 Create LLMS Documentation Files

- [ ] **Task 3.1.1:** Create heading.md documentation
  - **Path:** `src/DemoApp/wwwroot/llms-docs/sections/heading.md`
  - **Acceptance:** Documentation file created with component details
  - **Dependencies:** Task 1.4.5
  - **Content:** Component description, parameters, examples, usage notes

- [ ] **Task 3.1.2:** Create paragraph.md documentation
  - **Path:** `src/DemoApp/wwwroot/llms-docs/sections/paragraph.md`
  - **Acceptance:** Documentation file created with component details
  - **Dependencies:** Task 1.5.5
  - **Content:** Component description, parameters, examples, usage notes

- [ ] **Task 3.1.3:** Create span.md documentation
  - **Path:** `src/DemoApp/wwwroot/llms-docs/sections/span.md`
  - **Acceptance:** Documentation file created with component details
  - **Dependencies:** Task 1.6.5
  - **Content:** Component description, parameters, examples, usage notes

### 3.2 Update Build Script

- [ ] **Task 3.2.1:** Update Build-LlmsContext.ps1
  - **Path:** `src/DemoApp/Build-LlmsContext.ps1`
  - **Acceptance:** Script includes new typography documentation files
  - **Dependencies:** Tasks 3.1.1, 3.1.2, 3.1.3
  - **Changes:** Add heading.md, paragraph.md, span.md to file list

- [ ] **Task 3.2.2:** Run Build-LlmsContext.ps1
  - **Command:** `.\src\DemoApp\Build-LlmsContext.ps1`
  - **Acceptance:** Script runs successfully, llms-ctx.md updated
  - **Dependencies:** Task 3.2.1

- [ ] **Task 3.2.3:** Verify llms-ctx.md includes typography docs
  - **Path:** `src/DemoApp/wwwroot/llms-ctx.md`
  - **Acceptance:** File contains typography component documentation
  - **Dependencies:** Task 3.2.2

- [ ] **Task 3.2.4:** Commit documentation
  - **Command:**
    ```bash
    git add src/DemoApp/wwwroot/llms-docs/sections/
    git add src/DemoApp/Build-LlmsContext.ps1
    git add src/DemoApp/wwwroot/llms-ctx.md
    git commit -m "feat(typography): add component documentation"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 3.2.3

---

## Phase 4: Verification with Playwright MCP

### 4.1 Setup Verification Environment

- [ ] **Task 4.1.1:** Create verification folder
  - **Path:** `verification/`
  - **Acceptance:** Folder exists for storing screenshots
  - **Dependencies:** None
  - **Command:**
    ```powershell
    New-Item -ItemType Directory -Path "verification" -Force
    ```

- [ ] **Task 4.1.2:** Ensure DemoApp is running
  - **Command:** `dotnet run --project src/DemoApp/DemoApp.csproj`
  - **Acceptance:** App running, note the local URL (e.g., https://localhost:5001)
  - **Dependencies:** Task 2.6.1

### 4.2 Verify Heading Component

- [ ] **Task 4.2.1:** Capture Svelte Heading reference - Default
  - **Tool:** Playwright MCP `browser_action`
  - **URL:** `https://flowbite-svelte.com/docs/typography/heading`
  - **Screenshot:** `verification/svelte-heading-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.2.2:** Capture Blazor Heading - Default
  - **Tool:** Playwright MCP `browser_action`
  - **URL:** `https://localhost:5001/docs/components/typography/heading` (adjust port if needed)
  - **Screenshot:** `verification/blazor-heading-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.2.2

- [ ] **Task 4.2.3:** Compare Heading Default screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.2.1, 4.2.2

- [ ] **Task 4.2.4:** Capture Svelte Heading reference - Sizes
  - **Screenshot:** `verification/svelte-heading-sizes.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.2.5:** Capture Blazor Heading - Sizes
  - **Screenshot:** `verification/blazor-heading-sizes.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.2.3

- [ ] **Task 4.2.6:** Compare Heading Sizes screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.2.4, 4.2.5

- [ ] **Task 4.2.7:** Capture Svelte Heading reference - Gradient
  - **Screenshot:** `verification/svelte-heading-gradient.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.2.8:** Capture Blazor Heading - Gradient
  - **Screenshot:** `verification/blazor-heading-gradient.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.2.4

- [ ] **Task 4.2.9:** Compare Heading Gradient screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.2.7, 4.2.8

- [ ] **Task 4.2.10:** Refine Heading component based on findings
  - **Acceptance:** Component adjusted to match Svelte reference
  - **Dependencies:** Tasks 4.2.3, 4.2.6, 4.2.9
  - **Note:** May require CSS class adjustments

- [ ] **Task 4.2.11:** Re-verify Heading component
  - **Acceptance:** New screenshots confirm visual parity
  - **Dependencies:** Task 4.2.10

- [ ] **Task 4.2.12:** Commit Heading refinements
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Heading.*
    git add verification/
    git commit -m "feat(typography): refine Heading component based on visual verification"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 4.2.11

### 4.3 Verify Paragraph Component

- [ ] **Task 4.3.1:** Capture Svelte Paragraph reference - Default
  - **URL:** `https://flowbite-svelte.com/docs/typography/paragraph`
  - **Screenshot:** `verification/svelte-paragraph-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.3.2:** Capture Blazor Paragraph - Default
  - **URL:** `https://localhost:5001/docs/components/typography/paragraph`
  - **Screenshot:** `verification/blazor-paragraph-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.3.2

- [ ] **Task 4.3.3:** Compare Paragraph Default screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.3.1, 4.3.2

- [ ] **Task 4.3.4:** Capture Svelte Paragraph reference - Sizes
  - **Screenshot:** `verification/svelte-paragraph-sizes.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.3.5:** Capture Blazor Paragraph - Sizes
  - **Screenshot:** `verification/blazor-paragraph-sizes.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.3.3

- [ ] **Task 4.3.6:** Compare Paragraph Sizes screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.3.4, 4.3.5

- [ ] **Task 4.3.7:** Capture Svelte Paragraph reference - Alignments
  - **Screenshot:** `verification/svelte-paragraph-alignments.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.3.8:** Capture Blazor Paragraph - Alignments
  - **Screenshot:** `verification/blazor-paragraph-alignments.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.3.4

- [ ] **Task 4.3.9:** Compare Paragraph Alignments screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.3.7, 4.3.8

- [ ] **Task 4.3.10:** Refine Paragraph component based on findings
  - **Acceptance:** Component adjusted to match Svelte reference
  - **Dependencies:** Tasks 4.3.3, 4.3.6, 4.3.9

- [ ] **Task 4.3.11:** Re-verify Paragraph component
  - **Acceptance:** New screenshots confirm visual parity
  - **Dependencies:** Task 4.3.10

- [ ] **Task 4.3.12:** Commit Paragraph refinements
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Paragraph.*
    git add verification/
    git commit -m "feat(typography): refine Paragraph component based on visual verification"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 4.3.11

### 4.4 Verify Span Component

- [ ] **Task 4.4.1:** Capture Svelte Text/Span reference - Default
  - **URL:** `https://flowbite-svelte.com/docs/typography/text`
  - **Screenshot:** `verification/svelte-span-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.4.2:** Capture Blazor Span - Default
  - **URL:** `https://localhost:5001/docs/components/typography/span`
  - **Screenshot:** `verification/blazor-span-default.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.4.2

- [ ] **Task 4.4.3:** Compare Span Default screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.4.1, 4.4.2

- [ ] **Task 4.4.4:** Capture Svelte Text/Span reference - Decoration
  - **Screenshot:** `verification/svelte-span-decoration.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.4.5:** Capture Blazor Span - Decoration
  - **Screenshot:** `verification/blazor-span-decoration.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.4.3

- [ ] **Task 4.4.6:** Compare Span Decoration screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.4.4, 4.4.5

- [ ] **Task 4.4.7:** Capture Svelte Text/Span reference - Gradient
  - **Screenshot:** `verification/svelte-span-gradient.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.1

- [ ] **Task 4.4.8:** Capture Blazor Span - Gradient
  - **Screenshot:** `verification/blazor-span-gradient.png`
  - **Acceptance:** Screenshot captured successfully
  - **Dependencies:** Task 4.1.2, Task 2.4.4

- [ ] **Task 4.4.9:** Compare Span Gradient screenshots
  - **Acceptance:** Visual comparison documented, discrepancies noted
  - **Dependencies:** Tasks 4.4.7, 4.4.8

- [ ] **Task 4.4.10:** Refine Span component based on findings
  - **Acceptance:** Component adjusted to match Svelte reference
  - **Dependencies:** Tasks 4.4.3, 4.4.6, 4.4.9

- [ ] **Task 4.4.11:** Re-verify Span component
  - **Acceptance:** New screenshots confirm visual parity
  - **Dependencies:** Task 4.4.10

- [ ] **Task 4.4.12:** Commit Span refinements
  - **Command:**
    ```bash
    git add src/Flowbite/Components/Typography/Span.*
    git add verification/
    git commit -m "feat(typography): refine Span component based on visual verification"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 4.4.11

### 4.5 Final Verification

- [ ] **Task 4.5.1:** Create verification summary document
  - **Path:** `verification/summary.md`
  - **Acceptance:** Document lists all comparisons, findings, and resolutions
  - **Dependencies:** All Phase 4 verification tasks completed

- [ ] **Task 4.5.2:** Final build and test
  - **Command:** `dotnet build`
  - **Acceptance:** Entire solution builds without errors
  - **Dependencies:** All refinement tasks completed

---

## Phase 5: Final Review and Documentation

### 5.1 Code Review

- [ ] **Task 5.1.1:** Review all component code for consistency
  - **Acceptance:** Code follows Blazor idioms, consistent naming, proper documentation
  - **Dependencies:** All implementation tasks completed

- [ ] **Task 5.1.2:** Review demo pages for completeness
  - **Acceptance:** All examples render correctly, placeholders noted for future work
  - **Dependencies:** All Phase 2 tasks completed

### 5.2 Update Project Documentation

- [ ] **Task 5.2.1:** Update main README if needed
  - **Path:** `README.md`
  - **Acceptance:** Typography components mentioned if appropriate
  - **Dependencies:** All tasks completed

- [ ] **Task 5.2.2:** Update CHANGELOG
  - **Path:** `src/Flowbite/CHANGELOG.md` and `src/DemoApp/CHANGELOG.md`
  - **Acceptance:** New components and changes documented
  - **Dependencies:** All tasks completed

- [ ] **Task 5.2.3:** Commit documentation updates
  - **Command:**
    ```bash
    git add src/Flowbite/CHANGELOG.md
    git add src/DemoApp/CHANGELOG.md
    git add README.md
    git commit -m "docs(typography): update project documentation and changelogs"
    ```
  - **Acceptance:** Changes committed to feature branch
  - **Dependencies:** Task 5.2.2

### 5.3 Final Testing

- [ ] **Task 5.3.1:** Test in light mode
  - **Acceptance:** All components render correctly in light mode
  - **Dependencies:** Task 4.5.2

- [ ] **Task 5.3.2:** Test in dark mode
  - **Acceptance:** All components render correctly in dark mode
  - **Dependencies:** Task 4.5.2

- [ ] **Task 5.3.3:** Test responsive behavior
  - **Acceptance:** Components work on different screen sizes
  - **Dependencies:** Task 4.5.2

### 5.4 Push Approval

- [ ] **Task 5.4.1:** Review all commits
  - **Command:** `git log --oneline feature/typography-components ^develop`
  - **Acceptance:** All commits reviewed and properly formatted
  - **Dependencies:** All Phase 5 tasks completed

- [ ] **Task 5.4.2:** Request user approval to push
  - **Action:** Ask user: "All changes have been committed to the feature branch. Would you like to push these changes to the remote repository?"
  - **Options:** ["Yes, push to remote", "No, keep local only"]
  - **Acceptance:** User provides explicit approval
  - **Dependencies:** Task 5.4.1
  - **Note:** DO NOT push without explicit user approval

- [ ] **Task 5.4.3:** Push feature branch (if approved)
  - **Command:** `git push -u origin feature/typography-components`
  - **Acceptance:** Feature branch pushed to remote repository
  - **Dependencies:** Task 5.4.2 (only if user approved)
  - **Note:** Only execute if user explicitly approved in Task 5.4.2

---

## Success Criteria Checklist

- [ ] All three core components (Heading, Paragraph, Span) implemented
- [ ] All components compile without errors
- [ ] Demo pages created with comprehensive examples
- [ ] Visual parity with Flowbite Svelte verified via screenshots
- [ ] Documentation complete and integrated into LLMS context
- [ ] All components follow Blazor idiomatic patterns
- [ ] Type-safe with full IntelliSense support
- [ ] Extensible through AdditionalAttributes
- [ ] Light and dark mode support verified
- [ ] Responsive behavior verified

---

## Notes

- **Blocked Tasks:** If any task is blocked, mark with [!] and document the blocker
- **In Progress:** Mark tasks being worked on with [~]
- **Dependencies:** Always complete dependency tasks before starting dependent tasks
- **Verification:** Phase 4 is critical - don't skip visual verification steps
- **Iteration:** Be prepared to iterate on components based on verification findings

---

## Quick Reference Commands

```powershell
# Build Flowbite library
dotnet build src/Flowbite/Flowbite.csproj

# Build DemoApp
dotnet build src/DemoApp/DemoApp.csproj

# Run DemoApp
dotnet run --project src/DemoApp/DemoApp.csproj

# Build entire solution
dotnet build

# Update LLMS context
.\src\DemoApp\Build-LlmsContext.ps1
```

## Playwright MCP Quick Reference

```
# Launch browser
browser_action: launch
url: <target-url>

# Take screenshot
browser_action: browser_take_screenshot
filename: <path/to/screenshot.png>

# Navigate to new URL
browser_action: navigate
url: <new-url>

# Close browser
browser_action: close
