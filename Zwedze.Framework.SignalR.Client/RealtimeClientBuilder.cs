using System;
using Microsoft.AspNetCore.SignalR.Client;
using Zwedze.Framework.SignalR.Client.Internal;

namespace Zwedze.Framework.SignalR.Client
{
    public static class RealtimeClientFactory
    {
        public static IRealtimeClient<TRequests, TEvents> Build<TRequests, TEvents>(Action<HubConnectionBuilder> configureBuilder) 
            where TRequests : IRealtimeRequests
            where TEvents : IRealtimeEvents
        {
            var hubConnectionBuilder = new HubConnectionBuilder();
            configureBuilder(hubConnectionBuilder);
            return new SignalRClient<TRequests, TEvents>(hubConnectionBuilder.Build());
        }
    }
}