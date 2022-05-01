using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar;

namespace Zwedze.Demo.SignalR.SpeedControl.Realtime.Hubs
{
    public class RadarHub : Hub<IRadarEvents>, IRadarRequests
    {
        public Task ReportSpeed(SpeedReport report)
        {
            // The group of connection that have been configured to listen on this radar report.
            var group = Clients.Group(report.RadarId);
            // Will broadcast the speed report to all clients of this group.
            return group.OnSpeedReportReceived(report);
        }

        public Task ListeningOn(SpeedReportListening listening)
        {
            // The client connection id. Each connection to the hub has its own connectionId.
            // Storing the connectionId in a group will bind the client with a group.
            var connectionId = Context.ConnectionId;
            return Groups.AddToGroupAsync(connectionId, listening.RadarId);
        }
    }
}