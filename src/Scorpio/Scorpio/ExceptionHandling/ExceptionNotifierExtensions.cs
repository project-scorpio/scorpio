using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionNotifierExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionNotifier"></param>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        public static Task NotifyAsync(
            this IExceptionNotifier exceptionNotifier, 
            Exception exception,
            LogLevel? logLevel = null,
            bool handled = true)
        {
            Check.NotNull(exceptionNotifier, nameof(exceptionNotifier));

            return exceptionNotifier.NotifyAsync(
                new ExceptionNotificationContext(
                    exception,
                    logLevel,
                    handled
                )
            );
        }
    }
}