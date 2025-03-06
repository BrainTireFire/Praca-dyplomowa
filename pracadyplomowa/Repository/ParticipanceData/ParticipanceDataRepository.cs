using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository;

public class ParticipanceDataRepository : BaseRepository<ParticipanceData>, IParticipanceDataRepository
{
    public ParticipanceDataRepository(AppDbContext context) : base(context)
    {
    }

    public void RemoveByCharacterId(int characterId)
    {
        var participanceData = _context.Set<ParticipanceData>()
            .Where(pd => pd.R_CharacterId == characterId);

        if (participanceData.Any())
        {
            _context.Set<ParticipanceData>().RemoveRange(participanceData);
        }
    }

}