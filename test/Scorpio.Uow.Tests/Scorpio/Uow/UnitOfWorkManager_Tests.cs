
using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Uow
{
    public class UnitOfWorkManager_Tests : UnitOfWorkTestBase
    {
        [Fact]
        public void Begin()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin())
            {
                uow.ShouldBeOfType<NullUnitOfWork>();

            }

        }

        [Fact]
        public void Begin_2()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            using (var uow = manager.Begin(System.Transactions.TransactionScopeOption.Required))
            {
                uow.ShouldBeOfType<NullUnitOfWork>();
                using (var innerUow = manager.Begin(System.Transactions.TransactionScopeOption.Required))
                {
                    innerUow.ShouldBeOfType<InnerUnitOfWorkCompleteHandle>();

                }
            }
        }

        [Fact]
        public void Current()
        {
            var manager = ServiceProvider.GetService<IUnitOfWorkManager>();
            manager.Current.ShouldBeNull();
            using (var uow = manager.Begin())
            {
                manager.Current.ShouldBeOfType<NullUnitOfWork>();
                using (var innerUow = manager.Begin(System.Transactions.TransactionScopeOption.Required))
                {
                    manager.Current.ShouldBeOfType<NullUnitOfWork>();
                }
            }
            manager.Current.ShouldBeNull();
        }

    }
}
