using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Repository.Item;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class PowerController: BaseApiController
    {
        private readonly IImmaterialResourceBlueprintRepository _immaterialResourceBlueprintRepository;
        private readonly IMapper _mapper;

        public PowerController(IImmaterialResourceBlueprintRepository immaterialResourceBlueprintRepository, IMapper mapper)
        {
            _immaterialResourceBlueprintRepository = immaterialResourceBlueprintRepository;
            _mapper = mapper;
        }

        [HttpGet("immaterialResourceBlueprints")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies()
        {
            var immaterialResourceBlueprints = await _immaterialResourceBlueprintRepository.GetAll();


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

    }
}