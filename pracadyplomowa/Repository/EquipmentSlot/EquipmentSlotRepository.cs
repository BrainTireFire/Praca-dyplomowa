using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository.Item
{
    public class EquipmentSlotRepository : BaseRepository<EquipmentSlot>, IEquipmentSlotRepository
    {
        public EquipmentSlotRepository(AppDbContext context) : base(context)
        {
        }

        public Task<List<EquipmentSlot>> GetAllWithIds(List<int> ids)
        {
            return _context.EquipmentSlots.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}