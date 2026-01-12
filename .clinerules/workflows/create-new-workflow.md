<task name="Create New Workflow">

<task_objective>
Guide the user through the process of creating a new standardized Cline workflow file according to the established conventions. Supports two paths: analyzing existing work instructions with quality scoring, or collaborative step-by-step creation using output-first design principles. The output will be a properly structured workflow file in the .clinerules/workflows directory.
</task_objective>

<reference_material>
# Workflow Creation Reference Material

This section contains all the conventions, formatting rules, and examples needed to create properly structured workflow files.

## Workflow Quality Scoring Rubric

Use this rubric to evaluate existing work instructions (1-5 stars total):

| Criteria | Points | Description |
|----------|--------|-------------|
| **Clear Objective** | 1â˜… | Does it clearly state what the workflow accomplishes? |
| **Defined Inputs** | 1â˜… | Are all required inputs explicitly listed with sources? |
| **Traceable Steps** | 1â˜… | Can each step's output be traced back to specific inputs? |
| **Defined Outputs** | 1â˜… | Is the final deliverable clearly specified with format? |
| **Repeatability** | 1â˜… | Could someone else follow these instructions and get consistent results? |

### Quality Thresholds
- **â˜…â˜…â˜…â˜…â˜… (5/5)**: Excellent - Ready for workflow generation
- **â˜…â˜…â˜…â˜…â˜† (4/5)**: Good - Minor refinements recommended
- **â˜…â˜…â˜…â˜†â˜† (3/5)**: Fair - Several gaps need addressing
- **â˜…â˜…â˜†â˜†â˜† (2/5)**: Poor - Significant rework required
- **â˜…â˜†â˜†â˜†â˜† (1/5)**: Incomplete - Fundamental elements missing

## Common Workflow Anti-Patterns

Watch for these issues when analyzing or creating workflows:

1. **Orphan Inputs**: Data gathered but never used in any step
2. **Mysterious Outputs**: Results that can't be traced to specific inputs/logic
3. **Vague Instructions**: Steps like "gather relevant data" without specifying what data
4. **Missing Error Handling**: No guidance on what to do when steps fail
5. **Implicit Knowledge**: Assuming context that isn't documented

## Workflow File Structure

- All workflow files must be placed in the `.clinerules/workflows` directory
- Files should be named descriptively with kebab-case (e.g., `generate-trace-report-by-workitem.md`)
- Each workflow must be enclosed in `<task>` tags with a `name` attribute
- The task objective must be enclosed in `<task_objective>` tags
- The detailed sequence steps must be enclosed in `<detailed_sequence_steps>` tags
- The entire workflow should follow proper markdown formatting conventions

## Task Definition

- Begin each workflow file with `<task name="Descriptive Task Name">`
- End each workflow file with `</task>`
- The task name should be concise but descriptive of the workflow's purpose
- Example: `<task name="Generate Trace Report by Workitem">`

## Task Objective

- The task objective section should be enclosed in `<task_objective>` tags
- Provide a clear, concise description of what the workflow aims to accomplish
- Include any key tools or resources that will be used (e.g., MCP tools)
- Specify the expected output format (e.g., markdown file)
- Keep the objective to 1-3 sentences for clarity

## Detailed Sequence Steps

