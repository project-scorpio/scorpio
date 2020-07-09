using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Scorpio.EventBus.TestClasses;
using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.EventBus
{
    public class EventBus_Tests : IntegratedTest<ServicedEventHandlerModule>
    {
        private readonly IEventBus _eventBus;

        public EventBus_Tests()
        {
            _eventBus = ServiceProvider.GetService<IEventBus>();
        }

        [Fact]
        public void RegisterAction()
        {
            _eventBus.Subscribe<string>(s => Task.Run(() => Console.WriteLine(s)));
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
        }

        [Fact]
        public void RegisterEventHandler()
        {
            var mock = new Mock<IEventHandler<string>>();
            _eventBus.Subscribe(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance.ShouldBe(mock.Object);
        }


        [Fact]
        public void ShouldRegisterGenericEventHandler()
        {
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(TestEventData));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(TestEventData)].ShouldHaveSingleItem()
                .ShouldBeOfType<IocEventHandlerFactory>().GetHandler().EventHandler
                .ShouldBeOfType<ServicedEventHandler>();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<TransientEventHandlerFactory>().GetHandler().EventHandler
                .ShouldBeOfType<EmptyEventHandler>();
        }

        [Fact]
        public void RegisterEventHandlerFactory()
        {
            var mock = new Mock<IEventHandlerFactory>();
            _eventBus.Subscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldHaveSingleItem().ShouldBe(mock.Object);
        }

        [Fact]
        public void UnRegisterAction()
        {
            Task action(string s) => Task.Run(() => Console.WriteLine(s));
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.Clear();
            _eventBus.Subscribe((Func<string, Task>)action);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
            _eventBus.Unsubscribe((Func<string, Task>)action);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();
        }

        [Fact]
        public void UnRegisterEventHandler()
        {
            var mock = new Mock<IEventHandler<string>>();
            _eventBus.Subscribe(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance.ShouldBe(mock.Object);
            _eventBus.Unsubscribe(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldBeEmpty();
        }

        [Fact]
        public void UnRegisterEventHandlerFactory()
        {
            var mock = new Mock<IEventHandlerFactory>();
            _eventBus.Subscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldHaveSingleItem().ShouldBe(mock.Object);
            _eventBus.Unsubscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldBeEmpty();

        }
    }
}
