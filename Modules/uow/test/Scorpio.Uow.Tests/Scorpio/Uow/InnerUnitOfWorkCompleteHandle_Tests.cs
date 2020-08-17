using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Scorpio.TestBase;

using Shouldly;

using Xunit;

namespace Scorpio.Uow
{
    public class InnerUnitOfWorkCompleteHandle_Tests : IntegratedTest<TestModule>
    {
        [Fact]
        public void Complete()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                using (var innerUow = manager.Begin())
                {
                    innerUow.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>();
                    var handler = Substitute.For<EventHandler>();
                    innerUow.Completed += handler;
                    innerUow.Complete();
                    handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);
                }
            }
        }

        [Fact]
        public void CompleteAsync()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                using (var innerUow = manager.Begin())
                {
                    innerUow.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>();
                    var handler = Substitute.For<EventHandler>();
                    innerUow.Completed += handler;
                    Should.NotThrow(() => innerUow.CompleteAsync());
                    handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);
                }
            }
        }

        [Fact]
        public void Failed()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                var handler = Substitute.For<EventHandler<UnitOfWorkFailedEventArgs>>();
                using (var innerUow = manager.Begin())
                {
                    innerUow.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>();
                    innerUow.Failed += handler;
                }
                handler.ReceivedWithAnyArgs(1).Invoke(null, default);
            }
        }

        [Fact]
        public void Disposed()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                var handler = Substitute.For<EventHandler>();
                using (var innerUow = manager.Begin())
                {
                    innerUow.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>();
                    innerUow.Disposed += handler;
                }
                handler.ReceivedWithAnyArgs(1).Invoke(null, EventArgs.Empty);
            }
        }

    }
}
