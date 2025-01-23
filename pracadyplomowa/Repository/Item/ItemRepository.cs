using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Items;

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
        public async Task<Models.Entities.Items.Item> GetByIdWithSlotsPowersWithEffectsEffectsResources(int id)
        {
            return await  _context.Items
            .Where(i => i.Id == id)
            .Include(i => i.R_ItemIsEquippableInSlots)
            .Include(i => i.R_EquipItemGrantsAccessToPower)
                .ThenInclude(p => p.R_EffectBlueprints)
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

        public async Task<PagedList<Models.Entities.Items.Item>> GetOwnedItems(int OwnerId, ItemParams itemParams)
        {
            var query = _context.Items
                    .Where(c => c.R_OwnerId == OwnerId || c.R_OwnerId == null)
                    .Include(c => c.R_Owner)
                    .AsQueryable();

            // Filtering
            if(itemParams.IsBlueprint != null)
            query = query.ApplyBooleanFilter(itemParams.IsBlueprint, c =>
                c.IsBlueprint);

            return await PagedList<Models.Entities.Items.Item>.CreateAsync(query, 1, 10000000); //TODO do something smarter
        }

        public Dictionary<int, Models.Entities.Items.Item> GetItemsForEditabilityAnalysis(List<int> ids){
            return _context.Items
            .Where(i => ids.Contains(i.Id))
            .Include(i => i.R_BackpackHasItem)
            .ThenInclude(b => b.R_BackpackOfCharacter)
            .ThenInclude(c => c.R_Campaign)
            .ToDictionary(i => i.Id, i => i);
        }
    }
}