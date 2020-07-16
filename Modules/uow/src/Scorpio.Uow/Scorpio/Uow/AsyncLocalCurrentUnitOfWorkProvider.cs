using System.Threading;

using Scorpio.DependencyInjection;

namespace Scorpio.Uow
{
    class AsyncLocalCurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider, ISingletonDependency
    {
        public IUnitOfWork Current { get => GetCurrentUnitOfWork(); set => SetCurrentUnitOfWork(value); }

        private readonly AsyncLocal<IUnitOfWork> _currentUow;

        public AsyncLocalCurrentUnitOfWorkProvider()
        {
            _currentUow = new AsyncLocal<IUnitOfWork>();
        }
        private void SetCurrentUnitOfWork(IUnitOfWork value)
        {
            _currentUow.Value = value;
        }

        private IUnitOfWork GetCurrentUnitOfWork()
        {
            return _currentUow.Value;
        }
    }
}
