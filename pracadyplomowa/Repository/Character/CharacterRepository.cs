using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Repository
{
    public class CharacterRepository: BaseRepository<Character>, ICharacterRepository
    {

        public CharacterRepository(AppDbContext context): base(context){
        }
        
        public async Task<List<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId){
            List<CharacterSummaryDto> characters = await _context.Characters
            .Where(c => c.R_OwnerId == OwnerId)
            .Include(c=> c.R_CharacterBelongsToRace)
            .Include(c => c.R_CharacterHasLevelsInClass)
            .ThenInclude(cl => cl.R_Class)
            .Select(c => new CharacterSummaryDto(c.Id, c.Name, c.Description, c.R_CharacterBelongsToRace.Name, c.R_CharacterHasLevelsInClass.First().R_Class.Name)).ToListAsync();
            return characters;
        }

        public Task<Character> GetByIdWithAll(int Id){

            var characterLevel = _context.Characters
            .Where(c => c.Id == Id).Include(c => c.R_CharacterHasLevelsInClass).Count();

            var character = _context.Characters
            .Where(c => c.Id == Id)

            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels.Where(rl => rl.Level <= characterLevel))
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterBelongsToRace)
                .ThenInclude(r => r.R_RaceLevels)
                    .ThenInclude(rl => rl.R_ChoiceGroups)
                        .ThenInclude(cg => cg.R_Powers)

            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_Class)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_ChoiceGroups)
                    .ThenInclude(cg => cg.R_Powers)

            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_Effects)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_ChoiceGroup)
                    .ThenInclude(cg => cg.R_Powers)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_EffectsGranted)
            .Include(c => c.R_UsedChoiceGroups)
                .ThenInclude(cg => cg.R_PowersGranted)
            

            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_AffectedBy)
                .ThenInclude(eg => eg.R_OwnedByGroup)

            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)

            .Include(c => c.R_EquippedItems)
            
            .AsSplitQuery() // IMPORTANT !!!!! https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
            .FirstAsync();
            return character;
        }
    }
}