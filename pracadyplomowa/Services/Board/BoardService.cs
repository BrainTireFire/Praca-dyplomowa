using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Field;

namespace pracadyplomowa.Services.Board;

public class BoardService : IBoardService
{
    private readonly IBoardRepository _boardRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IMapper _mapper;

    public BoardService(IBoardRepository boardRepository, IFieldRepository fieldRepository, IMapper mapper)
    {
        _boardRepository = boardRepository;
        _fieldRepository = fieldRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<BoardSummaryDto>> GetBoardsAsync(int ownerId, BoardParams boardParams)
    {
        var boards = await _boardRepository.GetBoardSummaries(ownerId, boardParams);
        return boards;
    }
    
    public async Task<ActionResult<BoardSummaryDto>> GetBoardAsync(int boardId, int ownerId)
    {
        var board = await _boardRepository.GetBoardFullAsync(boardId, ownerId);
        if (board == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"Board not found with id {boardId}"));
        }

        // This check is not necessary because the board is already filtered by ownerId
        // if (board.R_OwnerId != ownerId)
        // {
        //     return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this board"));
        // }

        var boardSummary = _mapper.Map<BoardSummaryDto>(board);
        return new OkObjectResult(boardSummary);
    }
    
    public async Task<ActionResult> CreateBoardAsync(int ownerId, BoardCreateDto boardCreateDto)
    {
        var board = new Models.Entities.Campaign.Board(ownerId, boardCreateDto.Name, boardCreateDto.SizeX, boardCreateDto.SizeY, boardCreateDto.Description);
        var fields = boardCreateDto.Fields.Select(field => new Field(
            field.PositionX,
            field.PositionY,
            field.PositionZ,
            field.Color,
            field.FieldCoverLevel,
            field.FieldMovementCost,
            field.Description)
        ).ToList();

        foreach (var field in fields)
        {
            board.AddField(field);
        }

        _boardRepository.Add(board);
        await _boardRepository.SaveChanges();
        return new CreatedResult(string.Empty, null);
    }
    
    public async Task<ActionResult> RemoveBoardAsync(int boardId, int ownerId)
    {
        var board = _boardRepository.GetById(boardId);
        if (board == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"Board not found with id {boardId}"));
        }

        if (board.R_OwnerId != ownerId)
        {
            return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this board"));
        }
        
        foreach (var field in board.R_ConsistsOfFields)
        {
            _fieldRepository.Delete(field.Id);
        }
        
        _boardRepository.Delete(board.Id);
        
        await _boardRepository.SaveChanges();
        return new NoContentResult();
    }
    
    public async Task<ActionResult?> UpdateBoardAsync(int boardId, BoardUpdateDto boardUpdateDto, int ownerId)
    {
        var board = _boardRepository.GetById(boardId);
        if (board == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"Board not found with id {boardId}"));
        }

        if (board.R_OwnerId != ownerId)
        {
            return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this board"));
        }

        var updateResult = await UpdateFieldsAsync(boardId, boardUpdateDto.Fields);
        if (updateResult != null)
        {
            return updateResult;
        }

        UpdateBoardProperties(board, boardUpdateDto);
        _boardRepository.Update(board);
        await _boardRepository.SaveChanges();

        return null;
    }

    private async Task<ActionResult?> UpdateFieldsAsync(int boardId, ICollection<FieldUpdateDto> fieldsToUpdate)
    {
        foreach (var fieldToUpdate in fieldsToUpdate)
        {
            var field = _fieldRepository.GetById(fieldToUpdate.Id);
            if (field == null)
            {
                return new NotFoundObjectResult(new ApiResponse(404, $"Field not found with id {fieldToUpdate.Id}"));
            }

            if (field.R_BoardId != boardId)
            {
                return new BadRequestObjectResult(new ApiResponse(400, $"Field with id {fieldToUpdate.Id} does not belong to board with id {boardId}"));
            }

            UpdateFieldProperties(field, fieldToUpdate);
            _fieldRepository.Update(field);
        }

        await _fieldRepository.SaveChanges();
        return null;
    }
    
    private void UpdateFieldProperties(Field field, FieldUpdateDto fieldToUpdate)
    {
        if (!string.IsNullOrEmpty(fieldToUpdate.Color))
        {
            field.Color = fieldToUpdate.Color;
        }

        if (!string.IsNullOrEmpty(fieldToUpdate.Description))
        {
            field.Description = fieldToUpdate.Description;
        }        
        
        if (!string.IsNullOrEmpty(fieldToUpdate.FieldMovementCost))
        {
            if (Enum.TryParse<FieldMovementCostType>(fieldToUpdate.FieldMovementCost, true, out var movementCost))
            {
                field.FieldMovementCost = movementCost;
            }
            else
            {
                throw new ArgumentException($"Invalid value for FieldCoverLevel: {fieldToUpdate.FieldCoverLevel}");
            }
        }        
        
        if (!string.IsNullOrEmpty(fieldToUpdate.FieldCoverLevel))
        {
            if (Enum.TryParse<FieldCoverType>(fieldToUpdate.FieldCoverLevel, true, out var coverLevel))
            {
                field.FieldCoverLevel = coverLevel;
            }
            else
            {
                // Handle the case where the string does not match any enum value
                throw new ArgumentException($"Invalid value for FieldCoverLevel: {fieldToUpdate.FieldCoverLevel}");
            }
        }

        if (fieldToUpdate.PositionX > 0)
        {
            field.PositionX = (int)fieldToUpdate.PositionX;
        }

        if (fieldToUpdate.PositionY > 0)
        {
            field.PositionY = (int)fieldToUpdate.PositionY;
        }
        
        if (fieldToUpdate.PositionZ > 0)
        {
            field.PositionZ = (int)fieldToUpdate.PositionZ;
        }
    }
    
    private void UpdateBoardProperties(Models.Entities.Campaign.Board board, BoardUpdateDto boardUpdateDto)
    {
        if (!string.IsNullOrEmpty(boardUpdateDto.Name))
        {
            board.Name = boardUpdateDto.Name;
        }

        if (!string.IsNullOrEmpty(boardUpdateDto.Description))
        {
            board.Description = boardUpdateDto.Description;
        }

        if (boardUpdateDto.SizeX > 0)
        {
            board.SizeX = (int)boardUpdateDto.SizeX;
        }

        if (boardUpdateDto.SizeY > 0)
        {
            board.SizeY = (int)boardUpdateDto.SizeY;
        }
    }
}