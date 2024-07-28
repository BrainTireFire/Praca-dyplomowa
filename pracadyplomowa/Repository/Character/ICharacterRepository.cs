using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository
{
    public interface ICharacterRepository: IBaseRepository<Character>
    {
        public Task<List<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId);
        public Task<Character> GetByIdWithAll(int Id);
    }
}