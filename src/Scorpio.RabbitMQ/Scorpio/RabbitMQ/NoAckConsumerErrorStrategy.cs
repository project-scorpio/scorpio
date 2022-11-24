using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EasyNetQ;
using EasyNetQ.Consumer;
using EasyNetQ.Logging;

using RabbitMQ.Client;

namespace Scorpio.RabbitMQ
{
    internal class NoAckConsumerErrorStrategy : DefaultConsumerErrorStrategy
    {

#if NETSTANDARD2_0
        public NoAckConsumerErrorStrategy(IPersistentConnection connection, ISerializer serializer, IConventions conventions, ITypeNameSerializer typeNameSerializer, IErrorMessageSerializer errorMessageSerializer, ConnectionConfiguration configuration) : base(connection, serializer, conventions, typeNameSerializer, errorMessageSerializer, configuration)
        {
        }

        public override AckStrategy HandleConsumerError(ConsumerExecutionContext context, Exception exception)
        {
            var ex = exception;
            if (ex is AggregateException)
            {
                ex = ex.InnerException;
            }
            if (ex is NoAckWithRequeueException)
            {
                return AckStrategies.NackWithRequeue;
            }
            if (ex is NoAckWithoutRequeueException)
            {
                return AckStrategies.NackWithoutRequeue;
            }
            return base.HandleConsumerError(context, exception);
        }
#else
        public NoAckConsumerErrorStrategy(ILogger<DefaultConsumerErrorStrategy> logger, IConsumerConnection connection, ISerializer serializer, IConventions conventions, ITypeNameSerializer typeNameSerializer, IErrorMessageSerializer errorMessageSerializer, ConnectionConfiguration configuration) : base(logger, connection, serializer, conventions, typeNameSerializer, errorMessageSerializer, configuration)
        {
        }

        public override Task<AckStrategy> HandleConsumerErrorAsync(ConsumerExecutionContext context, Exception exception, CancellationToken cancellationToken)
        {
            var ex = exception;
            if (ex is AggregateException)
            {
                ex = ex.InnerException;
            }
            if (ex is NoAckWithRequeueException)
            {
                return Task.FromResult(AckStrategies.NackWithRequeue);
            }
            if (ex is NoAckWithoutRequeueException)
            {
                return Task.FromResult(AckStrategies.NackWithoutRequeue);
            }
            return base.HandleConsumerErrorAsync(context, exception, cancellationToken);
        }

#endif



    }
}
