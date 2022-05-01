using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service
{
    public class CopsHost : IHostedService, IAsyncDisposable
    {
        private readonly ILogger _logger;
        private readonly Func<ICopsService> _copsServiceFactory;
        private ICopsService _copsService;

        public CopsHost(Func<ICopsService> copsServiceFactory, ILogger<CopsHost> logger)
        {
            _copsServiceFactory = copsServiceFactory;
            _logger = logger;
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== {HostName} starting", nameof(CopsHost));
            _copsService = _copsServiceFactory();
            return _copsService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("== Stopping {HostName}", nameof(CopsHost));
            return _copsService.StopAsync(cancellationToken);
        }
    }
}