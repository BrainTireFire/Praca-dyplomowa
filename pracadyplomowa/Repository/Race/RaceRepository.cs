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

        public Task<Models.Entities.Characters.Race> GetRaceByIdWithRaceLevelAndChoiceGroups(int id, int level){
            Task<Models.Entities.Characters.Race> race = _context.Races
            .Where(r => r.Id == id)
            .Include(r => r.R_RaceLevels.Where(rl => rl.Level == level))
            .ThenInclude(r => r.R_ChoiceGroups)
            .ThenInclude(cg => cg.R_Effects)
            .Include(r => r.R_RaceLevels)
            .ThenInclude(cl => cl.R_ChoiceGroups)
            .ThenInclude(cg => cg.R_Powers)
            .FirstAsync();
            return race;
        }
    }
}