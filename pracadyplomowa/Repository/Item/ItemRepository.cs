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

        public async Task<Models.Entities.Items.Item> GetByName(string name)
        {
            return await _context.Items.Where(i => i.Name == name).FirstAsync();
        }
    }
}