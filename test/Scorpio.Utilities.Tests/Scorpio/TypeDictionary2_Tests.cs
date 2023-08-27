using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio
{
    public class TypeDictionary2_Tests
    {
        [Fact]
        public void Add()
        {
            var td = new TypeDictionary<IServiceProvider>();
            Should.Throw<ArgumentException>(() => td.Add("ServiceDescriptor", typeof(ServiceDescriptor)));
            Should.Throw<ArgumentException>(() => td.Add(new KeyValuePair<string, Type>("ServiceDescriptor", typeof(ServiceDescriptor))));
            Should.NotThrow(() => td.Add("ServiceDescriptor", typeof(ServiceProvider)));
            Should.Throw<ArgumentException>(() => td.Add<ServiceProvider>("ServiceDescriptor"));
            td.Clear();
            Should.NotThrow(() => td.Add<ServiceProvider>("ServiceDescriptor"));
        }

        [Fact]
        public void Property()
        {
            var td = new TypeDictionary<IServiceProvider>();
            td.Count.ShouldBe(0);
            td.IsReadOnly.ShouldBeFalse();
            td.Keys.ShouldBeEmpty();
            td.Values.ShouldBeEmpty();
            td[nameof(ServiceCollection)] = typeof(ServiceProvider);
            td.Count.ShouldBe(1);
            td.IsReadOnly.ShouldBeFalse();
            td.Keys.ShouldNotBeEmpty();
            td.Values.ShouldNotBeEmpty();
            td[nameof(ServiceCollection)].ShouldBe(typeof(ServiceProvider));
        }

        [Fact]
        public void Contains()
        {
            var td = new TypeDictionary<IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.Contains(nameof(ServiceCollection)).ShouldBeTrue();
            td.ContainsKey(nameof(ServiceCollection)).ShouldBeTrue();
            (td as ICollection<KeyValuePair<string, Type>>).Contains(new KeyValuePair<string, Type>(nameof(ServiceCollection), typeof(ServiceProvider))).ShouldBeTrue();
        }

        [Fact]
        public void GetOrDefault()
        {
            var td = new TypeDictionary<IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.GetOrDefault(nameof(ServiceCollection)).ShouldNotBeNull();
            td.GetOrDefault(nameof(IServiceCollection)).ShouldBeNull();
        }
        [Fact]
        public void Remove()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            td.Count.ShouldBe(1);
            td.ShouldContainKey(nameof(ServiceCollection));
            Should.Throw<ArgumentNullException>(() => td.Remove(null));
            Should.NotThrow(() => td.Remove(nameof(ServiceProvider)));
            Should.NotThrow(() => td.Remove(nameof(IServiceCollection))).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(nameof(ServiceCollection));
            Should.NotThrow(() => td.Remove(nameof(ServiceCollection))).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContainKey(nameof(ServiceCollection));

        }

     

        [Fact]
        public void ICollection_Remove()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var tdc = td as ICollection<KeyValuePair<string, Type>>;
            td.Count.ShouldBe(1);
            td.ShouldContainKey(nameof(ServiceCollection));
            Should.Throw<ArgumentNullException>(() => tdc.Remove(default).ShouldBeFalse());
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(nameof(IServiceCollection), typeof(ServiceProvider)))).ShouldBeFalse();
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(nameof(ServiceCollection), typeof(IServiceProvider)))).ShouldBeFalse();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(nameof(ServiceCollection));
            Should.NotThrow(() => tdc.Remove(KeyValuePair.Create(nameof(ServiceCollection), typeof(ServiceProvider)))).ShouldBeTrue();
            td.Count.ShouldBe(0);
            td.ShouldNotContainKey(nameof(ServiceCollection));

        }

        [Fact]
        public void CopyTo()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var tdc = td as ICollection<KeyValuePair<string, Type>>;
            var array = new KeyValuePair<string, Type>[1];
            Should.Throw<ArgumentNullException>(() => tdc.CopyTo(null, 0));
            Should.Throw<ArgumentException>(() => tdc.CopyTo(array, 1));
            Should.Throw<ArgumentOutOfRangeException>(() => tdc.CopyTo(array, -1));
            Should.NotThrow(() => tdc.CopyTo(array, 0));
            array[0].ShouldBe(KeyValuePair.Create(nameof(ServiceCollection), typeof(ServiceProvider)));
        }

        [Fact]
        public void GetEnumerator_T()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var e = (td as IEnumerable<KeyValuePair<string, Type>>).GetEnumerator();
            e.ShouldNotBeNull();
            e.Current.ShouldBe(default);
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(KeyValuePair.Create(nameof(ServiceCollection), typeof(ServiceProvider)));
        }

        [Fact]
        public void TryAdd()
        {
            var td = new TypeDictionary< IServiceProvider>();
            Should.NotThrow(() => td.TryAdd<ServiceProvider>(nameof(ServiceCollection))).ShouldBeTrue();
            td.Count.ShouldBe(1);
            td.ShouldContainKey(nameof(ServiceCollection));
            Should.NotThrow(() => td.TryAdd<ServiceProvider>(nameof(ServiceCollection))).ShouldBeFalse();
            td.Count.ShouldBe(1);
        }

        [Fact]
        public void TryGetValue()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            Should.NotThrow(() => { td.TryGetValue(nameof(IServiceCollection), out var value).ShouldBeFalse(); value.ShouldBeNull(); });
            Should.NotThrow(() => { td.TryGetValue(nameof(ServiceCollection), out var value).ShouldBeTrue(); value.ShouldBe(typeof(ServiceProvider)); });
        }

        [Fact]
        public void GetEnumerator()
        {
            var td = new TypeDictionary< IServiceProvider>
            {
                [nameof(ServiceCollection)] = typeof(ServiceProvider)
            };
            var e = (td as IEnumerable).GetEnumerator();
            e.ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => e.Current.ShouldBe(default));
            e.MoveNext().ShouldBeTrue();
            e.Current.ShouldBe(KeyValuePair.Create(nameof(ServiceCollection), typeof(ServiceProvider)));
        }
    }
}
