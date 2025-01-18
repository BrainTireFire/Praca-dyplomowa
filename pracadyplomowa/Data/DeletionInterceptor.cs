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
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;

            if (context != null)
            {
                var entries = context.ChangeTracker.Entries<EffectGroup>();

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Modified && entry.Entity.DeleteOnSave)
                    {
                        entry.State = EntityState.Deleted;
                    }
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}