#!/bin/sh
set -e  # Exit on error

REQUIRED_VERSION="9.0"
DOTNET_PATH="./dotnet/dotnet"

# Function to compare version numbers
version_greater_equal() {
    printf '%s\n%s\n' "$1" "$2" | sort -V | head -n1 | grep -q "^$2$"
}

# Check if dotnet is already installed and accessible
if command -v dotnet >/dev/null 2>&1; then
    CURRENT_VERSION=$(dotnet --version | cut -d'.' -f1,2)
    echo "Found .NET version: $CURRENT_VERSION"
    
    if version_greater_equal "$CURRENT_VERSION" "$REQUIRED_VERSION"; then
        echo "Using system-installed .NET $CURRENT_VERSION"
        DOTNET_PATH="dotnet"
    else
        echo "System .NET version $CURRENT_VERSION is older than required version $REQUIRED_VERSION. Will install required version."
        INSTALL_DOTNET=true
    fi
else
    echo "No system .NET installation found"
    INSTALL_DOTNET=true
fi

# Install .NET if needed
if [ "$INSTALL_DOTNET" = true ]; then
    echo "Installing .NET $REQUIRED_VERSION..."
    curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh -c $REQUIRED_VERSION -InstallDir ./dotnet
    DOTNET_PATH="./dotnet/dotnet"
    echo "Using .NET version: $($DOTNET_PATH --version)"
fi

# Get current branch
BRANCH=$(git branch --show-current)
echo "Building branch: $BRANCH"

if [ "$BRANCH" != "main" ]; then
    echo "Creating NuGet packages in ./nuget-local directory..."
    
    # Create nuget-local directory if it doesn't exist
    mkdir -p nuget-local
    
    # Build and pack Flowbite.Blazor
    echo "Building Flowbite.Blazor..."
    $DOTNET_PATH pack src/Flowbite/Flowbite.csproj -c Release -o nuget-local
    if [ $? -ne 0 ]; then
        echo "Error: Failed to pack Flowbite.Blazor"
        exit 1
    fi
    
    # Build and pack Flowbite.Blazor.ExtendedIcons
    echo "Building Flowbite.Blazor.ExtendedIcons..."
    $DOTNET_PATH pack src/Flowbite.ExtendedIcons/Flowbite.ExtendedIcons.csproj -c Release -o nuget-local
    if [ $? -ne 0 ]; then
        echo "Error: Failed to pack Flowbite.Blazor.ExtendedIcons"
        exit 1
    fi
    
    echo "NuGet packages created in nuget-local directory"
    
    # Publish DemoApp using local packages
    echo "Publishing DemoApp with local packages..."
    $DOTNET_PATH publish ./src/DemoApp/DemoApp.csproj -c Release -o dist /p:RestoreAdditionalProjectSources=$PWD/nuget-local
else
    # Main branch - use published packages
    echo "Publishing DemoApp with published packages..."
    $DOTNET_PATH publish ./src/DemoApp/DemoApp.csproj -c Release -o dist
fi

if [ $? -ne 0 ]; then
    echo "Error: Failed to publish DemoApp"
    exit 1
fi

echo "Successfully published to ./dist"