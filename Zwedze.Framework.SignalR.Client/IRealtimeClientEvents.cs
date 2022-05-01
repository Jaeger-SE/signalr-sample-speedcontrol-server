using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Zwedze.Framework.SignalR.Client
{
    public interface IRealtimeClientEvents<TEvents> where TEvents : IRealtimeEvents
    {
        /// <summary>
        ///     Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEvent(Expression<Func<TEvents, Func<Task>>> eventToBind, Func<Task> handler);

        #region BindEventHandler<> multi argument variants

        /// <summary>
        ///     Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEvent<T>(Expression<Func<TEvents, Func<T, Task>>> eventToBind, Func<T, Task> handler);

        /// <summary>
        ///     Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEvent<T1, T2>(Expression<Func<TEvents, Func<T1, T2, Task>>> eventToBind, Func<T1, T2, Task> handler);

        /// <summary>
        ///     Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEvent<T1, T2, T3>(
            Expression<Func<TEvents, Func<T1, T2, T3, Task>>> eventToBind, Func<T1, T2, T3, Task> handler);

        /// <summary>
        ///     Bind to a hub event. When the hub send a message of the given type, the handler will be invoked
        /// </summary>
        /// <param name="eventToBind">The event method exposed by the hub</param>
        /// <param name="handler">The method that should handle the event</param>
        void BindEvent<T1, T2, T3, T4>(
            Expression<Func<TEvents, Func<T1, T2, T3, T4, Task>>> eventToBind, Func<T1, T2, T3, T4, Task> handler);

        #endregion BindEventHandler<> multi argument variants
    }
}