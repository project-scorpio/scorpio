
using Shouldly;

using Xunit;

namespace System
{
    public class ComparableExtensions_Tests
    {
        [Fact]
        public void IsBetween()
        {
            2.IsBetween(1, 3).ShouldBeTrue();
            1.IsBetween(2, 3).ShouldBeFalse();
            'b'.IsBetween('a', 'c').ShouldBeTrue();
            'a'.IsBetween('b', 'c').ShouldBeFalse();
        }

        [Fact]
        public void IsIn()
        {
            1.IsIn(1, 2, 3, 4, 5, 6, 7).ShouldBeTrue();
            8.IsIn(1, 2, 3, 4, 5, 6, 7).ShouldBeFalse();
        }
    }
}
