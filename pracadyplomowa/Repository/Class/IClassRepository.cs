using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Repository.Class
{
    public interface IClassRepository: IBaseRepository<Models.Entities.Characters.Class>
    {

        public Task<List<Models.Entities.Characters.Class>> GetClassList();

        public Task<ClassLevel?> GetClassLevel(int classId, int level);

    }
}