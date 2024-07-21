using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Repository.Race
{
    public interface IRaceRepository: IBaseRepository<Models.Entities.Characters.Race>
    {
        public Task<List<Models.Entities.Characters.Race>> GetRaceList();
    }
}