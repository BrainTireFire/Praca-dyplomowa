using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Item;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class PowerController: BaseApiController
    {
        private readonly IImmaterialResourceBlueprintRepository _immaterialResourceBlueprintRepository;
        private readonly IPowerRepository _powerRepository;
        private readonly IMapper _mapper;

        public PowerController(IImmaterialResourceBlueprintRepository immaterialResourceBlueprintRepository, IPowerRepository powerRepository, IMapper mapper)
        {
            _immaterialResourceBlueprintRepository = immaterialResourceBlueprintRepository;
            _powerRepository = powerRepository;
            _mapper = mapper;
        }

        [HttpGet("immaterialResourceBlueprints")]
        public async Task<ActionResult<List<ImmaterialResourceBlueprintDto>>> GetImmaterialResourceBlueprints()
        {
            var immaterialResourceBlueprints = await _immaterialResourceBlueprintRepository.GetAll();


            List<ImmaterialResourceBlueprintDto> immaterialResourceBlueprintDtos = _mapper.Map<List<ImmaterialResourceBlueprintDto>>(immaterialResourceBlueprints);


            return Ok(immaterialResourceBlueprintDtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<PowerCompactDto>>> GetPowers()
        {
            var powers = await _powerRepository.GetAll();


            List<PowerCompactDto> powerDtos = _mapper.Map<List<PowerCompactDto>>(powers);


            return Ok(powerDtos);
        }

        [HttpGet("{powerId}")]
        public async Task<ActionResult<PowerFormDto>> GetPower(int powerId)
        {
            var power = await _powerRepository.GetByIdWithEffectBlueprintsAndMaterialResources(powerId);


            var powerDto = _mapper.Map<PowerFormDto>(power);


            return Ok(powerDto);
        }

    }
}