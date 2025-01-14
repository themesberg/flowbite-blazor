#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 9.0 -InstallDir ./dotnet
./dotnet/dotnet --version

# Generate and publish packages locally
./dotnet/dotnet pack src/Flowbite/Flowbite.csproj -c Release -o ./nuget-local
./dotnet/dotnet pack src/Flowbite.ExtendedIcons/Flowbite.ExtendedIcons.csproj -c Release -o ./nuget-local
echo "NuGet packages created in nuget-local directory"

# Publish release version using packages
./dotnet/dotnet publish ./src/DemoApp/DemoApp.csproj -c Release -o dist