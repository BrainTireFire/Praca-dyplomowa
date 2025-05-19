using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Item
{
    public class ItemCostRequirementRepository : BaseRepository<Models.Entities.Items.ItemCostRequirement>, IItemCostRequirementRepository
    {
        public ItemCostRequirementRepository(AppDbContext context) : base(context)
        {
        }


        public override void Update(Models.Entities.Items.ItemCostRequirement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            _context.Entry(entity).Reference(e => e.Worth).TargetEntry!.State = EntityState.Modified;
        }
    }
}