using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Scorpio.Data
{
    class DataModelCreatingContributor : IModelCreatingContributor
    {
        public void Contributor<TEntity>(ModelCreatingContributionContext<TEntity> context) where TEntity : class
        {
            ConfigureSoftDelete(context);
            ConfigureExtraProperties(context);
        }

        private static void ConfigureExtraProperties<TEntity>(ModelCreatingContributionContext<TEntity> context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<IHasExtraProperties>())
            {
                context.ModelBuilder.Entity<TEntity>(e =>
                {
                    e.Property(x => ((IHasExtraProperties)x).ExtraProperties).HasConversion(
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<Dictionary<string, object>>(s)
                        ).HasColumnName(nameof(IHasExtraProperties.ExtraProperties));
                });
            }
        }

        private static void ConfigureSoftDelete<TEntity>(ModelCreatingContributionContext<TEntity> context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<ISoftDelete>())
            {
                context.ModelBuilder.Entity<TEntity>(e =>
                {
                    e.Property(x => ((ISoftDelete)x).IsDeleted).IsRequired().HasColumnName(nameof(ISoftDelete.IsDeleted)).HasDefaultValue(false);
                });
            }
        }
    }
}
