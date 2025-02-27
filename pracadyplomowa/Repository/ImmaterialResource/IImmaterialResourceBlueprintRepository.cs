using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository.Item
{
    public interface IImmaterialResourceBlueprintRepository: IBaseRepository<ImmaterialResourceBlueprint>
    {
        public Task<ImmaterialResourceBlueprint> GetByName(string name);

        public Task<List<ImmaterialResourceBlueprint>> GetAllByIds(List<int> Ids);
        public Dictionary<int, ImmaterialResourceBlueprint> GetItemFamiliesForEditabilityAnalysis(List<int> ids);
        public Task<List<ImmaterialResourceBlueprint>> GetOwnedAndDefault(int userId);
        public Task<List<ImmaterialResourceBlueprint>> GetOwnedAndDefaultAndCurrent(int? powerId, int userId);
    }
}