using System.Collections.Generic;

using Shouldly;

using Xunit;

namespace System.Linq.Expressions
{
    public class TranslationBuilder_Tests
    {
        [Fact]
        public void Translate11()
        {
            Expression<Func<object, bool>> func1 = a => a != null;
            var func2 = func1.Translate().To<string>();
            func2.Compile().Invoke("asdf").ShouldBeTrue();
            func2.Compile().Invoke(null).ShouldBeFalse();
        }

        [Fact]
        public void Translate12()
        {
            Expression<Func<double, bool>> func1 = (a) => a != 0;
            var func2 = func1.Translate().To<int>(m => m.Map<double, int>(a => a));
            func2.Compile().Invoke(1).ShouldBeTrue();
            func2.Compile().Invoke(0).ShouldBeFalse();
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int>(null).Compile().Invoke(2));
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
        public void Translate22()
        {
            Expression<Func<double, long, double>> func1 = (a, b) => a + b;
            var func2 = func1.Translate().To<int, long>(m => m.Map<double, int>(a => a));
            func2.Compile().Invoke(1, 2).ShouldBe(3);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<double, int>(null).Compile());
        }

        [Fact]
        public void Translate3()
        {
            Expression<Func<object, object, object, object>> func1 = (a, b, c) => a.ToString() + b.ToString() + c.ToString();
            var func2 = func1.Translate().To<string, string, object>();
            func2.Compile().Invoke("abc", "def", "ghi").ShouldBe("abcdefghi");
        }

        [Fact]
        public void Translate32()
        {
            Expression<Func<double, double, long, double>> func1 = (a, b, c) => a + b + c;
            var func2 = func1.Translate().To<int, int, long>(m => m.Map<double, int>(a => a));
            func2.Compile().Invoke(1, 2, 3).ShouldBe(6);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate33()
        {
            Expression<Func<double, float, long, double>> func1 = (a, b, c) => a + b + c;
            var func2 = func1.Translate().To<int, int, long>(m => m.Map<double, int>(a => a).Map<float, int>(a => a));
            func2.Compile().Invoke(1, 2, 3).ShouldBe(6);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate34()
        {
            Expression<Func<double, float, long, double>> func1 = (a, b, c) => a + b + c;
            var func2 = func1.Translate().To<int, long, long>(m => m.Map<double, int>(a => a).Map<float, long>(a => a));
            func2.Compile().Invoke(1, 2, 3).ShouldBe(6);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, long>(null).Compile());
        }



        [Fact]
        public void Translate4()
        {
            Expression<Func<object, object, object, object, object>> func1 = (a, b, c, d) => a.ToString() + b.ToString() + c.ToString() + d.ToString();
            var func2 = func1.Translate().To<string, string, string, object>();
            func2.Compile().Invoke("abc", "def", "ghi", "jkl").ShouldBe("abcdefghijkl");
        }

        [Fact]
        public void Translate42()
        {
            Expression<Func<double, double, double, double, double>> func1 = (a, b, c, d) => a + b + c + d;
            var func2 = func1.Translate().To<int, int, int, long>(m => m.Map<double, int>(a => a).Map<double, long>(a => a));
            func2.Compile().Invoke(1, 2, 3, 4).ShouldBe(10);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate43()
        {
            Expression<Func<double, float, double, double, double>> func1 = (a, b, c, d) => a + b + c + d;
            var func2 = func1.Translate().To<int, int, int, long>(m => m.Map<double, int>(a => a).Map<float, int>(a => a).Map<double, long>(a => a));
            func2.Compile().Invoke(1, 2, 3, 4).ShouldBe(10);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate44()
        {
            Expression<Func<double, float, double, double, double>> func1 = (a, b, c, d) => a + b + c + d;
            var func2 = func1.Translate().To<int, long, int, long>(m => m.Map<double, int>(a => a).Map<float, long>(a => a).Map<double, long>(a => a));
            func2.Compile().Invoke(1, 2, 3, 4).ShouldBe(10);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate45()
        {
            Expression<Func<float, float, double, double, double>> func1 = (a, b, c, d) => a + b + c + d;
            var func2 = func1.Translate().To<int, long, int, long>(m => m.Map<double, int>(a => a).Map<float, int>(a => a).Map<float, long>(a => a).Map<double, long>(a => a));
            func2.Compile().Invoke(1, 2, 3, 4).ShouldBe(10);
            Should.Throw<ArgumentNullException>(() => func1.Translate().To<int, int, int, long>(null).Compile());
        }

        [Fact]
        public void Translate_Member()
        {
            var data = new List<User>
            {
                new User{ Id=1,Name="User1"},
                new User{ Id=2,Name="User2"},
                new User{ Id=3,Name="User3"},
                new User{ Id=4,Name="User4"},
                new User{ Id=5,Name="User5"},
                new User{ Id=6,Name="User6"},
                new User{ Id=7,Name="User7"},
            }.AsQueryable();
            Expression<Func<Person, Person>> func1 = a => new Person { Id = a.Id };
            var func2 = func1.Translate<Person>().To<User>();
            func2.Compile().Invoke(new User()).ShouldNotBeNull();
            data.Select(func2).FirstOrDefault().Id.ShouldBe(1);
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
