using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Item
{
    public class ImmaterialResourceBlueprintRepository : BaseRepository<Models.Entities.Powers.ImmaterialResourceBlueprint>, IImmaterialResourceBlueprintRepository
    {
        public ImmaterialResourceBlueprintRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Models.Entities.Powers.ImmaterialResourceBlueprint> GetByName(string name)
        {
            return await _context.ImmaterialResourceBlueprints.Where(i => i.Name == name).FirstAsync();
        }
    }
}