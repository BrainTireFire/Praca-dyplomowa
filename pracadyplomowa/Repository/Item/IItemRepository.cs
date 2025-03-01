using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository.Item
{
    public interface IItemRepository: IBaseRepository<Models.Entities.Items.Item>
    {
        public Task<Models.Entities.Items.Item> GetByName(string name);
        public Task<Models.Entities.Items.Item> GetByNameWithEquipmentSlots(string name);
        public Task<Models.Entities.Items.Item> GetByIdWithSlots(int id);
        public Task<Models.Entities.Items.Item> GetByIdWithSlotsPowersEffectsResources(int id);
        public Task<Models.Entities.Items.Item> GetByIdWithSlotsPowersWithEffectsEffectsResources(int id);
        public Task<PagedList<Models.Entities.Items.Item>> GetOwnedItems(int OwnerId, ItemParams itemParams);
        public Dictionary<int, Models.Entities.Items.Item> GetItemsForEditabilityAnalysis(List<int> ids);
    }
}