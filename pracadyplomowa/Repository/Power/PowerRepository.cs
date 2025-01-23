using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Repository
{
    public class PowerRepository : BaseRepository<Power>, IPowerRepository
    {
        public PowerRepository(AppDbContext context) : base(context)
        {

        }


        public async Task<Power> GetByIdWithEffectBlueprintsAndMaterialResources(int Id)
        {
            return await _context.Powers.Where(i => i.Id == Id)
            .Include(p => p.R_EffectBlueprints)
            .Include(p => p.R_ItemsCostRequirement)
            .ThenInclude(icr => icr.R_ItemFamily)
            .Include(p => p.R_UsesImmaterialResource)
            .FirstAsync();
        }
        public async Task<Power> GetAllByIdsWithEffectBlueprintsAndMaterialResources(List<int> Ids)
        {
            return await _context.Powers.Where(i => Ids.Contains(i.Id))
            .Include(p => p.R_EffectBlueprints)
            .Include(p => p.R_ItemsCostRequirement)
            .ThenInclude(icr => icr.R_ItemFamily)
            .Include(p => p.R_UsesImmaterialResource)
            .FirstAsync();
        }

        public Task<List<Power>> GetAllByIds(List<int> Ids){
            return _context.Powers.Where(i => Ids.Contains(i.Id)).ToListAsync();
        }

        public async Task<PagedList<Power>> GetAllPowersWithParams(PowerParams powerParams)
        {
            var query = _context.Powers.AsQueryable();

            // Filtering
            query = query.ApplyEnumFilter<Power, CastableBy>(
                powerParams.CastableBy ?? string.Empty,
                p => p.CastableBy
            );

            // Sorting
            query = powerParams.OrderBy switch
            {
                "name" => query.OrderBy(c => c.Name),
                "nameDesc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Name)
            };
            
            return await PagedList<Power>.CreateAsync(query, powerParams.PageNumber, powerParams.PageSize);
        }
    }
}