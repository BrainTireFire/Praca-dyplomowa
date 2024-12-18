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
        public Task<PagedList<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId, CharacterParams characterParams);
        public Task<Character> GetByIdWithAll(int Id);
        public Task<Character> GetByIdWithChoiceGroups(int Id);
        public Task<Character> GetByIdWithClassLevels(int Id);
        public Task<Character> GetCharacterEquipmentAndSlots(int id);
        public Task<Character> GetByIdWithCustomResources(int Id);
        public Task<Character> GetByIdWithKnownPowers(int Id);
    }
}