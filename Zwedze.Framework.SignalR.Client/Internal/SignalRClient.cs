using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Zwedze.Framework.SignalR.Client.Internal
{
    internal class SignalRClient<TRequests, TEvents> : IRealtimeClient<TRequests, TEvents>
        where TRequests : IRealtimeRequests
        where TEvents : IRealtimeEvents
    {
        private readonly HubConnection _connection;

        public SignalRClient(HubConnection connection)
        {
            _connection = connection;
        }

        /// <inheritdoc />
        public Task SendAsync(Expression<Action<TRequests>> call, CancellationToken cancellationToken = default)
        {
            var invocation = call.GetInvocation();
            if (!invocation.ParameterValues.Any())
                return _connection.InvokeAsync(invocation.MethodName, cancellationToken);

            if (invocation.ParameterValues.Length == 1)
                return _connection.InvokeAsync(invocation.MethodName, invocation.ParameterValues.First(),
                    cancellationToken);

            return _connection.InvokeAsync(invocation.MethodName, invocation.ParameterValues, cancellationToken);
        }

        /// <inheritdoc />
        public void BindEvent<T1, T2, T3>(Expression<Func<TEvents, Func<T1, T2, T3, Task>>> eventToBind,
            Func<T1, T2, T3, Task> handler)
        {
            var methodName = eventToBind.GetBinding()?.MethodName;
            _connection.On(methodName, handler);
        }

        /// <inheritdoc />
        public void BindEvent<T1, T2, T3, T4>(Expression<Func<TEvents, Func<T1, T2, T3, T4, Task>>> eventToBind,
            Func<T1, T2, T3, T4, Task> handler)
        {
            var methodName = eventToBind.GetBinding()?.MethodName;
            _connection.On(methodName, handler);
        }

        /// <inheritdoc />
        public ValueTask DisposeAsync()
        {
            return _connection.DisposeAsync();
        }

        /// <inheritdoc />
        public void BindEvent<T>(Expression<Func<TEvents, Func<T, Task>>> eventToBind, Func<T, Task> handler)
        {
            var methodName = eventToBind.GetBinding()?.MethodName;
            _connection.On(methodName, handler);
        }

        /// <inheritdoc />
        public void BindEvent<T1, T2>(Expression<Func<TEvents, Func<T1, T2, Task>>> eventToBind,
            Func<T1, T2, Task> handler)
        {
            var methodName = eventToBind.GetBinding()?.MethodName;
            _connection.On(methodName, handler);
        }

        /// <inheritdoc />
        public void BindEvent(Expression<Func<TEvents, Func<Task>>> eventToBind, Func<Task> handler)
        {
            var methodName = eventToBind.GetBinding()?.MethodName;
            _connection.On(methodName, handler);
        }

        /// <see cref="IRealtimeClient{TRequests, TEvents}" />
        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            return _connection.StartAsync(cancellationToken);
        }

        public Task DisconnectAsync(CancellationToken cancellationToken = default)
        {
            return _connection.StopAsync(cancellationToken);
        }

        public void OnClose(Func<Exception, Task> onCloseHandler)
        {
            _connection.Closed += onCloseHandler;
        }
        
        public void OnReconnected(Func<string, Task> onReconnectedHandler)
        {
            _connection.Reconnected += onReconnectedHandler;
        }
        
        public void OnReconnecting(Func<Exception, Task> onReconnectingHandler)
        {
            _connection.Reconnecting += onReconnectingHandler;
        }
    }
}