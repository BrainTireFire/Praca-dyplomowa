using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository;

public class ParticipanceDataRepository : BaseRepository<ParticipanceData>, IParticipanceDataRepository
{
    public ParticipanceDataRepository(AppDbContext context) : base(context)
    {
    }
}