# PowerShell 5.1 Compatible Script
[CmdletBinding()]
param(
    [Parameter(Mandatory=$true, Position=0, ParameterSetName="Generate")]
    [string]$HtmlFilePath,
    
    [Parameter(Mandatory=$true, Position=1, ParameterSetName="Generate")]
    [string]$OutputDirectory,

    [Parameter(Mandatory=$true, Position=2, ParameterSetName="Generate")]
    [ValidateSet("outline", "solid")]
    [string]$IconStyle,

    [Parameter(Mandatory=$true, ParameterSetName="Usage")]
    [switch]$ShowUsage
)

# Show usage if requested
if ($ShowUsage) {
    Write-Host @"
Generate-Icons.ps1 - Blazor Component Generator for Flowbite Icons

USAGE:
    .\Generate-Icons.ps1 <html-file> <output-dir> <icon-style>
    .\Generate-Icons.ps1 -HtmlFilePath <html-file> -OutputDirectory <output-dir> -IconStyle <outline|solid>
    .\Generate-Icons.ps1 -ShowUsage

PARAMETERS:
    -HtmlFilePath     Path to HTML file containing Flowbite icons
    -OutputDirectory  Directory where Blazor components will be saved
    -IconStyle        Style of icons being generated (outline or solid)
    -ShowUsage        Shows this usage information
"@
    exit 0
}

# Validate input file exists and is HTML
if (-not (Test-Path -Path $HtmlFilePath -PathType Leaf)) {
    Write-Error "Input file not found: $HtmlFilePath"
    exit 1
}
if (-not $HtmlFilePath.EndsWith('.html')) {
    Write-Error "Input file must be an HTML file"
    exit 1
}

# Validate or create output directory
if (Test-Path -Path $OutputDirectory -PathType Leaf) {
    Write-Error "Output path exists but is a file: $OutputDirectory"
    exit 1
}
if (-not (Test-Path -Path $OutputDirectory)) {
    New-Item -ItemType Directory -Path $OutputDirectory | Out-Null
    Write-Host "Created output directory: $OutputDirectory"
}

# Convert kebab-case to PascalCase
function ConvertTo-PascalCase {
    param([string]$Text)
    
    $parts = $Text -split '-'
    $result = foreach($part in $parts) {
        if ($part) {
            $part.Substring(0,1).ToUpper() + $part.Substring(1).ToLower()
        }
    }
    return [string]::Join('', $result)
}

# Create Blazor component content
function New-ComponentContent {
    param(
        [string]$Name,
        [string]$PathElement,
        [string]$IconStyle
    )
    
    if ($IconStyle -eq "outline") {
        $content = @"
@namespace Flowbite.Components.Icons
@inherits Flowbite.Components.Base.IconBase

<svg class="@CombinedClassNames"
     fill="none"
     stroke="currentColor"
     viewBox="0 0 24 24"
     xmlns="http://www.w3.org/2000/svg"
     aria-hidden="@AriaHidden"
     @attributes="AdditionalAttributes">
    <path stroke-linecap="round" 
          stroke-linejoin="round" 
          stroke-width="@StrokeWidth"
          $PathElement>
    </path>
</svg>
"@
    }
    else {
        $content = @"
@namespace Flowbite.Components.Icons
@inherits Flowbite.Components.Base.IconBase

<svg class="@CombinedClassNames"
     fill="none"
     viewBox="0 0 24 24"
     xmlns="http://www.w3.org/2000/svg"
     aria-hidden="@AriaHidden"
     @attributes="AdditionalAttributes">
    <path fill="currentColor"
          fill-rule="evenodd"
          clip-rule="evenodd"
          $PathElement>
    </path>
</svg>
"@
    }
    
    return $content
}

Write-Host "Reading HTML file..."
try {
    $htmlContent = Get-Content -Path $HtmlFilePath -Raw
}
catch {
    Write-Error "Failed to read HTML file: $_"
    exit 1
}

Write-Host "Parsing icons..."
# Updated pattern to match the specific structure from the HTML
$iconPattern = '(?s)<div class="mb-2[^"]*">.*?<svg[^>]*class="w-6 h-6[^"]*"[^>]*>(.*?)</svg>.*?<span[^>]*text-gray-[45]00[^>]*>([^<]+)</span>'
$matches = [regex]::Matches($htmlContent, $iconPattern)

if ($matches.Count -eq 0) {
    Write-Warning "No icons found in HTML file"
    exit 0
}

Write-Host "Found $($matches.Count) icons"
$successCount = 0
$errorCount = 0

foreach ($match in $matches) {
    $svgContent = $match.Groups[1].Value.Trim()
    $iconName = $match.Groups[2].Value.Trim()
    
    try {
        # Generate component name
        $componentName = "$(ConvertTo-PascalCase $iconName)Icon"
        Write-Host "Processing: $componentName"
        
        # Extract the d attribute from the path
        $pathMatch = [regex]::Match($svgContent, 'd="([^"]+)"')
        if (-not $pathMatch.Success) {
            Write-Warning "No path data found for icon: $iconName"
            continue
        }
        
        $pathData = $pathMatch.Groups[1].Value
        $pathElement = "d=""$pathData"""
        
        # Create component content
        $content = New-ComponentContent -Name $componentName -PathElement $pathElement -IconStyle $IconStyle
        
        # Save to file
        $outputPath = Join-Path $OutputDirectory "$componentName.razor"
        $content | Out-File -FilePath $outputPath -Encoding UTF8 -Force
        
        $successCount++
        Write-Host "Created: $outputPath" -ForegroundColor Green
    }
    catch {
        $errorCount++
        Write-Warning "Failed to process icon '$iconName': $_"
    }
}

Write-Host "`nGeneration Complete"
Write-Host "----------------"
Write-Host "Total Icons: $($matches.Count)"
Write-Host "Successful: $successCount"
Write-Host "Failed: $errorCount"

if ($errorCount -gt 0) {
    exit 1
}