using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    /// <summary>
    /// Implements EventBus as Singleton pattern.
    /// </summary>
    public abstract class EventBusBase : IEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        public EventBusOptions Options { get; }
        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="options"></param>
        protected EventBusBase(IServiceProvider serviceProvider, IOptions<EventBusOptions> options)
        {
            ServiceProvider = serviceProvider;
            Options = options.Value;
            HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
            Init();
        }

        private void Init() => SubscribeHandlers(Options.Handlers);

        /// <inheritdoc/>
        public virtual IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class => Subscribe(typeof(TEvent), new ActionEventHandler<TEvent>(action));

        /// <inheritdoc/>
        public virtual IDisposable Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler, new() => Subscribe(typeof(TEvent), new TransientEventHandlerFactory<THandler>(ServiceProvider.GetRequiredService<IHybridServiceScopeFactory>()));

        /// <inheritdoc/>
        public virtual IDisposable Subscribe(Type eventType, IEventHandler handler) => Subscribe(eventType, new SingleInstanceHandlerFactory(handler));
        /// <inheritdoc/>
        public virtual IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler) => Subscribe(typeof(TEvent), new SingleInstanceHandlerFactory(handler));

        /// <inheritdoc/>
        public virtual IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class => Subscribe(typeof(TEvent), factory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public abstract IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="action"></param>
        public abstract void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class;

        /// <inheritdoc/>
        public virtual void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : class => Unsubscribe(typeof(TEvent), handler);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public abstract void Unsubscribe(Type eventType, IEventHandler handler);

        /// <inheritdoc/>
        public virtual void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class => Unsubscribe(typeof(TEvent), factory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="factory"></param>
        public abstract void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        /// <inheritdoc/>
        public virtual void UnsubscribeAll<TEvent>() where TEvent : class => UnsubscribeAll(typeof(TEvent));

        /// <inheritdoc/>
        public abstract void UnsubscribeAll(Type eventType);

        /// <inheritdoc/>
        public virtual Task PublishAsync<TEvent>(TEvent eventData) where TEvent : class => PublishAsync(typeof(TEvent), eventData);

        /// <inheritdoc/>
        public abstract Task PublishAsync(Type eventType, object eventData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public virtual async Task TriggerHandlersAsync(Type eventType, object eventData)
        {
            var exceptions = new List<Exception>();

            await TriggerHandlersAsync(eventType, eventData, exceptions);

            if (exceptions.Any())
            {
                if (exceptions.Count == 1)
                {
                    exceptions[0].ReThrow();
                }

                throw new AggregateException("More than one error has occurred while triggering the event: " + eventType, exceptions);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventData"></param>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        protected virtual async Task TriggerHandlersAsync(Type eventType, object eventData, List<Exception> exceptions)
        {
            await new SynchronizationContextRemover();

            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories.ToArray())
                {
                    await TriggerHandlerAsync(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.GetTypeInfo().IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.GetTypeInfo().BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = Activator.CreateInstance(baseEventType, constructorArgs);
                    await TriggerHandlersAsync(baseEventType, baseEventData, exceptions);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlers"></param>
        protected virtual void SubscribeHandlers(ICollection<EventHandlerDescriptor> handlers)
        {
            foreach (var handler in handlers)
            {
                var interfaces = handler.HandlerType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                    {
                        continue;
                    }

                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        Subscribe(genericArgs[0], handler.GetEventHandlerFactory(ServiceProvider));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        protected abstract IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asyncHandlerFactory"></param>
        /// <param name="eventType"></param>
        /// <param name="eventData"></param>
        /// <param name="exceptions"></param>
        /// <returns></returns>
        protected virtual async Task TriggerHandlerAsync(IEventHandlerFactory asyncHandlerFactory, Type eventType, object eventData, List<Exception> exceptions)
        {
            using (var eventHandlerWrapper = asyncHandlerFactory.GetHandler())
            {
                try
                {
                    var handlerType = eventHandlerWrapper.EventHandler.GetType();
                    if (handlerType.IsAssignableToGenericType(typeof(IEventHandler<>)))
                    {
                        var method = typeof(IEventHandler<>)
                            .MakeGenericType(eventType)
                            .GetMethod(
                                nameof(IEventHandler<object>.HandleEventAsync),
                                new[] { eventType }
                            );

                        await (Task)method.Invoke(eventHandlerWrapper.EventHandler, new[] { eventData });
                    }
                    else
                    {
                        throw new ScorpioException("The object instance is not an event handler. Object type: " + handlerType.AssemblyQualifiedName);
                    }
                }
                catch (TargetInvocationException ex)
                {
                    exceptions.Add(ex.InnerException);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected class EventTypeWithEventHandlerFactories
        {
            /// <summary>
            /// 
            /// </summary>
            public Type EventType { get; }

            /// <summary>
            /// 
            /// </summary>
            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="eventType"></param>
            /// <param name="eventHandlerFactories"></param>
            public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        // Reference from
        // https://blogs.msdn.microsoft.com/benwilli/2017/02/09/an-alternative-to-configureawaitfalse-everywhere/
        protected struct SynchronizationContextRemover : INotifyCompletion
        {
            /// <summary>
            /// 
            /// </summary>
            public bool IsCompleted => SynchronizationContext.Current == null;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="continuation"></param>
            public void OnCompleted(Action continuation)
            {
                var prevContext = SynchronizationContext.Current;
                try
                {
                    SynchronizationContext.SetSynchronizationContext(null);
                    continuation();
                }
                finally
                {
                    SynchronizationContext.SetSynchronizationContext(prevContext);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public SynchronizationContextRemover GetAwaiter() => this;

            /// <summary>
            /// 
            /// </summary>
            public void GetResult()
            {
                // Method intentionally left empty.
            }
        }
    }
}
