using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Contracts;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service
{
    public class CopsService : ICopsService
    {
        private readonly string _copId;
        private readonly ILogger _logger;
        private readonly IRealtimeClient<INotificationRequests, INotificationEvents> _realtimeClient;

        public CopsService(string hubServerUri, string copId, ILogger<CopsService> logger)
        {
            _copId = copId;
            _logger = logger;
            _realtimeClient =
                RealtimeClientFactory.Build<INotificationRequests, INotificationEvents>(x =>
                    x.WithUrl($"{hubServerUri}/{HubUris.NotificationsUri}").WithAutomaticReconnect());
        }

        public async ValueTask DisposeAsync()
        {
            await _realtimeClient.DisposeAsync();
        }

        public Task StartAssignationOn(string radarId)
        {
            var assignationModel = new AssignationModel
            {
                RadarId = radarId
            };
            return _realtimeClient.SendAsync(x => x.StartAssignation(assignationModel));
        }

        public Task StopAssignationOn(string radarId)
        {
            var assignationModel = new AssignationModel
            {
                RadarId = radarId
            };
            return _realtimeClient.SendAsync(x => x.StopAssignation(assignationModel));
        }

        #region real-time configuration

        private void ConfigureConnectionEvents()
        {
            _realtimeClient.OnReconnecting(OnReconnecting);
            _realtimeClient.OnClose(OnClose);
        }

        private void ConfigureEvents()
        {
            _realtimeClient.BindEvent<SpeedViolationReport>(events => events.OnSpeedLimitViolation,
                OnSpeedLimitViolation);
        }

        private Task OnSpeedLimitViolation(SpeedViolationReport speedViolationReport)
        {
            _logger.LogInformation($"New speed violation received. Intercept '{speedViolationReport.CarPlate}' - Speed: {speedViolationReport.SpeedReported}.");
            return Task.CompletedTask;
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
            _logger.LogInformation("== {ServiceName} starting", nameof(CopsService));
            _logger.LogInformation(Art.Cops, _copId);
            ConfigureConnectionEvents();
            ConfigureEvents();

            return _realtimeClient.ConnectAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("== Stopping {ServiceName}", nameof(CopsService));
            return _realtimeClient.DisconnectAsync(cancellationToken);
        }

        #endregion
    }
}