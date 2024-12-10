using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Item;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class EquipmentSlotController: BaseApiController
    {
        private readonly IEquipmentSlotRepository _equipmentSlotRepository;
        private readonly IMapper _mapper;

        public EquipmentSlotController(IEquipmentSlotRepository equipmentSlotRepository, IMapper mapper)
        {
            _equipmentSlotRepository = equipmentSlotRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ItemListElementDto>>> GetSlots()
        {
            var slots = await _equipmentSlotRepository.GetAll();


            List<SlotDto> slotDtos = _mapper.Map<List<SlotDto>>(slots);


            return Ok(slotDtos);
        }

    }
}