using Shouldly;
using Xunit;
namespace System.Collections.Generic
{
    public class DictionaryExtensions_Tests
    {
        private Dictionary<string, string> _keyValuePairs = new Dictionary<string, string> {
            {"Key1","Value1" },
            {"Key2","Value2" },
            {"Key3","Value3" },
            {"Key4","Value4" },
        };

        [Fact]
        public void TryGetValue()
        {
            var _keyValuePairs = new Dictionary<string, object> {
                { "Key1","Value1" },
                { "Key2","Value2" },
                { "Key3","Value3" },
                { "Key4","Value4" },
            };
            _keyValuePairs.TryGetValue("key", out string value).ShouldBeFalse();
            value.ShouldBeNull();
            _keyValuePairs.TryGetValue("Key1", out value).ShouldBeTrue();
            value.ShouldBe("Value1");
        }

        [Fact]
        public void GetOrDefault()
        {
            _keyValuePairs.GetOrDefault("key").ShouldBeNull();
            _keyValuePairs.GetOrDefault("Key1").ShouldBe("Value1");
            (_keyValuePairs as IDictionary<string, string>).GetOrDefault("key").ShouldBeNull();
            (_keyValuePairs as IDictionary<string, string>).GetOrDefault("Key1").ShouldBe("Value1");
            (_keyValuePairs as IReadOnlyDictionary<string, string>).GetOrDefault("key").ShouldBeNull();
            (_keyValuePairs as IReadOnlyDictionary<string, string>).GetOrDefault("Key1").ShouldBe("Value1");
            var keyValuePairs = new Concurrent.ConcurrentDictionary<string, string>(_keyValuePairs);
            keyValuePairs.GetOrDefault("key").ShouldBeNull();
            keyValuePairs.GetOrDefault("Key1").ShouldBe("Value1");
        }

        [Fact]
        public void GetOrAdd()
        {
            _keyValuePairs.ContainsKey("key").ShouldBeFalse();
            _keyValuePairs.GetOrAdd("key", () => "value").ShouldBe("value");
            _keyValuePairs.ContainsKey("key").ShouldBeTrue();
            _keyValuePairs.GetOrAdd("key1", key => $"{key}value").ShouldBe("key1value");
            _keyValuePairs.GetOrAdd("Key1", () => "value").ShouldBe("Value1");
            _keyValuePairs.GetOrAdd("Key1", key => $"{key}value").ShouldBe("Value1");
        }
    }
}
