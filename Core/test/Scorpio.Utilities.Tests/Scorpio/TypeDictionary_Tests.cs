using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class TypeDictionary_Tests
    {
        [Fact]
        public void Add()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>();
            Should.Throw<ArgumentException>(() => td.Add(typeof(ServiceDescriptor), typeof(ServiceProvider)));
            Should.Throw<ArgumentException>(() => td.Add(typeof(ServiceCollection), typeof(ServiceDescriptor)));
            Should.Throw<ArgumentException>(() => td.Add(new KeyValuePair<Type, Type>(typeof(ServiceCollection), typeof(ServiceDescriptor))));
            Should.NotThrow(() => td.Add(typeof(ServiceCollection), typeof(ServiceProvider)));
            Should.Throw<ArgumentException>(() => td.Add<ServiceCollection, ServiceProvider>());
            td.Clear();
            Should.NotThrow(() => td.Add<ServiceCollection, ServiceProvider>());

        }

        [Fact]
        public void Property()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>();
            td.Count.ShouldBe(0);
            td.IsReadOnly.ShouldBeFalse();
            td.Keys.ShouldBeEmpty();
            td.Values.ShouldBeEmpty();
            td[typeof(ServiceCollection)] = typeof(ServiceProvider);
            td.Count.ShouldBe(1);
            td.IsReadOnly.ShouldBeFalse();
            td.Keys.ShouldNotBeEmpty();
            td.Values.ShouldNotBeEmpty();
            td[typeof(ServiceCollection)].ShouldBe(typeof(ServiceProvider));
        }

        [Fact]
        public void Contains()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.Contains<ServiceCollection>().ShouldBeTrue();
            td.ContainsKey(typeof(ServiceCollection)).ShouldBeTrue();
            (td as ICollection<KeyValuePair<Type, Type>>).Contains(new KeyValuePair<Type, Type>(typeof(ServiceCollection), typeof(ServiceProvider))).ShouldBeTrue();
        }

        [Fact]
        public void GetOrDefault()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.GetOrDefault<ServiceCollection>().ShouldNotBeNull();
            td.GetOrDefault<IServiceCollection>().ShouldBeNull();
        }
        [Fact]
        public void Remove()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.Throw<ArgumentNullException>(() => td.Remove(null));
            Should.Throw<ArgumentException>(() => td.Remove(typeof(ServiceProvider)));
            Should.NotThrow(() => td.Remove(typeof(IServiceCollection))).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove(typeof(ServiceCollection))).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContainKey(typeof(ServiceCollection));

        }

        [Fact]
        public void Remove_T()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove<IServiceCollection>()).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.NotThrow(() => td.Remove<ServiceCollection>()).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContainKey(typeof(ServiceCollection));

        }

        [Fact]
        public void ICollection_Remove()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var tdc = td as ICollection<KeyValuePair<Type, Type>>;
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.Throw<ArgumentNullException>(() => tdc.Remove(default).ShouldBeFalse());
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(typeof(IServiceCollection), typeof(ServiceProvider)))).ShouldBeFalse();
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(typeof(ServiceCollection), typeof(IServiceProvider)))).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(typeof(ServiceCollection), typeof(ServiceProvider)))).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContainKey(typeof(ServiceCollection));

        }

        [Fact]
        public void CopyTo()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var tdc = td as ICollection<KeyValuePair<Type, Type>>;
            var array = new KeyValuePair<Type, Type>[1];
            Should.Throw<ArgumentNullException>(() => tdc.CopyTo(null, 0));
            Should.Throw<ArgumentException>(() => tdc.CopyTo(array, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => tdc.CopyTo(array, -1));
            Should.NotThrow(() => tdc.CopyTo(array, 0));
            array[0].ShouldBe(KeyValuePair.Create(typeof(ServiceCollection), typeof(ServiceProvider)));
        }

        [Fact]
        public void GetEnumerator_T()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var e = (td as IEnumerable<KeyValuePair<Type, Type>>).GetEnumerator();
            e.ShouldNotBeNull();
            e.Current.ShouldBe(default);
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(KeyValuePair.Create(typeof(ServiceCollection), typeof(ServiceProvider)));
        }

        [Fact]
        public void TryAdd()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>();
            Should.NotThrow(() => td.TryAdd<ServiceCollection, ServiceProvider>()).ShouldBeTrue();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(typeof(ServiceCollection));
            Should.NotThrow(() => td.TryAdd<ServiceCollection, ServiceProvider>()).ShouldBeFalse();
            td.Count.ShouldBe(1);
        }

        [Fact]
        public void TryGetValue()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            Should.NotThrow(() => { td.TryGetValue(typeof(IServiceCollection), out var value).ShouldBeFalse(); value.ShouldBeNull(); });
            Should.NotThrow(() => { td.TryGetValue(typeof(ServiceCollection), out var value).ShouldBeTrue(); value.ShouldBe(typeof(ServiceProvider)); });
        }

        [Fact]
        public void GetEnumerator()
        {
            var td = new TypeDictionary<IServiceCollection, IServiceProvider>
            {
                [typeof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var e = (td as IEnumerable).GetEnumerator();
            e.ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => e.Current.ShouldBe(default));
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(KeyValuePair.Create(typeof(ServiceCollection), typeof(ServiceProvider)));
        }
    }
}
