using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Topology;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RabbitMQ.Client;

using Scorpio.Data;
using Scorpio.Initialization;
using Scorpio.Threading;

namespace Scorpio.EventBus
{
    internal class RabbitEventBus : EventBusBase, IInitializable
    {
        private readonly IBus _bus;
        private readonly RabbitMQEventBusOptions _options;
        private IExchange _exchange;
        private IQueue _queue;
        protected ConcurrentDictionary<string, Type> EventTypes { get; }
        /// <summary>
        /// Reference to the Logger.
        /// </summary>
        public ILogger<RabbitEventBus> Logger { get; }

        public IRabbitMqEventDataSerializer Serializer { get; }

        public RabbitEventBus(IServiceProvider serviceProvider, IBus bus, IEventErrorHandler errorHandler, IRabbitMqEventDataSerializer serializer, IOptions<RabbitMQEventBusOptions> options) : base(serviceProvider, errorHandler, options)
        {
            Logger = serviceProvider.GetService<ILogger<RabbitEventBus>>();
            _bus = bus;
            Serializer = serializer;
            _options = options.Value;
            EventTypes = new ConcurrentDictionary<string, Type>();
        }
        public virtual void Initialize()
        {
            _exchange = _bus.Advanced.ExchangeDeclare(_options.ExchangeName, c => c.AsDurable(true).WithType("direct"));
            _queue = _bus.Advanced.QueueDeclare(_options.ClientName, c => c.AsDurable(true).AsExclusive(false).AsAutoDelete(false));
            _bus.Advanced.Consume(_queue, ProcessEventAsync);
            SubscribeHandlers(Options.Handlers);
        }

        private async Task<AckStrategy> ProcessEventAsync(byte[] buffer, MessageProperties messageProperties, MessageReceivedInfo messageReceivedInfo)
        {
            var eventName = messageReceivedInfo.RoutingKey;
            var eventType = EventTypes.GetOrDefault(eventName);
            if (eventType == null)
            {
                return AckStrategies.Ack;
            }
            var eventData = Serializer.Deserialize(buffer, eventType);
            await TriggerHandlersAsync(eventType, eventData, errorContext =>
            {
                var retryAttempt = 0;
                if (messageProperties.Headers != null &&
                    messageProperties.Headers.ContainsKey(EventErrorHandlerBase.RetryAttemptKey))
                {
                    retryAttempt = (int)messageProperties.Headers[EventErrorHandlerBase.RetryAttemptKey];
                }
                errorContext.EventData = eventData;
                errorContext.SetProperty(EventErrorHandlerBase.HeadersKey, messageProperties);
                errorContext.SetProperty(EventErrorHandlerBase.RetryAttemptKey, retryAttempt);
            });
            return AckStrategies.Ack;
        }

        public override async Task PublishAsync(Type eventType, object eventData)
        {
            await PublishAsync(eventType, eventData, null);
        }

        public async Task PublishAsync(Type eventType, object eventData, MessageProperties properties, Dictionary<string, object> headersArguments = null)
        {
            var eventName = EventNameAttribute.GetNameOrDefault(eventType);
            var body = Serializer.Serialize(eventData);
            if (properties == null)
            {
                properties = new MessageProperties
                {
                    DeliveryMode = 2,
                    MessageId = Guid.NewGuid().ToString("N")
                };
            }
            SetEventMessageHeaders(properties, headersArguments);
            await _bus.Advanced.PublishAsync(_exchange, eventName, true, properties, body);
        }

        private void SetEventMessageHeaders(MessageProperties properties, Dictionary<string, object> headersArguments)
        {
            if (headersArguments == null)
            {
                return;
            }

            properties.Headers ??= new Dictionary<string, object>();

            foreach (var header in headersArguments)
            {
                properties.Headers[header.Key] = header.Value;
            }
        }

        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType)
                           .Locking(factories =>
                           {
                               factories.Add(factory);
                               if (factories.Count == 1)
                               {
                                   factories.Binding = _bus.Advanced.Bind(_exchange, _queue, EventNameAttribute.GetNameOrDefault(eventType));
                               }
                               return factories;
                           });

            return new DisposeAction(() => Unsubscribe(eventType, factory));
        }


        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory) =>
            GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Action(f => f.Remove(factory)))
                                                  .Action(f => !f.Any(), f => _bus.Advanced.Unbind(f.Binding));

        public override void UnsubscribeAll(Type eventType) => GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Action(f => f.Clear()).Action(f => _bus.Advanced.Unbind(f.Binding)));


        private new EventHandlerFactoryList GetOrCreateHandlerFactories(Type eventType)
        {
            return HandlerFactories.GetOrAdd(eventType, (type) =>
            {
                var eventName = EventNameAttribute.GetNameOrDefault(type);
                EventTypes[eventName] = type;
                return new EventHandlerFactoryList();
            }) as EventHandlerFactoryList;
        }

        private class EventHandlerFactoryList : List<IEventHandlerFactory>
        {
            public IBinding Binding { get; set; }
        }
    }
}
