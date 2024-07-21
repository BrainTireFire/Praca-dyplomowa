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
        
        public async Task<List<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId){
            List<CharacterSummaryDto> characters = await _context.Characters
            .Where(c => c.R_OwnerId == OwnerId)
            .Include(c=> c.R_CharacterBelongsToRace)
            .Include(c => c.R_CharacterHasLevelsInClass)
            .ThenInclude(cl => cl.R_Class)
            .Select(c => new CharacterSummaryDto(c.Id, c.Name, c.Description, c.R_CharacterBelongsToRace.Name, c.R_CharacterHasLevelsInClass.First().R_Class.Name)).ToListAsync();
            return characters;
        }
    }
}