using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications;

namespace Zwedze.Demo.SignalR.SpeedControl.Realtime.Hubs
{
    public class CopsNotificationsHub : Hub<INotificationEvents>, INotificationRequests
    {
        public Task SendSpeedViolation(SpeedViolationReport speedViolationReport)
        {
            return Clients.Group(FormatRadarGroupId(speedViolationReport.RadarId)).OnSpeedLimitViolation(speedViolationReport);
        }

        public Task StartAssignation(AssignationModel assignationModel)
        {
            var connectionId = Context.ConnectionId;
            return Groups.AddToGroupAsync(connectionId, FormatRadarGroupId(assignationModel.RadarId));
        }

        public Task StopAssignation(AssignationModel assignationModel)
        {
            var connectionId = Context.ConnectionId;
            return Groups.RemoveFromGroupAsync(connectionId, FormatRadarGroupId(assignationModel.RadarId));
        }

        private static string FormatRadarGroupId(string radarId)
        {
            return $"cops:{radarId}";
        }
    }
}
