using System.Collections.Generic;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using Scorpio.EntityFrameworkCore;

namespace Scorpio.Data
{
    class DataModelCreatingContributor : IModelCreatingContributor
    {
        public void Contributor<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            ConfigureSoftDelete<TEntity>(context);
            ConfigureExtraProperties<TEntity>(context);
        }

        private static void ConfigureExtraProperties<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<IHasExtraProperties>())
            {
                context.EntityType.AddAnnotation("test", "testValue");
                context.ModelBuilder.Entity<TEntity>(e =>
                {
                    e.Property(x => ((IHasExtraProperties)x).ExtraProperties).HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<Dictionary<string, object>>(s)
                        ).HasColumnName(nameof(IHasExtraProperties.ExtraProperties));
                });

            }
        }

        private static void ConfigureSoftDelete<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<ISoftDelete>())
            {
                context.ModelBuilder.Entity<TEntity>(e =>
                {
                    e.Property(nameof(ISoftDelete.IsDeleted)).IsRequired().HasColumnName(nameof(ISoftDelete.IsDeleted)).HasDefaultValue(false);
                });
            }
        }
    }
}
