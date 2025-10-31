#!/usr/bin/env pwsh

Write-Host "Using dotnet verison $(dotnet --version)"
Write-Host "Create NuGet packages in .\nuget-local directory..."

# Build and pack Flowbite.Blazor
dotnet pack src/Flowbite/Flowbite.csproj -c Release -o nuget-local
if ($LASTEXITCODE -ne 0) { exit 1 }

# Build and pack Flowbite.Blazor.ExtendedIcons
dotnet pack src/Flowbite.ExtendedIcons/Flowbite.ExtendedIcons.csproj -c Release -o nuget-local
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "NuGet packages created in nuget-local directory" -ForegroundColor Green

# Publist the DemoApp to .\dist
Write-Host "Delete and Publish to .\dist directory..."
if (Test-Path dist)
{ 
    Remove-Item -Path dist -Recurse -Force
}

dotnet publish ./src/DemoApp/DemoApp.csproj -c Release -o dist
if ($LASTEXITCODE -ne 0) { exit 1 }
Write-Host "Successfully published to .\dist" -ForegroundColor Green

Write-Host "To serve locally:"
Write-Host "   1. Ensure you have the tool dotnet-sevre installed"
Write-Host "   2  run: cd .\dist\wwwroot; dotnet serve"
Write-Host ""