using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository
{
    public interface IPowerRepository: IBaseRepository<Power>
    {
        public Task<Power> GetByIdWithEffectBlueprintsAndMaterialResources(int Id);
        public Task<Power> GetAllByIdsWithEffectBlueprintsAndMaterialResources(List<int> Ids);
        public Task<List<Power>> GetAllByIds(List<int> Ids);
        public Task<PagedList<Power>> GetAllPowersWithParams(PowerParams powerParams);
    }
}