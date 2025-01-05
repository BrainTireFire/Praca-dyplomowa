namespace pracadyplomowa.Repository.Field;

public interface IFieldRepository: IBaseRepository<Models.Entities.Campaign.Field>
{
    public Task<Models.Entities.Campaign.Field?> GetByIdWithPowers(int id);
}