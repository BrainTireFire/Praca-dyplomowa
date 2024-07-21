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
    }
}