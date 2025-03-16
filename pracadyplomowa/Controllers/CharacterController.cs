using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using pracadyplomowa.Authorization.AuthorizationAttributes;
using pracadyplomowa.Const;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Race;
using pracadyplomowa.Services;
using pracadyplomowa.Repository.UnitOfWork;
using static pracadyplomowa.Models.Entities.Characters.ChoiceGroup;
using static pracadyplomowa.Models.Entities.Powers.SkillEffectInstance;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class CharacterController(IUnitOfWork unitOfWork, IMapper mapper, ICharacterService characterService) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICharacterService _characterService = characterService;

        [HttpGet("mycharacters")]
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters([FromQuery] CharacterParams characterParams)
        {
            var userId = User.GetUserId();
            var isNpc = false;
            var characters = await _unitOfWork.CharacterRepository.GetCharacterSummaries(userId, isNpc, characterParams);

            Response.AddPaginationHeader(characters);

            return Ok(characters);
        }

        [HttpGet("npcCharacters")]
        public async Task<ActionResult<CharacterSummaryDto>> GetNpcCharacters([FromQuery] CharacterParams characterParams)
        {
            var userId = User.GetUserId();
            var isNpc = true;
            var characters = await _unitOfWork.CharacterRepository.GetCharacterSummaries(userId, isNpc, characterParams);

            Response.AddPaginationHeader(characters);

            return Ok(characters);
        }

        [HttpDelete("{characterId}")]
        public async Task<ActionResult> DeleteCharacter(int characterId)
        {
            if (!_characterService.CheckExistenceAndReadEditAccess(characterId, User.GetUserId(), [Character.AccessLevels.Read], out var errorResult, out var grantedAccessLevels))
            {
                return errorResult;
            }
            _unitOfWork.CharacterRepository.Delete(characterId);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewCharacter(CharacterInsertDto characterDto)
        {
            var race = await _unitOfWork.RaceRepository.GetRaceByIdWithRaceLevelAndChoiceGroupsAndSlots(characterDto.RaceId, 1);
            if (race == null)
            {
                return BadRequest(new ApiResponse(400, "Race with Id " + characterDto.RaceId + " does not exist"));
            }
            
            ClassLevel? classLevel = null;

            if (!characterDto.IsNpc)
            {
                if (characterDto.StartingClassId == null)
                {
                    return BadRequest(new ApiResponse(400, "StartingClassId is required for non-NPC characters."));
                }

                classLevel = await _unitOfWork.ClassRepository.GetClassLevelWithChoiceGroups(characterDto.StartingClassId.Value, 1);
                if (classLevel == null)
                {
                    return BadRequest(new ApiResponse(400, $"First level of Class with Id {characterDto.StartingClassId} does not exist"));
                }
            }

            var ownerId = User.GetUserId();
            var character = new Character(
                characterDto.Name,
                characterDto.IsNpc,
                characterDto.Strength,
                characterDto.Dexterity,
                characterDto.Constitution,
                characterDto.Intelligence,
                characterDto.Wisdom,
                characterDto.Charisma,
                classLevel,
                race,
                ownerId,
                0 // 0 xp on start
            );

            var item = (await _unitOfWork.ItemRepository.GetByNameWithEquipmentSlots("Iron longsword")).CloneInstance();
            item.R_OwnerId = User.GetUserId();
            var item2 = item.CloneInstance();
            item2.R_OwnerId = User.GetUserId();

            character.R_CharacterHasBackpack = new Backpack()
            {
                R_BackpackOfCharacter = character,
                R_BackpackHasItems = [item, item2]
            };

            var rightPalmSlot = item.R_ItemIsEquippableInSlots.FirstOrDefault(x => x.Name == "Right palm");
            if (rightPalmSlot != null)
            {
                character.EquipItem(item, rightPalmSlot);
            }

            var leftPalmSlot = item2.R_ItemIsEquippableInSlots.FirstOrDefault(x => x.Name == "Left palm");
            if (leftPalmSlot != null)
            {
                character.EquipItem(item2, leftPalmSlot);
            }

            if (item2 is MeleeWeapon weapon && weapon.Versatile)
            {
                weapon.EquipVersatile();
            }

            _unitOfWork.CharacterRepository.Add(character);
            await _unitOfWork.SaveChangesAsync();

            return Created();
        }

        [HttpGet("{characterId}")]
        public async Task<ActionResult> GetCharacter(int characterId)
        {
            if (!_characterService.CheckExistenceAndReadEditAccess(characterId, User.GetUserId(), [Character.AccessLevels.Read], out var errorResult, out var grantedAccessLevels))
            {
                return errorResult;
            }
            var character = await _unitOfWork.CharacterRepository.GetByIdWithAll(characterId);

            var characterDto = new CharacterFullDto(character)
            {
                AccessLevels = grantedAccessLevels
            };
            return Ok(characterDto);
        }

        [HttpPatch("{characterId}")]
        public async Task<ActionResult> UpdateCharacterDescriptionAndName(int characterId, [FromBody] CharacterDescriptionUpdateDto data)
        {
            if (!_characterService.CheckExistenceAndReadEditAccess(characterId, User.GetUserId(), [Character.AccessLevels.EditDescriptiveFields], out var errorResult, out var grantedAccessLevels, out var character))
            {
                return errorResult;
            }

            character!.Name = data.Name;
            character.Description = data.Description;
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("{characterId}/choiceGroups")]
        public async Task<ActionResult> GetCharactersChoiceGroups(int characterId)
        {
            var character = _unitOfWork.CharacterRepository.GetById(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            character = await _unitOfWork.CharacterRepository.GetByIdWithChoiceGroups(characterId);

            var choiceGroupDto = ChoiceGroupDto.Get(character);
            return Ok(choiceGroupDto);
        }

        [HttpPost("{characterId}/choiceGroups/use")]
        public async Task<ActionResult> GenerateChoiceGroupUsage(int characterId, [FromBody] List<ChoiceGroupUsageDto> ChoiceGroupUsageDtos)
        {
            var character = _unitOfWork.CharacterRepository.GetById(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            character = await _unitOfWork.CharacterRepository.GetByIdForChoice(characterId);
            var choiceGroupsEnumerable = character.R_CharacterHasLevelsInClass.SelectMany(cl => cl.R_ChoiceGroups).Union(character.R_CharacterBelongsToRace.R_RaceLevels.SelectMany(cl => cl.R_ChoiceGroups));
            try
            {
                foreach (ChoiceGroupUsageDto choiceGroupUsageDto in ChoiceGroupUsageDtos)
                {
                    var choiceGroup = choiceGroupsEnumerable.Where(cg => cg.Id == choiceGroupUsageDto.Id).FirstOrDefault();
                    if (choiceGroup == null)
                    {
                        return NotFound(new ApiResponse(404, "Choice group with Id " + choiceGroupUsageDto.Id + " was not found"));
                    }
                    bool allEffectChoicesCorrect = choiceGroupUsageDto.EffectIds.All(item => choiceGroup.R_Effects.Select(e => e.Id).ToList().Contains(item));
                    if (!allEffectChoicesCorrect)
                    {
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected effects"));
                    }
                    bool allPowerAlwaysAvailableChoicesCorrect = choiceGroupUsageDto.PowerAlwaysAvailableIds.All(item => choiceGroup.R_PowersAlwaysAvailable.Select(e => e.Id).ToList().Contains(item));
                    if (!allPowerAlwaysAvailableChoicesCorrect)
                    {
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected powers"));
                    }
                    bool allPowerToPreapreChoicesCorrect = choiceGroupUsageDto.PowerToPrepareIds.All(item => choiceGroup.R_PowersToPrepare.Select(e => e.Id).ToList().Contains(item));
                    if (!allPowerToPreapreChoicesCorrect)
                    {
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected powers"));
                    }
                    bool allResourceChoicesCorrect = choiceGroupUsageDto.ResourceIds.All(item => choiceGroup.R_Resources.Select(e => e.Id).ToList().Contains(item));
                    if (!allResourceChoicesCorrect)
                    {
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected resources"));
                    }
                    var selectedEffects = choiceGroup.R_Effects.Where(e => choiceGroupUsageDto.EffectIds.Contains(e.Id)).ToList();
                    var selectedPowersAlwaysAvailable = choiceGroup.R_PowersAlwaysAvailable.Where(p => choiceGroupUsageDto.PowerAlwaysAvailableIds.Contains(p.Id)).ToList();
                    var selectedPowersToPrepare = choiceGroup.R_PowersToPrepare.Where(p => choiceGroupUsageDto.PowerToPrepareIds.Contains(p.Id)).ToList();
                    var selectedResources = choiceGroup.R_Resources.Where(r => choiceGroupUsageDto.ResourceIds.Contains(r.Id)).ToList();
                    var totalPicks = selectedEffects.Count + selectedPowersAlwaysAvailable.Count + selectedPowersToPrepare.Count + selectedResources.Count;
                    if (totalPicks != 0 && totalPicks == choiceGroup.NumberToChoose)
                    {
                        try
                        {
                            choiceGroup.Generate(character, selectedEffects, selectedPowersAlwaysAvailable, selectedPowersToPrepare, selectedResources);
                        }
                        catch (ExpertiseException ex)
                        {
                            return BadRequest(new ApiResponse(400, ex.Message));
                        }
                    }
                    else if (totalPicks != 0 && totalPicks != choiceGroup.NumberToChoose)
                    {
                        return BadRequest(new ApiResponse(400, "Incorrect number of choices"));
                    }
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (InvalidChoiceGroupSelectionException exception)
            {
                return BadRequest(new ApiResponse(400, exception.Message));
            }
            return Ok();
        }

        [HttpGet("{characterId}/classes/nextLevels")]
        public async Task<ActionResult> GetNextLevelsInClasses(int characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithClassLevels(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            // var currentLevels = character.R_CharacterHasLevelsInClass.GroupBy(cl => cl.R_ClassId).Select(g => new ClassLevel(g.Max(g => g.Level)){
            //     Id = g.Key,
            // });
            var allClassesWithLevels = await _unitOfWork.ClassRepository.GetClassesWithClassLevels(true);
            var firstLevels = allClassesWithLevels.SelectMany(c => c.R_ClassLevels).Where(cl => cl.Level == 1).ToList();

            var notPosessedLevels = new List<ClassLevel>();
            foreach (var characterClass in allClassesWithLevels)
            {
                foreach (var classLevel in characterClass.R_ClassLevels.OrderBy(cl => cl.Level))
                {
                    if (!character.R_CharacterHasLevelsInClass.Contains(classLevel))
                    {
                        notPosessedLevels.Add(classLevel);
                        break;
                    }
                }
            }
            notPosessedLevels = await _unitOfWork.ClassRepository.GetClassLevelsWithChoiceGroups(notPosessedLevels.Select(cl => cl.Id).ToList());
            var result = notPosessedLevels.Select(cl => new
            {
                Id = cl.Id,
                ClassId = cl.R_ClassId,
                Name = cl.R_Class.Name,
                Level = cl.Level,
                ChoiceGroups = cl.R_ChoiceGroups.Select(cg => new ChoiceGroupDto(cg, character)),
                HitDice = cl.HitDie,
                HitPoints = cl.HitPoints,
            }).ToList();
            return Ok(result);
        }

        [HttpPost("{characterId}/classes/nextLevels/{nextClassLevelId}/use")]
        public async Task<ActionResult> SelectNextClassLevel(int characterId, int nextClassLevelId)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithChoiceGroups(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            var classLevel = await _unitOfWork.ClassRepository.GetClassLevelsWithChoiceGroups([nextClassLevelId]);
            if (classLevel.Count == 0)
            {
                return BadRequest(new ApiResponse(400, "Class level with Id " + characterId + " does not exist"));
            }
            character.AddClassLevel(classLevel[0]);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{characterId}/equipmentSlots")]
        public async Task<ActionResult> GetCharacterEquipmentAndSlots(int characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterEquipmentAndSlots(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            var characterDto = new CharacterEquipmentAndSlotsDto();
            characterDto.Items = character.R_CharacterHasBackpack.R_BackpackHasItems.Select(i => new CharacterEquipmentAndSlotsDto.Item()
            {
                Id = i.Id,
                Name = i.Name,
                ItemFamily = new CharacterEquipmentAndSlotsDto.ItemFamily()
                {
                    Id = i.R_ItemInItemsFamily.Id,
                    Name = i.R_ItemInItemsFamily.Name
                },
                Slots = i.R_EquipData != null ? i.R_EquipData.R_Slots.Select(s => new CharacterEquipmentAndSlotsDto.Slot()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList() : [],
                EquippableInSlots = i.R_ItemIsEquippableInSlots.Select(s => new CharacterEquipmentAndSlotsDto.Slot()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            }).ToList();
            characterDto.Slots = character.R_CharacterBelongsToRace.R_EquipmentSlots.Select(s => new CharacterEquipmentAndSlotsDto.Slot()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return Ok(characterDto);
        }

        [HttpPost("{characterId}/equipmentSlots/{slotId}/equip/{itemId}")]
        public async Task<ActionResult> EquipItemInSlot(int characterId, int slotId, int itemId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterEquipmentAndSlots(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            if (!character.R_CharacterBelongsToRace.R_EquipmentSlots.Where(s => s.Id == slotId).Any())
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not have slot with Id " + slotId));
            }
            if (!character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).Any())
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not have item with Id " + itemId));
            }
            if (!character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).First().R_ItemIsEquippableInSlots.Where(s => s.Id == slotId).Any())
            {
                return BadRequest(new ApiResponse(400, "Item with Id " + itemId + " is not equippable in slot with Id " + slotId));
            }
            character.EquipItem(character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).First(), character.R_CharacterBelongsToRace.R_EquipmentSlots.Where(s => s.Id == slotId).First());
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{characterId}/equipmentSlots/{slotId}/unequip/{itemId}")]
        public async Task<ActionResult> UnequipItemInSlot(int characterId, int slotId, int itemId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterEquipmentAndSlots(characterId);
            if (character == null)
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            if (!character.R_CharacterBelongsToRace.R_EquipmentSlots.Where(s => s.Id == slotId).Any())
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not have slot with Id " + slotId));
            }
            if (!character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).Any())
            {
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not have item with Id " + itemId));
            }
            if (!character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).First().R_ItemIsEquippableInSlots.Where(s => s.Id == slotId).Any())
            {
                return BadRequest(new ApiResponse(400, "Item with Id " + itemId + " is not equippable in slot with Id " + slotId));
            }
            character.UnequipItem(character.R_CharacterHasBackpack.R_BackpackHasItems.Where(i => i.Id == itemId).First());
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{characterId}/equipment")]
        public async Task<ActionResult> AddItemToCharacterEquipment(int characterId, [FromBody] int itemId)
        {
            var character = await _unitOfWork.CharacterRepository.GetCharacterEquipment(characterId);
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if (item != null)
            {
                if (character == null)
                {
                    return NotFound("Character with id: ${characterId} was not found");
                }
                if (item.IsBlueprint)
                {
                    item = item.CloneInstance();
                }
                character.R_CharacterHasBackpack.R_BackpackHasItems.Add(item);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return NotFound("Item with id: ${itemId} was not found");
            }
            return Ok();
        }

        [HttpGet("{characterId}/resources")]
        public async Task<ActionResult<List<SlotDto>>> GetCharacterResources(int characterId)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithCustomResources(characterId);
            if (character == null)
            {
                return NotFound("Character with this Id does not exist");
            }
            var resources = character.R_ImmaterialResourceInstances.GroupBy(r => new { r.R_BlueprintId, r.R_Blueprint.Name, r.Level }).Select(g => new ImmaterialResourceAmountDto()
            {
                BlueprintId = g.Key.R_BlueprintId,
                Name = g.Key.Name,
                Level = g.Key.Level,
                Count = g.Count()
            }).ToList();
            return Ok(resources);
        }

        [HttpPatch("{characterId}/resources")]
        public async Task<ActionResult<List<SlotDto>>> SetCharacterResources(int characterId, [FromBody] List<ImmaterialResourceAmountDto> resourceDtos)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithCustomResources(characterId);
            if (character == null)
            {
                return NotFound("Character with this Id does not exist");
            }
            // var resourceBlueprints = await _immaterialResourceBlueprintRepository.GetAllByIds(resourceDtos.Select(x => x.BlueprintId).ToList());
            character.R_ImmaterialResourceInstances.Clear();
            var resourceInstances = resourceDtos
                .SelectMany(dto =>
                    Enumerable.Range(0, dto.Count).Select(_ => new ImmaterialResourceInstance
                    {
                        R_BlueprintId = dto.BlueprintId,
                        Level = dto.Level
                    })
                ).ToList();
            character.R_ImmaterialResourceInstances.AddRange(resourceInstances);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{characterId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> GetCharacterCustomSourceKnownPowers(int characterId)
        { // returns list of powers which do not have source
            var character = await _unitOfWork.CharacterRepository.GetByIdWithKnownPowers(characterId);
            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var powers = _mapper.Map<List<PowerCompactDto>>(character.R_PowersKnown);
            return Ok(powers);
        }

        [HttpGet("{characterId}/powersPrepared")]
        public async Task<ActionResult<List<SlotDto>>> GetCharacterAllPreparedPowers(int characterId)
        { // returns list of powers which do not have source
            var character = await _unitOfWork.CharacterRepository.GetByIdWithPreparedPowers(characterId);

            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var powers = _mapper.Map<List<PowerCompactDto>>(character.R_PowersPrepared.SelectMany(ps => ps.R_PreparedPowers).ToList());
            return Ok(powers);
        }

        [HttpGet("{characterId}/powersPrepared/class/{classId}")]
        public async Task<ActionResult<List<SlotDto>>> GetCharacterPreparedPowersForClass(int characterId, int classId)
        { // returns list of powers which do not have source
            var character = await _unitOfWork.CharacterRepository.GetByIdWithPreparedPowers(characterId);

            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var powers = _mapper.Map<List<PowerCompactDto>>(character.R_PowersPrepared.Where(ps => ps.R_ClassId == classId).SelectMany(ps => ps.R_PreparedPowers).ToList());
            return Ok(powers);
        }

        [HttpGet("{characterId}/powersToPrepare")]
        public async Task<ActionResult<List<PowersToPrepareDto>>> GetCharacterPowersToPrepare(int characterId)
        { // returns list of powers which do not have source
            var character = await _unitOfWork.CharacterRepository.GetByIdWithPowersToPrepare(characterId);
            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var powers = character
            .R_UsedChoiceGroups
            .Select(ucg => ucg.R_ChoiceGroup)
            .Select(cg => cg.R_GrantedByClassLevel)
            .Where(cl => cl != null)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            .Select(cl => cl.R_Class)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            .Distinct()
            .Select(x => new PowersToPrepareDto()
            {
                PowerList = _mapper.Map<List<PowerCompactDto>>(
                    x.R_ClassLevels
                    .SelectMany(cl => cl.R_ChoiceGroups)
                    .SelectMany(cg => cg.R_UsageInstances)
                    .SelectMany(cgu => cgu.R_PowersToPrepareGranted)
                    .ToList()
                        .Union(x.R_ClassLevels
                        .Select(cl => cl.R_Class)
                        .Distinct()
                        .SelectMany(cl => cl.R_AccessiblePowers)
                        )
                ),
                numberToChoose = x.MaximumPreparedSpellsFormula.Roll(character),
                classId = x.Id,
                className = x.Name
            }).ToList();
            return Ok(powers);
        }

        [HttpPatch("{characterId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> SetCharacterPowers(int characterId, [FromBody] List<PowerCompactDto> powerDtos)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithKnownPowers(characterId);
            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var powers = _unitOfWork.PowerRepository.GetAllByIds(powerDtos.Select(x => x.Id).ToList());
            character.R_PowersKnown.Clear();
            character.R_PowersKnown.AddRange(await powers);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{characterId}/powersPrepared/class/{classId}")]
        public async Task<ActionResult<List<SlotDto>>> SetCharacterPowersPrepared(int characterId, [FromBody] List<PowerCompactDto> powerDtos, int classId)
        {
            var character = await _unitOfWork.CharacterRepository.GetByIdWithPreparedPowers(characterId);
            if (character == null)
            {
                return NotFound("character with this Id does not exist");
            }
            var maxPowers = character.GetMaximumPreparedPowers(classId);
            if (maxPowers < powerDtos.Count)
            {
                return BadRequest($"You cannot prepare more than {maxPowers} for class with id: {classId}");
            }
            var powers = _unitOfWork.PowerRepository.GetAllByIds(powerDtos.Select(x => x.Id).ToList());
            if (character.R_PowersPrepared.Where(ps => ps.R_ClassId == classId).FirstOrDefault() == null)
            {
                character.R_PowersPrepared.Add(new PowerSelection()
                {
                    R_ClassId = classId,
                    R_CharacterId = characterId
                });
            }
            else
            {
                character.R_PowersPrepared.Where(ps => ps.R_ClassId == classId).First().R_PreparedPowers.Clear();
            }
            character.R_PowersPrepared.Where(ps => ps.R_ClassId == classId).First().R_PreparedPowers.AddRange(await powers);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{characterId}/constantEffects")]
        public async Task<ActionResult> AddNewEffectInstance([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int characterId)
        {
            var effectInstance = _mapper.Map<EffectInstance>(effectDto);

            effectInstance.R_TargetedCharacterId = characterId;

            _unitOfWork.EffectInstanceRepository.Add(effectInstance);
            await _unitOfWork.SaveChangesAsync();
            return Ok(effectInstance.Id);
        }

        [HttpPost("{characterId}/temporaryEffects")]
        public async Task<ActionResult> AddNewTemporaryEffectInstance([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int characterId)
        {
            var effectInstance = _mapper.Map<EffectInstance>(effectDto);

            effectInstance.R_OwnedByGroup = new EffectGroup()
            {
                DurationLeft = effectDto.DurationLeft,
                Name = "Custom"
            };

            effectInstance.R_TargetedCharacterId = characterId;

            _unitOfWork.EffectInstanceRepository.Add(effectInstance);
            await _unitOfWork.SaveChangesAsync();
            return Ok(effectInstance.Id);
        }

        [HttpPatch("{characterId}/xp")]
        public async Task<ActionResult> UpdateXP(int characterId, [FromBody] int xp)
        {
            if (!_characterService.CheckExistenceAndReadEditAccess(characterId, User.GetUserId(), [Character.AccessLevels.EditDescriptiveFields], out var errorResult, out var grantedAccessLevels, out var character))
            {
                return errorResult;
            }
            character!.ExperiencePoints = xp;

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{characterId}/allPowersForEncounter")]
        public async Task<ActionResult<List<PowerForEncounterDto>>> GetPowersForEncounter(int characterId){
            if (!_characterService.CheckExistenceAndReadEditAccess(characterId, User.GetUserId(), [Character.AccessLevels.Read], out var errorResult, out var grantedAccessLevels, out var character))
            {
                return errorResult;
            }
            character = await _unitOfWork.CharacterRepository.GetByIdWithAll(characterId);
            var powers = await _unitOfWork.PowerRepository.GetAllByIdsWithEffectBlueprintsAndMaterialResources(character.AllPowers.Select(x => x.Id).ToList());

            var result = new List<PowerForEncounterDto>();
            foreach(var power in character.AllPowers){
                var materialComponentsRequired = power.R_ItemsCostRequirement.Select(x => new PowerForEncounterDto.MaterialComponentDto(){
                    Id = x.R_ItemFamilyId,
                    Name = x.R_ItemFamily.Name,
                    Cost = x.Worth
                }).ToList();
                var materialComponentsSatisfied = character.HasAllMaterialComponentsForPower(power);
                var mainHandsOccupied = character.R_EquippedItems.SelectMany(x => x.R_Slots)
                                                                         .Where(x => x.Type == SlotType.MainHand)
                                                                         .Count();
                var offHandsOccupied = character.R_EquippedItems.SelectMany(x => x.R_Slots)
                                                                         .Where(x => x.Type == SlotType.OffHand)
                                                                         .Count();
                var mainHandsPossesed = character.R_CharacterBelongsToRace.R_EquipmentSlots.Where(x => x.Type == SlotType.MainHand).Count();
                var offHandsPossesed = character.R_CharacterBelongsToRace.R_EquipmentSlots.Where(x => x.Type == SlotType.OffHand).Count();
                var spellcastingFocusHeld = character.R_EquippedItems.Where(x => x.R_Item.IsSpellFocus && x.R_Slots.Where(y => y.Type == SlotType.MainHand || y.Type == SlotType.MainHand).Any()).Any();
                bool somaticComponentRequirementSatisfied = mainHandsPossesed - mainHandsOccupied > 0 || offHandsOccupied - offHandsPossesed > 0 || spellcastingFocusHeld || !power.SomaticComponent;
                bool vocalComponentSatisfied = !character.HasAnyCondition([Condition.Muffled, Condition.Petrified]) || !power.VerbalComponent;
                var powerDto = new PowerForEncounterDto(){
                    Id = power.Id,
                    Name = power.Name,
                    Description = power.Description,
                    ResourceName = power.R_UsesImmaterialResource?.Name,
                    AvailableLevels = [],
                    ActionTypeRequired = power.RequiredActionType,
                    RequiredResourceAvailable = true,
                    MaterialComponents = materialComponentsRequired,
                    RequiredMaterialComponentsAvailable = materialComponentsSatisfied,
                    SomaticComponentRequirementSatisfied = somaticComponentRequirementSatisfied,
                    VocalComponentRequirementSatisfied = vocalComponentSatisfied,
                    Range = power.Range,
                    MaxTargets = power.MaxTargets,
                    AreaShape = power.AreaShape,
                    AreaSize = power.AreaSize,
                    CastableBy = power.CastableBy,
                    PowerType = power.PowerType,
                    TargetType = power.TargetType
                };
                var levelsCastable = new List<int>();
                if(power.UpcastBy == UpcastBy.ResourceLevel){
                    var powerLevels = power.R_EffectBlueprints.Select(x => x.Level).Distinct().Order().ToList();
                    levelsCastable.AddRange(power.R_EffectBlueprints.Select(x => x.Level).Distinct().Where(x => power.RequiredResourceAvailable(character, x)));
                    foreach(var level in powerLevels){
                        foreach(var resourceLevel in character.AllImmaterialResourceInstances.Where(x => !x.NeedsRefresh && x.Level >= level && x.R_BlueprintId == power.R_UsesImmaterialResource?.Id).Select(x => x.Level).Order().ToList()){
                            powerDto.AvailableLevels.Add(new PowerForEncounterDto.ImmaterialResourceSelection(){
                                PowerLevel = level,
                                ResourceLevel = resourceLevel
                            });
                        }
                    }
                    powerDto.RequiredResourceAvailable = powerDto.AvailableLevels.Count > 0;
                }
                result.Add(powerDto);
            }
            return result;
        }

        [HttpGet("{characterId}/concentration")]
        public async Task<ActionResult<ConcentrationDataDto>> GetPowerConcentratedOn([FromRoute] int characterId)
        {
            var effectGroup = await _unitOfWork.EffectGroupRepository.GetEffectGroupConcentratedOn(characterId);
            var result = new ConcentrationDataDto(){
                EffectGroupId = effectGroup?.Id,
                EffectGroupName = effectGroup?.Name,
                EffectGroupDurationLeft = effectGroup?.DurationLeft,
            };
            return Ok(result);
        }

        [HttpDelete("{characterId}/concentration")]
        public async Task<ActionResult> DropConcentration([FromRoute] int characterId)
        {
            var effectGroup = await _unitOfWork.EffectGroupRepository.GetEffectGroupConcentratedOn(characterId);
            effectGroup?.Disperse();
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}