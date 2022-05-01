using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.ApiService;
using Zwedze.Demo.SignalR.SpeedControl.Contracts;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.Service
{
    public class PoliceOfficeService : IPoliceOfficeService
    {
        private readonly ILogger _logger;
        private readonly IRealtimeClient<INotificationRequests, INotificationEvents> _notificationsRealtimeClient;
        private readonly IList<string> _observedRadarIds;
        private readonly string _policeOfficeId;
        private readonly IInfractionApiService _infractionApiService;
        private readonly IRealtimeClient<IRadarRequests, IRadarEvents> _radarRealtimeClient;

        public PoliceOfficeService(string hubServerUri, string policeOfficeId,
            string observedRadarIdsStr, IInfractionApiService infractionApiService, ILogger<PoliceOfficeService> logger)
        {
            _policeOfficeId = policeOfficeId;
            _infractionApiService = infractionApiService;
            _observedRadarIds = observedRadarIdsStr.Split(",");
            _logger = logger;
            _radarRealtimeClient =
                RealtimeClientFactory.Build<IRadarRequests, IRadarEvents>(x =>
                    x.WithUrl($"{hubServerUri}/{HubUris.RadarUri}").WithAutomaticReconnect());
            _notificationsRealtimeClient =
                RealtimeClientFactory.Build<INotificationRequests, INotificationEvents>(x =>
                    x.WithUrl($"{hubServerUri}/{HubUris.NotificationsUri}").WithAutomaticReconnect());
        }

        public async ValueTask DisposeAsync()
        {
            await _radarRealtimeClient.DisposeAsync();
        }

        #region real-time configuration

        private void ConfigureConnectionEvents()
        {
            _radarRealtimeClient.OnReconnecting(OnReconnecting);
            _radarRealtimeClient.OnClose(OnClose);
            _notificationsRealtimeClient.OnReconnecting(OnReconnecting);
            _notificationsRealtimeClient.OnClose(OnClose);
        }

        private void ConfigureEvents()
        {
            _radarRealtimeClient.BindEvent<SpeedReport>(events => events.OnSpeedReportReceived, OnSpeedReport);
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

        private async Task OnSpeedReport(SpeedReport speedReport)
        {
            _logger.LogInformation("New speed report: {Json}", JsonSerializer.Serialize(speedReport));
            if(speedReport.Speed > 35)
            {
                // Speed violation
                await _infractionApiService.LogFileAsync(speedReport.CarPlate, speedReport.Speed, speedReport.RadarId);
                // Send a cop after the car
                await _notificationsRealtimeClient.SendAsync(x => x.SendSpeedViolation(new SpeedViolationReport
                {
                    CarPlate = speedReport.CarPlate,
                    RadarId = speedReport.RadarId,
                    SpeedReported = speedReport.Speed
                }));
            }
        }

        #endregion

        #region service lifetime managment

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("== {ServiceName} starting", nameof(PoliceOfficeService));
            _logger.LogInformation(Art.Police, _policeOfficeId);
            ConfigureConnectionEvents();
            ConfigureEvents();

            await _radarRealtimeClient.ConnectAsync(cancellationToken);
            await _notificationsRealtimeClient.ConnectAsync(cancellationToken);

            // Register this instance as a client for radars
            foreach (var radarId in _observedRadarIds)
            {
                var listeningModel = new SpeedReportListening
                {
                    RadarId = radarId
                };
                await _radarRealtimeClient.SendAsync(x => x.ListeningOn(listeningModel), cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("== Stopping {ServiceName}", nameof(PoliceOfficeService));
            await _radarRealtimeClient.DisconnectAsync(cancellationToken);
            await _notificationsRealtimeClient.DisconnectAsync(cancellationToken);
        }

        #endregion
    }
}