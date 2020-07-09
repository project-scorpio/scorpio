using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Scorpio.Data
{
    class HasExtraPropertiesSaveChangeHandler : EntityFrameworkCore.IOnSaveChangeHandler
    {
        public Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            return Task.CompletedTask;
        }

        public Task PreSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            entries.ForEach(entry =>
           {
               if ((entry.Entity is IHasExtraProperties) && entry.State != Microsoft.EntityFrameworkCore.EntityState.Deleted)
               {
                   entry.Property(nameof(IHasExtraProperties.ExtraProperties)).IsModified = true;
               }
           });
            return Task.CompletedTask;
        }
    }
}
