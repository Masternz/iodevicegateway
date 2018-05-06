# code set up
dotnet new sln -n IODeviceGateway-service
dotnet new webapi --name IODeviceGateway-api
# dotnet new react --name clock-feature
dotnet new xunit --name IODeviceGateway-tests
dotnet sln add IODeviceGateway-api/IODeviceGateway-api.csproj
# dotnet sln add clock-feature/clock-feature.csproj
dotnet sln add IODeviceGateway-tests/IODeviceGateway-tests.csproj