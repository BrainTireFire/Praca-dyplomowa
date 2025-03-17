using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.DTOs.Session;
using pracadyplomowa.Errors;
using pracadyplomowa.Hubs;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.DTOs.Session;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Entities.Powers.EffectInstances;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Board;
using pracadyplomowa.Repository.Encounter;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.Websockets.Notification;

namespace pracadyplomowa.Services.Encounter;

public class EncounterService : IEncounterService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IHubContext<SessionHub> _hubContext;
    private readonly INotificationService _notificationService;
    
    public EncounterService(
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        IMapper mapper,
        IHubContext<SessionHub> hubContext,
        INotificationService notificationService
   )
    {
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _hubContext = hubContext;
        _notificationService = notificationService;
    }
    
    public async Task<PagedList<EncounterShortDto>> GetEncountersAsync(int ownedId, int campaignId, EncounterParams encounterParams)
    {
        var encounters = await _unitOfWork.EncounterRepository.GetEncounters(ownedId, campaignId, encounterParams);
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
                R_OwnerId = user.Id,
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

    public async Task<ActionResult> ToogleEncounterActiveAsync(int ownerId, int encounterId)
    {
        try
        {
            var encounter = _unitOfWork.EncounterRepository.GetById(encounterId);
                
            if (encounter == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
            }
            
            encounter.IsActive = !encounter.IsActive;
            
            await _unitOfWork.SaveChangesAsync();
            return new NoContentResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new ApiResponse(400, e.Message));
        }
    }

    public async Task<ActionResult> SetEncounterPositionAsync(int ownerId, int encounterId, SetEncounterPositionDto setEncounterPositionDto)
    {
        try
        {
            var encounter = await _unitOfWork.EncounterRepository.GetEncounterForPositionUpdate(encounterId);
            
            if (encounter == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
            }
            
            if (encounter.R_OwnerId != ownerId)
            {
                return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this encounter"));
            }
            
            foreach (var fieldUpdate in setEncounterPositionDto.FieldsToUpdate)
            {
                if (fieldUpdate.ParticipanceDataId == null)
                {
                    // Add new participant (participance data)
                    var character = _unitOfWork.CharacterRepository.GetById(fieldUpdate.CharacterId);
                    if (character == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, "Character with Id " + fieldUpdate.CharacterId + " does not exist"));
                    }
                    
                    var existingParticipient = encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == fieldUpdate.CharacterId);
                    
                    var field = _unitOfWork.FieldRepository.GetById(fieldUpdate.FieldId);
                    if (field == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, $"Field with Id {fieldUpdate.FieldId} does not exist"));
                    }
                    
                    if (field.FieldMovementCost == FieldMovementCostType.Impassable)
                    {
                        return new BadRequestObjectResult(new ApiResponse(400, $"Field is impassable {field.Id}"));
                    }
                    
                    if (existingParticipient?.R_Character != null)
                    {
                        field.UpdateParticipanceData(existingParticipient);
                    }
                    else
                    {
                        var newParticipance = encounter.AddParticipance(character);
                        field.UpdateParticipanceData(newParticipance);
                    }
                }
                else
                {
                    // Update existing participance and field
                    var participanceData = encounter.R_Participances.FirstOrDefault(x => x.Id == fieldUpdate.ParticipanceDataId.Value);
                    if (participanceData == null)
                    {
                        return new NotFoundObjectResult(new ApiResponse(400, "ParticipanceData with Id " + fieldUpdate.ParticipanceDataId.Value + " does not exist"));
                    }

                    if (participanceData.R_Character.Id != fieldUpdate.CharacterId) continue;
                    
                    participanceData.UpdateCharacter(participanceData.R_Character);

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

            foreach (var characterId in setEncounterPositionDto.ParticipanceToDelete)
            {
                _unitOfWork.ParticipanceDataRepository.RemoveByCharacterId(characterId);
                encounter.RemoveParticipanceByCharacterId(characterId);
            }
            
            encounter.IsActive = setEncounterPositionDto.IsActive;
            await _unitOfWork.SaveChangesAsync();

            return new NoContentResult();
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(new ApiResponse(400, e.Message));
        }
    }
    
    public async Task<ActionResult> RemoveEncounterAsync(int ownerId, int encounterId)
    {
        try
        {
            var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummaryForDelete(encounterId);
            
            if (encounter == null)
            {
                return new NotFoundObjectResult(new ApiResponse(400, "Encounter with Id " + encounterId + " does not exist"));
            }
            
            if (encounter.R_Owner == null || encounter.R_Owner.Id != ownerId)
            {
                return new UnauthorizedObjectResult(new ApiResponse(401, "You are not the owner of this encounter"));
            }
            
            await _unitOfWork.BeginTransactionAsync();
            
            foreach (var participance in encounter.R_Participances)
            {
                if (participance.R_OccupiedField != null)
                {
                    participance.R_OccupiedField.R_OccupiedBy = null;
                }
                _unitOfWork.ParticipanceDataRepository.Delete(participance.Id);
            }
                
            var campaign = _unitOfWork.CampaignRepository.GetById(encounter.R_Campaign.Id);
            
            if (campaign != null)
            {
                campaign.R_CampaignHasEncounters.Remove(encounter);
            }
            
            var board = _unitOfWork.BoardRepository.GetById(encounter.R_Board.Id);
            
            if (board != null)
            {
                board.R_Encounter = null;
            }
            
            _unitOfWork.EncounterRepository.Delete(encounterId);
            
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return new NoContentResult();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
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
            IsNpc = participance.R_Character.IsNpc,
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

    public async Task NextTurn(int encounterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);
        var participances = encounter.R_Participances.OrderBy(x => x.InitiativeOrder);
        var x = participances.SkipWhile(x => !x.ActiveTurn);
        ParticipanceData participanceData;
        if(x.Count() > 1){
            participanceData = x.Skip(1).First();
        }
        else{
            participanceData = participances.First();
        }
        participances.ToList().ForEach(x => x.ActiveTurn = false);
        participanceData.ActiveTurn = true;
        participanceData.NumberOfActionsTaken = 0;
        participanceData.NumberOfBonusActionsTaken = 0;
        participanceData.NumberOfAttacksTaken = 0;
        participanceData.DistanceTraveled = 0;
        var character = await _unitOfWork.CharacterRepository.GetByIdWithAll(participanceData.R_CharacterId);
        List<string> messages = [];
        character.StartNextTurn(messages);
        if(participanceData == participances.First()){
            List<EffectGroup> effectGroups = await _unitOfWork.EffectGroupRepository.GetAllEffectGroupsPresentInEncounter(encounterId);
            foreach(var effectGroup in effectGroups){
                effectGroup.TickDuration();
            }
        }

        await CommitAndReport(encounter, messages);
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
            CharacterName = x.R_Character.Name,
            ActionsTaken = x.NumberOfActionsTaken,
            BonusActionsTaken = x.NumberOfBonusActionsTaken,
            AttacksMade = x.NumberOfAttacksTaken,
            MovementUsed = x.DistanceTraveled,
            TotalActions = x.R_Character.TotalActionsPerTurn,
            TotalAttacksPerAction = x.R_Character.TotalAttacksPerTurn,
            TotalBonusActions = x.R_Character.TotalBonusActionsPerTurn,
            TotalMovement = x.R_Character.TotalMovementPerTurn,
            Hitpoints = character.Hitpoints,
            MaxHitpoints = character.MaxHealth,
            TemporaryHitpoints = character.TemporaryHitpoints
        }).First();
        return result;
    }
    public async Task UpdateParticipanceData(int encounterId, int characterId, Models.DTOs.Session.ParticipanceDataDto participanceDataDto){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipance(encounterId, characterId);
        var participance = encounter.R_Participances.First(x => x.R_CharacterId == characterId);
        participance.DistanceTraveled = participanceDataDto.MovementUsed;
        participance.NumberOfActionsTaken = participanceDataDto.ActionsTaken;
        participance.NumberOfBonusActionsTaken = participanceDataDto.BonusActionsTaken;
        participance.NumberOfAttacksTaken = participanceDataDto.AttacksMade;
        var character = await _unitOfWork.CharacterRepository.GetByIdWithAll(characterId);
        character.Hitpoints = participanceDataDto.Hitpoints;
        character.TemporaryHitpoints = participanceDataDto.TemporaryHitpoints;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteParticipanceData(int encounterId, int characterId, int userId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipance(encounterId, characterId);
        if(encounter.R_OwnerId != userId){
            throw new SessionBadRequestException("You are not Dungeon Master");
        }
        var participance = encounter.R_Participances.Where(i => i.R_CharacterId == characterId).First() ?? throw new SessionBadRequestException("Specified character does not take part in the encounter");
        _unitOfWork.ParticipanceDataRepository.Delete(participance.Id);
        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task<List<int>> MoveCharacter(int encounterId, int characterId, List<int> fieldIds){
        if(fieldIds.Count == 0){
            return fieldIds;
        }
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummaryWithFieldPowers(encounterId);
        foreach(var characterIdInLoop in encounter.R_Participances.Select(x => x.R_CharacterId).ToList()){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(characterIdInLoop);
        }
        var participance = encounter.R_Participances.First(x => x.R_Character.Id == characterId);
        var character = participance.R_Character;
        List<Field> fields = [];
        foreach(var fieldId in fieldIds){
            fields.Add(encounter.R_Board.R_ConsistsOfFields.First(x => x.Id == fieldId));
        }
        var traversableFields = character.CanTraversePath(fields);
        var easilyTraversableFields = traversableFields.Where(x => x.ActualMovementCost == FieldMovementCostType.Low).ToList();
        var difficultTraversableFields = traversableFields.Where(x => x.ActualMovementCost == FieldMovementCostType.High).ToList();
        var impassableFields = traversableFields.Where(x => x.ActualMovementCost == FieldMovementCostType.Impassable).ToList();
        var remainingMovementCapacityInFeet = character.Speed - participance.DistanceTraveled;
        var movementCost = easilyTraversableFields.Count + difficultTraversableFields.Count * 2;
        var missingMovementCapacity = remainingMovementCapacityInFeet / 5 - movementCost;
        if(missingMovementCapacity <= 0){
            missingMovementCapacity *= -1;
        }
        else{
            missingMovementCapacity = 0;
        }
        for(int i = 0; i < missingMovementCapacity; i++){
            if(traversableFields.Count != 0){
                traversableFields.RemoveAt(traversableFields.Count - 1);
            }
        }
        List<string> messages = [];
        var traversableIds = traversableFields.Select(x => x.Id).ToList();
        if(traversableFields.Count == fields.Count){
            foreach(var field in traversableFields){
                character.Move(encounter, field, messages);
            }
            participance.DistanceTraveled += movementCost * 5;
            await CommitAndReport(encounter, messages);
        }
        return traversableIds;
    }

    public class SessionException(string message) : Exception(message) {
    }
    public class SessionNotFoundException(string message) : SessionException(message) {
    }
    public class SessionBadRequestException(string message) : SessionException(message) {
    }

    public async Task<HitType> MakeWeaponAttackRoll(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        Weapon weapon = (Weapon)((character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character"));
        if (rangedAttack && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
            throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        }

        var attackRollResult = await MakeWeaponAttackRoll(encounter, character, weapon, target, rangedAttack, casterApprovedEffectIds, targetApprovedEffectIds, []);
        return attackRollResult;
    }
    public async Task<Character.WeaponHitResult> ApplyWeaponHit(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, bool criticalHit, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        Weapon weapon = (Weapon)((character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character"));
        if (rangedAttack && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
            throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        }

        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }

        var result = character.ApplyWeaponHitEffects(encounter, weapon, target, criticalHit, []);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public async Task<AttackRollAndDamageResultDto> AttackRollAndDamage(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        Weapon weapon = (Weapon)((character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character"));
        if (rangedAttack && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
            throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        }
        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        var attackRollResult = character.CheckIfWeaponHitSuccessfull(encounter, weapon, target, rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee, []);
        AttackRollAndDamageResultDto attackRollAndDamageResult = new()
        {
            HitType = attackRollResult
        };
        if (attackRollResult == HitType.Hit || attackRollResult == HitType.CriticalHit){
            attackRollAndDamageResult.WeaponHitResult =  character.ApplyWeaponHitEffects(encounter, weapon, target, attackRollResult == HitType.CriticalHit, []);
        }
        await _unitOfWork.SaveChangesAsync();
        return attackRollAndDamageResult;
    }


    public async Task<ConditionalEffectsSetDto> GetConditionalEffects(int encounterId, int characterId, int targetId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        // var weapon = (character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character");
        // if (rangedAttack && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
        //     throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        // }


        var result = new ConditionalEffectsSetDto
        {
            CasterConditionalEffects = [.. character.AllEffects.Where(x => x.Conditional == true)
            .Select(x => new ConditionalEffectsSetDto.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })],
            TargetConditionalEffects = [.. target.AllEffects.Where(x => x.Conditional == true)
            .Select(x => new ConditionalEffectsSetDto.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })]
        };
        return result;
    }

    public async Task<ConditionalEffectsSetForManyTargetsDto> GetConditionalEffects(int encounterId, int characterId, List<int> targetIds){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var targetList = new List<Character>();
        foreach(var id in targetIds){
            var target = encounter.R_Participances.First(x => x.R_CharacterId == id).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
            targetList.Add(target);
        }

        var result = new ConditionalEffectsSetForManyTargetsDto()
        {
            CasterConditionalEffects = [.. character.AllEffects.Where(x => x.Conditional == true)
            .Select(x => new ConditionalEffectsSetForManyTargetsDto.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })]
        };
        foreach(var target in targetList){
            result.TargetData.Add(new ConditionalEffectsSetForManyTargetsDto.TargetDataDto(){
                TargetId = target.Id,
                TargetName = target.Name,
                TargetConditionalEffects = [.. target.AllEffects.Where(x => x.Conditional == true)
                                                .Select(x => new ConditionalEffectsSetForManyTargetsDto.ConditionalEffectDto(){
                                                    EffectId = x.Id,
                                                    EffectName = x.Name,
                                                    EffectDescription = x.Description
                                                })]});
        }
        return result;
    }

    public async Task<PowerDataForResolutionDto> GetPowerData(int encounterId, int characterId, int powerId, int? powerLevel, int? resourceLevel){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        Power power = character.AllPowers.FirstOrDefault(x => x.Id == powerId) ?? throw new SessionBadRequestException("Specified power is not available for this character");

        await _unitOfWork.PowerRepository.GetAllByIdsWithEffectBlueprintsAndMaterialResources([power.Id]);
        if(power.UpcastBy == UpcastBy.ResourceLevel && resourceLevel == null){
            throw new SessionBadRequestException("Power upcastable by resource level must have the level of used resource specified");
        }
        if(power.UpcastBy == UpcastBy.ResourceLevel && resourceLevel == null){
            bool resourceAvailable = character.AllImmaterialResourceInstances
                                                    .Where(x => x.R_BlueprintId == power.R_UsesImmaterialResourceId && !x.NeedsRefresh)
                                                    .Select(x => x.Level)
                                                    .Where(x => x == resourceLevel)
                                                    .Any();
            if(!resourceAvailable){
                throw new SessionBadRequestException("Resource of specified level not available");
            }
        }

        if(power.UpcastBy == UpcastBy.CharacterLevel){
            powerLevel = character.Level;
        }
        if(power.UpcastBy == UpcastBy.ClassLevel){
            powerLevel = character.GetLevelInClass((int)power.R_ClassForUpcastingId);
        }
        

        var result = new PowerDataForResolutionDto
        {
            PowerId = power.Id,
            PowerName = power.Name,
            // AvailableImmaterialResourceLevels = availableImmaterialResourceLevels,
            ResourceName = power.R_UsesImmaterialResource?.Name! 
        };

        int maximumApplicableEffectLevel = 0;
        foreach(var effect in  power.R_EffectBlueprints){
            int searchedLevel = 0;
            if(power.UpcastBy == UpcastBy.ResourceLevel){
                searchedLevel = (int)powerLevel!;
            }
            else if(power.UpcastBy == UpcastBy.CharacterLevel){
                searchedLevel = character.Level;
            }
            else if(power.UpcastBy == UpcastBy.ClassLevel){
                searchedLevel = character.GetLevelInClass((int)power.R_ClassForUpcastingId!);
            }

            if(effect.Level <= searchedLevel && effect.Level > maximumApplicableEffectLevel){
                maximumApplicableEffectLevel = effect.Level;
            }
        }
        foreach(var savedGroup in power.R_EffectBlueprints.Where(x => powerLevel == null || x.Level == maximumApplicableEffectLevel || power.UpcastBy == UpcastBy.NotUpcasted).GroupBy(x => x.Saved).ToList()){
            var saved = savedGroup.Key ? 1 : 0;
            result.PowerEffects.Add(saved, []);
            foreach(var effect in savedGroup){
                result.PowerEffects.GetValueOrDefault(saved)!.Add(new PowerDataForResolutionDto.PowerEffectDto(){
                    PowerEffectId = effect.Id,
                    PowerEffectName = effect.Name,
                    PowerEffectDescription = effect.Description,
                });
            }
        }

        return result;
    }

    public async Task<WeaponDamageAndPowersDto> GetWeaponData(int encounterId, int characterId, int weaponId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        Weapon weapon = (Weapon)((character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character"));
        await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersWithEffectsEffectsResources(weapon.Id);
        var result = new WeaponDamageAndPowersDto();
        result.WeaponId = weaponId;
        result.WeaponName = weapon.Name;

        DiceSet damageDiceSet = weapon.DamageValue.getPersonalizedSet(character);
        List<WeaponDamageAndPowersDto.DamageValueDto> damageTypeOnHit = [];
        damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
            DamageType = weapon.DamageType,
            DamageValue = new DiceSetDto(damageDiceSet),
            DamageSource = weapon.Name
        });
        foreach(var effect in weapon.R_AffectedBy.OfType<DamageEffectInstance>().Where(x => x.EffectType.DamageEffect == Models.Enums.EffectOptions.DamageEffect.ExtraWeaponDamage)){
            damageDiceSet = effect.DiceSet.getPersonalizedSet(character);
            damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
                DamageType = weapon.DamageType,
                DamageValue = new DiceSetDto(damageDiceSet),
                DamageSource = effect.Name
            });
        }
        foreach(var effect in weapon.R_AffectedBy.OfType<MagicEffectInstance>()){
            damageDiceSet = effect.DiceSet.getPersonalizedSet(character);
            damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
                DamageType = weapon.DamageType,
                DamageValue = new DiceSetDto(damageDiceSet),
                DamageSource = effect.Name
            });
        }
        foreach(var effect in weapon.R_AffectedBy.OfType<DamageEffectInstance>().Where(x => x.EffectType.DamageEffect == Models.Enums.EffectOptions.DamageEffect.DamageDealt)){
            damageDiceSet = effect.DiceSet.getPersonalizedSet(character);
            damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
                DamageType = (DamageType)effect.EffectType.DamageEffect_DamageType!,
                DamageValue = new DiceSetDto(damageDiceSet),
                DamageSource = effect.Name
            });
        }
        foreach(var effect in character.R_AffectedBy.OfType<DamageEffectInstance>().Where(x => x.EffectType.DamageEffect == Models.Enums.EffectOptions.DamageEffect.ExtraWeaponDamage)){
            damageDiceSet = effect.DiceSet.getPersonalizedSet(character);
            damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
                DamageType = weapon.DamageType,
                DamageValue = new DiceSetDto(damageDiceSet),
                DamageSource = character.Name
            });
        }
        foreach(var effect in character.R_AffectedBy.OfType<DamageEffectInstance>().Where(x => x.EffectType.DamageEffect == Models.Enums.EffectOptions.DamageEffect.DamageDealt)){
            damageDiceSet = effect.DiceSet.getPersonalizedSet(character);
            damageTypeOnHit.Add(new WeaponDamageAndPowersDto.DamageValueDto(){
                DamageType = (DamageType)effect.EffectType.DamageEffect_DamageType!,
                DamageValue = new DiceSetDto(damageDiceSet),
                DamageSource = character.Name
            });
        }

        foreach(var power in weapon.R_EquipItemGrantsAccessToPower.Where(x => x.CastableBy == CastableBy.OnWeaponHit)){
            var powerDto = new WeaponDamageAndPowersDto.PowersOnHitDto(){
                PowerId = power.Id,
                PowerName = power.Name,
                PowerDescription = power.Description
            };
            foreach(var savedGroup in power.R_EffectBlueprints.GroupBy(x => x.Saved).ToList()){
                var saved = savedGroup.Key ? 1 : 0;
                powerDto.PowerEffects.Add(saved, []);
                foreach(var effect in savedGroup){
                    powerDto.PowerEffects.GetValueOrDefault(saved)!.Add(new WeaponDamageAndPowersDto.PowersOnHitDto.PowerEffectDto(){
                        PowerEffectId = effect.Id,
                        PowerEffectName = effect.Name,
                        PowerEffectDescription = effect.Description,
                    });
                }
            }
            result.PowersOnHit.Add(powerDto);
        }
        result.DamageValues = damageTypeOnHit;
        return result;
    }

    public async Task<WeaponAttackResultDto> MakeWeaponAttack(int encounterId, int characterId, int weaponId, int targetId, bool isRanged, WeaponAttackIncomingDataDto approvedConditionalEffects){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var participance = encounter.R_Participances.First(x => x.R_CharacterId == characterId);
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        int initialTargetHealth = target.Hitpoints + target.TemporaryHitpoints;
        Weapon weapon = (Weapon)((character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character"));
        
        if(weapon.R_EquipData!.R_Slots.Where(x => x.Type == SlotType.MainHand).Any())
        {
            var attacksPerActionLeft = character.TotalAttacksPerTurn - participance.NumberOfAttacksTaken;
            var actionsLeft = character.TotalActionsPerTurn - participance.NumberOfActionsTaken;
            if((attacksPerActionLeft == character.TotalAttacksPerTurn || attacksPerActionLeft == 0) && actionsLeft > 0){
                participance.NumberOfActionsTaken++;
                participance.NumberOfAttacksTaken = 1;
            }
            else if(attacksPerActionLeft > 0){
                participance.NumberOfAttacksTaken++;
            }
            else{
                throw new SessionBadRequestException("Character cannot make more main-hand attacks");
            }
        }
        else{
            var bonusActionsLeft = character.TotalBonusActionsPerTurn - participance.NumberOfBonusActionsTaken;
            if(bonusActionsLeft > 0){
                participance.NumberOfBonusActionsTaken++;
            }
            else{
                throw new SessionBadRequestException("Character cannot make more off-hand attacks");
            }
        }

        if (isRanged && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
            throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        }
        await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersWithEffectsEffectsResources(weapon.Id);
        List<string> messages = [];
        var attackRollResult = await MakeWeaponAttackRoll(encounter, character, weapon, target, isRanged, approvedConditionalEffects.WeaponAttackConditionalEffects.CasterConditionalEffects, approvedConditionalEffects.WeaponAttackConditionalEffects.TargetConditionalEffects, messages);
        var result = new WeaponAttackResultDto(){
            AttackRollResult = attackRollResult
        };
        if(attackRollResult == HitType.Hit || attackRollResult == HitType.CriticalHit){
            var damageRollResult = await ApplyWeaponHit(encounter, character, weapon, target, isRanged, attackRollResult == HitType.CriticalHit, approvedConditionalEffects.WeaponAttackConditionalEffects.CasterConditionalEffects, approvedConditionalEffects.WeaponAttackConditionalEffects.TargetConditionalEffects, messages);
            foreach (var power in weapon.R_EquipItemGrantsAccessToPower.Where(x => x.CastableBy == CastableBy.OnWeaponHit))
            {
                var conditionalEffectsForPower = approvedConditionalEffects.Powers.Find(x => x.PowerId == power.Id)!.PowerConditionalEffects;
                var powerRollResult = await CheckWeaponPowerHit(encounter, character, weapon, power, target, conditionalEffectsForPower.CasterConditionalEffects, conditionalEffectsForPower.TargetConditionalEffects, messages);
                result.PowerResult.Add(new WeaponAttackResultDto.PowerUsageResultDto(){
                    PowerName = power.Name,
                    Success = powerRollResult == HitType.Hit || powerRollResult == HitType.CriticalHit
                });
                
                await ApplyWeaponPowerHit(encounter, character, weapon, power, target, powerRollResult, conditionalEffectsForPower.CasterConditionalEffects, conditionalEffectsForPower.TargetConditionalEffects, messages);
            }
        }
        int finalTargetHealth = target.Hitpoints + target.TemporaryHitpoints;
        result.TotalDamage = initialTargetHealth - finalTargetHealth;
        result.HitpointsLeft = target.Hitpoints;
        await CommitAndReport(encounter, messages);
        
        return result;
    }



    private async Task<HitType> MakeWeaponAttackRoll(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Character target, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds, List<string> messages){
        foreach(var effect in character.AllEffects.Where(x => !casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in target.AllEffects.Where(x => !targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }

        var result = character.CheckIfWeaponHitSuccessfull(encounter, weapon, target, rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee, messages);
        return result;
    }

    private async Task<Character.WeaponHitResult> ApplyWeaponHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Character target, bool rangedAttack, bool criticalHit, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds, List<string> messages){
        foreach(var effect in character.AllEffects.Where(x => !casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in target.AllEffects.Where(x => !targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }

        var result = character.ApplyWeaponHitEffects(encounter, weapon, target, criticalHit, messages);
        return result;
    }

    private async Task<HitType> CheckWeaponPowerHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Power power, Character target, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds, List<string> messages){
        foreach(var effect in character.AllEffects.Where(x => !casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in target.AllEffects.Where(x => !targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }

        var result = weapon.CheckIfPowerHitSuccessfull(encounter, power, [target], messages);
        return result[target.Id];
    }

    private async Task ApplyWeaponPowerHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Power power, Character target, HitType hitType, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds, List<string> messages){
        foreach(var effect in character.AllEffects.Where(x => !casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in target.AllEffects.Where(x => !targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = false;
        }
        foreach(var effect in character.AllEffects.Where(x => casterApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }
        foreach(var effect in target.AllEffects.Where(x => targetApprovedEffectIds.Contains(x.Id))){
            effect.ConditionalApproved = true;
        }

        var result = weapon.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, hitType}}, null, null, out var generatedEffects, messages);
        foreach(var effect in generatedEffects){
            effect.Resolve(messages);
        }
        // foreach(var group in generatedEffects.Where(x => x.R_OwnedByGroup != null).Select(x => x.R_OwnedByGroup).Distinct()){
        //     group?.TickDuration();
        // }
        return;
    }

    public async Task<CastPowerResultDto> CastPower(int encounterId, int characterId, int powerId, int? powerLevel, int? immaterialResourceLevel,  CastPowerIncomingDataDto incomingDataDto){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var participance = encounter.R_Participances.First(x => x.R_CharacterId == characterId);
        Dictionary<int, Character> targetMap = new();
        foreach(var targetId in incomingDataDto.ConditionalEffects.TargetConditionalEffects.Keys){
            if(encounter.R_Participances.Select(x => x.R_CharacterId).Contains(targetId)){
                targetMap.Add(targetId, encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character);
            }   
            else{
                throw new SessionBadRequestException("Target character does not take part in specified encounter");
            }
        }
        Power power = (character.AllPowers.FirstOrDefault(x => x.Id == powerId)) ?? throw new SessionBadRequestException("Specified power is not available to casting character");
        power = await _unitOfWork.PowerRepository.GetByIdWithEffectBlueprintsAndMaterialResources(powerId);
        var bonusActionsAvailable = character.TotalBonusActionsPerTurn - participance.NumberOfBonusActionsTaken;
        var actionsAvailable = character.TotalActionsPerTurn - participance.NumberOfActionsTaken;
        var attacksAvailable = character.TotalAttacksPerTurn - participance.NumberOfAttacksTaken;

        //Action analysis
        if(power.RequiredActionType == ActionType.Action && character.TotalActionsPerTurn - participance.NumberOfActionsTaken <= 0){
            throw new SessionBadRequestException("No actions left");
        }
        if(power.RequiredActionType == ActionType.Action && character.TotalActionsPerTurn - participance.NumberOfActionsTaken > 0){
            participance.NumberOfActionsTaken++;
        }

        if(power.RequiredActionType == ActionType.BonusAction && character.TotalBonusActionsPerTurn - participance.NumberOfBonusActionsTaken <= 0){

            throw new SessionBadRequestException("No bonus actions left");
        }
        if(power.RequiredActionType == ActionType.BonusAction && character.TotalBonusActionsPerTurn - participance.NumberOfBonusActionsTaken > 0){
            participance.NumberOfBonusActionsTaken++;
        }

        if(power.RequiredActionType == ActionType.WeaponAttack && character.TotalAttacksPerTurn - participance.NumberOfAttacksTaken <= 0){
            if(character.TotalActionsPerTurn - participance.NumberOfActionsTaken <= 0){
                throw new SessionBadRequestException("No actions left");
            }
            else if(character.TotalActionsPerTurn - participance.NumberOfActionsTaken > 0){
                participance.NumberOfActionsTaken++;
                participance.NumberOfAttacksTaken = 0;
            }
        }
        if(power.RequiredActionType == ActionType.WeaponAttack && character.TotalAttacksPerTurn - participance.NumberOfAttacksTaken > 0){
            participance.NumberOfAttacksTaken++;
            throw new SessionBadRequestException("No attacks left");
        }

        //set approved effects
        foreach(var effect in character.AllEffects){
            effect.ConditionalApproved = incomingDataDto.ConditionalEffects.CasterConditionalEffects.Contains(effect.Id);
        }
        foreach(var targetId in incomingDataDto.ConditionalEffects.TargetConditionalEffects.Keys){
            foreach(var effect in targetMap[targetId].AllEffects){
                effect.ConditionalApproved = incomingDataDto.ConditionalEffects.TargetConditionalEffects[targetId].Contains(effect.Id);
            }
        }

        List<string> messages = [];
        var hitMap = character.CheckIfPowerHitSuccessfull(encounter, power, targetMap.Values.ToList(), messages);
        Dictionary<Character, HitType> characterToHitMap = new ();
        foreach(var hit in hitMap){
            characterToHitMap.Add(targetMap[hit.Key], hit.Value);
        }
        var outcome = character.ApplyPowerEffects(power, characterToHitMap, immaterialResourceLevel, powerLevel,  out var generatedEffects, messages);
        if(outcome == Models.Entities.Interfaces.Outcome.ImmaterialResourceUnavailable){
            throw new SessionBadRequestException("Character doesn't have immaterial resource of specified level");
        }
        if(outcome == Models.Entities.Interfaces.Outcome.InsufficientMaterialComponents){
            throw new SessionBadRequestException("Character doesn't have enough material resources");
        }
        CastPowerResultDto result = new();
        result.HitMap = hitMap;
        foreach(var target in targetMap.Values){
            result.NameMap.Add(target.Id, target.Name);
        }
        
        await CommitAndReport(encounter, messages);
        
        return result;
    }

    public async Task MoveUpQueue(int encounterId, int characterId, int userId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipancesAndCampaign(encounterId) ?? throw new SessionNotFoundException("Encounter not found");
        if(encounter.R_OwnerId != userId){
            throw new SessionBadRequestException("You are not Dungeon Master");
        }
        // Find the index of the item with the given id
        var participanceList = encounter.R_Participances.OrderBy(x => x.InitiativeOrder).ToList();
        int currentIndex = participanceList.FindIndex(p => p.R_CharacterId == characterId);

        if (currentIndex == -1) throw new SessionNotFoundException("Character not found");

        int predecessorIndex = currentIndex == 0 ? participanceList.Count - 1 : currentIndex - 1;

        (participanceList[predecessorIndex].InitiativeOrder, participanceList[currentIndex].InitiativeOrder) = (participanceList[currentIndex].InitiativeOrder, participanceList[predecessorIndex].InitiativeOrder);

        await _unitOfWork.SaveChangesAsync();
    }

        public async Task MoveDownQueue(int encounterId, int characterId, int userId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipancesAndCampaign(encounterId) ?? throw new SessionNotFoundException("Encounter not found");
        if(encounter.R_OwnerId != userId){
            throw new SessionBadRequestException("You are not Dungeon Master");
        }
        // Find the index of the item with the given id
        var participanceList = encounter.R_Participances.OrderBy(x => x.InitiativeOrder).ToList();
        int currentIndex = participanceList.FindIndex(p => p.R_CharacterId == characterId);

        if (currentIndex == -1) throw new SessionNotFoundException("Character not found");

        int successorIndex = (currentIndex == participanceList.Count - 1) ? 0 : currentIndex + 1;

        (participanceList[successorIndex].InitiativeOrder, participanceList[currentIndex].InitiativeOrder) = (participanceList[currentIndex].InitiativeOrder, participanceList[successorIndex].InitiativeOrder);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task CommitAndReport(Models.Entities.Campaign.Encounter encounter, List<string> messages){
        string finalMessage = messages.Aggregate( "", (acc, current) => acc += current + "\n");
        if(!finalMessage.IsNullOrEmpty()){
            _unitOfWork.ActionLogRepository.Add(new ActionLog(){
                Content =  finalMessage,
                Source = "System",
                Time = DateTime.Now,
                R_CampaignId = (int)encounter.R_CampaignId!,
                EncounterId = encounter.Id
            });
        }
        await _unitOfWork.SaveChangesAsync();
        
        if(!finalMessage.IsNullOrEmpty()){
            await _hubContext.Clients.Group(encounter.Id.ToString()).SendAsync("ReceiveMessage", new MessageDto(){
                Username = "System",
                Message = finalMessage,
            });
        }
    }
}