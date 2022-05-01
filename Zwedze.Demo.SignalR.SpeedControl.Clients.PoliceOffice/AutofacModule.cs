using System.Diagnostics;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.ApiService;
using Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice.Service;

namespace Zwedze.Demo.SignalR.SpeedControl.Clients.PoliceOffice
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x =>
                {
                    Debug.WriteLine(((IConfigurationRoot) x.Resolve<IConfiguration>()).GetDebugView());
                    return new PoliceOfficeService(
                        x.Resolve<IConfiguration>()["Realtime:Hub"],
                        x.Resolve<IConfiguration>()["PoliceOffice:Id"],
                        x.Resolve<IConfiguration>()["PoliceOffice:ObservedRadars"],
                        x.Resolve<IInfractionApiService>(),
                        x.Resolve<ILogger<PoliceOfficeService>>());
                })
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<StubInfractionApiService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}