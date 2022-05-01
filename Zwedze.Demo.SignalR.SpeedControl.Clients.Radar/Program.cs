using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Menu;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar
{
    internal static class Program
    {
        private static readonly IDictionary<string, string> ArgsSwitchMapping = new Dictionary<string, string>
        {
            {"--id", "Radar:Id"},
            {"--hub-uri", "Realtime:HubUri"}
        };

        private static async Task Main(string[] args)
        {
            var host = CreateHost(args);
            await host.StartAsync();

            var shouldExit = false;
            do
            {
                var menuSelection = PrintMenu(host);
                if (menuSelection == ExitMenuAction.Instance || menuSelection.Action == null)
                    shouldExit = true;
                else
                    await menuSelection.Action();
            } while (!shouldExit);

            await host.StopAsync();
        }

        private static IMenuAction PrintMenu(IHost host)
        {
            var menu = host.Services.GetService<IMenuAction[]>() ?? Array.Empty<IMenuAction>();
            
            Console.WriteLine("\n======\nWhat do ou wanna do ?");
            foreach (var menuItem in menu) Console.WriteLine($"({menuItem.UserResponse}) {menuItem.Description}");

            Console.Write(@"> ");
            var response = Console.ReadLine();
            var selection = menu.SingleOrDefault(mi => mi.UserResponse == response);
            return selection ?? PrintMenu(host);
        }

        private static LoggerConfiguration GetLoggerConfiguration()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .Enrich.FromLogContext();
        }

        private static IHost CreateHost(string[] args)
        {
            var logger = GetLoggerConfiguration()
                .CreateLogger();

            logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(services => services.AddHostedService<RadarHost>())
                .ConfigureAppConfiguration(builder => { builder.AddCommandLine(args, ArgsSwitchMapping); })
                .ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacModule()); })
                .UseSerilog((_, loggerConfiguration) =>
                    loggerConfiguration.MinimumLevel.Debug().WriteTo.Console().Enrich.FromLogContext())
                .Build();

            return host;
        }
    }
}