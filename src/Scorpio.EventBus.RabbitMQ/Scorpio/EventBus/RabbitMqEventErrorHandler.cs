using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EasyNetQ;

using Microsoft.Extensions.Options;
using RabbitMQ.Client;

using Scorpio.Data;

using Scorpio.DependencyInjection;

namespace Scorpio.EventBus
{
    internal class RabbitMqEventErrorHandler : EventErrorHandlerBase
    {
        public RabbitMqEventErrorHandler(
            IOptions<EventBusOptions> options)
            : base(options)
        {
        }

        protected override async Task RetryAsync(EventExecutionErrorContext context)
        {
            if (Options.RetryStrategyOptions.IntervalMillisecond > 0)
            {
                await Task.Delay(Options.RetryStrategyOptions.IntervalMillisecond);
            }

            context.TryGetRetryAttempt(out var retryAttempt);

            await context.EventBus.As<RabbitEventBus>().PublishAsync(
                context.EventType,
                context.EventData,
                context.GetProperty(HeadersKey).As<MessageProperties>(),
                new Dictionary<string, object>
                {
                    {RetryAttemptKey, ++retryAttempt},
                    {"exceptions", context.Exceptions.Select(x => x.ToString()).ToList()}
                });
        }

        protected override Task MoveToDeadLetterAsync(EventExecutionErrorContext context)
        {
            ThrowOriginalExceptions(context);

            return Task.CompletedTask;
        }
    }
}
