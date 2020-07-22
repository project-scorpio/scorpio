using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;

using Xunit;

namespace System.Linq.Expressions
{
    public class ConditionalPredicateBuilder_Tests
    {
        [Fact]
        public void AndAlso_T()
        {
            Expression<Func<int, bool>> func1 = a => a == 1;
            Expression<Func<int, bool>> func2 = a => a == 1;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(1).ShouldBeTrue();
        }

        [Fact]
        public void AndAlso_TT()
        {
            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.AndAlso(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeFalse();
            func.Compile().Invoke(4, 5).ShouldBeFalse();
        }
        [Fact]
        public void OrElse_TT()
        {
            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.OrElse(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeTrue();
            func.Compile().Invoke(4, 2).ShouldBeTrue();
            func.Compile().Invoke(4, 5).ShouldBeFalse();
        }
    }
}
