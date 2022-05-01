using System.Threading.Tasks;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar
{
    /// <summary>
    ///     Defines the events that client may receive from the hub.
    /// </summary>
    public interface IRadarEvents : IRealtimeEvents
    {
        /// <summary>
        ///     When a new speed report has been emit by a radar monitored by the client.
        /// </summary>
        /// <param name="report">The speed report; e.g: contains the speed, the car plate or the location.</param>
        Task OnSpeedReportReceived(SpeedReport report);
    }
}