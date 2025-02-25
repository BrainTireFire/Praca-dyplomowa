using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository.Item
{
    public class ImmaterialResourceBlueprintRepository : BaseRepository<ImmaterialResourceBlueprint>, IImmaterialResourceBlueprintRepository
    {
        public ImmaterialResourceBlueprintRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ImmaterialResourceBlueprint> GetByName(string name)
        {
            return await _context.ImmaterialResourceBlueprints.Where(i => i.Name == name).FirstAsync();
        }

        public Task<List<ImmaterialResourceBlueprint>> GetAllByIds(List<int> Ids){
            return _context.ImmaterialResourceBlueprints.Where(i => Ids.Contains(i.Id)).ToListAsync();
        }
        public Dictionary<int, ImmaterialResourceBlueprint> GetItemFamiliesForEditabilityAnalysis(List<int> ids){
            
            return _context.ImmaterialResourceBlueprints
            .Where(i => ids.Contains(i.Id))
            .ToDictionary(i => i.Id, i => i);
        }
    }
}