using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ModelCreatingContributionContext<TEntity> where TEntity :class
    {
        /// <summary>
        /// 
        /// </summary>
        public ModelBuilder ModelBuilder { get; }

        /// <summary>
        /// 
        /// </summary>
        public IMutableEntityType EntityType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        public ModelCreatingContributionContext(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            ModelBuilder = modelBuilder;
            EntityType = entityType;
        }
    }
}
