using System;
using System.Threading.Tasks;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu
{
    internal class StopAssignationForRueDeLaLoiToHubMenuAction : IMenuAction
    {
        private readonly ICopsService _copsService;

        public StopAssignationForRueDeLaLoiToHubMenuAction(ICopsService copsService)
        {
            _copsService = copsService;
        }

        public string UserResponse => "stop loi";
        public string Description => "Stop assignation for Rue de la Loi";

        public Func<Task> Action => () => _copsService.StopAssignationOn("rue-de-la-loi");
    }
}