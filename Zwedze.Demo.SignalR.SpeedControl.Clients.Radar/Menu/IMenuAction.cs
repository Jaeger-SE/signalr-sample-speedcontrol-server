using System;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Menu
{
    internal interface IMenuAction
    {
        string UserResponse { get; }
        string Description { get; }
        Func<Task> Action { get; }
    }
}