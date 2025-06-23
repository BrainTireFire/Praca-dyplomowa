using pracadyplomowa.Models.DTOs;

namespace pracadyplomowa.Repository.AuctionLog;

public interface IActionLogRepository : IBaseRepository<Models.Entities.Campaign.ActionLog>
{
    Task<ActionLogDto[]> GetByEncounterId(int encounterId);
}