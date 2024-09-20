using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Field;
using pracadyplomowa.Services.Board;

namespace pracadyplomowa.Controllers;

[Authorize]
public class BoardController : BaseApiController
{
    private readonly IBoardRepository _boardRepository;
    private readonly IBoardService _boardService;

    public BoardController(IBoardRepository boardRepository, IBoardService boardService)
    {
        _boardRepository = boardRepository;
        _boardService = boardService;
    }

    [HttpGet("myboards")]
    public async Task<ActionResult<IEnumerable<BoardSummaryDto>>> GetBoards([FromQuery] BoardParams boardParams)
    {
        var userId = User.GetUserId();
        var boards = await _boardService.GetBoardsAsync(userId, boardParams);

        Response.AddPaginationHeader(boards);

        return Ok(boards);
    }

    [HttpGet("{boardId}")]
    public async Task<ActionResult<BoardSummaryDto>> GetBoard(int boardId)
    {
        var userId = User.GetUserId();
        var result = await _boardService.GetBoardAsync(boardId, userId);

        return result;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateBoard(BoardCreateDto boardCreateDto)
    {
        var userId = User.GetUserId();
        var result = await _boardService.CreateBoardAsync(userId, boardCreateDto);
        
        return result;
    }
    
    [HttpPut("{boardId}")]
    public async Task<ActionResult> UpdateBoard(int boardId, BoardUpdateDto boardUpdateDto)
    {
        var userId = User.GetUserId();
        var result = await _boardService.UpdateBoardAsync(boardId, boardUpdateDto, userId);

        return result ?? NoContent();
    }

    [HttpDelete("{boardId}")]
    public async Task<ActionResult> DeleteBoard(int boardId)
    {
        var userId = User.GetUserId();
        var result = await _boardService.RemoveBoardAsync(boardId, userId);
        return result;
    }
}