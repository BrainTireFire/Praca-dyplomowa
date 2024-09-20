using pracadyplomowa.Models.DTOs.Board;

namespace pracadyplomowa.Repository.Board;

public interface IBoardRepository: IBaseRepository<Models.Entities.Campaign.Board>
{
    Task<PagedList<BoardSummaryDto>> GetBoardSummaries(int ownerId, BoardParams boardParams);
    Task<Models.Entities.Campaign.Board?> GetBoardFullAsync(int boardId, int ownerId);
}