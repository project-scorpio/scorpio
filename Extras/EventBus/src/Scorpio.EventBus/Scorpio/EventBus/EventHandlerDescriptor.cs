using System;
using System.Reflection;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EventHandlerDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        private IEventHandlerFactory _factory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationType"></param>
        /// <param name="handlerType"></param>
        public EventHandlerDescriptor(Type handlerType, EventHandlerActivationType activationType)
        {
            ActivationType = activationType;
            HandlerType = handlerType;
        }

        /// <summary>
        /// 
        /// </summary>
        public EventHandlerActivationType ActivationType { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerType"></param>
        /// <param name="activationType"></param>
        /// <returns></returns>
        public static EventHandlerDescriptor Describe(Type handlerType, EventHandlerActivationType activationType)
        {
            if (!handlerType.IsAssignableTo<IEventHandler>())
            {
                throw new ArgumentException($"{nameof(handlerType)} should be derived from {typeof(IEventHandler)}", nameof(handlerType));
            }
            return new EventHandlerDescriptor(handlerType, activationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <param name="activationType"></param>
        /// <returns></returns>
        public static EventHandlerDescriptor Describe<TEventHandler>(EventHandlerActivationType activationType) where TEventHandler : class, IEventHandler
        {
            return Describe(typeof(TEventHandler), activationType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventHandlerDescriptor ServiceProvider<TEventHandler>() where TEventHandler : class, IEventHandler
        {
            return Describe<TEventHandler>(EventHandlerActivationType.ByServiceProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventHandlerDescriptor Singleton<TEventHandler>() where TEventHandler : class, IEventHandler
        {
            return Describe<TEventHandler>(EventHandlerActivationType.Singleton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public static EventHandlerDescriptor Transient<TEventHandler>() where TEventHandler : class, IEventHandler
        {
            return Describe<TEventHandler>(EventHandlerActivationType.Transient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public IEventHandlerFactory GetEventHandlerFactory(IHybridServiceScopeFactory serviceProvider)
        {
            return _factory ??= CreateFactory(serviceProvider);
        }

        private IEventHandlerFactory CreateFactory(IHybridServiceScopeFactory serviceProvider)
        {
            switch (ActivationType)
            {
                case EventHandlerActivationType.ByServiceProvider:
                    return new IocEventHandlerFactory(serviceProvider, HandlerType);
                case EventHandlerActivationType.Singleton:
                    return new SingleInstanceHandlerFactory(Activator.CreateInstance(HandlerType) as IEventHandler);
                case EventHandlerActivationType.Transient:
                    return new TransientEventHandlerFactory(HandlerType);
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EventHandlerActivationType
    {
        /// <summary>
        /// 
        /// </summary>
        ByServiceProvider,

        /// <summary>
        /// 
        /// </summary>
        Singleton,

        /// <summary>
        /// 
        /// </summary>
        Transient,
    }
}
