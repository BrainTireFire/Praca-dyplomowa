using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository
{
    public class EffectGroupRepository(AppDbContext context) : BaseRepository<EffectGroup>(context), IEffectGroupRepository
    {
        public Task<List<EffectGroup>> GetAllEffectGroupsPresentInEncounter(int encounterId){
            return _context.EffectGroups.Where(x => 
                x.IsConstant == false && x.R_OwnedEffects.Any(ei => 
                    ei.R_TargetedCharacterId != null && ei.R_TargetedCharacter!.R_CharactersParticipatesInEncounters.Any(y => 
                        y.R_EncounterId == encounterId
                    )
                )
            )
            .Include(x => x.R_OwnedEffects)
                .ThenInclude(x => x.R_TargetedCharacter)
                    .ThenInclude(x => x!.R_CharactersParticipatesInEncounters.Where(y => y.R_EncounterId == encounterId))
            .AsSplitQuery()
            .ToListAsync();
        }
    }
}