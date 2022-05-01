using System;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Menu
{
    internal class ExitMenuAction : IMenuAction
    {
        public static readonly ExitMenuAction Instance = new();

        private ExitMenuAction()
        {
        }

        public string UserResponse { get; } = "q";
        public string Description { get; } = "Exit";

        public Func<Task> Action => () => Task.CompletedTask;
    }
}