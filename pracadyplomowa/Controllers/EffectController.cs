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
    public class EffectController: BaseApiController
    {
        private const int MAX_CLASS_LEVEL = 20;
        private readonly IItemFamilyRepository _itemFamilyRepository;
        private readonly IMapper _mapper;

        public EffectController(IItemFamilyRepository itemFamilyRepository, IMapper mapper)
        {
            _itemFamilyRepository = itemFamilyRepository;
            _mapper = mapper;
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies()
        {
            var itemFamilies = await _itemFamilyRepository.GetAll();


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

    }
}