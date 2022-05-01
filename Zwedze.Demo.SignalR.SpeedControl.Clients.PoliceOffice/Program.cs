using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice
{
    internal class Program
    {
        private static readonly IDictionary<string, string> ArgsSwitchMapping = new Dictionary<string, string>
        {
            {"--id", "PoliceOffice:Id"},
            {"--radars", "PoliceOffice:ObservedRadars"},
            {"--hub-uri", "Realtime:Hub"}
        };

        private static async Task Main(string[] args)
        {
            await CreateHost(args);
        }

        private static LoggerConfiguration GetLoggerConfiguration()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .Enrich.FromLogContext();
        }

        private static Task CreateHost(string[] args)
        {
            var logger = GetLoggerConfiguration()
                .CreateLogger();

            logger.Information("Application Starting");

            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services => services.AddHostedService<PoliceOfficeHost>())
                .ConfigureAppConfiguration(builder => { builder.AddCommandLine(args, ArgsSwitchMapping); })
                .ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacModule()); })
                .UseSerilog((_, loggerConfiguration) =>
                    loggerConfiguration.MinimumLevel.Debug().WriteTo.Console().Enrich.FromLogContext())
                .RunConsoleAsync();
        }
    }
}