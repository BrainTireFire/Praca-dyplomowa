using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository.Class
{
    public class ClassRepository: BaseRepository<Models.Entities.Characters.Class>, IClassRepository
    {

        public ClassRepository(AppDbContext context): base(context){
        }

        public async Task<List<Models.Entities.Characters.Class>> GetClassList(){
            List<Models.Entities.Characters.Class> classes = await _context.Classes.ToListAsync();
            return classes;
        }

        public async Task<ClassLevel?> GetClassLevel(int classId, int level){
            ClassLevel? classes = await _context.ClassLevels.FirstOrDefaultAsync(cl => cl.Level == level && cl.R_ClassId == classId);
            return classes;
        }


        public async Task<ClassLevel?> GetClassLevelById(int classLevelId){
            ClassLevel? classes = await _context.ClassLevels.FirstOrDefaultAsync(cl => cl.Id == classLevelId);
            return classes;
        }

        public async Task<ClassLevel?> GetClassLevelWithChoiceGroups(int classId, int level){
            ClassLevel? classes = await _context.ClassLevels
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Resources).ThenInclude(r => r.R_Blueprint)
            .FirstOrDefaultAsync(cl => cl.Level == level && cl.R_ClassId == classId);
            return classes;
        }

        public async Task<List<ClassLevel>> GetClassLevelsWithChoiceGroups(List<int> ids){
            List<ClassLevel> classes = await _context.ClassLevels
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Effects)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersAlwaysAvailable)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_PowersToPrepare)
            .Include(cl => cl.R_ChoiceGroups).ThenInclude(cg => cg.R_Resources).ThenInclude(r => r.R_Blueprint)
            .Include(cl => cl.HitDie)
            .Where(cl => ids.Contains(cl.Id))
            .AsSplitQuery()
            .ToListAsync();
            return classes;
        }

        public Task<List<Models.Entities.Characters.Class>> GetClassesWithClassLevels(bool track){
            var x = _context.Classes
            .Include(c => c.R_ClassLevels)
            .ThenInclude(cl => cl.HitDie);

            if(!track){
                return x.AsNoTracking().ToListAsync();
            }
            else{
                return x.ToListAsync();
            }
        }
        
    }
}