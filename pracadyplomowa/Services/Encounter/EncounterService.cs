using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
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

    public async Task<EncounterSummaryDto> GetEncounterAsync(int encounterId, int userId)
    {
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId);
        var encounterSummary = _mapper.Map<EncounterSummaryDto>(encounter);
        encounterSummary.AmIGameMaster = userId == encounter.R_OwnerId;
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

    public async Task<ActionResult> RollInitiativeAsync(int encounterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId);
        if (encounter == null)
        {
            return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
        }
        var characters = new List<Character>();
        var participatingCharacters = encounter.R_Participances.Select(p => p.R_Character).ToList();
        foreach(var character in participatingCharacters){
            characters.Add(await _unitOfWork.CharacterRepository.GetByIdWithAll(character.Id));
        }
        foreach(var participanceData in encounter.R_Participances){
            participanceData.InitiativeRollResult = new DiceSet(){d20 = 1}.Roll(null) + participanceData.R_Character.Initiative;
        }
        int i = 1;
        encounter.R_Participances.OrderByDescending(x => x.InitiativeRollResult).ToList().ForEach((x) => {x.InitiativeOrder = i++; x.ActiveTurn = false;});
        var firstParticipance = encounter.R_Participances.OrderByDescending(x => x.InitiativeRollResult).FirstOrDefault();
        if(firstParticipance != null){
            firstParticipance.ActiveTurn = true;
        }
        await  _unitOfWork.SaveChangesAsync();
        return new OkResult();
    }
    
    public async Task<ActionResult> GetInitiativeQueueAsync(int encounterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithPlayerDetails(encounterId);
        if (encounter == null)
        {
            return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
        }
        var result = encounter.R_Participances.OrderBy(x => x.InitiativeOrder).Select(participance => new InitiativeQueueItemDto(){
            CharacterId = participance.R_CharacterId,
            Name = participance.R_Character.Name,
            PlayerName = participance.R_Character.R_Owner!.UserName ?? "Unknown",
            PlaceInQueue = participance.InitiativeOrder,
            InitiativeRollResult = participance.InitiativeRollResult,
            ActiveTurn = participance.ActiveTurn
        });
        return new OkObjectResult(result);
    }
    
    public async Task<ActionResult> ModifyInitiativeQueueAsync(int encounterId, List<ModifyInitiativeQueueOrderItem> newQueue){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);
        if (encounter == null)
        {
            return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
        }
        foreach(var item in newQueue){
            var participance = encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == item.CharacterId);
            if(participance != null){
                participance.InitiativeOrder = item.PlaceInQueue;
            }
            else{
                return new BadRequestObjectResult(new ApiResponse(400, $"Character with ID: {item.CharacterId} is not taking part in this encounter"));
            }
        }
        await _unitOfWork.SaveChangesAsync();
        return new OkResult();
    }

    public async Task SetActiveTurn(int encounterId, int activeCharacterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);

        var participance = encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == activeCharacterId);
        if(participance != null){
            encounter.R_Participances.ToList().ForEach(x => x.ActiveTurn = false);
            participance.ActiveTurn = true;
        }
        await _unitOfWork.SaveChangesAsync();
        return;
    }

    // public ActionResult CheckIfIsGM(int encounterId, int userId){
    //     var encounter = _unitOfWork.EncounterRepository.GetById(encounterId);
    //     var isGm = encounter.R_OwnerId == userId;
    //     return new OkObjectResult(isGm);
    // }

    public bool CheckIfIsGM(int encounterId, int userId){
        var encounter = _unitOfWork.EncounterRepository.GetById(encounterId);
        return encounter.R_OwnerId == userId;
    }
    public async Task<bool> CheckIfItsMyTurn(int encounterId, int characterId, int userId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);
        var isGM = encounter.R_OwnerId == userId;
        var result = encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId && (x.R_Character.R_OwnerId == userId || isGM));
        if(result == null){
            return false;
        }
        else{
            return result.ActiveTurn;
        }
    }
    public async Task<List<int>> GetControlledCharacters(int encounterId, int userId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);
        var result = encounter.R_Participances.Where(x => x.R_Character.R_OwnerId == userId).Select(x => x.R_CharacterId).ToList();
        return result;
    }
    public async Task<Models.DTOs.Session.ParticipanceDataDto> GetParticipanceData(int encounterId, int characterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipance(encounterId, characterId);
        var character = await _unitOfWork.CharacterRepository.GetByIdWithAll(characterId);
        var result = encounter.R_Participances.Where(x => x.R_CharacterId == characterId).Select(x => new Models.DTOs.Session.ParticipanceDataDto(){
            ActionsTaken = x.NumberOfActionsTaken,
            BonusActionsTaken = x.NumberOfBonusActionsTaken,
            AttacksMade = x.NumberOfAttacksTaken,
            MovementUsed = x.DistanceTraveled,
            TotalActions = x.R_Character.TotalActionsPerTurn,
            TotalAttacksPerAction = x.R_Character.TotalAttacksPerTurn,
            TotalBonusActions = x.R_Character.TotaBonusActionsPerTurn,
            TotalMovement = x.R_Character.TotalMovementPerTurn
        }).First();
        return result;
    }
}