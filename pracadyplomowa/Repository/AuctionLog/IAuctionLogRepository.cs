namespace pracadyplomowa.Repository.AuctionLog;

public interface IAuctionLogRepository : IBaseRepository<Models.Entities.Campaign.ActionLog>
{
    Models.Entities.Campaign.ActionLog GetByEncounterId(int encounterId);
}