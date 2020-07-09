using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

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
        public static EventHandlerDescriptor ByServiceProvider<TEventHandler>() where TEventHandler : class, IEventHandler
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
        public IEventHandlerFactory GetEventHandlerFactory(IServiceProvider serviceProvider)
        {
            return _factory ??= CreateFactory(serviceProvider);
        }

        private IEventHandlerFactory CreateFactory(IServiceProvider serviceProvider)
        {
            var serviceScopeFactory = serviceProvider.GetRequiredService<IHybridServiceScopeFactory>();
            return ActivationType switch
            {
                EventHandlerActivationType.ByServiceProvider => new IocEventHandlerFactory(serviceScopeFactory, HandlerType),
                EventHandlerActivationType.Singleton => new SingleInstanceHandlerFactory(ActivatorUtilities.CreateInstance(serviceProvider, HandlerType) as IEventHandler),
                EventHandlerActivationType.Transient => new TransientEventHandlerFactory(serviceScopeFactory, HandlerType),
                _ => null,
            };
        }
    }
}
