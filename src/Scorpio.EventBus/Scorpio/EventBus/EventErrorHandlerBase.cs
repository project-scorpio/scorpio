using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EventErrorHandlerBase : IEventErrorHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public const string HeadersKey = "headers";

        /// <summary>
        /// 
        /// </summary>
        public const string RetryAttemptKey = "retryAttempt";

        /// <summary>
        /// 
        /// </summary>
        protected EventBusOptions Options { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected EventErrorHandlerBase(IOptions<EventBusOptions> options)
        {
            Options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task HandleAsync(EventExecutionErrorContext context)
        {
            if (!await ShouldHandleAsync(context))
            {
                ThrowOriginalExceptions(context);
            }

            if (await ShouldRetryAsync(context))
            {
                await RetryAsync(context);
                return;
            }

            await MoveToDeadLetterAsync(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract Task RetryAsync(EventExecutionErrorContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract Task MoveToDeadLetterAsync(EventExecutionErrorContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task<bool> ShouldHandleAsync(EventExecutionErrorContext context)
        {
            if (!Options.EnabledErrorHandle)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(Options.ErrorHandleSelector == null || Options.ErrorHandleSelector.Invoke(context.EventType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual Task<bool> ShouldRetryAsync(EventExecutionErrorContext context)
        {
            if (Options.RetryStrategyOptions == null)
            {
                return Task.FromResult(false);
            }

            if (!context.TryGetRetryAttempt(out var retryAttempt))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(Options.RetryStrategyOptions.MaxRetryAttempts > retryAttempt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected virtual void ThrowOriginalExceptions(EventExecutionErrorContext context)
        {
            if (context.Exceptions.Count == 1)
            {
                context.Exceptions[0].ReThrow();
            }

            throw new AggregateException(
                "More than one error has occurred while triggering the event: " + context.EventType,
                context.Exceptions);
        }
    }
}
