FROM microsoft/aspnetcore-build as builder
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

RUN mkdir -p /root/src/app
WORKDIR /root/src/app
COPY . api
WORKDIR /root/src/app/api

RUN dotnet restore ./IODeviceGateway.api.csproj  
#RUN dotnet publish -c release -o published
RUN dotnet publish -c release -o published -r linux-arm

#FROM microsoft/aspnetcore
FROM microsoft/dotnet:2.0.0-runtime-stretch-arm32v7
WORKDIR /app/

COPY --from=builder /root/src/app/api/published .

CMD ["dotnet", "IODeviceGateway.api.dll"]

# Run this Docker file with 
# docker build -t masternz/simple-api:<1.0> .
# Push to docker hub
#docker push masternz/simple:<1.0>
# run the docker image
#docker run -p <externalPort>:<internalPort> masternz/simple-api:1.0