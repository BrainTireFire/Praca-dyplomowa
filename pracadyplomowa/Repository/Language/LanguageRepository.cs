using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Repository.Item
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(AppDbContext context) : base(context)
        {
        }

    }
}