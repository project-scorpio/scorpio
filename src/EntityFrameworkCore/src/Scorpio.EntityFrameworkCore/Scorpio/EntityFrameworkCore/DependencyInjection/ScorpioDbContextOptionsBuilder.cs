using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Scorpio.Entities;
using Scorpio.Repositories;
using Scorpio.Repositories.EntityFrameworkCore;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    internal class ScorpioDbContextOptionsBuilder : IScorpioDbContextOptionsBuilder
    {
        public IServiceCollection Services { get; }

        public ScorpioDbContextOptionsBuilder(IServiceCollection services) => Services = services;

    }

    internal class ScorpioDbContextOptionsBuilder<TDbContext> : ScorpioDbContextOptionsBuilder, IScorpioDbContextOptionsBuilder<TDbContext>
    where TDbContext : ScorpioDbContext<TDbContext>
    {
        public Type DefaultRepositoryType { get; set; } = typeof(EfCoreRepository<,,>);

        public ITypeDictionary<IEntity, IRepository> RepositoryTypes { get; }

        public List<Action<DbContextOptionsBuilder<TDbContext>>> OptionsActions { get; }

        public ScorpioDbContextOptionsBuilder(IServiceCollection services) : base(services)
        {
            OptionsActions = new List<Action<DbContextOptionsBuilder<TDbContext>>>();
            RepositoryTypes = new TypeDictionary<IEntity, IRepository>();
        }

        public void AddRepository(Type entityType, Type repositoryType)
        {
            if (!entityType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntity<>)))
            {
                throw new ScorpioException($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity<>).AssemblyQualifiedName}.");
            }
            if (!repositoryType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEfCoreRepository<>)))
            {
                throw new ScorpioException($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEfCoreRepository<>).AssemblyQualifiedName}.");
            }
            RepositoryTypes.Add(entityType, repositoryType);
        }

        public void AddRepository<TEntity, TRepository>()
            where TEntity : class, IEntity
            where TRepository : IRepository<TEntity> => RepositoryTypes.Add<TEntity, TRepository>();


    }

}
