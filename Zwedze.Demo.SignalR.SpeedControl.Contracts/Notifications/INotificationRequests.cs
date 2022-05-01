using System.Threading.Tasks;
using Zwedze.Framework.SignalR.Client;

namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications
{
    /// <summary>
    ///     Defines the SignalR Hub methods available for clients to call.
    /// </summary>
    public interface INotificationRequests : IRealtimeRequests
    {
        /// <summary>
        ///     Report a new measured speed.
        /// </summary>
        /// <param name="speedViolationReport">The speed report; e.g: contains the speed, the car plate.</param>
        Task SendSpeedViolation(SpeedViolationReport speedViolationReport);

        /// <summary>
        ///     Register the cop on a specific radar. 
        /// </summary>
        /// <param name="assignationModel">The assignation model; e.g: contains the radar id</param>
        Task StartAssignation(AssignationModel assignationModel);
        
        /// <summary>
        ///     Release the cop from her/him last assignation. 
        /// </summary>
        /// <param name="assignationModel">The assignation model; e.g: contains the radar id</param>
        Task StopAssignation(AssignationModel assignationModel);
    }
}