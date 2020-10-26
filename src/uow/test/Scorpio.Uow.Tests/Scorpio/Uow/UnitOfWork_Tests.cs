using System;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Uow
{
    public class UnitOfWork_Tests : IntegratedTest<TestModule>
    {
        [Fact]
        public void Complete()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                var handler = Substitute.For<EventHandler>();
                uow.Completed += handler;
                uow.Complete();
                handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);
            }
        }

        [Fact]
        public void Begin()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                Should.Throw<ArgumentNullException>(() => uow.ShouldBeOfType<NullUnitOfWork>().Begin(null));
                Should.Throw<ScorpioException>(() => uow.ShouldBeOfType<NullUnitOfWork>().Begin(new UnitOfWorkOptions()));
            }
        }

        [Fact]
        public void Complete_E()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                Should.NotThrow(() => uow.Complete());
                Should.Throw<ScorpioException>(() => uow.Complete());
            }
        }

        [Fact]
        public void CompleteAsync()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                var handler = Substitute.For<EventHandler>();
                uow.Completed += handler;
                Should.NotThrow(() => uow.CompleteAsync());
                handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);
            }
        }

        [Fact]
        public void CompleteAsync_E()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                Should.NotThrow(() => uow.CompleteAsync());
                Should.Throw<ScorpioException>(() => uow.CompleteAsync());
            }
        }

        [Fact]
        public void Failed()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            var handler = Substitute.For<EventHandler<UnitOfWorkFailedEventArgs>>();
            using (var uow = manager.Begin())
            {
                uow.Failed += handler;
            }
            handler.ReceivedWithAnyArgs(1).Invoke(null, default);
        }

        [Fact]
        public void Disposed()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            var handler = Substitute.For<EventHandler>();
            using (var uow = manager.Begin())
            {
                uow.Disposed += handler;
            }
            handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);

        }

        [Fact]
        public void Outer()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                using (var innerUow = manager.Begin(System.Transactions.TransactionScopeOption.RequiresNew))
                {
                    innerUow.ShouldBeOfType<NullUnitOfWork>().Outer.ShouldBe(uow);
                }
            }
        }

    }
}
