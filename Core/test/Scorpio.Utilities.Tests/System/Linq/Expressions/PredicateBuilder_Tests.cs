
using Shouldly;

using Xunit;

namespace System.Linq.Expressions
{
    public class PredicateBuilder_Tests
    {
        [Fact]
        public void True()
        {
            PredicateBuilder.True<string>().Compile()("test").ShouldBeTrue();
            PredicateBuilder.True<string>().Compile()(null).ShouldBeTrue();
        }

        [Fact]
        public void False()
        {
            PredicateBuilder.False<string>().Compile()("test").ShouldBeFalse();
            PredicateBuilder.False<string>().Compile()(null).ShouldBeFalse();
        }

        [Fact]
        public void Equal_T1()
        {
            Expression<Func<int, bool>> func1 = a => a == 1;
            Expression<Func<int, bool>> func2 = a => a == 1;
            var func = func1.Equal(func2);
            func.Compile().Invoke(1).ShouldBeTrue();
        }

        [Fact]
        public void Equal_T2()
        {
            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.Equal(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeFalse();
        }

        [Fact]
        public void Equal_T3()
        {
            Expression<Func<int, int, int, bool>> func1 = (a, b, c) => a + b == c;
            Expression<Func<int, int, int, bool>> func2 = (a, b, c) => a - b == c;
            var func = func1.Equal(func2);
            func.Compile().Invoke(0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(1, 2, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 10).ShouldBeFalse();
        }

        [Fact]
        public void Equal_T4()
        {
            Expression<Func<int, int, int, int, bool>> func1 = (a, b, c, d) => a + b == c + d;
            Expression<Func<int, int, int, int, bool>> func2 = (a, b, c, d) => a - b == c - d;
            var func = func1.Equal(func2);
            func.Compile().Invoke(0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(1, 2, 4, 6).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 10, 0).ShouldBeFalse();
        }

        [Fact]
        public void Equal_T5()
        {
            Expression<Func<int, int, int, int, int, bool>> func1 = (a, b, c, d, e) => a + b == c + d - e;
            Expression<Func<int, int, int, int, int, bool>> func2 = (a, b, c, d, e) => a - b == c - d + e;
            var func = func1.Equal(func2);
            func.Compile().Invoke(0, 0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(1, 2, 4, 6, 8).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 10, 0, 0).ShouldBeFalse();
        }


    }


}
