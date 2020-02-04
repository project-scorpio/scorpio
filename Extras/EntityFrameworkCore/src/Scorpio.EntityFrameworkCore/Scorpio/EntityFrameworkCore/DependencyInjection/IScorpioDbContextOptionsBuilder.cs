using Microsoft.EntityFrameworkCore;

using Scorpio.Entities;
using Scorpio.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioDbContextOptionsBuilder<TDbContext> : IScorpioDbContextOptionsBuilder
        where TDbContext : ScorpioDbContext<TDbContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TRepository"></typeparam>
        void AddRepository<TEntity, TRepository>()
            where TEntity : class, IEntity
            where TRepository : IRepository<TEntity>;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="repositoryType"></param>
        void AddRepository(Type entityType, Type repositoryType);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioDbContextOptionsBuilder
    {

    }
}
