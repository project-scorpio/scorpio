using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Moq;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

using Scorpio.EventBus.TestClasses;

using Shouldly;

using Xunit;

namespace Scorpio.EventBus
{
    public class EventBus_Tests : EventBusTestBase
    {


        [Fact]
        public void Subscribe_Action()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var subscriber = _eventBus.Subscribe<string>((o,s) => Task.Run(() => Console.WriteLine(s)));
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
            subscriber.Dispose();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldBeEmpty();

        }

        [Fact]
        public void Subscribe_S()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var mock = new Mock<IEventHandler<string>>();
            var subscriber = _eventBus.Subscribe(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().GetHandler().EventHandler.ShouldBe(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance.ShouldBe(mock.Object);
            subscriber.Dispose();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<SingleInstanceHandlerFactory>().ShouldBeEmpty();
        }

        [Fact]
        public void Subscribe_T()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var subscriber = _eventBus.Subscribe<string, EmptyEventHandler>();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<TransientEventHandlerFactory<EmptyEventHandler>>().ShouldHaveSingleItem()
                .ShouldBeOfType<TransientEventHandlerFactory<EmptyEventHandler>>().GetHandler().EventHandler.ShouldBeOfType<EmptyEventHandler>();
            subscriber.Dispose();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].OfType<TransientEventHandlerFactory<EmptyEventHandler>>().ShouldBeEmpty();
        }

        [Fact]
        public void Subscribe_TT()
        {
            var _eventBus = GetRequiredService<IEventBus>();
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
        public void Subscribe_F()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var mock = new Mock<IEventHandlerFactory>();
            var subscriber = _eventBus.Subscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldHaveSingleItem().ShouldBe(mock.Object);
            subscriber.Dispose();
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldNotContain(mock.Object);
        }

        [Fact]
        public void Unsubscribe_A()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            Task action(object sender,string s) => Task.Run(() => Console.WriteLine(s));

            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.Clear();
            _eventBus.Subscribe((Func<object,string, Task>)action);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldHaveSingleItem()
                .ShouldBeOfType<SingleInstanceHandlerFactory>().HandlerInstance
                .ShouldBeOfType<ActionEventHandler<string>>();
            _eventBus.Unsubscribe((Func<object,string, Task>)action);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();
        }

        [Fact]
        public void Unsubscribe()
        {
            var _eventBus = GetRequiredService<IEventBus>();
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
        public void Unsubscribe_F()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var mock = new Mock<IEventHandlerFactory>();
            _eventBus.Subscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>().HandlerFactories.ShouldContainKey(typeof(string));
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldHaveSingleItem().ShouldBe(mock.Object);
            _eventBus.Unsubscribe<string>(mock.Object);
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].Where(f => f.GetType() == mock.Object.GetType()).ShouldBeEmpty();

        }

        [Fact]
        public void UnsubscribeAll()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            _eventBus.UnsubscribeAll<string>();
            _eventBus.ShouldBeOfType<LocalEventBus>()
                .HandlerFactories[typeof(string)].ShouldBeEmpty();

        }

        [Fact]
        public void Publish()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var action = Substitute.For<Func<object, GenericEventData<string>, Task>>();
            action.Invoke(this,default).ReturnsForAnyArgs(Task.Delay(100).ContinueWith(t => Task.Delay(100)));
            var action2 = Substitute.For<Func<object,GenericEventData<object>, Task>>();
            action2.Invoke(this,default).ReturnsForAnyArgs(Task.CompletedTask);
            using (_eventBus.Subscribe<GenericEventData<string>, EmptyEventHandler<GenericEventData<string>>>())
            {
                using (_eventBus.Subscribe(action2))
                {
                    using (_eventBus.Subscribe(action))
                    {
                        Should.NotThrow(() => _eventBus.PublishAsync(this,new GenericEventData<string>("test")));
                        action.ReceivedWithAnyArgs(1).Invoke(this, default);
                        action2.ReceivedWithAnyArgs(1).Invoke(this, default);
                    }
                }

            }
        }
        [Fact]
        public void Publish_E()
        {
            var _eventBus = GetRequiredService<IEventBus>();
            var action = Substitute.For<Func<object,GenericEventData<string>, Task>>();
            action.Invoke(this, default).ThrowsForAnyArgs<NotSupportedException>();
            var action2 = Substitute.For<Func<object, GenericEventData<object>, Task>>();
            action2.Invoke(this, default).ThrowsForAnyArgs(c => new TargetInvocationException(new ScorpioException()));
            using (_eventBus.Subscribe(action))
            {
                Should.Throw<NotSupportedException>(() => _eventBus.PublishAsync(this,new GenericEventData<string>("test")));
            }

            using (_eventBus.Subscribe(action2))
            {
                Should.Throw<ScorpioException>(() => _eventBus.PublishAsync(this,new GenericEventData<string>("test")));
            }

            using (_eventBus.Subscribe(action2))
            {
                using (_eventBus.Subscribe(action))
                {
                    Should.Throw<AggregateException>(() => _eventBus.PublishAsync(this,new GenericEventData<string>("test")));
                }

            }

        }
    }
}
