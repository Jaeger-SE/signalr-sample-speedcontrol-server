using System;
using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu
{
    internal interface IMenuAction
    {
        string UserResponse { get; }
        string Description { get; }
        Func<Task> Action { get; }
    }
}