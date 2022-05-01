using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service
{
    public class RadarHost : IHostedService, IAsyncDisposable
    {
        private readonly ILogger _logger;
        private readonly Func<IRadarService> _radarServiceFactory;
        private IRadarService _radarService;

        public RadarHost(Func<IRadarService> radarServiceFactory, ILogger<RadarHost> logger)
        {
            _radarServiceFactory = radarServiceFactory;
            _logger = logger;
        }

        public ValueTask DisposeAsync()
        {
            return _radarService.DisposeAsync();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== Host starting");
            _radarService = _radarServiceFactory();
            return _radarService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== Stopping host");
            return _radarService.StopAsync(cancellationToken);
        }
    }
}