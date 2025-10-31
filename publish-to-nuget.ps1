#!/usr/bin/env pwsh

# Get NuGet API Key from environment variable
$apiKey = $env:NUGET_API_KEY

# Error handling function
function Write-ErrorAndExit {
  param(
    $message
  )
  Write-Host $message -ForegroundColor Red
  exit 1
}

# Check for NuGet API Key in environment variable
if (-not $apiKey) {
  Write-ErrorAndExit "NuGet API Key not found in environment variable 'NUGET_API_KEY'. Please set the environment variable and try again."
}

# Check if current branch is main
$currentBranch = git rev-parse --abbrev-ref HEAD
if ($currentBranch -ne "main") {
  Write-ErrorAndExit "Publishing is only allowed from the 'main' branch. Current branch: $currentBranch"
}

# Clear all the old nuget-local folder
Write-Host "Clear all the old nuget-local folder..."
rm -r -force .\nuget-local

# Build and pack Flowbite.Blazor
Write-Host "Building and packing Flowbite.Blazor..."
dotnet pack src/Flowbite/Flowbite.csproj -c Release -o nuget-local
if ($LASTEXITCODE -ne 0) { 
  Write-ErrorAndExit "Error occurred while packing Flowbite.Blazor." 
}

# Build and pack Flowbite.ExtendedIcons
Write-Host "Building and packing Flowbite.ExtendedIcons..."
dotnet pack src/Flowbite.ExtendedIcons/Flowbite.ExtendedIcons.csproj -c Release -o nuget-local
if ($LASTEXITCODE -ne 0) { 
  Write-ErrorAndExit "Error occurred while packing Flowbite.ExtendedIcons." 
}

Write-Host "NuGet packages created in nuget-local directory" -ForegroundColor Green

# Publish to NuGet.org
Write-Host "Publishing Flowbite libraries to NuGet.org..."

# Publish Flowbite.Blazor
dotnet nuget push .\nuget-local\Flowbite.*.nupkg -s https://api.nuget.org/v3/index.json -k $apiKey --skip-duplicate
if ($LASTEXITCODE -ne 0) {
  Write-ErrorAndExit "An error occurred while publishing Flowbite.Blazor."
}

# Publish Flowbite.ExtendedIcons (skip duplicate if exists)
dotnet nuget push .\nuget-local\Flowbite.ExtendedIcons.*.nupkg -s https://api.nuget.org/v3/index.json -k $apiKey --skip-duplicate
Write-Host "Flowbite libraries published successfully to NuGet.org!" -ForegroundColor Green
Write-Host ""
Write-Host "Goto URL https://www.nuget.org/packages?q=flowbite to view the published packages."
Write-Host ""