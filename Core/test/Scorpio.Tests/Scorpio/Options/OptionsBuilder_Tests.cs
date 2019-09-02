using Microsoft.Extensions.DependencyInjection;
using Scorpio.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Shouldly;
namespace Scorpio.Tests.Scorpio.Options
{
    public class OptionsBuilder_Tests
    {
        private readonly OptionsBuilder<TestOptions> _optionsBuilder;
        private readonly IServiceCollection _serviceCollection;
        public OptionsBuilder_Tests()
        {
            _serviceCollection = new ServiceCollection();
            _optionsBuilder = new OptionsBuilder<TestOptions>(_serviceCollection,"");
        }

        [Fact]
        public void PreConfigure()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure(o => { });
            _serviceCollection.ShouldContainSingleton(typeof(IPreConfigureOptions<TestOptions>), typeof(PreConfigureOptions<TestOptions>))
                .ImplementationInstance.ShouldBeOfType<PreConfigureOptions<TestOptions>>().ShouldNotBeNull();
        }
        [Fact]
        public void PreConfigureD1()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure<Service1>((o,s) => { });
            _serviceCollection.ShouldContainTransient(typeof(IPreConfigureOptions<TestOptions>))
                .ImplementationFactory.ShouldBeOfType<Func<IServiceProvider, IPreConfigureOptions<TestOptions>>>();
        }

        [Fact]
        public void PreConfigureD2()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure<Service1, Service2>((o, s1, s2) => { });
            _serviceCollection.ShouldContainTransient(typeof(IPreConfigureOptions<TestOptions>))
                .ImplementationFactory.ShouldBeOfType<Func<IServiceProvider, IPreConfigureOptions<TestOptions>>>();
        }
        [Fact]
        public void PreConfigureD3()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure<Service1, Service2, IServiceProvider>((o, s1, s2, s3) => { });
            _serviceCollection.ShouldContainTransient(typeof(IPreConfigureOptions<TestOptions>))
                .ImplementationFactory.ShouldBeOfType<Func<IServiceProvider, IPreConfigureOptions<TestOptions>>>();
        }
        [Fact]
        public void PreConfigureD4()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure<Service1, Service2, IServiceProvider, IService3>((o, s1, s2, s3, s4) => { });
            _serviceCollection.ShouldContainTransient(typeof(IPreConfigureOptions<TestOptions>))
                .ImplementationFactory.ShouldBeOfType<Func<IServiceProvider, IPreConfigureOptions<TestOptions>>>();
        }
        [Fact]
        public void PreConfigureD5()
        {
            _serviceCollection.Clear();
            _optionsBuilder.PreConfigure<Service1, Service2, IServiceProvider, IService3,IService1>((o, s1, s2, s3, s4,s5) => { });
            _serviceCollection.ShouldContainTransient(typeof(IPreConfigureOptions<TestOptions>))
                .ImplementationFactory.ShouldBeOfType<Func< IServiceProvider,IPreConfigureOptions<TestOptions>>>();
        }
    }
}
