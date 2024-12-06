using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;

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
    }
}