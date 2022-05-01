using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zwedze.Demo.SignalR.SpeedControl.Realtime.HostConfig;

namespace Zwedze.Demo.SignalR.SpeedControl.Realtime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builder) => builder.AddJsonFile("appsettings.json", false))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        ServiceCollectionConfiguration.Configure(services, context.Configuration);
                    });
                    webBuilder.Configure((_, appBuilder) =>
                    {
                        var applicationLifetime = appBuilder.ApplicationServices.GetService<IHostApplicationLifetime>();
                        MiddlewareConfiguration.Configure(appBuilder, applicationLifetime);
                    });
                });
        }
    }
}