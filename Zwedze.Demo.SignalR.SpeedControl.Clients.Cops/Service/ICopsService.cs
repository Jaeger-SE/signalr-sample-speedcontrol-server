using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service
{
    public interface ICopsService : IAsyncDisposable
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        
        Task StartAssignationOn(string radarId);
        Task StopAssignationOn(string radarId);
    }
}