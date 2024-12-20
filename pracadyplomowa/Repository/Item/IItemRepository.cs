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
    }
}