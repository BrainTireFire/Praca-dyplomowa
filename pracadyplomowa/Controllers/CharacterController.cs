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
    public class CharacterController(ICharacterRepository characterRepository, IClassRepository classRepository, IRaceRepository raceRepository, IMapper mapper, ITokenService tokenService) : BaseApiController
    {
        
        private readonly ICharacterRepository _characterRepository = characterRepository;
        private readonly IClassRepository _classRepository = classRepository;
        private readonly IRaceRepository _raceRepository = raceRepository;
        private readonly IMapper _mapper = mapper;

        private readonly ITokenService _tokenService = tokenService;

        [HttpGet("mycharacters")]
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters()
        {
            var token = Request.Cookies[ConstVariables.COOKIE_NAME];
            var principal = _tokenService.GetPrincipalFromExpiredToken(token!);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var characters = await _characterRepository.GetCharacterSummaries(int.Parse(userIdClaim));

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
            
            Console.WriteLine("test3");
            AbilityEffectInstance dexterity = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.DEXTERITY;
            strength.DiceSet.flat = characterDto.Dexterity;
            
            AbilityEffectInstance constitution = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.CONSTITUTION;
            strength.DiceSet.flat = characterDto.Constitution;
            
            AbilityEffectInstance intelligence = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.INTELLIGENCE;
            strength.DiceSet.flat = characterDto.Intelligence;
            
            AbilityEffectInstance wisdom = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.WISDOM;
            strength.DiceSet.flat = characterDto.Wisdom;
            
            AbilityEffectInstance charisma = new();
            strength.AbilityEffectType.AbilityEffect = AbilityEffect.Bonus;
            strength.AbilityEffectType.AbilityEffect_Ability = Ability.CHARISMA;
            strength.DiceSet.flat = characterDto.Charisma;

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
            var token = Request.Cookies[ConstVariables.COOKIE_NAME];
            var principal = _tokenService.GetPrincipalFromExpiredToken(token!);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            Console.WriteLine(userIdClaim);
            character.R_OwnerId = int.Parse(userIdClaim);

            _characterRepository.Add(character);
            await _characterRepository.SaveChanges();

            Console.WriteLine("testX");
            return Created();
        }
        
        [HttpGet("{characterId}")]
        public async Task<ActionResult> getCharacter(int characterId){
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