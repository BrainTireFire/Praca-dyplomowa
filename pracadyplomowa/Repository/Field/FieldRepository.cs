using Microsoft.EntityFrameworkCore;

namespace pracadyplomowa.Repository.Field;

public class FieldRepository: BaseRepository<Models.Entities.Campaign.Field>, IFieldRepository
{
    public FieldRepository(AppDbContext context) : base(context)
    {
    }
    public Task<Models.Entities.Campaign.Field?> GetByIdWithPowers(int id){
        return _context.Fields.Where(field => field.Id == id).Include(field => field.R_CasterPowers).FirstOrDefaultAsync();
    }
}