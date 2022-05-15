using System;
using System.Collections.Generic;

using Scorpio.ExceptionHandling;
using Scorpio.Logging;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        /// <param name="level"></param>
        public static void LogException(this ILogger logger, Exception ex, LogLevel? level = null) => LogException(logger, ex, ex.Message, level);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public static void LogException(this ILogger logger, Exception ex, string message, LogLevel? level = null)
        {
            var selectedLevel = level ?? ex.GetLogLevel();

            logger.Log(selectedLevel, ex, message);
            LogKnownProperties(logger, ex, selectedLevel);
            LogSelfLogging(logger, ex);
            LogData(logger, ex, selectedLevel);
        }

        private static void LogKnownProperties(ILogger logger, Exception exception, LogLevel logLevel)
        {
            if (exception is IHasErrorCode exceptionWithErrorCode)
            {
                logger.Log(logLevel, "Code:" + exceptionWithErrorCode.Code);
            }

            if (exception is IHasErrorDetails exceptionWithErrorDetails)
            {
                logger.Log(logLevel, "Details:" + exceptionWithErrorDetails.Details);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        private static void LogData(ILogger logger, Exception exception, LogLevel logLevel)
        {
            if (exception.Data == null || exception.Data.Count <= 0)
            {
                return;
            }

            logger.Log(logLevel, "---------- Exception Data ----------");

            foreach (var key in exception.Data.Keys)
            {
                logger.Log(logLevel, $"{key} = {exception.Data[key]}");
            }
        }

        private static void LogSelfLogging(ILogger logger, Exception exception)
        {
            var loggingExceptions = new List<IExceptionWithSelfLogging>();
            LogSelfLogging(loggingExceptions, exception);
            foreach (var ex in loggingExceptions)
            {
                ex.Log(logger);
            }

        }

        private static void LogSelfLogging(List<IExceptionWithSelfLogging> selfLoggings, Exception exception)
        {

            switch (exception)
            {
                case IExceptionWithSelfLogging ex:
                    selfLoggings.Add(ex);
                    break;
                case AggregateException aggException:
                    {
                        aggException.InnerExceptions.ForEach(e => LogSelfLogging(selfLoggings, e));
                        break;
                    }
                default:
                    break;
            }

        }
    }
}
