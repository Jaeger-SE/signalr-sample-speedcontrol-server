using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Zwedze.Framework.SignalR.Client
{
    /// <summary>
    ///     Connection management and dispatch for clients.
    ///     <para>Represents an established connection</para>
    ///     <para>Dispose of the hub client to close the connection</para>
    /// </summary>
    public interface IRealtimeClient<TRequests, TEvents> : IAsyncDisposable, IRealtimeClientEvents<TEvents>
        where TRequests : IRealtimeRequests
        where TEvents : IRealtimeEvents
    {
        /// <summary>
        ///     Send a message to the hub, using a client-to-hub contract method
        /// </summary>
        /// <param name="call">Expression calling to hub. Use like <code>hub => hub.MyMethod("hello", "world")</code></param>
        /// <param name="cancellationToken">
        ///     The token to monitor for cancellation requests. The default value is
        ///     <see cref="CancellationToken.None" />.
        /// </param>
        Task SendAsync(Expression<Action<TRequests>> call, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Connect to the the hub 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ConnectAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Disconnect to the the hub 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DisconnectAsync(CancellationToken cancellationToken = default);

        void OnClose(Func<Exception, Task> onCloseHandler);

        void OnReconnected(Func<string, Task> onReconnectedHandler);

        void OnReconnecting(Func<Exception, Task> onReconnectingHandler);
    }
}