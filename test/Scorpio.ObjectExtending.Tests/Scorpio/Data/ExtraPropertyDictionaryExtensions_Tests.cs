using System;

using Shouldly;

using Xunit;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtraPropertyDictionaryExtensions_Tests
    {

        [Fact]
        public void ToEnum_T()
        {
            var dict = new ExtraPropertyDictionary
            {
                { "enum", StringComparison.OrdinalIgnoreCase },
                { "string-enum", nameof(StringComparison.OrdinalIgnoreCase) },
                {"string","string" }
            };
            Should.Throw<ArgumentException>(() => dict.ToEnum<StringComparison>("string"));
            Should.NotThrow(() => dict.ToEnum<StringComparison>("enum")).ShouldBe(StringComparison.OrdinalIgnoreCase);
            Should.NotThrow(() => dict.ToEnum<StringComparison>("string-enum")).ShouldBe(StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ToEnum()
        {
            var dict = new ExtraPropertyDictionary
            {
                { "enum", StringComparison.OrdinalIgnoreCase },
                { "string-enum", nameof(StringComparison.OrdinalIgnoreCase) },
                {"string","string" }
            };
            Should.Throw<ArgumentException>(() => dict.ToEnum("string",typeof(StringComparison)));
            Should.NotThrow(() => dict.ToEnum("enum", typeof(StringComparison))).ShouldBe(StringComparison.OrdinalIgnoreCase);
            Should.NotThrow(() => dict.ToEnum("string-enum", typeof(StringComparison))).ShouldBe(StringComparison.OrdinalIgnoreCase);
            Should.NotThrow(() => dict.ToEnum("string", typeof(string))).ShouldBe("string");
        }


    }
}
