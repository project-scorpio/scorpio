
using Shouldly;

using Xunit;

namespace System.Linq.Expressions
{
    public class ConditionalPredicateBuilder_Tests
    {
        [Fact]
        public void AndAlso_T1()
        {
            Expression<Func<int, bool>> func1 = a => a == 1;
            Expression<Func<int, bool>> func2 = a => a == 1;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(1).ShouldBeTrue();
        }

        [Fact]
        public void AndAlso_T2()
        {

            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeFalse();
            func.Compile().Invoke(4, 5).ShouldBeFalse();
        }

        [Fact]
        public void AndAlso_T3()
        {
            Expression<Func<int, int, int, bool>> func1 = (a, b, c) => a + b == c;
            Expression<Func<int, int, int, bool>> func2 = (a, b, c) => a - b == c;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 10).ShouldBeFalse();
            func.Compile().Invoke(5, 1, 1).ShouldBeFalse();
        }

        [Fact]
        public void AndAlso_T4()
        {
            Expression<Func<int, int, int, int, bool>> func1 = (a, b, c, d) => a + b == c + d;
            Expression<Func<int, int, int, int, bool>> func2 = (a, b, c, d) => a - b == c - d;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 6, 4).ShouldBeFalse();
            func.Compile().Invoke(5, 1, 1, 2).ShouldBeFalse();
        }

        [Fact]
        public void AndAlso_T5()
        {
            Expression<Func<int, int, int, int, int, bool>> func1 = (a, b, c, d, e) => a + b == c + d - e;
            Expression<Func<int, int, int, int, int, bool>> func2 = (a, b, c, d, e) => a - b == c - d + e;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(0, 0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(5, 5, 6, 5, 1).ShouldBeFalse();
            func.Compile().Invoke(5, 1, 1, 2, 3).ShouldBeFalse();
        }

        [Fact]
        public void OrElse_T1()
        {
            Expression<Func<int, bool>> func1 = a => a == 0;
            Expression<Func<int, bool>> func2 = a => a > -1;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(0).ShouldBeTrue();
            func.Compile().Invoke(2).ShouldBeTrue();
            func.Compile().Invoke(-2).ShouldBeFalse();
        }

        [Fact]
        public void OrElse_T2()
        {
            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeTrue();
            func.Compile().Invoke(4, 5).ShouldBeFalse();
        }

        [Fact]
        public void OrElse_T3()
        {
            Expression<Func<int, int, int, bool>> func1 = (a, b, c) => a + b == c;
            Expression<Func<int, int, int, bool>> func2 = (a, b, c) => a - b == c;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(6, 4, 2).ShouldBeTrue();
            func.Compile().Invoke(4, 5, 20).ShouldBeFalse();
        }

        [Fact]
        public void OrElse_T4()
        {
            Expression<Func<int, int, int, int, bool>> func1 = (a, b, c, d) => a + b == c + d;
            Expression<Func<int, int, int, int, bool>> func2 = (a, b, c, d) => a - b == c - d;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(6, 4, 2, 8).ShouldBeTrue();
            func.Compile().Invoke(4, 5, 20, 1).ShouldBeFalse();
        }

        [Fact]
        public void OrElse_T5()
        {
            Expression<Func<int, int, int, int, int, bool>> func1 = (a, b, c, d, e) => a + b == c + d - e;
            Expression<Func<int, int, int, int, int, bool>> func2 = (a, b, c, d, e) => a - b == c - d + e;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(0, 0, 0, 0, 0).ShouldBeTrue();
            func.Compile().Invoke(6, 4, 2, 8, 8).ShouldBeTrue();
            func.Compile().Invoke(4, 5, 20, 1, 10).ShouldBeFalse();
        }

    }
}
