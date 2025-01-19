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
            TotalBonusActions = x.R_Character.TotaBonusActionsPerTurn,
            TotalMovement = x.R_Character.TotalMovementPerTurn
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

    public async Task<WeaponAttackConditionalEffectsDtos> GetConditionalEffectForWeaponAttackRoll(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        var weapon = (character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character");
        if (rangedAttack && ((weapon is MeleeWeapon meleeWeapon && !meleeWeapon.Thrown) || weapon is not RangedWeapon)){
            throw new SessionBadRequestException("Can't perform a ranged attack with this weapon");
        }

        var result = new WeaponAttackConditionalEffectsDtos
        {
            WeaponId = weaponId,
            WeaponName = weapon.Name,
            WeaponDescription = weapon.Description,
            CasterConditionalEffects = [.. character.AllEffects
            .OfType<AttackRollEffectInstance>()
            .Where(x => x.Conditional && x.EffectType.AttackRollEffect_Range == (rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee)).Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })]
        };
        result.TargetsConditionalEffects.Add(targetId, new WeaponAttackConditionalEffectsDtos.TargetDto(){
            TargetName = target.Name,
            TargetConditionalEffects = [.. target.AllEffects
        .OfType<ArmorClassEffectInstance>()
        .Where(x => x.Conditional).Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
            EffectId = x.Id,
            EffectName = x.Name,
            EffectDescription = x.Description
        })]
        });
        // var result = new WeaponAttackConditionalEffectsDtos
        // {
        //     WeaponId = weaponId,
        //     WeaponName = weapon.Name,
        //     WeaponDescription = weapon.Description,
        //     CasterConditionalEffects = [.. character.AllEffects
        //     .Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
        //         EffectId = x.Id,
        //         EffectName = x.Name,
        //         EffectDescription = x.Description
        //     })]
        // };
        // result.TargetsConditionalEffects.Add(targetId, new WeaponAttackConditionalEffectsDtos.TargetDto(){
        //     TargetName = target.Name,
        //     TargetConditionalEffects = [.. target.AllEffects
        // .Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
        //     EffectId = x.Id,
        //     EffectName = x.Name,
        //     EffectDescription = x.Description
        // })]
        // });
        return result;
    }

    public class SessionException(string message) : Exception(message) {
    }
    public class SessionNotFoundException(string message) : SessionException(message) {
    }
    public class SessionBadRequestException(string message) : SessionException(message) {
    }

    public async Task<HitType> MakeAttackRoll(int encounterId, int characterId, int weaponId, int targetId, bool rangedAttack, List<int> casterApprovedEffectIds, List<int> targetApprovedEffectIds){
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

        var result = character.CheckIfWeaponHitSuccessfull(encounter, weapon, target, rangedAttack ? Models.Enums.EffectOptions.AttackRollEffect_Range.Ranged : Models.Enums.EffectOptions.AttackRollEffect_Range.Melee);
        return result;
    }

    

    public async Task<WeaponAttackConditionalEffectsDtos> GetConditionalEffectForWeaponHit(int encounterId, int characterId, int weaponId, int targetId){
        var encounter = await _unitOfWork.EncounterRepository.GetEncounterWithParticipances(encounterId) ?? throw new SessionNotFoundException("Encounter with specified Id does not exist");
        foreach (var x in encounter.R_Participances.Select(x => x.R_Character)){
            await _unitOfWork.CharacterRepository.GetByIdWithAll(x.Id);
        }
        var character = (encounter.R_Participances.FirstOrDefault(x => x.R_CharacterId == characterId)?.R_Character) ?? throw new SessionBadRequestException("Attacking character does not take part in specified encounter");
        var target = encounter.R_Participances.First(x => x.R_CharacterId == targetId).R_Character ?? throw new SessionBadRequestException("Target character does not take part in specified encounter");
        var weapon = (character.R_EquippedItems.FirstOrDefault(x => x.R_ItemId == weaponId)?.R_Item) ?? throw new SessionBadRequestException("Specified weapon is not equipped by attacking character");

        var result = new WeaponAttackConditionalEffectsDtos
        {
            WeaponId = weaponId,
            WeaponName = weapon.Name,
            WeaponDescription = weapon.Description,
            CasterConditionalEffects = [.. character.AllEffects
            .OfType<DamageEffectInstance>()
            .Where(x => x.Conditional).Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
                EffectId = x.Id,
                EffectName = x.Name,
                EffectDescription = x.Description
            })]
        };
        result.TargetsConditionalEffects.Add(targetId, new WeaponAttackConditionalEffectsDtos.TargetDto(){
            TargetName = target.Name,
            TargetConditionalEffects = [.. target.AllEffects
        .OfType<ResistanceEffectInstance>()
        .Where(x => x.Conditional).Select(x => new WeaponAttackConditionalEffectsDtos.ConditionalEffectDto(){
            EffectId = x.Id,
            EffectName = x.Name,
            EffectDescription = x.Description
        })]
        });
        return result;
    }
}