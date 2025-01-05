using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class EquipmentSlotController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EquipmentSlotController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ItemListElementDto>>> GetSlots()
        {
            var slots = await _unitOfWork.EquipmentSlotRepository.GetAll();
            List<SlotDto> slotDtos = _mapper.Map<List<SlotDto>>(slots);

            return Ok(slotDtos);
        }

    }
}