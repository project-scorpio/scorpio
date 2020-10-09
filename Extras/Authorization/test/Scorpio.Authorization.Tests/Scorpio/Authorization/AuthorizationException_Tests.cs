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

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Vulnerability", "S5773:Types allowed to be deserialized should be restricted", Justification = "<挂起>")]
        public void Serializable()
        {
            var ex = new AuthorizationException("Test", new ScorpioException("InnerException"));
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, ex);
            stream.Seek(0, SeekOrigin.Begin);
            var act = formatter.Deserialize(stream);
            act.ShouldBeOfType<AuthorizationException>().InnerException.ShouldBeOfType<ScorpioException>().Message.ShouldBe("InnerException");
        }

    }
}
