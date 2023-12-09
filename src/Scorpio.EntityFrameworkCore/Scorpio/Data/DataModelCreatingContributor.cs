using System.Collections.Generic;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
#if !NET8_0_OR_GREATER
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif
using Scorpio.EntityFrameworkCore;

namespace Scorpio.Data
{
    internal class DataModelCreatingContributor : IModelCreatingContributor
    {
#if NET8_0_OR_GREATER
        private static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions();
#endif
        public void Contributor<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            ConfigureSoftDelete<TEntity>(context);
            ConfigureExtraProperties<TEntity>(context);
        }

        private static void ConfigureExtraProperties<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<IHasExtraProperties>())
            {
                context.ModelBuilder.Entity<TEntity>(e => e.Property(x => ((IHasExtraProperties)x).ExtraProperties).HasConversion(
#if NET8_0_OR_GREATER
                        d => JsonSerializer.Serialize(d, _serializerOptions),
                        s => JsonSerializer.Deserialize<ExtraPropertyDictionary>(s, _serializerOptions)
#else
                        d => JsonConvert.SerializeObject(d, Formatting.None),
                        s => JsonConvert.DeserializeObject<ExtraPropertyDictionary>(s)
#endif
                        ).HasColumnName(nameof(IHasExtraProperties.ExtraProperties)));

            }
        }

        private static void ConfigureSoftDelete<TEntity>(ModelCreatingContributionContext context) where TEntity : class
        {
            if (typeof(TEntity).IsAssignableTo<ISoftDelete>())
            {
                context.ModelBuilder.Entity<TEntity>(e => e.Property(nameof(ISoftDelete.IsDeleted)).IsRequired().HasColumnName(nameof(ISoftDelete.IsDeleted)).HasDefaultValue(false));
            }
        }
    }
}
