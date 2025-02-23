using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Repository.Item
{
    public class ItemFamilyRepository : BaseRepository<Models.Entities.Items.ItemFamily>, IItemFamilyRepository
    {
        public ItemFamilyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ItemFamily> GetByName(string name)
        {
            return await _context.ItemFamilies.Where(i => i.Name == name).FirstAsync();
        }

        public Task<List<ItemFamily>> GetOwnedAndDefault(int userId){
            return _context.ItemFamilies.Where(iFamily => iFamily.R_OwnerId == userId || iFamily.R_OwnerId == null).ToListAsync();
        }
        public Dictionary<int, ItemFamily> GetItemFamiliesForEditabilityAnalysis(List<int> ids){
            
            return _context.ItemFamilies
            .Where(i => ids.Contains(i.Id))
            .ToDictionary(i => i.Id, i => i);
        }

        public Task<List<ItemFamily>> GetOwnedAndDefaultAndCurrent(int? itemId, int userId){
            return _context.ItemFamilies.Where(iFamily => iFamily.R_OwnerId == userId || iFamily.R_OwnerId == null || iFamily.R_ItemFamilyInItems.Any(item => item.Id == itemId)).ToListAsync();
        }
    }
}