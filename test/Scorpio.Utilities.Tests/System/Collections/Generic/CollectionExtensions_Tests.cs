using System.Collections.ObjectModel;

using Shouldly;

using Xunit;

namespace System.Collections.Generic
{
    public class CollectionExtensions_Tests
    {
        [Fact]
        public void AddIfNotContains_With_Predicate()
        {
            var collection = new List<int> { 4, 5, 6 };

            collection.AddIfNotContains(x => x == 5, () => 5);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(x => x == 42, () => 42);
            collection.Count.ShouldBe(4);

            collection.AddIfNotContains(x => x < 8, () => 8);
            collection.Count.ShouldBe(4);

            collection.AddIfNotContains(x => x > 999, () => 8);
            collection.Count.ShouldBe(5);
        }
        [Fact]
        public void AddIfNotContains()
        {
            var collection = new List<int> { 4, 5, 6 };

            collection.AddIfNotContains(5);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(4);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(8);
            collection.Count.ShouldBe(4);
        }

        [Fact]
        public void AddIfNotContainsWhitComparer()
        {
            var collection = new List<int> { 4, 5, 6 };

            collection.AddIfNotContains(5, EqualityComparer<int>.Default);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(4, EqualityComparer<int>.Default);
            collection.Count.ShouldBe(3);

            collection.AddIfNotContains(8, EqualityComparer<int>.Default);
            collection.Count.ShouldBe(4);
        }
        [Fact]
        public void IsNullOrEmpty()
        {
            List<int> collection = null;
            collection.IsNullOrEmpty().ShouldBeTrue();
            collection = new List<int>();
            collection.IsNullOrEmpty().ShouldBeTrue();
            collection.Add(1);
            collection.IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void RemoveAll_List()
        {
            ICollection<int> collection = new List<int> { 3, 4, 4, 5, 5, 5, 6, 6, 7, 8, 8, 8, 9 };
            collection.Count.ShouldBe(13);
            collection.RemoveAll(i => i == 4).Count.ShouldBe(2);
            collection.Count.ShouldBe(11);
            collection.RemoveAll(i => i == 2).Count.ShouldBe(0);
            collection.Count.ShouldBe(11);
            collection.RemoveAll(i => i == 5).Count.ShouldBe(3);
            collection.Count.ShouldBe(8);
        }
        [Fact]
        public void RemoveAll()
        {
            ICollection<int> collection = new HashSet<int> { 3, 4, 4, 5, 5, 5, 6, 6, 7, 8, 8, 8, 9 };
            collection.Count.ShouldBe(7);
            collection.RemoveAll(i => i == 4).Count.ShouldBe(1);
            collection.Count.ShouldBe(6);
            collection.RemoveAll(i => i == 2).Count.ShouldBe(0);
            collection.Count.ShouldBe(6);
            collection.RemoveAll(i => i == 5).Count.ShouldBe(1);
            collection.Count.ShouldBe(5);
        }

        [Fact]
        public void GetOrDefault()
        {
            ICollection<int> collection = new List<int> { 3, 4, 4, 5, 5, 5, 6, 6, 7, 8, 8, 8, 9 };
            collection.GetOrDefault(i => i == 5).ShouldBe(5);
            collection.GetOrDefault(i => i == 5, -1).ShouldBe(5);
            collection.GetOrDefault(i => i == 10).ShouldBe(0);
            collection.GetOrDefault(i => i == 10, -1).ShouldBe(-1);
        }

        [Fact]
        public void GetOrAdd()
        {
            ICollection<int> collection = new List<int> { 3, 4, 5 };
            collection.GetOrAdd(i => i == 5, () => 5).ShouldBe(5);
            collection.ShouldNotContain(15);
            collection.GetOrAdd(i => i == 15, () => 15).ShouldBe(15);
            collection.ShouldContain(15);
            collection.GetOrAdd(i => i == 10, () => 11).ShouldBe(11);
            collection.ShouldNotContain(10);
            collection.ShouldContain(11);
        }
    }
}
