using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zwedze.Demo.SignalR.SpeedControl.Contracts;
using Zwedze.Demo.SignalR.SpeedControl.Realtime.Hubs;

namespace Zwedze.Demo.SignalR.SpeedControl.Realtime.HostConfig
{
    public static class MiddlewareConfiguration
    {
        public static void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // To enable the UseEndpoints
            app.UseRouting();
            
            app.UseEndpoints(ep =>
            {
                // Configure active signalR hubs
                ep.MapHub<RadarHub>(HubUris.RadarUri);
                ep.MapHub<CopsNotificationsHub>(HubUris.NotificationsUri);

                // Configure health monitoring endpoint
                ep.MapHealthChecksUI(option => option.UIPath = "/health-ui");
            });

            // Configure what happens on app shutdown
            // Ensure any buffered events are sent at shutdown
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}