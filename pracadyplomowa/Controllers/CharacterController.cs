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
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters([FromQuery] UserParams userParams)
        {
            var userId = User.GetUserId();
            var characters = await _characterRepository.GetCharacterSummaries(userId, userParams);
            
            Response.AddPaginationHeader(characters);
            
            return Ok(characters);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateNewCharacter(CharacterInsertDto characterDto){
            var race = _raceRepository.GetById(characterDto.RaceId);
            if(race == null){
                return BadRequest(new ApiResponse(400, "Race with Id " + characterDto.RaceId + " does not exist"));
            }
            ClassLevel? classLevel = await _classRepository.GetClassLevel(characterDto.StartingClassId, 1);
            if(classLevel == null){
                return BadRequest(new ApiResponse(400, "First level of Class with Id " + characterDto.StartingClassId + " does not exist"));
            }
            Console.WriteLine("test1");
            var character = new Character
            {
                Name = characterDto.Name,
                R_CharacterBelongsToRace = race
            };
            character.R_CharacterHasLevelsInClass.Add(classLevel);

            Console.WriteLine("test2");
            AbilityEffectInstance strength = new();
            Console.WriteLine("test2a");
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            Console.WriteLine("test2b");
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.STRENGTH;
            Console.WriteLine("test2c");
            strength.DiceSet.flat = characterDto.Strength;
            strength.Name = "Strength base";
            strength.Description = "Strength base";
            strength.SourceName = "Base";
            
            Console.WriteLine("test3");
            AbilityEffectInstance dexterity = new();
            dexterity.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            dexterity.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            dexterity.DiceSet.flat = characterDto.Dexterity;
            dexterity.Name = "Dexterity base";
            dexterity.Description = "Dexterity base";
            dexterity.SourceName = "Base";
            
            AbilityEffectInstance constitution = new();
            constitution.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            constitution.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            constitution.DiceSet.flat = characterDto.Constitution;
            constitution.Name = "Constitution base";
            constitution.Description = "Constitution base";
            constitution.SourceName = "Base";
            
            AbilityEffectInstance intelligence = new();
            intelligence.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            intelligence.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            intelligence.DiceSet.flat = characterDto.Intelligence;
            intelligence.Name = "Intelligence base";
            intelligence.Description = "Intelligence base";
            intelligence.SourceName = "Base";
            
            AbilityEffectInstance wisdom = new();
            wisdom.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            wisdom.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            wisdom.DiceSet.flat = characterDto.Wisdom;
            wisdom.Name = "Wisdom base";
            wisdom.Description = "Wisdom base";
            wisdom.SourceName = "Base";
            
            AbilityEffectInstance charisma = new();
            charisma.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            charisma.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            charisma.DiceSet.flat = characterDto.Charisma;
            charisma.Name = "Charisma base";
            charisma.Description = "Charisma base";
            charisma.SourceName = "Base";

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

            // basicStats.R_TargetedCharacters.Add(character);
            character.R_AffectedBy.Add(basicStats);

            // Console.WriteLine("User id: ", HttpContext.User.FindFirst(ClaimTypes.NameIdentifier));
            // character.R_OwnerId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            // Console.WriteLine("test");

            // loading current user id
            var userId = User.GetUserId();
            Console.WriteLine(userId);
            character.R_OwnerId = userId;

            _characterRepository.Add(character);
            await _characterRepository.SaveChanges();

            Console.WriteLine("testX");
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
    }
}