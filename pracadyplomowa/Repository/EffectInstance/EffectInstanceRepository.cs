using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Repository
{
    public class EffectInstanceRepository : BaseRepository<EffectInstance>, IEffectInstanceRepository
    {
        public EffectInstanceRepository(AppDbContext context) : base(context)
        {

        }
    }
}