using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
