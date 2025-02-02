namespace pracadyplomowa.Repository.AuctionLog;

public interface IActionLogRepository : IBaseRepository<Models.Entities.Campaign.ActionLog>
{
    Models.Entities.Campaign.ActionLog GetByEncounterId(int encounterId);
}