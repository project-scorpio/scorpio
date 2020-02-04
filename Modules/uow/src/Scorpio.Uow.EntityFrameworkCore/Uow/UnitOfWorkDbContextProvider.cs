using Microsoft.EntityFrameworkCore;
using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    internal class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;

        public UnitOfWorkDbContextProvider(
            IServiceProvider serviceProvider,
           IUnitOfWorkManager unitOfWorkManager,
           IConnectionStringResolver connectionStringResolver)
        {
            _serviceProvider = serviceProvider;
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
        }
        public TDbContext GetDbContext()
        {
            if (!(_unitOfWorkManager.Current is EfUnitOfWork uow))
            {
                throw new  NotSupportedException($"UnitOfWork is not type of {typeof(EfUnitOfWork).FullName}.");
            }
            var connectionString = _connectionStringResolver.Resolve<TDbContext>();
           return uow.GetOrCreateDbContext<TDbContext>(connectionString);
        }


    }
}
