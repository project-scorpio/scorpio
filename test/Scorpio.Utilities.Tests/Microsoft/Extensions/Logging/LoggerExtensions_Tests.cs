using System;
using System.Collections.Generic;
using System.Linq;

using Scorpio.ExceptionHandling;
using Scorpio.Logging;

using Shouldly;

using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Extensions.Logging
{
    public class LoggerExtensions_Tests
    {
        private readonly ITestOutputHelper _output;

        public LoggerExtensions_Tests(ITestOutputHelper output) => _output = output;

        [Fact]
        public void LogKnownProperties()
        {
            var logger = _output.BuildLoggerFor<ListExtensions_Tests>();
            logger.LogException(new LogKnownException("code", "details"));
            logger.Count.ShouldBe(3);
            logger.Entries.First().Exception.ShouldBeOfType<LogKnownException>().Code.ShouldBe("code");
            logger.Last.Exception.ShouldBeNull();
            logger.Last.Message.ShouldContain("details");
            logger.Last.LogLevel.ShouldBe(LogLevel.Error);
        }

        [Fact]
        public void LogData()
        {
            var logger = _output.BuildLoggerFor<ListExtensions_Tests>();
            var ex = new ApplicationException("message");
            ex.Data.Add("Key1", "Data1");
            ex.Data.Add("Key2", "Data2");
            logger.LogException(ex);
            logger.Count.ShouldBe(4);
            logger.Entries.First().Exception.ShouldBeOfType<ApplicationException>().Message.ShouldBe("message");
            logger.Last.Exception.ShouldBeNull();
            logger.Last.Message.ShouldContain("Data2");
            logger.Last.LogLevel.ShouldBe(LogLevel.Error);
        }

        [Fact]
        public void LogSelfLogging()
        {
            var logger = _output.BuildLoggerFor<ListExtensions_Tests>();
            var ex = new AggregateException("message", new SelfLoggingException("selfloggin"));
            logger.LogException(ex);
            logger.Count.ShouldBe(2);
            logger.Entries.First().Exception.ShouldBeOfType<AggregateException>().InnerException.ShouldNotBeNull();
            logger.Last.Exception.ShouldBeOfType<SelfLoggingException>().Message.ShouldBe("selfloggin");
            logger.Last.Message.ShouldContain("selfloggin");
            logger.Last.LogLevel.ShouldBe(LogLevel.Error);
        }

        [Serializable]
        public class LogKnownException : Exception, IHasErrorCode, IHasErrorDetails
        {
            public LogKnownException(string code, string details)
            {
                Code = code;
                Details = details;
            }
            public LogKnownException(string message) : base(message) { }
            public LogKnownException(string message, Exception inner) : base(message, inner) { }
            protected LogKnownException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

            public string Details { get; }

            public string Code { get; }
        }


        [Serializable]
        public class SelfLoggingException : Exception, IExceptionWithSelfLogging
        {
            public SelfLoggingException() { }
            public SelfLoggingException(string message) : base(message) { }
            public SelfLoggingException(string message, Exception inner) : base(message, inner) { }
            protected SelfLoggingException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

            public void Log(ILogger logger) => logger.LogError(this, $"SelfLogging:{Message}");
        }

    }
}
