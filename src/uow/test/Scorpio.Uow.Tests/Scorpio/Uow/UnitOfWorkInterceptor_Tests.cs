using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;

using Shouldly;

using Xunit;

namespace Scorpio.Uow
{
    public class UnitOfWorkInterceptor_Tests : UnitOfWorkTestBase
    {

        [Fact]
        public void Method1()
        {
            var service = ServiceProvider.GetService<IUnitOfWorkTestInterface>();
            Should.NotThrow(() => service.Method1());
        }

        [Fact]
        public void Method1Async()
        {
            var service = ServiceProvider.GetService<IUnitOfWorkTestInterface>();
            Should.NotThrow(() => service.Method1Async());
        }

        [Fact]
        public void DisableMethod()
        {
            var service = ServiceProvider.GetService<IUnitOfWorkTestInterface>();
            Should.NotThrow(() => service.DisableMethod());
        }
    }

    [UnitOfWork]
    public interface IUnitOfWorkTestInterface
    {
        void Method1();
        Task Method1Async();

        void DisableMethod();
    }

    [UnitOfWork]
    public class UnitOfWorkTestInterface : IUnitOfWorkTestInterface, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkTestInterface(IUnitOfWorkManager unitOfWorkManager) => _unitOfWorkManager = unitOfWorkManager;

        [DisableUnitOfWork]
        public  virtual void DisableMethod() => _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().Options.Scope.ShouldBe(System.Transactions.TransactionScopeOption.Suppress);

        public virtual void Method1() => _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>().Options.Scope.ShouldBe(System.Transactions.TransactionScopeOption.Required);

        public virtual Task Method1Async()
        {
            _unitOfWorkManager.Current.ShouldBeOfType<NullUnitOfWork>();
            return Task.CompletedTask;
        }
    }
}
