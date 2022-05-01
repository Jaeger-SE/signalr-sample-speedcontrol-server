using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Menu;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Radar.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Radar
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new RadarService(x.Resolve<IConfiguration>()["Realtime:HubUri"],
                    x.Resolve<IConfiguration>()["Radar:Id"],
                    x.Resolve<ILogger<RadarService>>()))
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder
                .Register(x => new SendSpeedReportToHubMenuAction(x.Resolve<IRadarService>()))
                .AsImplementedInterfaces();
            builder
                .RegisterInstance(ExitMenuAction.Instance)
                .AsImplementedInterfaces();
        }
    }
}