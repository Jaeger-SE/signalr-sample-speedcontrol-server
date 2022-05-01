using System;
using System.Threading.Tasks;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu
{
    internal class StopAssignationForRueNeuveToHubMenuAction : IMenuAction
    {
        private readonly ICopsService _copsService;

        public StopAssignationForRueNeuveToHubMenuAction(ICopsService copsService)
        {
            _copsService = copsService;
        }

        public string UserResponse => "stop neuve";
        public string Description => "Stop assignation for Rue Neuve";

        public Func<Task> Action => () => _copsService.StopAssignationOn("rue-neuve");
    }
}