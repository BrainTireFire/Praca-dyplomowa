using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Encounter;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Services.Encounter;

public class EncounterService : IEncounterService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    
    public EncounterService(
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        IMapper mapper
   )
    {
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, EncounterParams encounterParams)
    {
        var encounters = await _unitOfWork.EncounterRepository.GetEncounters(ownedId, encounterParams);
        var encounterSummary = _mapper.MapPagedList<Models.Entities.Campaign.Encounter, EncounterShortDto>(encounters);
        return encounterSummary;
    }

    public async Task<EncounterSummaryDto> GetEncounterAsync(int encounterId)
    {
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId);
        var encounterSummary = _mapper.Map<EncounterSummaryDto>(encounter);
        return encounterSummary;
    }

    public async Task<ActionResult> CreateEncounterAsync(int ownerId, CreateEncounterDto createEncounterDto)
    {
        // using var transactionScope = new TransactionScope(
        //     TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            var board = _unitOfWork.BoardRepository.GetById(createEncounterDto.BoardId);
            if (board == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Board with Id " + createEncounterDto.BoardId + " does not exist"));
            }
        
            var campaign = _unitOfWork.CampaignRepository.GetById(createEncounterDto.CampaignId);
            if (campaign == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, $"Campaign with Id {createEncounterDto.CampaignId} does not exist"));
            }
            
            var user = await _accountRepository.GetUserById(ownerId);
            if (user == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, $"User with Id {ownerId} does not exist"));
            }
        
            var characters = new List<Character>();
        
            foreach (var characterId in createEncounterDto.CharactersIds)
            {
                var character = _unitOfWork.CharacterRepository.GetById(characterId);
                if (character == null)
                {
                    return new NotFoundObjectResult(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
                }
                characters.Add(character);
            }
            
            var encounter = new Models.Entities.Campaign.Encounter
            {
                R_Owner = user,
                Name = createEncounterDto.Name,
                R_Campaign = campaign,
                R_Board = board
            };
            
            foreach (var character in characters)
            {
                encounter.AddParticipance(character);
            }
            
            _unitOfWork.EncounterRepository.Add(encounter);
            await _unitOfWork.SaveChangesAsync();
            
            // transactionScope.Complete();

            return new CreatedResult(string.Empty, null);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new ApiResponse(400, e.Message));
        }
    }

    public async Task<ActionResult> UpdateEncounterAsync(int ownerId, int encounterId, UpdateEncounterDto updateEncounterDto)
    {
        try
        {
            var encounter = _unitOfWork.EncounterRepository.GetById(encounterId);
            
            if (encounter == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
            }
            
            // if (encounter.R_Owner.Id != ownerId)
            // {
            //     return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this encounter"));
            // }
            
            foreach (var fieldUpdate in updateEncounterDto.FieldsToUpdate)
            {
                if (fieldUpdate.ParticipanceDataId == null)
                {
                    // Add new participant (participance data)
                    var character = _unitOfWork.CharacterRepository.GetById(fieldUpdate.CharacterId);
                    if (character == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, "Character with Id " + fieldUpdate.CharacterId + " does not exist"));
                    }

                    var newParticipance = encounter.AddParticipance(character);

                    var field = _unitOfWork.FieldRepository.GetById(fieldUpdate.FieldId);
                    if (field == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, $"Field with Id {fieldUpdate.FieldId} does not exist"));
                    }

                    if (field.FieldMovementCost == FieldMovementCostType.Impassable)
                    {
                        return new BadRequestObjectResult(new ApiResponse(400, $"Field is impassable {field.Id}"));
                    }

                    field.UpdateParticipanceData(newParticipance);
                }
                else
                {
                    // Update existing participance and field
                    var participanceData = _unitOfWork.ParticipanceDataRepository.GetById(fieldUpdate.ParticipanceDataId.Value);
                    if (participanceData == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, "ParticipanceData with Id " + fieldUpdate.ParticipanceDataId.Value + " does not exist"));
                    }

                    var character = _unitOfWork.CharacterRepository.GetById(fieldUpdate.CharacterId);
                    if (character == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, "Character with Id " + fieldUpdate.CharacterId + " does not exist"));
                    }

                    participanceData.UpdateCharacter(character);

                    var field = _unitOfWork.FieldRepository.GetById(fieldUpdate.FieldId);
                    if (field == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, $"Field with Id {fieldUpdate.FieldId} does not exist"));
                    }
                    
                    if (field.FieldMovementCost == FieldMovementCostType.Impassable)
                    {
                        return new BadRequestObjectResult(new ApiResponse(400, $"Field is impassable {field.Id}"));
                    }

                    field.UpdateParticipanceData(participanceData);
                }
            }
            
            encounter.IsActive = true;
            await _unitOfWork.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new ApiResponse(400, e.Message));
        }
    }
}