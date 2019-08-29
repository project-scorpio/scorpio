using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Collections.Generic
{
    public class EnumerableExtensions_Tests
    {
        [Fact]
        public void IsNullOrEmpty()
        {
            IEnumerable<int> enumerable = null;
            enumerable.IsNullOrEmpty().ShouldBeTrue();
            enumerable = new List<int>();
            enumerable.IsNullOrEmpty().ShouldBeTrue();
            (enumerable as List<int>).Add(10);
            enumerable.IsNullOrEmpty().ShouldBeFalse();
        }

        [Fact]
        public void ExpandToString()
        {
            var enumerable = new List<int> { 4, 5, 6 };
            enumerable.ExpandToString(",").ShouldBe("4,5,6");
        }
        [Fact]
        public void ExpandToStringAsString()
        {
            var enumerable = new List<string> { "a", "bc", "d" };
            enumerable.ExpandToString(",").ShouldBe("a,bc,d");
        }

        [Fact]
        public void WhereIf()
        {
            IEnumerable<int> enumerable = new List<int> { 3, 4, 4, 5, 5, 5, 6, 6, 7, 8, 8, 8, 9 };
            enumerable.WhereIf(true, f => f == 4).Count().ShouldBe(2);
            enumerable.WhereIf(false, f => f == 4).Count().ShouldBe(13);
            enumerable.WhereIf(true, (f, i) => i == 2).ShouldHaveSingleItem();
            enumerable.WhereIf(true, (f, i) => f == 4).Count().ShouldBe(2);
            enumerable.WhereIf(false, (f, i) => i == 2).Count().ShouldBe(13);
        }
    }
}
