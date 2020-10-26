using System;
using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class Check_Tests
    {
        [Fact]
        public void NotNull()
        {
            Should.Throw<ArgumentNullException>(() => Check.NotNull<string>(null, "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentNullException>(() => Check.NotNull<string>(null, "para", "msg")).Message.ShouldBe("msg (Parameter 'para')");
            Should.NotThrow(() => Check.NotNull("value", "para")).ShouldBe("value");
            Should.NotThrow(() => Check.NotNull("value", "para", "msg")).ShouldBe("value");
        }

        [Fact]
        public void NotNullOrWhiteSpace()
        {
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace(null, "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace("", "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace(" ", "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace("\t", "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace("\n", "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrWhiteSpace("\r", "para")).ParamName.ShouldBe("para");
            Should.NotThrow(() => Check.NotNullOrWhiteSpace("value", "para")).ShouldBe("value");
        }

        [Fact]
        public void NotNullOrEmpty()
        {
            Should.Throw<ArgumentException>(() => Check.NotNullOrEmpty(null, "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrEmpty("", "para")).ParamName.ShouldBe("para");
            Should.NotThrow(() => Check.NotNullOrEmpty(" ", "para")).ShouldBe(" ");
            Should.NotThrow(() => Check.NotNullOrEmpty("\t", "para")).ShouldBe("\t");
            Should.NotThrow(() => Check.NotNullOrEmpty("\n", "para")).ShouldBe("\n");
            Should.NotThrow(() => Check.NotNullOrEmpty("\r", "para")).ShouldBe("\r");
            Should.NotThrow(() => Check.NotNullOrEmpty("value", "para")).ShouldBe("value");
        }
        [Fact]
        public void NotNullOrEmpty_T()
        {
            Should.Throw<ArgumentException>(() => Check.NotNullOrEmpty<string>(null, "para")).ParamName.ShouldBe("para");
            Should.Throw<ArgumentException>(() => Check.NotNullOrEmpty(new List<string>(), "para")).ParamName.ShouldBe("para");
            Should.NotThrow(() => Check.NotNullOrEmpty(new List<string> { "value" }, "para")).ShouldHaveSingleItem().ShouldBe("value");
        }
    }
}
