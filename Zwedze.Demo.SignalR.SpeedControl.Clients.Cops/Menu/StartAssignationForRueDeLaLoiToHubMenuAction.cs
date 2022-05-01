using System;
using System.Threading.Tasks;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu
{
    internal class StartAssignationForRueDeLaLoiToHubMenuAction : IMenuAction
    {
        private readonly ICopsService _copsService;

        public StartAssignationForRueDeLaLoiToHubMenuAction(ICopsService copsService)
        {
            _copsService = copsService;
        }

        public string UserResponse => "start loi";
        public string Description => "Start assignation for Rue de la Loi";

        public Func<Task> Action => () => _copsService.StartAssignationOn("rue-de-la-loi");
    }
}