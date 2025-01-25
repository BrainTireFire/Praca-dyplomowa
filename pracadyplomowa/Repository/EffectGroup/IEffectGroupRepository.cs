using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository
{
    public interface IEffectGroupRepository: IBaseRepository<EffectGroup>
    {
        Task<List<EffectGroup>> GetAllEffectGroupsPresentInEncounter(int encounterId);
    }
}