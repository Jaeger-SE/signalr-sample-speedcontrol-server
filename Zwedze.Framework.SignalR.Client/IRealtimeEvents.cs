using System.Threading.Tasks;

namespace Zwedze.Framework.SignalR.Client
{
    public interface IRealtimeEvents
    {
        Task OnSessionExpired();
    }
}