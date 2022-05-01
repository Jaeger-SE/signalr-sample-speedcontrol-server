using System.Threading.Tasks;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar
{
    /// <summary>
    ///     Defines the SignalR Hub methods available for clients to call.
    /// </summary>
    public interface IRadarRequests : IRealtimeRequests
    {
        /// <summary>
        ///     Report a new measured speed.
        /// </summary>
        /// <param name="report">The speed report; e.g: contains the speed, the car plate or the location.</param>
        Task ReportSpeed(SpeedReport report);

        /// <summary>
        ///     Configure the client as a receiver for new speed report events.
        /// </summary>
        /// <param name="listening">
        ///     Information required to select the right group for the client and avoid broadcasting him
        ///     not-expected-reports.
        /// </param>
        Task ListeningOn(SpeedReportListening listening);
    }
}