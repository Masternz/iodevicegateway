FROM microsoft/aspnetcore-build as builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN mkdir -p /root/src/app
WORKDIR /root/src/app
COPY . api
WORKDIR /root/src/app/api

RUN dotnet restore ./IODeviceEmulator.api.csproj  
RUN dotnet publish -c release -o published
#RUN dotnet publish -c release -o published -r linux-arm

FROM microsoft/aspnetcore
#FROM microsoft/dotnet:2.0.0-runtime-stretch-arm32v7
WORKDIR /app/

COPY --from=builder /root/src/app/api/published .

CMD ["dotnet", "IODeviceEmulator.api.dll"]

# Create a network
# docker network create --driver bridge iot-net
# Run this Docker file with 
# docker build -t masternz/iodevice-emulator-api:1.2 .
# Push to docker hub
#docker push masternz/iodevice-emulator-api:1.2
# run the docker image
#docker run -d --name iot-emulator --network iot-net -p 8088:8088 masternz/iodevice-emulator-api:1.2