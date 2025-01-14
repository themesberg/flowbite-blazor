#!/bin/sh
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh -c 9.0 -InstallDir ./dotnet
./dotnet/dotnet --version

# Publish release version using packages
./dotnet/dotnet publish ./src/DemoApp/DemoApp.csproj -c Release -o dist