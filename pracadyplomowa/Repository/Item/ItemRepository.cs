using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Item
{
    public class ItemRepository : BaseRepository<Models.Entities.Items.Item>, IItemRepository
    {
        public ItemRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Models.Entities.Items.Item> GetByIdWithSlots(int id)
        {
            return await  _context.Items
            .Where(i => i.Id == id)
            .Include(i => i.R_ItemIsEquippableInSlots)
            .FirstAsync();
        }

        public async Task<Models.Entities.Items.Item> GetByIdWithSlotsPowersEffectsResources(int id)
        {
            return await  _context.Items
            .Where(i => i.Id == id)
            .Include(i => i.R_ItemIsEquippableInSlots)
            .Include(i => i.R_EquipItemGrantsAccessToPower)
            .Include(i => i.R_ItemGrantsResources)
            .Include(i => i.R_EffectsOnEquip)
            .FirstAsync();
        }

        public async Task<Models.Entities.Items.Item> GetByName(string name)
        {
            return await _context.Items.Where(i => i.Name == name).FirstAsync();
        }
        public async Task<Models.Entities.Items.Item> GetByNameWithEquipmentSlots(string name)
        {
            return await _context.Items.Where(i => i.Name == name).Include(i => i.R_ItemIsEquippableInSlots).FirstAsync();
        }
    }
}