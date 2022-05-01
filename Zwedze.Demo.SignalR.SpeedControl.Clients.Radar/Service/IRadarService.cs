using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service
{
    public interface IRadarService: IAsyncDisposable
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        
        Task FlashCar(double speed, string carPlate = default, CancellationToken cancellationToken = default);
    }
}