using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Scorpio.DependencyInjection;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionNotifier : IExceptionNotifier, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger<ExceptionNotifier> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public ExceptionNotifier(IHybridServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = Check.NotNull(serviceScopeFactory, nameof(serviceScopeFactory));
            Logger = NullLogger<ExceptionNotifier>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task NotifyAsync(ExceptionNotificationContext context)
        {
            Check.NotNull(context, nameof(context));

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var exceptionSubscribers = scope.ServiceProvider
                    .GetServices<IExceptionSubscriber>();

                foreach (var exceptionSubscriber in exceptionSubscribers)
                {
                    try
                    {
                        await exceptionSubscriber.HandleAsync(context);
                    }
                    catch (Exception e)
                    {
                        Logger.LogWarning($"Exception subscriber of type {exceptionSubscriber.GetType().AssemblyQualifiedName} has thrown an exception!");
                        Logger.LogException(e, LogLevel.Warning);
                    }
                }
            }
        }
    }
}
