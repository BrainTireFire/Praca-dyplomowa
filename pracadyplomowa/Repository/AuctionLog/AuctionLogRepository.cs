using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository.AuctionLog;

public class AuctionLogRepository  : BaseRepository<Models.Entities.Campaign.ActionLog>, IAuctionLogRepository
{
    public AuctionLogRepository(AppDbContext context) : base(context)
    {
    }


    public ActionLog GetByEncounterId(int encounterId)
    {
        return _context.ActionLogs.FirstOrDefault(al => al.EncounterId == encounterId);
    }
}