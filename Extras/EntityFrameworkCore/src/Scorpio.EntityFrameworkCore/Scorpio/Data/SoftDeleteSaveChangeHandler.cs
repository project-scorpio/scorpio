using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Scorpio.Data
{
    class SoftDeleteSaveChangeHandler : EntityFrameworkCore.IOnSaveChangeHandler
    {
        public Task PostSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            return Task.CompletedTask;
        }

        public async Task PreSaveChangeAsync(IEnumerable<EntityEntry> entries)
        {
            await entries.ForEachAsync(async entry =>
            {
                if (!(entry.Entity is ISoftDelete) || entry.State != Microsoft.EntityFrameworkCore.EntityState.Deleted)
                {
                    return;
                }
                await entry.ReloadAsync();
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                entry.Entity.As<ISoftDelete>().IsDeleted = true;
            });
        }
    }
}
