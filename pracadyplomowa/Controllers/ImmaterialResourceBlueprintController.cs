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
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Race;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class ImmaterialResourceBlueprintController: BaseApiController
    {
        private readonly IImmaterialResourceBlueprintRepository _resourceRepository;
        private readonly IMapper _mapper;

        public ImmaterialResourceBlueprintController(IImmaterialResourceBlueprintRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ImmaterialResourceBlueprintDto>> GetAll()
        {
            var resources = await _resourceRepository.GetAll();
            List<ImmaterialResourceBlueprintDto> resourceDTOs = _mapper.Map<List<ImmaterialResourceBlueprintDto>>(resources);
            
            return Ok(resourceDTOs);
        }
    }
}