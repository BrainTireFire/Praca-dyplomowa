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
        public Task<List<ImmaterialResourceBlueprint>> GetOwnedAndDefault(int userId){
            return _context.ImmaterialResourceBlueprints.Where(resource => resource.R_OwnerId == userId || resource.R_OwnerId == null).ToListAsync();
        }

        public Task<List<ImmaterialResourceBlueprint>> GetOwnedAndDefaultAndCurrent(int? powerId, int userId){
            return _context.ImmaterialResourceBlueprints.Where(resource => resource.R_OwnerId == userId || resource.R_OwnerId == null || resource.R_PowersRequiringThis.Any(power => power.Id == powerId)).ToListAsync();
        }
    }
}