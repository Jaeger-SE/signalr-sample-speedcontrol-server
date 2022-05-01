using System.Threading.Tasks;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications
{
    public interface INotificationEvents : IRealtimeEvents
    {
        Task OnSpeedLimitViolation(SpeedViolationReport speedViolationReport);
    }
}