using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace System.Linq.Expressions
{
    public class PredicateBuilder_Tests
    {
        [Fact]
        public void Equal_T()
        {
            Expression<Func<int, bool>> func1 = a => a == 1;
            Expression<Func<int, bool>> func2 = a => a == 1;
            var func = func1.Equal(func2);
            func.Compile().Invoke(1).ShouldBeTrue();
        }

        [Fact]
        public void Equal_TT()
        {
            Expression<Func<int, int, bool>> func1 = (a, b) => a + b == 10;
            Expression<Func<int, int, bool>> func2 = (a, b) => a - b == 2;
            var func = func1.Equal(func2);
            func.Compile().Invoke(6, 4).ShouldBeTrue();
            func.Compile().Invoke(5, 5).ShouldBeFalse();
        }

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
        public void Translate()
        {
            Expression<Func<object, bool>> func1 = a => a != null;
            var func2 = func1.Translate().To<string>();
            func2.Compile().Invoke("asdf").ShouldBeTrue();
            func2.Compile().Invoke(null).ShouldBeFalse();
        }

        [Fact]
        public void Translate2()
        {
            Expression<Func<object, object, object>> func1 = (a, b) => a.ToString() + b.ToString();
            var func2 = func1.Translate().To<string, object>();
            func2.Compile().Invoke("asdf", "sdf").ShouldBe("asdfsdf");
            func2.Compile().Invoke("asdf", "asdf").ShouldBe("asdfasdf");
        }

        [Fact]
        public void Translate1()
        {
            Expression<Func<double, bool>> func1 = (a) => a != 0;
            var func2 = func1.Translate().To<int>(m => m.Map<double, int>(a => a));
            func2.Compile().Invoke(1).ShouldBeTrue();
            func2.Compile().Invoke(0).ShouldBeFalse();
            Expression<Func<double, long, double>> expression1 = (a, b) => a + b;
            expression1.Translate().To<int, long>(m => m.Map<double, int>(a => a)).Compile().Invoke(2, 3).ShouldBe(5);
        }
    }
}
