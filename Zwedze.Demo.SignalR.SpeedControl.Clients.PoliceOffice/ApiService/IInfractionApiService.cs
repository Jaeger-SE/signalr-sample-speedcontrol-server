using System.Threading.Tasks;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.ApiService
{
    public interface IInfractionApiService
    {
        Task LogFileAsync(string carPlate, double speed, string radarId);
    }
}