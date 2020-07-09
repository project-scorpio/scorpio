
using System;
using System.Collections.Generic;

using Scorpio.Entities;
using Scorpio.Repositories.EntityFrameworkCore;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    internal class EfCoreRepositoryRegistrar<TDbContext>
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        private readonly ScorpioDbContextOptionsBuilder<TDbContext> _optionsBuilder;

        public EfCoreRepositoryRegistrar(ScorpioDbContextOptionsBuilder<TDbContext> optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }

        public void RegisterRepositories()
        {
            typeof(TDbContext).GetEntityTypes().ForEach(RegisterForEntity);
        }

        private void RegisterForEntity(Type entityType)
        {
            var repositoryType = _optionsBuilder.RepositoryTypes.GetOrDefault(entityType) ?? GetDefaultRepositoryType(entityType);
            _optionsBuilder.Services.AddRepository(entityType, repositoryType);
        }

        private Type GetDefaultRepositoryType(Type entityType)
        {
            var defaultType = _optionsBuilder.DefaultRepositoryType;
            if (defaultType == typeof(EfCoreRepository<,,>))
            {
                var primaryKeyType = EntityHelper.FindPrimaryKeyType(entityType);
                defaultType = defaultType.MakeGenericType(typeof(TDbContext), entityType, primaryKeyType);
            }
            return defaultType;
        }
    }
}
