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
        public Task<PagedList<CharacterSummaryDto>> GetCharacterSummaries(int OwnerId, bool isNpc, CharacterParams characterParams);
        public Task<List<Character>> GetCharactersByUserId(int userId);
        public Task<Character> GetByIdWithAll(int Id);
        public Task<Character> GetByIdWithChoiceGroups(int Id);
        public Task<Character> GetByIdForChoice(int Id);
        public Task<Character> GetByIdWithClassLevels(int Id);
        public Task<Character> GetCharacterEquipmentAndSlots(int id);
        public Task<Character> GetCharacterEquipment(int id);
        public Task<Character> GetCharacterWithCoinSack(int id);
        public Task<Character> GetByIdWithCustomResources(int Id);
        public Task<Character> GetByIdWithKnownPowers(int Id);
        public Task<Character> GetByIdWithPreparedPowers(int Id);
        public Task<Character> GetByIdWithPowersToPrepare(int Id);
        public Dictionary<int, Character> GetCharactersForAccessAnalysis(List<int> ids);
    }
}
