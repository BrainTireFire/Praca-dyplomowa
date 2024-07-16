using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository
{
    public class CharacterRepository: ICharacterRepository
    {
        private readonly AppDbContext _context;

        public CharacterRepository(AppDbContext context){
            _context = context;
        }
        
        public async Task<List<Character>> GetCharacterSummaries(int OwnerId){
            List<Character> characters = await _context.Characters.Where(c => c.R_OwnerId == OwnerId).Select(c => new CharacterSummaryDto(c.Id, c.Name, c.Description, c).ToListAsync();

        }
    }
}