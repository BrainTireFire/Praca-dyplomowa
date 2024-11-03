using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Item
{
    public class ItemFamilyRepository : BaseRepository<Models.Entities.Items.ItemFamily>, IItemFamilyRepository
    {
        public ItemFamilyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Models.Entities.Items.ItemFamily> GetByName(string name)
        {
            return await _context.ItemFamilies.Where(i => i.Name == name).FirstAsync();
        }
    }
}