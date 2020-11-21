using System;

using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
namespace Scorpio.Uow
{
    internal class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;

        public UnitOfWorkDbContextProvider(
            IUnitOfWorkManager unitOfWorkManager,
            IConnectionStringResolver connectionStringResolver)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
        }
        public TDbContext GetDbContext()
        {
            if (_unitOfWorkManager.Current is not EfUnitOfWork uow)
            {
                throw new NotSupportedException($"UnitOfWork is not type of {typeof(EfUnitOfWork).FullName}.");
            }
            var connectionString = _connectionStringResolver.Resolve<TDbContext>();
            return uow.GetOrCreateDbContext<TDbContext>(connectionString);
        }
    }
}
