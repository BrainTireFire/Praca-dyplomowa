using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Authorization.AuthorizationAttributes;
using pracadyplomowa.Const;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Race;
using static pracadyplomowa.Models.Entities.Characters.ChoiceGroup;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class CharacterController(ICharacterRepository characterRepository, IClassRepository classRepository, IRaceRepository raceRepository, IMapper mapper) : BaseApiController
    {
        
        private readonly ICharacterRepository _characterRepository = characterRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IRaceRepository _raceRepository = raceRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet("mycharacters")]
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters()
        {
            var userId = User.GetUserId();
            var characters = await _characterRepository.GetCharacterSummaries(userId);

            return Ok(characters);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateNewCharacter(CharacterInsertDto characterDto){
            var race = await _raceRepository.GetRaceByIdWithRaceLevelAndChoiceGroups(characterDto.RaceId, 1);
            if(race == null){
                return BadRequest(new ApiResponse(400, "Race with Id " + characterDto.RaceId + " does not exist"));
            }
            ClassLevel? classLevel = await _classRepository.GetClassLevelWithChoiceGroups(characterDto.StartingClassId, 1);
            if(classLevel == null){
                return BadRequest(new ApiResponse(400, "First level of Class with Id " + characterDto.StartingClassId + " does not exist"));
            }
            var ownerId = User.GetUserId();
            var character = new Character(
                characterDto.Name, 
                characterDto.Strength, 
                characterDto.Dexterity, 
                characterDto.Constitution, 
                characterDto.Intelligence, 
                characterDto.Wisdom, 
                characterDto.Charisma, 
                classLevel, 
                race, 
                ownerId
            );

            _characterRepository.Add(character);
            await _characterRepository.SaveChanges();

            return Created();
        }
        
        [HttpGet("{characterId}")]
        public async Task<ActionResult> GetCharacter(int characterId){
            var character = _characterRepository.GetById(characterId);
            if(character == null){
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            character = await _characterRepository.GetByIdWithAll(characterId);

            var characterDto = new CharacterFullDto(character);
            return Ok(characterDto);
        }

        
        
        [HttpGet("{characterId}/choiceGroups")]
        public async Task<ActionResult> GetCharactersChoiceGroups(int characterId){
            var character = _characterRepository.GetById(characterId);
            if(character == null){
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            character = await _characterRepository.GetByIdWithChoiceGroups(characterId);

            var choiceGroupDto = ChoiceGroupDto.Get(character);
            return Ok(choiceGroupDto);
        }

        [HttpPost("{characterId}/choiceGroups/use")]
        public async Task<ActionResult> GenerateChoiceGroupUsage(int characterId, [FromBody] List<ChoiceGroupUsageDto> ChoiceGroupUsageDtos){
            var character = _characterRepository.GetById(characterId);
            if(character == null){
                return BadRequest(new ApiResponse(400, "Character with Id " + characterId + " does not exist"));
            }
            character = await _characterRepository.GetByIdWithChoiceGroups(characterId);
            var choiceGroupsEnumerable = character.R_CharacterHasLevelsInClass.SelectMany(cl => cl.R_ChoiceGroups).Union(character.R_CharacterBelongsToRace.R_RaceLevels.SelectMany(cl => cl.R_ChoiceGroups));
            try{
                foreach(ChoiceGroupUsageDto choiceGroupUsageDto in ChoiceGroupUsageDtos){
                    var choiceGroup = choiceGroupsEnumerable.Where(cg => cg.Id == choiceGroupUsageDto.Id).FirstOrDefault();
                    if(choiceGroup == null){
                        return NotFound(new ApiResponse(404, "Choice group with Id " + choiceGroupUsageDto.Id + " was not found"));
                    }
                    bool allEffectChoicesCorrect = choiceGroupUsageDto.EffectIds.All(item => choiceGroup.R_Effects.Select(e => e.Id).ToList().Contains(item));
                    if(!allEffectChoicesCorrect){
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected effects"));
                    }
                    bool allPowerChoicesCorrect = choiceGroupUsageDto.PowerIds.All(item => choiceGroup.R_Powers.Select(e => e.Id).ToList().Contains(item));
                    if(!allPowerChoicesCorrect){
                        return BadRequest(new ApiResponse(400, "Choice group with Id " + choiceGroupUsageDto.Id + " does not contain selected powers"));
                    }
                    var selectedEffects = choiceGroup.R_Effects.Where(e => choiceGroupUsageDto.EffectIds.Contains(e.Id)).ToList();
                    var selectedPowers = choiceGroup.R_Powers.Where(p => choiceGroupUsageDto.PowerIds.Contains(p.Id)).ToList();
                    var totalPicks = selectedEffects.Count + selectedPowers.Count;
                    if(totalPicks != 0 && totalPicks == choiceGroup.NumberToChoose){
                        choiceGroup.Generate(character, selectedEffects, selectedPowers);
                    }
                    else if(totalPicks != 0 && totalPicks != choiceGroup.NumberToChoose){
                        return BadRequest(new ApiResponse(400, "Incorrect number of choices"));
                    }
                }
                await _characterRepository.SaveChanges();
            }
            catch(InvalidChoiceGroupSelectionException exception){
                return BadRequest(new ApiResponse(400, exception.Message));
            }
            return Ok();
        }
    }
}