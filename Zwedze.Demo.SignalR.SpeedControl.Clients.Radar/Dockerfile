FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src

COPY ["Zwedze.Demo.SignalR.SpeedControl.Clients.Radar/Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.csproj", "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar/"]
COPY ["Zwedze.Demo.SignalR.SpeedControl.Contracts/Zwedze.Demo.SignalR.SpeedControl.Contracts.csproj", "Zwedze.Demo.SignalR.SpeedControl.Contracts/"]
RUN dotnet restore "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar/Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.csproj"

COPY ["Zwedze.Demo.SignalR.SpeedControl.Clients.Radar/", "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar/"]
COPY ["Zwedze.Demo.SignalR.SpeedControl.Contracts/", "Zwedze.Demo.SignalR.SpeedControl.Contracts/"]

WORKDIR "/src/Zwedze.Demo.SignalR.SpeedControl.Clients.Radar"
RUN dotnet build "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:5.0 as final

ARG ID
ENV ID ${ID}
ARG RADAR_HUB_URI
ENV RADAR_HUB_URI ${RADAR_HUB_URI}

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.dll"]
CMD ["--id=${ID}", "--radar-hub-uri=${RADAR_HUB_URI}"]