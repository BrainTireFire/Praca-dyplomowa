using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs.Board;

namespace pracadyplomowa.Services.Board;

public interface IBoardService
{
    Task<ActionResult?> UpdateBoardAsync(int boardId, BoardUpdateDto boardUpdateDto, int ownerId);
    Task<PagedList<BoardSummaryDto>> GetBoardsAsync(int ownerId, BoardParams boardParams);
    Task<PagedList<BoardShortDto>> GetBoardsShortAsync(int ownerId, BoardParams boardParams);
    Task<ActionResult<BoardSummaryDto>> GetBoardAsync(int boardId, int ownerId);
    Task<ActionResult> CreateBoardAsync(int ownerId, BoardCreateDto boardCreateDto);
    Task<ActionResult> RemoveBoardAsync(int boardId, int ownerId);
}