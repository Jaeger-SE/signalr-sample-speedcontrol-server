FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src

COPY ["Zwedze.Demo.SignalR.SpeedControl.Realtime/Zwedze.Demo.SignalR.SpeedControl.Realtime.csproj", "Zwedze.Demo.SignalR.SpeedControl.Realtime/"]
COPY ["Zwedze.Demo.SignalR.SpeedControl.Contracts/Zwedze.Demo.SignalR.SpeedControl.Contracts.csproj", "Zwedze.Demo.SignalR.SpeedControl.Contracts/"]
RUN dotnet restore "Zwedze.Demo.SignalR.SpeedControl.Realtime/Zwedze.Demo.SignalR.SpeedControl.Realtime.csproj"

COPY ["Zwedze.Demo.SignalR.SpeedControl.Realtime/", "Zwedze.Demo.SignalR.SpeedControl.Realtime/"]
COPY ["Zwedze.Demo.SignalR.SpeedControl.Contracts/", "Zwedze.Demo.SignalR.SpeedControl.Contracts/"]

WORKDIR "/src/Zwedze.Demo.SignalR.SpeedControl.Realtime"
RUN dotnet build "Zwedze.Demo.SignalR.SpeedControl.Realtime.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "Zwedze.Demo.SignalR.SpeedControl.Realtime.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:5.0 as final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zwedze.Demo.SignalR.SpeedControl.Realtime.dll"]