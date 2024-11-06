using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository.Item
{
    public interface IItemFamilyRepository: IBaseRepository<Models.Entities.Items.ItemFamily>
    {
        public Task<Models.Entities.Items.ItemFamily> GetByName(string name);
    }
}