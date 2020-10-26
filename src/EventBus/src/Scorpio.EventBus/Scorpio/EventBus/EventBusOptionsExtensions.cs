using System;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventBusOptionsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="handlerType"></param>
        /// <param name="activationType"></param>
        /// <returns></returns>
        public static EventBusOptions AddHandler(this EventBusOptions options, Type handlerType, EventHandlerActivationType activationType)
        {
            options.AddHandler(EventHandlerDescriptor.Describe(handlerType, activationType));
            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="activationType"></param>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventBusOptions AddHandler<TEventHandler>(this EventBusOptions options, EventHandlerActivationType activationType)
            where TEventHandler : class, IEventHandler
        {
            options.AddHandler(EventHandlerDescriptor.Describe<TEventHandler>(activationType));
            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventBusOptions AddSingletonHandler<TEventHandler>(this EventBusOptions options)
            where TEventHandler : class, IEventHandler
        {
            options.AddHandler(EventHandlerDescriptor.Singleton<TEventHandler>());
            return options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventBusOptions AddTransientHandler<TEventHandler>(this EventBusOptions options)
            where TEventHandler : class, IEventHandler
        {
            options.AddHandler(EventHandlerDescriptor.Transient<TEventHandler>());
            return options;
        }


    }
}
