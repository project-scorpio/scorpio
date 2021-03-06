﻿using System;
using System.Transactions;

using Microsoft.Extensions.DependencyInjection;

using Scorpio.DependencyInjection;
namespace Scorpio.Uow
{
    /// <summary>
    /// Unit of work manager.
    /// </summary>
    internal class UnitOfWorkManager : IUnitOfWorkManager, ISingletonDependency
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public IActiveUnitOfWork Current => _currentUnitOfWorkProvider.Current;

        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkManager(
            IServiceProvider serviceProvider,
            ICurrentUnitOfWorkProvider currentUnitOfWorkProvider
           )
        {
            _serviceProvider = serviceProvider;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;

        }

        public IUnitOfWorkCompleteHandle Begin() => Begin(new UnitOfWorkOptions());

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope) => Begin(new UnitOfWorkOptions { Scope = scope });

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {

            var outerUow = _currentUnitOfWorkProvider.Current;

            if ((options.Scope ?? TransactionScopeOption.Required) == TransactionScopeOption.Required && outerUow != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }

            var uow = CreateNewUnitOfWork();
            uow.Begin(options);
            return uow;
        }

        private IUnitOfWork CreateNewUnitOfWork()
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var outerUow = _currentUnitOfWorkProvider.Current;

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                unitOfWork.SetOuter(outerUow);

                _currentUnitOfWorkProvider.Current = unitOfWork;

                unitOfWork.Disposed += (sender, args) =>
                {
                    _currentUnitOfWorkProvider.Current = outerUow;
                    scope.Dispose();
                };

                return unitOfWork;

            }
        }
    }
}
