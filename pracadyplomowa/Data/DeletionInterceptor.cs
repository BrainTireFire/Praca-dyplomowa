using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Data
{
    public class DeletionInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context != null)
            {
                var entries = context.ChangeTracker.Entries<EffectGroup>();

                foreach (var entry in entries)
                {
                    if (entry.Entity.DeleteOnSave)
                    {
                        entry.State = EntityState.Deleted;
                    }
                }
            }

            await base.SavingChangesAsync(eventData, result, cancellationToken);
            return result;
        }
    }
}