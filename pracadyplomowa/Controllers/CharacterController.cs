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
        public async Task<ActionResult<CharacterSummaryDto>> GetCharacters()
        {
            var userId = User.GetUserId();
            var characters = await _characterRepository.GetCharacterSummaries(userId);

            return Ok(characters);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateNewCharacter(CharacterInsertDto characterDto){
            var race = _raceRepository.GetById(characterDto.RaceId);
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
    }
}