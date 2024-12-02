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
    }
}