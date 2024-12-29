using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Board;
using pracadyplomowa.Models.DTOs.Map.Field;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Field;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Services.Board;

public class BoardService : IBoardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private const string COLOR_DEFAULT = "#1F2421";
    private const string FIELD_COVER_LEVEL_DEFAULT = "NoCover";
    private const string FIELD_MOVEMENT_COST_DEFAULT = "Low";

    public BoardService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PagedList<BoardSummaryDto>> GetBoardsAsync(int ownerId, BoardParams boardParams)
    {
        var boards = await _unitOfWork.BoardRepository.GetBoardSummaries(ownerId, boardParams);
        return boards;
    }
    
    public async Task<PagedList<BoardShortDto>> GetBoardsShortAsync(int ownerId, BoardParams boardParams)
    {
        var boards = await _unitOfWork.BoardRepository.GetBoardsShort(ownerId, boardParams);
        return boards;
    }
    
    public async Task<ActionResult<BoardSummaryDto>> GetBoardAsync(int boardId, int ownerId)
    {
        var board = await _unitOfWork.BoardRepository.GetBoardFullAsync(boardId, ownerId);
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
        
        AddMissingFieldsToBoard(board, fields);

        _unitOfWork.BoardRepository.Add(board);
        await _unitOfWork.SaveChangesAsync();
        return new CreatedResult(string.Empty, null);
    }

    public async Task<ActionResult> RemoveBoardAsync(int boardId, int ownerId)
    {
        var board = _unitOfWork.BoardRepository.GetById(boardId);
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
            _unitOfWork.FieldRepository.Delete(field.Id);
        }
        
        _unitOfWork.BoardRepository.Delete(board.Id);
        
        await _unitOfWork.SaveChangesAsync();
        return new NoContentResult();
    }
    
    public async Task<ActionResult?> UpdateBoardAsync(int boardId, BoardUpdateDto boardUpdateDto, int ownerId)
    {
        var board = _unitOfWork.BoardRepository.GetById(boardId);
        if (board == null)
        {
            return new NotFoundObjectResult(new ApiResponse(404, $"Board not found with id {boardId}"));
        }

        if (board.R_OwnerId != ownerId)
        {
            return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this board"));
        }
        
        var updateResult = await UpdateFieldsAsync(boardId, board, boardUpdateDto.Fields);
        if (updateResult != null)
        {
            return updateResult;
        }

        UpdateBoardProperties(board, boardUpdateDto);
        
        if (boardUpdateDto.SizeX > 0 && boardUpdateDto.SizeY > 0)
        {
            var fields = board.R_ConsistsOfFields.ToList();
            AddMissingFieldsToBoard(board, fields);
        }
        
        _unitOfWork.BoardRepository.Update(board);
        await _unitOfWork.SaveChangesAsync();

        return null;
    }
    
    private static void AddMissingFieldsToBoard(Models.Entities.Campaign.Board board, List<Field> fields)
    {
        // Create and add remaining fields that are not provided
        for (int positionX = 0; positionX < board.SizeX; positionX++)
        {
            for (int positionY = 0; positionY < board.SizeY; positionY++)
            {
                // Check if a field already exists at this position
                if (!fields.Any(f => f.PositionX == positionX && f.PositionY == positionY))
                {
                    // Create a default field if not already provided
                    var defaultField = new Field(
                        positionX,
                        positionY,
                        1,
                        COLOR_DEFAULT,
                        FIELD_COVER_LEVEL_DEFAULT,
                        FIELD_MOVEMENT_COST_DEFAULT
                    );
                
                    board.AddField(defaultField);
                }
            }
        }
    }

    private async Task<ActionResult?> UpdateFieldsAsync(int boardId, Models.Entities.Campaign.Board board, ICollection<FieldUpdateDto> fieldsToUpdate)
    {
        foreach (var fieldToUpdate in fieldsToUpdate)
        {
            if (fieldToUpdate.Id == null || fieldToUpdate.Id == 0)
            {
                var newField = new Field(
                    (int) fieldToUpdate.PositionX,
                    (int) fieldToUpdate.PositionY,
                    (int) fieldToUpdate.PositionZ,
                    fieldToUpdate.Color,
                    fieldToUpdate.FieldCoverLevel,
                    fieldToUpdate.FieldMovementCost,
                    fieldToUpdate.Description);
                
                
                _unitOfWork.FieldRepository.Add(newField);
                board.AddField(newField);
                _unitOfWork.BoardRepository.Update(board);
            }
            else
            {
                var field = _unitOfWork.FieldRepository.GetById(fieldToUpdate.Id);
                if (field == null)
                {
                    return new NotFoundObjectResult(new ApiResponse(404,
                        $"Field not found with id {fieldToUpdate.Id}"));
                }

                if (field.R_BoardId != boardId)
                {
                    return new BadRequestObjectResult(new ApiResponse(400,
                        $"Field with id {fieldToUpdate.Id} does not belong to board with id {boardId}"));
                }

                UpdateFieldProperties(field, fieldToUpdate);
                _unitOfWork.FieldRepository.Update(field);
            }
        }

        await _unitOfWork.SaveChangesAsync();
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

        }        
        
        if (!string.IsNullOrEmpty(fieldToUpdate.FieldCoverLevel))
        {
            if (Enum.TryParse<FieldCoverType>(fieldToUpdate.FieldCoverLevel, true, out var coverLevel))
            {
                field.FieldCoverLevel = coverLevel;
            }
            else
            {
                
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