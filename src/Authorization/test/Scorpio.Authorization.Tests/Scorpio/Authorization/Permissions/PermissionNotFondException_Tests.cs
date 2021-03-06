﻿using System.IO;

using Shouldly;

using Xunit;

namespace Scorpio.Authorization.Permissions
{
    public class PermissionNotFondException_Tests
    {
        [Fact]
        public void PermissionName()
        {
            var ex = new PermissionNotFondException("TestPermission");
            ex.PermissionName.ShouldBe("TestPermission");
        }
        [Fact]
        public void Message()
        {
            var ex = new PermissionNotFondException("TestPermission", "Message");
            ex.PermissionName.ShouldBe("TestPermission");
            ex.Message.ShouldBe("Message");
        }

        [Fact]
        public void InnerException()
        {
            var ex = new PermissionNotFondException("Test", "Message", new ScorpioException("InnerException"));
            ex.PermissionName.ShouldBe("Test");
            ex.Message.ShouldBe("Message");
            ex.InnerException.ShouldBeOfType<ScorpioException>().Message.ShouldBe("InnerException");
        }

        [Fact]
        public void Serializable()
        {
            var ex = new PermissionNotFondException("Test", "Message", new ScorpioException("InnerException"));
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, ex);
            stream.Seek(0, SeekOrigin.Begin);
            var act = formatter.Deserialize(stream);
            act.ShouldBeOfType<PermissionNotFondException>().InnerException.ShouldBeOfType<ScorpioException>().Message.ShouldBe("InnerException");
        }

    }
}
