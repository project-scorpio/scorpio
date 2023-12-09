using System.IO;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization
{
    public class AuthorizationException_Tests
    {
        [Fact]
        public void LogLevel()
        {
            var ex = new AuthorizationException();
            ex.LogLevel.ShouldBe(Microsoft.Extensions.Logging.LogLevel.Warning);
        }

        [Fact]
        public void InnerException()
        {
            var ex = new AuthorizationException("Test", new ScorpioException("InnerException"));
            ex.InnerException.ShouldBeOfType<ScorpioException>().Message.ShouldBe("InnerException");
        }
#if !NET8_0_OR_GREATER

        [Fact]
        public void Serializable()
        {
            var ex = new AuthorizationException("Test", new ScorpioException("InnerException"));
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var stream = new MemoryStream();
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            formatter.Serialize(stream, ex);
            stream.Seek(0, SeekOrigin.Begin);
            var act = formatter.Deserialize(stream);
            act.ShouldBeOfType<AuthorizationException>().InnerException.ShouldBeOfType<ScorpioException>().Message.ShouldBe("InnerException");
#pragma warning restore SYSLIB0011 // 类型或成员已过时
        }
#endif

    }
}
