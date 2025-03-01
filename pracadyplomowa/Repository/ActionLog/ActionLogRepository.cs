using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository.AuctionLog;

public class ActionLogRepository  : BaseRepository<Models.Entities.Campaign.ActionLog>, IActionLogRepository
{
    public ActionLogRepository(AppDbContext context) : base(context)
    {
    }


    public ActionLog GetByEncounterId(int encounterId)
    {
        return _context.ActionLogs.FirstOrDefault(al => al.EncounterId == encounterId);
    }
}