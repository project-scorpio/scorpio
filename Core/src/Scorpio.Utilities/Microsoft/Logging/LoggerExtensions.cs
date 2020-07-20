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
        public static void LogException(this ILogger logger, Exception ex, LogLevel? level = null)
        {
            var selectedLevel = level ?? ex.GetLogLevel();

            logger.Log(selectedLevel, ex.Message, ex);
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

            switch (exception)
            {
                case IExceptionWithSelfLogging ex:
                    loggingExceptions.Add(ex);
                    break;
                case AggregateException aggException when aggException.InnerException != null:
                    {
                        if (aggException.InnerException is IExceptionWithSelfLogging inner)
                        {
                            loggingExceptions.Add(inner);
                        }
                        foreach (var item in aggException.InnerExceptions)
                        {
                            if (item is IExceptionWithSelfLogging ex)
                            {
                                loggingExceptions.AddIfNotContains(ex);
                            }
                        }
                        break;
                    }
                default:
                    break;
            }

            foreach (var ex in loggingExceptions)
            {
                ex.Log(logger);
            }
        }
    }
}
