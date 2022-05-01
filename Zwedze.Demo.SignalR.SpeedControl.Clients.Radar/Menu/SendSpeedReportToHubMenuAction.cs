using System;
using System.Threading.Tasks;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Menu
{
    internal class SendSpeedReportToHubMenuAction : IMenuAction
    {
        private readonly IRadarService _radarService;

        public SendSpeedReportToHubMenuAction(IRadarService radarService)
        {
            _radarService = radarService;
        }

        public string UserResponse => "flash";
        public string Description => "Send a flash message with a specific speed";

        public Func<Task> Action =>
            () =>
            {
                string speedStr, carPlate;
                double speed;
                do
                {
                    Console.Write("> Speed: ");
                    speedStr = Console.ReadLine();
                    Console.Write("> Car plate (leave blank for default value): ");
                    carPlate = Console.ReadLine();
                } while (!double.TryParse(speedStr, out speed));

                return _radarService.FlashCar(speed, carPlate);
            };
    }
}