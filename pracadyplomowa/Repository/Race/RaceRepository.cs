using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Repository
{
    public class RaceRepository: BaseRepository<Models.Entities.Characters.Race>, IRaceRepository
    {
        // private readonly AppDbContext _context;

        public RaceRepository(AppDbContext context): base(context){
        }


        public Task<List<Models.Entities.Characters.Race>> GetRaceList(){
            Task<List<Models.Entities.Characters.Race>> races = _context.Races.ToListAsync();
            return races;
        }
    }
}