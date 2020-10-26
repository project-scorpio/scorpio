using System.Linq;

using Scorpio.EventBus.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.EventBus
{
    public class EventBusOptions_Tests
    {
        [Fact]
        public void AddHandler()
        {
            var options = new EventBusOptions();
            options.AddHandler(EventHandlerDescriptor.Transient<EmptyEventHandler>());
            options.Handlers.Count.ShouldBe(1);
            options.Handlers.First().HandlerType.ShouldBe(typeof(EmptyEventHandler));
            options.Handlers.First().ActivationType.ShouldBe(EventHandlerActivationType.Transient);
        }

        [Fact]
        public void AddHandler_Type()
        {
            var options = new EventBusOptions();
            options.AddHandler(typeof(EmptyEventHandler), EventHandlerActivationType.Transient);
            options.Handlers.Count.ShouldBe(1);
            options.Handlers.First().HandlerType.ShouldBe(typeof(EmptyEventHandler));
            options.Handlers.First().ActivationType.ShouldBe(EventHandlerActivationType.Transient);
        }

        [Fact]
        public void AddHandler_T()
        {
            var options = new EventBusOptions();
            options.AddHandler<EmptyEventHandler>(EventHandlerActivationType.Transient);
            options.Handlers.Count.ShouldBe(1);
            options.Handlers.First().HandlerType.ShouldBe(typeof(EmptyEventHandler));
            options.Handlers.First().ActivationType.ShouldBe(EventHandlerActivationType.Transient);
        }

        [Fact]
        public void AddTransientHandler()
        {
            var options = new EventBusOptions();
            options.AddTransientHandler<EmptyEventHandler>();
            options.Handlers.Count.ShouldBe(1);
            options.Handlers.First().HandlerType.ShouldBe(typeof(EmptyEventHandler));
            options.Handlers.First().ActivationType.ShouldBe(EventHandlerActivationType.Transient);
        }
        [Fact]
        public void AddSingletonHandler()
        {
            var options = new EventBusOptions();
            options.AddSingletonHandler<EmptyEventHandler>();
            options.Handlers.Count.ShouldBe(1);
            options.Handlers.First().HandlerType.ShouldBe(typeof(EmptyEventHandler));
            options.Handlers.First().ActivationType.ShouldBe(EventHandlerActivationType.Singleton);
        }
    }
}
