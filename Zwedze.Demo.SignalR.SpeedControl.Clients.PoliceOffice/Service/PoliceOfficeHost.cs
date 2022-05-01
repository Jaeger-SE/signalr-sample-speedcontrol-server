using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.Service
{
    public class PoliceOfficeHost : IHostedService, IAsyncDisposable
    {
        private readonly ILogger _logger;
        private readonly Func<IPoliceOfficeService> _policeOfficeServiceFactory;
        private IPoliceOfficeService _policeOfficeService;

        public PoliceOfficeHost(Func<IPoliceOfficeService> policeOfficeServiceFactory, ILogger<PoliceOfficeHost> logger)
        {
            _policeOfficeServiceFactory = policeOfficeServiceFactory;
            _logger = logger;
        }

        public ValueTask DisposeAsync()
        {
            // return _policeOfficeService.DisposeAsync();
            return ValueTask.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== {HostName} starting", nameof(PoliceOfficeHost));
            _policeOfficeService = _policeOfficeServiceFactory();
            return _policeOfficeService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== Stopping {HostName}", nameof(PoliceOfficeHost));
            return _policeOfficeService.StopAsync(cancellationToken);
        }
    }
}