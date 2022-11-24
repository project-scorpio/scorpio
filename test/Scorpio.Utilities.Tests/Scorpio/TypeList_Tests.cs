using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class TypeList_Tests
    {
        [Fact]
        public void Add()
        {
            var td = new TypeList<IServiceCollection>();
            Should.Throw<ArgumentException>(() => td.Add(typeof(ServiceDescriptor)));
            Should.Throw<ArgumentNullException>(() => td.Add(null));
            Should.NotThrow(() => td.Add(typeof(ServiceCollection)));
            td.Count.ShouldBe(1);
            td[0].ShouldBe(typeof(ServiceCollection));
            Should.NotThrow(() => td.Add<ServiceCollection>());
            td.Count.ShouldBe(2);
            td[1].ShouldBe(typeof(ServiceCollection));
        }

        [Fact]
        public void Property()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            td.Count.ShouldBe(1);
            td.IsReadOnly.ShouldBeFalse();
            td[0].ShouldBe(typeof(ServiceCollection));
            Should.Throw<ArgumentException>(() => td[0] = typeof(ServiceProvider));
            Should.NotThrow(() => td[0] = typeof(IServiceCollection));
            td.IsReadOnly.ShouldBeFalse();
            td[0].ShouldBe(typeof(IServiceCollection));
        }

        [Fact]
        public void Contains()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            td.Contains<ServiceCollection>().ShouldBeTrue();
            td.Contains(typeof(ServiceCollection)).ShouldBeTrue();
            td.Contains<IServiceCollection>().ShouldBeFalse();
            td.Contains(typeof(IServiceCollection)).ShouldBeFalse();
            Should.Throw<ArgumentException>(() => td.Contains(typeof(IServiceProvider)));
        }


        [Fact]
        public void Remove()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            td.Count.ShouldBe(1);
            td.ShouldContain(typeof(ServiceCollection));
            Should.Throw<ArgumentNullException>(() => td.Remove(null));
            Should.Throw<ArgumentException>(() => td.Remove(typeof(ServiceProvider)));
            Should.NotThrow(() => td.Remove(typeof(IServiceCollection))).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContain(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove(typeof(ServiceCollection))).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContain(typeof(ServiceCollection));

        }

        [Fact]
        public void Remove_T()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            td.Count.ShouldBe(1);
            td.ShouldContain(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove<IServiceCollection>()).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContain(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove<ServiceCollection>()).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContain(typeof(ServiceCollection));

        }



        [Fact]
        public void CopyTo()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            var array = new Type[1];
            Should.Throw<ArgumentNullException>(() => td.CopyTo(null, 0));
            Should.Throw<ArgumentException>(() => td.CopyTo(array, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => td.CopyTo(array, -1));
            Should.NotThrow(() => td.CopyTo(array, 0));
            array[0].ShouldBe(typeof(ServiceCollection));
        }

        [Fact]
        public void GetEnumerator_T()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            var e = (td as IEnumerable<Type>).GetEnumerator();
            e.ShouldNotBeNull();
            e.Current.ShouldBe(default);
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(typeof(ServiceCollection));
        }

        [Fact]
        public void TryAdd()
        {
            var td = new TypeList<IServiceCollection>();
            Should.NotThrow(() => td.TryAdd<ServiceCollection>()).ShouldBeTrue();
            td.Count.ShouldBe(1);
            td.ShouldContain(typeof(ServiceCollection));
            Should.NotThrow(() => td.TryAdd<ServiceCollection>()).ShouldBeFalse();
            td.Count.ShouldBe(1);
        }

        [Fact]
        public void GetEnumerator()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            var e = (td as IEnumerable).GetEnumerator();
            e.ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => e.Current.ShouldBe(default));
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(typeof(ServiceCollection));
        }

        [Fact]
        public void Insert()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            td.Count.ShouldBe(1);
            td[0].ShouldBe(typeof(ServiceCollection));
            Should.Throw<ArgumentException>(() => td.Insert(0, typeof(ServiceDescriptor)));
            Should.Throw<ArgumentNullException>(() => td.Insert(0, null));
            Should.Throw<ArgumentOutOfRangeException>(() => td.Insert(-1, typeof(IServiceCollection)));
            Should.Throw<ArgumentOutOfRangeException>(() => td.Insert(2, typeof(IServiceCollection)));
            Should.NotThrow(() => td.Insert(0, typeof(IServiceCollection)));
            td[0].ShouldBe(typeof(IServiceCollection));
            td[1].ShouldBe(typeof(ServiceCollection));

        }

        [Fact]
        public void IndexOf()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection) };
            Should.Throw<ArgumentException>(() => td.IndexOf(typeof(ServiceDescriptor)));
            Should.Throw<ArgumentNullException>(() => td.IndexOf(null));
            Should.NotThrow(() => td.IndexOf(typeof(IServiceCollection)).ShouldBe(-1));
            Should.NotThrow(() => td.IndexOf(typeof(ServiceCollection)).ShouldBe(0));
        }

        [Fact]
        public void RemoveAt()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection), typeof(IServiceCollection) };
            td.Count.ShouldBe(2);
            td[0].ShouldBe(typeof(ServiceCollection));
            td[1].ShouldBe(typeof(IServiceCollection));
            Should.Throw<ArgumentOutOfRangeException>(() => td.RemoveAt(2));
            Should.Throw<ArgumentOutOfRangeException>(() => td.RemoveAt(-1));
            Should.NotThrow(() => td.RemoveAt(0));
            td[0].ShouldBe(typeof(IServiceCollection));

        }

        [Fact]
        public void Clear()
        {
            var td = new TypeList<IServiceCollection> { typeof(ServiceCollection), typeof(IServiceCollection) };
            td.Count.ShouldBe(2);
            td[0].ShouldBe(typeof(ServiceCollection));
            td[1].ShouldBe(typeof(IServiceCollection));
            Should.NotThrow(() => td.Clear());
            td.ShouldBeEmpty();
            Should.NotThrow(() => td.Clear());
            td.ShouldBeEmpty();

        }
    }
}
