FROM microsoft/aspnetcore-build as builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN mkdir -p /root/src/app
WORKDIR /root/src/app
COPY . api
WORKDIR /root/src/app/api

RUN dotnet restore ./IODeviceGateway.api.csproj  
RUN dotnet publish -c release -o published
#RUN dotnet publish -c release -o published -r linux-arm

FROM microsoft/aspnetcore
#FROM microsoft/dotnet:2.0.0-runtime-stretch-arm32v7
WORKDIR /app/

COPY --from=builder /root/src/app/api/published .

CMD ["dotnet", "IODeviceGateway.api.dll"]

# Run this Docker file with 
# docker build -t masternz/devicegateway-api:1.1 .
# Push to docker hub
#docker push masternz/devicegateway-api:1.1
# run the docker image
#docker run -d --env ioDeviceBaseURI=http://iot-emulator:8088 --name iot-gateway --network iot-net -p 8089:8089 masternz/devicegateway-api:1.1