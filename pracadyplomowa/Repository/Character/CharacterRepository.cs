using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository
{
    public class CharacterRepository: BaseRepository<Character>, ICharacterRepository
    {

        public CharacterRepository(AppDbContext context): base(context){
        }
        
        public async Task<PagedList<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId, CharacterParams characterParams)
        {
            var query =  _context.Characters
                    .Where(c => c.R_OwnerId == OwnerId)
                    .Include(c => c.R_CharacterBelongsToRace)
                    .Include(c => c.R_CharacterHasLevelsInClass)
                    .ThenInclude(cl => cl.R_Class)
                    .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(characterParams.ClassName))
            {
                query = query.Where(c => 
                    c.R_CharacterBelongsToRace.Name == characterParams.ClassName);
            }
            
            // Sorting
            query = characterParams.OrderBy switch
            {
                "name" => query.OrderBy(c => c.Name),
                "nameDesc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Name)
            };
            
            var charactersSumaries = query.Select(c => new CharacterSummaryDto(
                c.Id, 
                c.Name, 
                c.Description,
                c.R_CharacterBelongsToRace.Name,
                c.R_CharacterHasLevelsInClass.First().R_Class.Name
            ));
            
            return await PagedList<CharacterSummaryDto>.CreateAsync(charactersSumaries, characterParams.PageNumber, characterParams.PageSize);
        }

        public Task<Character> GetByIdWithAll(int Id){
            var character = _context.Characters
            .Where(c => c.Id == Id)

            .Include(c => c.R_CharacterBelongsToRace)

            .Include(c => c.R_CharacterHasLevelsInClass)
                .ThenInclude(cl => cl.R_Class)

            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersKnown)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_EffectBlueprints)
            .Include(c => c.R_PowersPrepared)
                .ThenInclude(p => p.R_UsesImmaterialResource)

            .Include(c => c.R_AffectedBy)
                .ThenInclude(eg => eg.R_OwnedEffects)

            .Include(c => c.R_CharacterHasBackpack)
                .ThenInclude(b => b.R_BackpackHasItems)

            .Include(c => c.R_EquippedItems)
            
            .FirstAsync();
            return character;
        }
    }
}