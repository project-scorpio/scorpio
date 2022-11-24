using System;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;
using Scorpio.EventBus.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.EventBus
{
    public class EventHandlerDescriptor_Tests
    {
        [Fact]
        public void Describe()
        {
            Should.Throw<ArgumentException>(() => EventHandlerDescriptor.Describe(typeof(object), EventHandlerActivationType.Singleton));
            var act = EventHandlerDescriptor.Describe(typeof(EmptyEventHandler), EventHandlerActivationType.Singleton);
            act.ActivationType.ShouldBe(EventHandlerActivationType.Singleton);
            act.HandlerType.ShouldBe(typeof(EmptyEventHandler));
        }

        [Fact]
        public void Describe_T()
        {
            var act = EventHandlerDescriptor.Describe<EmptyEventHandler>(EventHandlerActivationType.Singleton);
            act.ActivationType.ShouldBe(EventHandlerActivationType.Singleton);
            act.HandlerType.ShouldBe(typeof(EmptyEventHandler));
        }

        [Fact]
        public void ByServiceProvider()
        {
            var act = EventHandlerDescriptor.ByServiceProvider<EmptyEventHandler>();
            act.ActivationType.ShouldBe(EventHandlerActivationType.ByServiceProvider);
            act.HandlerType.ShouldBe(typeof(EmptyEventHandler));
        }

        [Fact]
        public void Singleton()
        {
            var act = EventHandlerDescriptor.Singleton<EmptyEventHandler>();
            act.ActivationType.ShouldBe(EventHandlerActivationType.Singleton);
            act.HandlerType.ShouldBe(typeof(EmptyEventHandler));
        }

        [Fact]
        public void Transient()
        {
            var act = EventHandlerDescriptor.Transient<EmptyEventHandler>();
            act.ActivationType.ShouldBe(EventHandlerActivationType.Transient);
            act.HandlerType.ShouldBe(typeof(EmptyEventHandler));
        }

        [Fact]
        public void GetEventHandlerFactory()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHybridServiceScopeFactory, DefaultServiceScopeFactory>()
                .AddTransient<EmptyEventHandler>()
                .BuildServiceProvider();
            var handler = EventHandlerDescriptor.ByServiceProvider<EmptyEventHandler>()
                 .GetEventHandlerFactory(serviceProvider).ShouldBeOfType<IocEventHandlerFactory>()
                 .GetHandler();
            handler.EventHandler.ShouldBeOfType<EmptyEventHandler>();
            handler.Dispose();
            EventHandlerDescriptor.Singleton<EmptyEventHandler>()
                .GetEventHandlerFactory(serviceProvider).ShouldBeOfType<SingleInstanceHandlerFactory>()
                .GetHandler().EventHandler.ShouldBeOfType<EmptyEventHandler>();
            EventHandlerDescriptor.Transient<EmptyEventHandler>()
                .GetEventHandlerFactory(serviceProvider).ShouldBeOfType<TransientEventHandlerFactory>()
                .GetHandler().EventHandler.ShouldBeOfType<EmptyEventHandler>();
        }
    }
}
