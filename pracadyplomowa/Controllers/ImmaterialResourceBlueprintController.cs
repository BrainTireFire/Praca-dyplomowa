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
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class ImmaterialResourceBlueprintController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImmaterialResourceBlueprintController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ImmaterialResourceBlueprintDto>> GetAll()
        {
            var resources = await _unitOfWork.ImmaterialResourceBlueprintRepository.GetAll();
            List<ImmaterialResourceBlueprintDto> resourceDTOs = _mapper.Map<List<ImmaterialResourceBlueprintDto>>(resources);
            
            return Ok(resourceDTOs);
        }
    }
}