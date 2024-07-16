using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository;

namespace pracadyplomowa.Controllers
{
    public class CharacterController: BaseApiController
    {
        
        private readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> GetCharacters(int userId)
        {
            var characters = await _characterRepository.GetCharacters(userId);

            if (userExists != null)
            {
                return BadRequest("Username already exists on this username: " + registerDto.Username);
            }


            return Ok(new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            });
        }
    }
}