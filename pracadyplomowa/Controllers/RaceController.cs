using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository.Race;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class RaceController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RaceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RaceDTO>> GetRaces()
        {
            var classes = await _unitOfWork.RaceRepository.GetRaceList();
            List<RaceDTO> raceDTOs = _mapper.Map<List<RaceDTO>>(classes);
            
            return Ok(raceDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<RaceDTO>> PostRace([FromBody] RaceDTO race){
            Race characterRace = new Race{Name = race.Name, Size = race.Size, Speed = race.Speed};
            _unitOfWork.RaceRepository.Add(characterRace);
            
            await _unitOfWork.SaveChangesAsync();

            return Ok(characterRace);
        }
    }
}