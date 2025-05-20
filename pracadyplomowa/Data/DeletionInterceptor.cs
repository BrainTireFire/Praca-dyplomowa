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
                //Mark entity state as deleted if DeleteOnSave flag is set
                var effectInstanceEntries = context.ChangeTracker.Entries<EffectInstance>();
                var deletedEffectInstances = new List<EffectInstance>();
                foreach (var entry in effectInstanceEntries)
                {
                    if (entry.Entity.DeleteOnSave)
                    {
                        deletedEffectInstances.Add(entry.Entity);
                    }
                }
                //Find all effect groups related to the effects with DeleteOnSave == true, and load all of their related effects
                var effectGroups = context.Set<EffectGroup>()
                                            .Where(e => e.R_OwnedEffects
                                                                .Where(x => deletedEffectInstances.Contains(x))
                                                                .Select(x => x.R_OwnedByGroupId)
                                                                .Contains(e.Id)
                                            ).Include(e => e.R_OwnedEffects).ToList();
                //If all of the effects related to the effect group are marked for deletion, then mark entire Effect Group for deletion by setting DeleteOnSave = true
                foreach(var effectGroup in effectGroups){
                    if(effectGroup.R_OwnedEffects.Count <= 0 || !effectGroup.R_OwnedEffects.Any(x => x.DeleteOnSave == false)){
                        effectGroup.DeleteOnSave = true;
                    }
                }
                //If EffectGroup has DeleteOnSave == true, mark entity state as Deleted
                var effectGroupEntries = context.ChangeTracker.Entries<EffectGroup>();
                foreach (var entry in effectGroupEntries)
                {
                    if (entry.Entity.DeleteOnSave)
                    {
                        entry.State = EntityState.Deleted;
                    }
                }
                foreach (var entry in effectInstanceEntries)
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