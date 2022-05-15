using Shouldly;

using Xunit;
namespace System.Collections.Generic
{
    public class DictionaryExtensions_Tests
    {
        private readonly Dictionary<string, string> _keyValuePairs = new Dictionary<string, string> {
            {"Key1","Value1" },
            {"Key2","Value2" },
            {"Key3","Value3" },
            {"Key4","Value4" },
        };

        [Fact]
        public void TryGetValue()
        {
            var dic = new Dictionary<string, object> {
                {"Key1","Value1" },
                {"Key2","Value2" },
                {"Key3","Value3" },
                {"Key4","Value4" },
            };

            dic.TryGetValue<string>("key", out var value).ShouldBeFalse();
            value.ShouldBeNull();
            dic.TryGetValue("Key1", out value).ShouldBeTrue();
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

        [Fact]
        public void AddOrUpdate()
        {
            var dic = new Dictionary<string, object> {
                {"Key1","Value1" },
                {"Key2","Value2" },
                {"Key3","Value3" },
                {"Key4","Value4" },
            };
            dic.Count.ShouldBe(4);
            dic.AddOrUpdate("Key1", () => "New Value");
            dic.Count.ShouldBe(4);
            dic.GetValueOrDefault("Key1").ShouldBe("New Value");
            dic.AddOrUpdate("Key5", () => "New Value");
            dic.Count.ShouldBe(5);
            dic.GetValueOrDefault("Key5").ShouldBe("New Value");
        }
        [Fact]
        public void AddOrUpdateFact()
        {
            var dic = new Dictionary<string, int>
            {
                {"key1",1 }
            };

            dic.AddOrUpdate("key", k => 1, (k, v) => v + 1).ShouldBe(1);
            dic.AddOrUpdate("key", k => 1, (k, v) => v + 1).ShouldBe(2);
            dic.AddOrUpdate("key1", k => 1, (k, v) => v + 1).ShouldBe(2);
            dic.AddOrUpdate("key", k => 1, (k, v) => v + 1).ShouldBe(3);
        }
    }
}
