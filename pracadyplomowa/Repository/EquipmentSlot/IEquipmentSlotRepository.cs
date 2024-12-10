using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Repository.Item
{
    public interface IEquipmentSlotRepository: IBaseRepository<EquipmentSlot>
    {
        public Task<List<EquipmentSlot>> GetAllWithIds(List<int> ids);
    }
}