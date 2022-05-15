using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Scorpio.Entities;
using Scorpio.Repositories;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ServiceCollectionRepositoryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="entityType"></param>
        /// <param name="repositoryImplementationType"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepository(this IServiceCollection services, Type entityType, Type repositoryImplementationType)
        {
            RegisterInterface<IReadOnlyBasicRepository<Entity>>(services, repositoryImplementationType, entityType, () =>
            {
                RegisterInterface<IReadOnlyRepository<Entity>>(services, repositoryImplementationType, entityType, () => { });
                RegisterInterface<IBasicRepository<Entity>>(services, repositoryImplementationType, entityType, () =>
                    RegisterInterface<IRepository<Entity>>(services, repositoryImplementationType, entityType, () => { })
                );
            });

            var primaryKeyType = EntityHelper.FindPrimaryKeyType(entityType);
            if (primaryKeyType != null)
            {
                RegisterInterface<IReadOnlyBasicRepository<Entity<int>, int>>(services, repositoryImplementationType, entityType, primaryKeyType, () =>
                  {
                      RegisterInterface<IReadOnlyRepository<Entity<int>, int>>(services, repositoryImplementationType, entityType, primaryKeyType, () => { });
                      RegisterInterface<IBasicRepository<Entity<int>, int>>(services, repositoryImplementationType, entityType, primaryKeyType, () =>
                          RegisterInterface<IRepository<Entity<int>, int>>(services, repositoryImplementationType, entityType, primaryKeyType, () => { })
                      );
                  });
            }
            return services;
        }

        private static void RegisterInterface<TRepository>(IServiceCollection services, Type implementationType, Type entityType, Action action)
        {
            var readOnlyRepositoryInterface = typeof(TRepository).GetGenericTypeDefinition().MakeGenericType(entityType);
            if (readOnlyRepositoryInterface.IsAssignableFrom(implementationType))
            {
                services.TryAddTransient(readOnlyRepositoryInterface, implementationType);
                action();
            }

        }
        private static void RegisterInterface<TRepository>(IServiceCollection services, Type implementationType, Type entityType, Type primaryKeyType, Action action)
        {
            var readOnlyRepositoryInterface = typeof(TRepository).GetGenericTypeDefinition().MakeGenericType(entityType, primaryKeyType);
            if (readOnlyRepositoryInterface.IsAssignableFrom(implementationType))
            {
                services.TryAddTransient(readOnlyRepositoryInterface, implementationType);
                action();
            }

        }
    }
}
