using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Campaign;

namespace pracadyplomowa.Repository.AuctionLog;

public class ActionLogRepository  : BaseRepository<Models.Entities.Campaign.ActionLog>, IActionLogRepository
{
    public ActionLogRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<ActionLogDto[]> GetByEncounterId(int encounterId)
    {
        return await _context.ActionLogs
            .Where(al => al.EncounterId == encounterId)
            .Select(al => new ActionLogDto
            {
                Content = al.Content,
                Source = al.Source
            })
            .ToArrayAsync();
    }

}