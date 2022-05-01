using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.ApiService
{
    internal class StubInfractionApiService : IInfractionApiService
    {
        private ILogger _logger;

        public StubInfractionApiService(ILogger<IInfractionApiService> logger)
        {
            _logger = logger;
        }

        public Task LogFileAsync(string carPlate, double speed, string radarId)
        {
            _logger.LogInformation($"Call the infraction api to log a speed violation of {speed} for {carPlate} at {radarId}");
            // Since this demo is based on signalr, no time wasted to build an API to receive that call.
            return Task.CompletedTask;
        }
    }
}