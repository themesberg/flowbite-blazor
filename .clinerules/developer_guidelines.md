# Flowbite Blazor Component Library

This project involves converting the Flowbite React component library to ASP.NET Blazor 8.0. The project plan is at file path `{{project_plan_file}}`

## File Paths

- **Project Root:** `C:/Users/tschavey/projects/peakflames/flowbite-blazor`
- **Docs Directory:** `{{project_root_dir}}/docs`
- **Project Plan:** `{{docs_dir}}/project-plan.md`
- **Flowbite Blazor Library (csproj):** `{{project_root_dir}}/src/Flowbite`
- **Demo App (csproj):** `{{project_root_dir}}/src/DemoApp`
- **Flowbite React UI Source:** `C:/Users/tschavey/projects/javascript_projects/flowbite-react/packages/ui/src`
- **Flowbite React Component Docs:** `C:/Users/tschavey/projects/javascript_projects/flowbite-react/apps/web/content/docs/components`
- **LLMS Context:** `{{project_root_dir}}src/DemoApp/wwwroot/llms-ctx.md`

## Custom Instructions

### General

- Prefer to use the following MCP Servers and their tools:
    - `filesystem`
    - `fetch`

### Blazor Component Development

1. Use 4 spaces for indentation.
2. Follow C# naming conventions:
    - `PascalCase` for public members and types
    - `_camelCase` for private fields
    - Parameters must be public properties
3. Component organization:
    - One component per file
    - Place in appropriate feature folder under `Components/`
    - Use partial classes for complex components
4. Documentation:
    - XML comments for public APIs
    - Include parameter descriptions
    - Document event callbacks
5. Prefer C# code in `.cs` file rather than `.razor` file.
6. Prefer functions over lambda expressions for event handlers.

7. **Icon Usage**:
    - Always use icons from the `Flowbite.Icons` or `Flowbite.ExtendedIcons` libraries.
    - Do not use external icon libraries such as Font Awesome.
    - If a suitable icon does not exist, a new one must be created in one of the internal libraries.


### Migrating Flowbite React Components to Blazor

1. First, you need to understand how the Flowbite React component was implemented
2. Read the flowbite React library source code located in the file path `flowbite_react_ui_src_dir`
3. The demo pages examples be as close to the original React doc pages as possible where the React source file is `flowbite_react_component_docs_dir/{{COMPONENT_NAME}}.mdx`
4. The deme pages shall be located at `src/DemoApp/Pages/Docs/components/{{COMPONENT_NAME}}Page.razor`
5. Update the DemoAppSidebar.razor component to have the link to the component demo page.
6. Use exiting Flowbite Blazor Components if needing components in examples. See LLMS Context Document file id = `llms-ctx` for list of components and and user-focused documentation.
7. The demo page are implemented following this Razor template:

    ```razor
    @page "/docs/components/{{COMPONENT_NAME}}"

    <PageTitle>{{COMPONENT_NAME}} - Flowbite Blazor</PageTitle>

    <main class="p-6 space-y-4 max-w-4xl">
        <h2>{{COMPONENT_NAME}} Examples<h2>

        <div class="space-y-8">
            <ComponentExample 
                Title="{{EXAMPLE_NAME}}"
                Description="{{EXAMPLE_DESCRIPTION}}"
                RazorCode="@(@"{{EXMPLE_RAW_RAZOR_CODE}})"
                SupportedLanguages="@(new[] { "razor" })">
                <PreviewContent>
                    {{EXMPLE_RAW_RAZOR_CODE}}
                </PreviewContent>
            </ComponentExample>

            ...more example sections

        </div>
    </main>
    ```

8. The Component implementation is implemented following this template of Razor and C# code behind:

    ```razor
    @using Flowbite.Base
    @inherits FlowbiteComponentBase
    ...continue with component specific implementation
    ```

    ```csharp
    namespace Flowbite.Components;

    public partial class {{COMPONENT_NAME}}
    {
      ...continue with component-specific implementation
    }
    ```

9. For established practices refer to the Button and Tooltip components, for example practice the project prefesr using c# Enumerations
10. Prefer the user to manually run the app to view the website to confirm the implementation is satisfactory

### Building the DemoApp project

When building the DemoApp project, execute this command:

```powershell
cd C:\Users/tschavey\projects\peakflames\flowbite-blazor\src\DemoApp"; dotnet build
```
