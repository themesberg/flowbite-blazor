# Flowbite Blazor Utility Scripts

This directory contains utility scripts used for various automation tasks in the Flowbite Blazor project.

## Scripts

### Generate-Icons.ps1

PowerShell script for generating Blazor components from Flowbite SVG icons.

#### Usage

1. Go to the Flowbite Icons website and using the browser's developer tools, copy the HTML content of the icon section
1. Save the HTML content to the a file `tmp\flowbite_icons_snippet.html`
1. Then run the script from the command line:

    ```powershell
    .\Generate-Icons.ps1 -HtmlFilePath tmp/flowbite_icons_snippet.html -OutputDirectory src/Flowbite/Components/Icons -IconStyle outline
    ```

#### Parameters

1. `HtmlFilePath`: Path to the HTML file containing Flowbite icons
1. `OutputDirectory`: Directory where the generated Blazor components will be saved
1. `IconStyle`: Style of icons being generated ("outline" or "solid")

#### Examples

```powershell
# Generate outline style components
.\Generate-Icons.ps1 -HtmlFilePath ".\tmp\icons_flowbite_svg_outline_arrows.html" -OutputDirectory "..\Components\Icons" -IconStyle outline

# Generate solid style components
.\Generate-Icons.ps1 -HtmlFilePath ".\tmp\icons_flowbite_svg_solid_arrows.html" -OutputDirectory "..\Components\Icons" -IconStyle solid

# Show usage information
.\Generate-Icons.ps1 -ShowUsage
```

#### How It Works

1. Reads an HTML file containing Flowbite SVG icons
1. Extracts icon names and SVG paths
1. Generates Blazor components with proper namespaces and inheritance
1. Names components using PascalCase convention (e.g., "angle-down" → "AngleDownIcon")
1. Applies the appropriate SVG template based on icon style (outline or solid)

#### Requirements

1. PowerShell 5.1 or later
1. Input HTML file must contain Flowbite-formatted icon definitions
1. Write permissions to the output directory

## Contributing

When adding new utility scripts to this directory:

1. Follow PowerShell best practices
1. Include detailed help information using comment-based help
1. Document the script in this README
1. Include error handling and input validation
1. Add usage examples
