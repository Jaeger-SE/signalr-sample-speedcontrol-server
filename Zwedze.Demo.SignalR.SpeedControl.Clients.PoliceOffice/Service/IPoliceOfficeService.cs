using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.Service
{
    public interface IPoliceOfficeService : IAsyncDisposable
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}