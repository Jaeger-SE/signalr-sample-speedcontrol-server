using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zwedze.Demo.SignalR.SpeedControl.Contracts;

namespace Zwedze.Demo.SignalR.SpeedControl.Realtime.HostConfig
{
    public static class ServiceCollectionConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var signalrConfig = configuration["SignalR:Domain"];
            services.AddSignalR();
            services.AddHealthChecks()
                .AddSignalRHub($"{signalrConfig}/{HubUris.NotificationsUri}", "notifications-hub")
                .AddSignalRHub($"{signalrConfig}/{HubUris.RadarUri}", "radar-hub");

            services.AddHealthChecksUI(setup =>
                {
                    setup.AddHealthCheckEndpoint("HealthCheck", "/health");
                    setup.SetEvaluationTimeInSeconds(10);
                })
                .AddInMemoryStorage();
        }
    }
}