using System.Runtime.Serialization;
using System.Text.RegularExpressions;

using Microsoft.Extensions.Logging;

using Scorpio.Logging;

using Shouldly;

using Xunit;

namespace System
{
    public class ExceptionExtensions_Tests
    {
        [Fact]
        public void ReThrow()
        {
            const string RethrowMessageSubstring = "End of stack trace";
            var e = new FormatException();
            for (var i = 0; i < 3; i++)
            {
                Should.Throw<FormatException>(() => e.ReThrow()).ShouldBeSameAs(e);
                i.ShouldBe(Regex.Matches(e.StackTrace, RethrowMessageSubstring).Count);
            }
        }

        [Fact]
        public void GetLogLevel()
        {
            new FormatException().GetLogLevel().ShouldBe(LogLevel.Error);
            new FormatException().GetLogLevel(LogLevel.Critical).ShouldBe(LogLevel.Critical);
            new LogLevelException(LogLevel.Information).GetLogLevel().ShouldBe(LogLevel.Information);
            new LogLevelException(LogLevel.Information).GetLogLevel(LogLevel.Error).ShouldBe(LogLevel.Information);
        }
    }

    public class LogLevelException : Exception, IHasLogLevel
    {
        public LogLevelException()
        {
        }
        public LogLevelException(LogLevel logLevel) => LogLevel = logLevel;

        public LogLevelException(string message) : base(message)
        {
        }
        public LogLevelException(string message, LogLevel logLevel) : base(message) => LogLevel = logLevel;

        public LogLevelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public LogLevelException(string message, Exception innerException, LogLevel logLevel) : base(message, innerException) => LogLevel = logLevel;

        protected LogLevelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LogLevel LogLevel { get; set; }
    }
}
