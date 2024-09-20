namespace pracadyplomowa.Repository.Field;

public class FieldRepository: BaseRepository<Models.Entities.Campaign.Field>, IFieldRepository
{
    public FieldRepository(AppDbContext context) : base(context)
    {
    }
}