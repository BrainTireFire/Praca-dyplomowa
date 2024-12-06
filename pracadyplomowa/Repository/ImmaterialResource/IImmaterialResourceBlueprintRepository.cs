using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository.Item
{
    public interface IImmaterialResourceBlueprintRepository: IBaseRepository<Models.Entities.Powers.ImmaterialResourceBlueprint>
    {
        public Task<Models.Entities.Powers.ImmaterialResourceBlueprint> GetByName(string name);
    }
}