- The detailed steps section should be enclosed in `<detailed_sequence_steps>` tags
- Begin with a level 1 heading that includes the workflow name and "Process - Detailed Sequence of Steps"
- Organize major steps as level 2 headings (##) with sequential numbering
- Use numbered lists for substeps under each major step
- Indent sublists by 4 spaces
- Surround all headings with newlines for proper formatting
- Surround all code blocks with newlines

## Tool Usage Conventions

- Explicitly reference tools using backticks (e.g., `ask_followup_question`)
- For user interaction, use the `ask_followup_question` tool
- For accessing external data, use appropriate MCP tools (e.g., `use_mcp_tool`)
- For file operations, specify directory checks and file creation steps
- Always end workflows with the `attempt_completion` tool to present results

## Step Formatting

- Major steps should follow a logical sequence
- Each major step should have a clear purpose
- Substeps should provide specific actions to take
- Include examples where helpful (e.g., example IDs, formats)
- For output generation steps, clearly specify:
  - Directory structure and creation
  - File naming conventions
  - Content structure with detailed subsections

## Example Workflow Structure

```markdown
<task name="Example Workflow">

<task_objective>
Brief description of what this workflow accomplishes and its output.
</task_objective>

<detailed_sequence_steps>
# Example Workflow Process - Detailed Sequence of Steps

## 1. First Major Step

1. First substep with specific action.
   
2. Second substep with specific action.

## 2. Second Major Step

1. First substep with specific action.
   
2. Second substep with specific action.
   - Additional detail or example

## 3. Generate Output

1. Organize outputs under the root directory outputs/ 

2. Check if output directory exists, create if needed.

3. Create output file with specified content:
   i. First content section
   ii. Second content section
   iii. Third content section

4. Use the `attempt_completion` command to present results.

</detailed_sequence_steps>

</task>
```

## Available Tools

You will determiine this as part of the Preparation Work within the detailed steps.

## MCP Rules

- Obtain confluence content using the atlassian MCP tool `confluence_get_page` with a page id. If given a url (e.g. https://flyarcher.atlassian.net/wiki/spaces/SE/pages/{{PAGE_ID}}/SYSPR+Classification+guidance) then use the PAGE_ID to obtain the page content.

</reference_material>

<detailed_sequence_steps>
# Create New Workflow Process - Detailed Sequence of Steps

## 0. Preperation Work

1. Research and determine the full list of built-in tools (e.g. - `execute_command`, `read_file`, `new_task`, `list_files`, etc)
2. Reseacch and determine the full list of MCP tools.

## 1. Choose Creation Path

1. Use the `ask_followup_question` command to ask the USER how they would like to create their workflow:
   - **Option A**: "I have existing work instructions or workflow text to start with"
   - **Option B**: "Collaborate step-by-step to create a new workflow"

2. Based on the USER's response:
   - If Option A: Proceed to **Step 2A (Analyze Existing Work Instructions)**
   - If Option B: Proceed to **Step 2B (Collaborative Output-First Design)**

---

## 2A. Analyze Existing Work Instructions

1. Use the `ask_followup_question` command to ask the USER to provide their existing work instruction or workflow text.

2. Analyze the provided text against the Workflow Quality Scoring Rubric from the reference material:
   - **Clear Objective** (1â˜…): Does it state what the workflow accomplishes?
   - **Defined Inputs** (1â˜…): Are all required inputs explicitly listed?
   - **Traceable Steps** (1â˜…): Can each step's output be traced to inputs?
   - **Defined Outputs** (1â˜…): Is the final deliverable clearly specified?
   - **Repeatability** (1â˜…): Could someone else follow these instructions consistently?

3. Present the quality analysis to the USER:
   ```
   **Workflow Quality Score: â˜…â˜…â˜…â˜†â˜† (3/5)**
   
   âœ… Clear Objective - [assessment]
   âœ… Defined Outputs - [assessment]
   âš ï¸ Defined Inputs - [issue found]
   âŒ Traceable Steps - [issue found]
   âŒ Repeatability - [issue found]
   
   **Recommended improvements:**
   1. [Specific improvement]
   2. [Specific improvement]
   ```

4. Use the `ask_followup_question` command to work through each identified issue with the USER, refining the workflow until it reaches 4+ stars.

5. Once the workflow is refined, proceed to **Step 3 (Suggest Workflow Name)**.

---

## 2B. Collaborative Output-First Design

### Phase 1: Define the Output

1. Use the `ask_followup_question` command to ask: "What is the single most important deliverable or output of this workflow?"

2. Use the `ask_followup_question` command to ask: "What format should this output be in?" (e.g., markdown file, JSON, report, code file, terminal output)

3. Use the `ask_followup_question` command to ask: "Who consumes this output and what decisions do they make with it?"

### Phase 2: Identify Minimum Required Inputs

1. Use the `ask_followup_question` command to ask: "What is the absolute minimum information needed to produce this output?"

2. For each proposed input, validate its necessity by asking: "If you couldn't have [input], could you still produce the output? How?"

3. Use the `ask_followup_question` command to determine the source of each input:
   - User-provided at runtime
   - Retrieved via MCP tool (provide list of available MCP servers from reference material)
   - Read from file or configuration
   - Derived from other inputs

### Phase 3: Define Transformation Steps

1. For each major step, determine:
   - What input(s) does this step consume?
   - What does this step produce that the next step needs?

2. Build a simple traceability chain:
   ```
   Input A â†’ Step 1 â†’ Intermediate B â†’ Step 2 â†’ Output C
   Input D â†’ Step 2 â†’ Output C
   ```

3. Validate the workflow by checking:
   - No orphan inputs (every input is used)
   - No mysterious outputs (every output traces to inputs)
   - Clear transformation logic at each step

4. Present the traceability analysis to the USER and use `ask_followup_question` to confirm or refine.

5. Proceed to **Step 3 (Suggest Workflow Name)**.

---

## 3. Suggest Workflow Name

1. Analyze the refined workflow content (objective, inputs, outputs, key actions).

2. Generate 3-5 workflow name suggestions using kebab-case format. Consider:
   - Action verb + primary object (e.g., `generate-trace-report`)
   - Action + context/source (e.g., `export-polarion-requirements`)
   - Output description (e.g., `test-coverage-summary`)

3. Use the `ask_followup_question` command to present the suggestions:
   ```
   Based on your workflow to [brief description], here are suggested names:
   1. `suggested-name-one.md`
   2. `suggested-name-two.md`
   3. `suggested-name-three.md`
   
   Which would you prefer, or would you like to suggest a different name?
   ```

4. Record the selected workflow filename.

---

## 4. Define Task Objective Statement

1. Formulate a clear, concise task objective statement (1-3 sentences) based on:
   - The refined workflow purpose
   - Key MCP tools or resources used
   - Expected output format

2. Use the `ask_followup_question` command to present the draft objective to the USER for approval or refinement.

---

## 5. Generate Workflow File

1. Verify the `.clinerules/workflows` directory exists. If not, create it.

2. Use the Example Workflow Structure from the reference material as the template.

3. Create a markdown file named `.clinerules/workflows/{{workflow-filename}}.md` with the following structure:
   i. Task definition with name attribute
   ii. Task objective section
   iii. Detailed sequence steps section with proper formatting
   iv. Proper tool references and formatting conventions

4. Use the `write_to_file` command to write the completed workflow file.

5. Use the `attempt_completion` command to present the USER with:
   - The completed workflow file location
   - A summary of the workflow (objective, inputs, outputs, steps)
   - The final quality score (if Path A was used)

</detailed_sequence_steps>
