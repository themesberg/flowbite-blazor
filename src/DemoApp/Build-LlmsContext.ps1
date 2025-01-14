# Build-LlmsContext.ps1
# This script concatenates individual documentation files into a single llms-ctx.md file

# Get the script's directory path
$scriptDir = $PSScriptRoot
if (-not $scriptDir) {
    $scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
}

# Define absolute paths
$sourceDir = Join-Path $scriptDir "wwwroot/llms-docs"
$outputFile = Join-Path $scriptDir "wwwroot/llms-ctx.md"

Write-Host "Building llms-ctx.md documentation..."
Write-Host "Source directory: $sourceDir"
Write-Host "Output file: $outputFile"

# Verify source directory exists
if (-not (Test-Path $sourceDir)) {
    Write-Error "Source directory not found: $sourceDir"
    exit 1
}

# Initialize output content
$content = @()

# Add project wrapper
$projectFile = Join-Path $sourceDir "project.md"
Write-Host "Adding project wrapper from: $projectFile"
if (Test-Path $projectFile) {
    $content += Get-Content $projectFile -Raw
} else {
    Write-Error "Project file not found: $projectFile"
    exit 1
}

# Start docs section
$content += "`n<docs>`n"

# Add documentation sections in order
Write-Host "Adding documentation sections..."
$sectionFiles = @(
    "01-quickstart.md",
    "02-icons.md",
    "03-patterns.md"
)

$sectionsDir = Join-Path $sourceDir "sections"
foreach ($file in $sectionFiles) {
    $filePath = Join-Path $sectionsDir $file
    if (Test-Path $filePath) {
        Write-Host "  Adding section: $file"
        $content += Get-Content $filePath -Raw
        $content += "`n"
    } else {
        Write-Warning "Section file not found: $filePath"
    }
}

# Close docs section
$content += "</docs>`n"
$content += "</project>`n"

# Create output directory if it doesn't exist
$outputDir = Split-Path -Parent $outputFile
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
}

# Write the combined content to the output file
$content | Set-Content $outputFile -Force

Write-Host "Documentation has been compiled to $outputFile"
