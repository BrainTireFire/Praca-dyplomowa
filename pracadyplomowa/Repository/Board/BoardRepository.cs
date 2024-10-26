using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.DTOs.Board;

namespace pracadyplomowa.Repository.Board;

public class BoardRepository: BaseRepository<Models.Entities.Campaign.Board>, IBoardRepository
{
    private readonly IMapper _mapper;
    
    public BoardRepository(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<PagedList<BoardSummaryDto>> GetBoardSummaries(int ownerId, BoardParams boardParams)
    {
        var query = _context.Boards
            .Where(b => b.R_OwnerId == ownerId)
            .Include(b => b.R_ConsistsOfFields)
            .AsQueryable();

        // Filtering
        query = query.ApplyFilter(boardParams.Name, c => 
            c.Name);
            
        // Sorting
        query = boardParams.OrderBy switch
        {
            "name" => query.OrderBy(c => c.Name),
            "nameDesc" => query.OrderByDescending(c => c.Name),
            _ => query.OrderBy(c => c.Name)
        };

        var boardsSumaries = query.ProjectTo<BoardSummaryDto>(_mapper.ConfigurationProvider);
            
        return await PagedList<BoardSummaryDto>.CreateAsync(boardsSumaries, boardParams.PageNumber, boardParams.PageSize);
    }
    
    public async Task<PagedList<BoardShortDto>> GetBoardsShort(int ownerId, BoardParams boardParams)
    {
        var query = _context.Boards
            .Where(b => b.R_OwnerId == ownerId)
            .AsQueryable();

        // Filtering
        query = query.ApplyFilter(boardParams.Name, c => 
            c.Name);
            
        // Sorting
        query = boardParams.OrderBy switch
        {
            "name" => query.OrderBy(c => c.Name),
            "nameDesc" => query.OrderByDescending(c => c.Name),
            _ => query.OrderBy(c => c.Name)
        };

        var boardsSumaries = query.ProjectTo<BoardShortDto>(_mapper.ConfigurationProvider);
            
        return await PagedList<BoardShortDto>.CreateAsync(boardsSumaries, boardParams.PageNumber, boardParams.PageSize);
    }
    
    public Task<Models.Entities.Campaign.Board?> GetBoardFullAsync(int boardId, int ownerId)
    {
        return _context.Boards
            .Include(b => b.R_ConsistsOfFields)
            .FirstOrDefaultAsync(b => b.Id == boardId && b.R_OwnerId == ownerId);
    }
}