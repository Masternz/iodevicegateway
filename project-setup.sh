# code set up
dotnet new sln -n IODeviceGateway.service
dotnet new webapi --name IODeviceGateway.api
dotnet new xunit --name IODeviceGateway.tests
dotnet new webapi --name IODeviceEmulator.api
dotnet new react --name IODevice.Web
dotnet new xunit --name IODeviceEmulator.tests

dotnet sln add IODeviceGateway.api/IODeviceGateway.api.csproj
dotnet sln add IODeviceGateway.tests/IODeviceGateway.tests.csproj
dotnet sln add IODeviceEmulator.api/IODeviceEmulator.api.csproj
dotnet sln add IODevice.Web/IODevice.Web.csproj
dotnet sln add IODeviceEmulator.api/IODeviceEmulator.api.csproj
dotnet sln add IODeviceEmulator.tests/IODeviceEmulator.tests.csproj


# Project references
dotnet add IODeviceEmulator.tests/IODeviceEmulator.tests.csproj reference IODeviceEmulator.api/IODeviceEmulator.api.csproj 
