using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Class;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Controllers
{
    public class CharacterController(ICharacterRepository characterRepository, IClassRepository classRepository, IRaceRepository raceRepository, IMapper mapper) : BaseApiController
    {
        
        private readonly ICharacterRepository _characterRepository = characterRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IRaceRepository _raceRepository = raceRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet("mycharacters")]
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters(int userId)
        {
            var characters = await _characterRepository.GetCharacterSummaries(userId);

            return Ok(characters);
        }

        public async Task<ActionResult> CreateNewCharacter(CharacterInsertDto characterDto){
            var race = _raceRepository.GetById(characterDto.RaceId);
            if(race == null){
                return BadRequest("Race with Id " + characterDto.RaceId + " does not exist");
            }
            ClassLevel? classLevel = await _classRepository.GetClassLevel(characterDto.StartingClassId, 1);
            if(classLevel == null){
                return BadRequest("Class with Id " + characterDto.StartingClassId + " does not exist");
            }

            var character = new Character
            {
                Name = characterDto.Name,
                R_CharacterBelongsToRace = race
            };
            character.R_CharacterHasLevelsInClass.Add(classLevel);

            AbilityEffectInstance strength = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Strength
            };
            
            AbilityEffectInstance dexterity = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Dexterity
            };
            
            AbilityEffectInstance constitution = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Constitution
            };
            
            AbilityEffectInstance intelligence = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Constitution
            };
            
            AbilityEffectInstance wisdom = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Wisdom
            };
            
            AbilityEffectInstance charisma = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            strength.DiceSet = new DiceSet
            {
                flat = characterDto.Charisma
            };

            EffectGroup basicStats = new()
            {
                IsConstant = true
            };
            basicStats.R_OwnedEffects.Add(strength);
            basicStats.R_OwnedEffects.Add(dexterity);
            basicStats.R_OwnedEffects.Add(constitution);
            basicStats.R_OwnedEffects.Add(intelligence);
            basicStats.R_OwnedEffects.Add(wisdom);
            basicStats.R_OwnedEffects.Add(charisma);
            basicStats.R_TargetedCharacters.Add(character);

            

            return Ok();
        }
    }
}