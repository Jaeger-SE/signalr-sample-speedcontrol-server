using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Contracts;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service
{
    public class RadarService : IRadarService
    {
        private readonly ILogger _logger;
        private readonly CarPlateGenerator _plateGenerator;
        private readonly string _radarId;
        private readonly IRealtimeClient<IRadarRequests, IRadarEvents> _realtimeClient;

        public RadarService(string hubServerUri, string radarId, ILogger<RadarService> logger)
        {
            _radarId = radarId;
            _logger = logger;
            _plateGenerator = new CarPlateGenerator();
            _realtimeClient =
                RealtimeClientFactory.Build<IRadarRequests, IRadarEvents>(x =>
                    x.WithUrl($"{hubServerUri}/{HubUris.RadarUri}").WithAutomaticReconnect());
        }

        public async ValueTask DisposeAsync()
        {
            await _realtimeClient.DisposeAsync();
        }

        public Task FlashCar(double speed, string carPlate = default, CancellationToken cancellationToken = default)
        {
            var speedReport = new SpeedReport
            {
                Speed = speed,
                CarPlate = string.IsNullOrWhiteSpace(carPlate) ? _plateGenerator.Generate() : carPlate,
                RadarId = _radarId
            };
            return _realtimeClient.SendAsync(x => x.ReportSpeed(speedReport), cancellationToken);
        }

        #region real-time configuration

        private void ConfigureConnectionEvents()
        {
            _realtimeClient.OnReconnecting(OnReconnecting);
            _realtimeClient.OnClose(OnClose);
        }

        private Task OnReconnecting(Exception exception)
        {
            _logger.LogWarning("SignalR connection is reconnecting. Exception is {Exception}", exception);
            return Task.CompletedTask;
        }

        private Task OnClose(Exception exception)
        {
            if (exception != null)
                // Then the closed event is not intentional
                _logger.LogError("SignalR connection is closed");

            return Task.CompletedTask;
        }


        #endregion

        #region service lifetime managment

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("== {ServiceName} starting", nameof(RadarService));
            _logger.LogInformation(Art.Radar, _radarId);
            ConfigureConnectionEvents();

            return _realtimeClient.ConnectAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("== Stopping {ServiceName}", nameof(RadarService));
            await _realtimeClient.DisconnectAsync(cancellationToken);
        }

        #endregion
    }
}