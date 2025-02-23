using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Repository.Item
{
    public interface IItemFamilyRepository: IBaseRepository<ItemFamily>
    {
        public Task<ItemFamily> GetByName(string name);

        public Task<List<ItemFamily>> GetOwnedAndDefault(int userId);
        public Dictionary<int, ItemFamily> GetItemFamiliesForEditabilityAnalysis(List<int> ids);
        Task<List<ItemFamily>> GetOwnedAndDefaultAndCurrent(int? itemFamilyId, int userId);
        Task<List<ItemFamily>> GetOwnedAndDefaultAndCurrentForEffectBlueprint(int? effectId, int userId);

        Task<List<ItemFamily>> GetOwnedAndDefaultAndCurrentForEffectInstance(int? effectId, int userId);
    }
}