using System;
using System.Threading.Tasks;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu
{
    internal class StartAssignationForRueNeuveToHubMenuAction : IMenuAction
    {
        private readonly ICopsService _copsService;

        public StartAssignationForRueNeuveToHubMenuAction(ICopsService copsService)
        {
            _copsService = copsService;
        }

        public string UserResponse => "start neuve";
        public string Description => "Start assignation for Rue Neuve";

        public Func<Task> Action => () => _copsService.StartAssignationOn("rue-neuve");
    }
}