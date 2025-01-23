using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
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

    public async Task NextTurn(int encounterId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId);

        var x = encounter.R_Participances.SkipWhile(x => !x.ActiveTurn);
        ParticipanceData participanceData;
        if(x.Count() > 1){
            participanceData = x.Skip(1).First();
        }
        else{
            participanceData = encounter.R_Participances.First();
        }
        encounter.R_Participances.ToList().ForEach(x => x.ActiveTurn = false);
        participanceData.ActiveTurn = true;
        participanceData.NumberOfActionsTaken = 0;
        participanceData.NumberOfBonusActionsTaken = 0;
        participanceData.NumberOfAttacksTaken = 0;
        participanceData.DistanceTraveled = 0;
        var character = await _unitOfWork.CharacterRepository.GetByIdWithAll(participanceData.R_CharacterId);
        character.StartNextTurn();

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
    
    public async Task<List<int>> MoveCharacter(int encounterId, int characterId, List<int> fieldIds){
        if(fieldIds.Count == 0){
            return fieldIds;
        }
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterSummary(encounterId);
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
        var remainingMovementCapacityInFeet = character.Speed - participance.DistanceTraveled;
        var missingMovementCapacity = remainingMovementCapacityInFeet / 5 - traversableFields.Count;
        if(missingMovementCapacity <= 0){
            missingMovementCapacity *= -1;
        }
        else{
            missingMovementCapacity = 0;
        }
        for(int i = 0; i < missingMovementCapacity; i++){
            traversableFields.RemoveAt(traversableFields.Count - 1);
        }
        var traversableIds = traversableFields.Select(x => x.Id).ToList();
        if(traversableFields.Count == fields.Count){
            character.Move(encounter, traversableFields.Last());
            participance.DistanceTraveled += traversableFields.Count * 5;
        }
        await _unitOfWork.SaveChangesAsync();
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

        var attackRollResult = await MakeWeaponAttackRoll(encounter, character, weapon, target, rangedAttack, casterApprovedEffectIds, targetApprovedEffectIds);
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

        var result = character.ApplyWeaponHitEffects(encounter, weapon, target, criticalHit);
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
        var attackRollResult = character.CheckIfWeaponHitSuccessfull(encounter, weapon, target, rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee);
        AttackRollAndDamageResultDto attackRollAndDamageResult = new()
        {
            HitType = attackRollResult
        };
        if (attackRollResult == HitType.Hit || attackRollResult == HitType.CriticalHit){
            attackRollAndDamageResult.WeaponHitResult =  character.ApplyWeaponHitEffects(encounter, weapon, target, attackRollResult == HitType.CriticalHit);
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
            CasterConditionalEffects = [.. character.AllEffects
            .Select(x => new ConditionalEffectsSetDto.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })],
            TargetConditionalEffects = [.. target.AllEffects
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
            CasterConditionalEffects = [.. character.AllEffects
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
                TargetConditionalEffects = [.. target.AllEffects
                                                .Select(x => new ConditionalEffectsSetForManyTargetsDto.ConditionalEffectDto(){
                                                    EffectId = x.Id,
                                                    EffectName = x.Name,
                                                    EffectDescription = x.Description
                                                })]});
        }
        return result;
    }

    public async Task<PowerDataForResolutionDto> GetPowerData(int encounterId, int characterId, int powerId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        Power power = character.AllPowers.FirstOrDefault(x => x.Id == powerId) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character");
        await _unitOfWork.PowerRepository.GetAllByIdsWithEffectBlueprintsAndMaterialResources([power.Id]);
        List<int> availableImmaterialResourceLevels = character.AllImmaterialResourceInstances
                                                            .Where(x => x.R_BlueprintId == power.R_UsesImmaterialResourceId)
                                                            .Select(x => x.Level)
                                                            .Where(x => power.R_EffectBlueprints.Select(y => y.Level).Contains(x))
                                                            .ToList();
        var result = new PowerDataForResolutionDto
        {
            PowerId = power.Id,
            PowerName = power.Name,
            AvailableImmaterialResourceLevels = availableImmaterialResourceLevels,
            ResourceName = power.R_UsesImmaterialResource?.Name! 
        };
        foreach (var levelGroup in power.R_EffectBlueprints.GroupBy(x => x.Level).ToList()){
            var level = levelGroup.Key;
            result.PowerEffects.Add(level, []);
            foreach(var savedGroup in levelGroup.GroupBy(x => x.Saved).ToList()){
                var saved = savedGroup.Key ? 1 : 0;
                result.PowerEffects.GetValueOrDefault(level)!.Add(saved, []);
                foreach(var effect in savedGroup){
                    result.PowerEffects.GetValueOrDefault(level)!.GetValueOrDefault(saved)!.Add(new PowerDataForResolutionDto.PowerEffectDto(){
                        PowerEffectId = effect.Id,
                        PowerEffectName = effect.Name,
                        PowerEffectDescription = effect.Description,
                    });
                }
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
            foreach(var effect in power.R_EffectBlueprints){
                powerDto.PowerEffects.Add(new WeaponDamageAndPowersDto.PowersOnHitDto.PowerEffectDto(){
                    PowerEffectId = effect.Id,
                    PowerEffectName = effect.Name,
                    PowerEffectDescription = effect.Description,
                });
            }
            result.PowersOnHit.Add(powerDto);
        }
        result.DamageValues = damageTypeOnHit;
        return result;
    }

    public async Task<WeaponAttackResultDto> MakeWeaponAttack(int encounterId, [FromQuery] int characterId, [FromQuery] int weaponId, [FromQuery] int targetId, [FromQuery] bool isRanged, [FromBody] WeaponAttackIncomingDataDto approvedConditionalEffects){
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
        var attackRollResult = await MakeWeaponAttackRoll(encounter, character, weapon, target, isRanged, approvedConditionalEffects.WeaponAttackConditionalEffects.CasterConditionalEffects, approvedConditionalEffects.WeaponAttackConditionalEffects.TargetConditionalEffects);
        var result = new WeaponAttackResultDto(){
            AttackRollResult = attackRollResult
        };
        if(attackRollResult == HitType.Hit || attackRollResult == HitType.CriticalHit){
            var damageRollResult = await ApplyWeaponHit(encounter, character, weapon, target, isRanged, attackRollResult == HitType.CriticalHit, approvedConditionalEffects.WeaponAttackConditionalEffects.CasterConditionalEffects, approvedConditionalEffects.WeaponAttackConditionalEffects.TargetConditionalEffects);
            foreach (var power in weapon.R_EquipItemGrantsAccessToPower.Where(x => x.CastableBy == CastableBy.OnWeaponHit))
            {
                var conditionalEffectsForPower = approvedConditionalEffects.Powers.Find(x => x.PowerId == power.Id)!.PowerConditionalEffects;
                var powerRollResult = await CheckWeaponPowerHit(encounter, character, weapon, power, target, conditionalEffectsForPower.CasterConditionalEffects, conditionalEffectsForPower.TargetConditionalEffects);
                result.PowerResult.Add(new WeaponAttackResultDto.PowerUsageResultDto(){
                    PowerName = power.Name,
                    Success = powerRollResult == HitType.Hit || powerRollResult == HitType.CriticalHit
                });
                if(powerRollResult == HitType.Hit || powerRollResult == HitType.CriticalHit){
                    await ApplyWeaponPowerHit(encounter, character, weapon, power, target, powerRollResult, conditionalEffectsForPower.CasterConditionalEffects, conditionalEffectsForPower.TargetConditionalEffects);
                }
            }
        }
        int finalTargetHealth = target.Hitpoints + target.TemporaryHitpoints;
        result.TotalDamage = initialTargetHealth - finalTargetHealth;
        result.HitpointsLeft = target.Hitpoints;
        await _unitOfWork.SaveChangesAsync();
        return result;
    }



    private async Task<HitType> MakeWeaponAttackRoll(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Character target, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
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

        var result = character.CheckIfWeaponHitSuccessfull(encounter, weapon, target, rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee);
        return result;
    }

    private async Task<Character.WeaponHitResult> ApplyWeaponHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Character target, bool rangedAttack, bool criticalHit, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
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

        var result = character.ApplyWeaponHitEffects(encounter, weapon, target, criticalHit);
        return result;
    }

    private async Task<HitType> CheckWeaponPowerHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Power power, Character target, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
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

        var result = weapon.CheckIfPowerHitSuccessfull(encounter, power, [target]);
        return result[target.Id];
    }

    private async Task ApplyWeaponPowerHit(Models.Entities.Campaign.Encounter encounter, Character character, Weapon weapon, Power power, Character target, HitType hitType, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
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

        var result = weapon.ApplyPowerEffects(power, new Dictionary<Character, HitType>(){{target, hitType}}, null, out var generatedEffects);
        foreach(var effect in generatedEffects){
            effect.Resolve();
        }
        foreach(var group in generatedEffects.Where(x => x.R_OwnedByGroup != null).Select(x => x.R_OwnedByGroup).Distinct()){
            group?.TickDuration();
        }
        return;
    }
}