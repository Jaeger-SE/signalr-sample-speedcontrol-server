using System.Diagnostics;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Menu;
using Zwedze.Demo.SignalR.SpeedControl.Clients.Cops.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.Cops
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x =>
                {
                    Debug.WriteLine(((IConfigurationRoot) x.Resolve<IConfiguration>()).GetDebugView());
                    return new CopsService(
                        x.Resolve<IConfiguration>()["Realtime:Hub"],
                        x.Resolve<IConfiguration>()["Cops:Id"],
                        x.Resolve<ILogger<CopsService>>());
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            
            builder.Register(x => new StartAssignationForRueNeuveToHubMenuAction(x.Resolve<ICopsService>())).AsImplementedInterfaces();
            builder.Register(x => new StartAssignationForRueDeLaLoiToHubMenuAction(x.Resolve<ICopsService>())).AsImplementedInterfaces();
            builder.Register(x => new StopAssignationForRueNeuveToHubMenuAction(x.Resolve<ICopsService>())).AsImplementedInterfaces();
            builder.Register(x => new StopAssignationForRueDeLaLoiToHubMenuAction(x.Resolve<ICopsService>())).AsImplementedInterfaces();
            builder
                .RegisterInstance(ExitMenuAction.Instance)
                .AsImplementedInterfaces();
        }
    }
}