using System.Collections.Generic;

using Moq;

using Shouldly;

using Xunit;

namespace Scorpio.Options
{
    public class ExtensibleOptionsExtensions
    {
        [Fact]
        public void SetOption()
        {
            var mock = new Mock<ExtensibleOptions>();
            var options = mock.Object;
            options.SetOption("option1", "value");
            options.ExtendedOption.ShouldHaveSingleItem().ShouldBe(KeyValuePair.Create<string, object>("option1", "value"));
        }
        [Fact]
        public void GetOption()
        {
            var mock = new Mock<ExtensibleOptions>();
            var options = mock.Object;
            options.SetOption("option1", "value");
            options.GetOption<string>("option1").ShouldBe("value");
            options.GetOption<string>("option2").ShouldBeNull();
        }
    }
}
