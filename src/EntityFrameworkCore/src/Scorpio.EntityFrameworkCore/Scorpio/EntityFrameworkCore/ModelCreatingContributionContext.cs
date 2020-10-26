using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ModelCreatingContributionContext
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
