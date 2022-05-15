using System;

using Microsoft.Extensions.Logging;

namespace Scorpio.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionNotificationContext
    {
        /// <summary>
        /// The exception object.
        /// </summary>

        public Exception Exception { get; }

        /// <summary>
        /// 
        /// </summary>
        public LogLevel LogLevel { get; }

        /// <summary>
        /// True, if it is handled.
        /// </summary>
        public bool Handled { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        /// <param name="handled"></param>
        public ExceptionNotificationContext(
            Exception exception,
            LogLevel? logLevel = null,
            bool handled = true)
        {
            Exception = Check.NotNull(exception, nameof(exception));
            LogLevel = logLevel ?? exception.GetLogLevel();
            Handled = handled;
        }
    }
}