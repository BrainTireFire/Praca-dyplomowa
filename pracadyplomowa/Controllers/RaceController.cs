using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Controllers
{
    public class RaceController: BaseApiController
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IMapper _mapper;

        public RaceController(IRaceRepository raceRepository, IMapper mapper)
        {
            _raceRepository = raceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RaceDTO>> GetRaces()
        {
            var classes = await _raceRepository.GetRaceList();


            List<RaceDTO> raceDTOs = _mapper.Map<List<RaceDTO>>(classes);


            return Ok(raceDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<RaceDTO>> PostRace([FromBody] string name){
            Race characterRace = new Race(name);
            _raceRepository.Add(characterRace);
            await _raceRepository.SaveChanges();

            return Ok(characterRace);
        }
    }
}