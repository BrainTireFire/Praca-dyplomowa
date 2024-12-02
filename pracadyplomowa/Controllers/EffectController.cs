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
    public class EffectController: BaseApiController
    {
        private const int MAX_CLASS_LEVEL = 20;
        private readonly IItemFamilyRepository _itemFamilyRepository;
        private readonly IEffectBlueprintRepository _effectBlueprintRepository;
        private readonly IMapper _mapper;

        public EffectController(IItemFamilyRepository itemFamilyRepository, IEffectBlueprintRepository effectBlueprintRepository, IMapper mapper)
        {
            _itemFamilyRepository = itemFamilyRepository;
            _effectBlueprintRepository = effectBlueprintRepository;
            _mapper = mapper;
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies()
        {
            var itemFamilies = await _itemFamilyRepository.GetAll();


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

        [HttpGet("blueprint/{effectId}")]
        public async Task<ActionResult<EffectBlueprintFormDto>> GetEffectBlueprint(int effectId)
        {
            var effectBlueprint = _effectBlueprintRepository.GetById(effectId);


            var effectBlueprintDtos = _mapper.Map<EffectBlueprintFormDto>(effectBlueprint);


            return Ok(effectBlueprintDtos);
        }



        // [HttpPatch]
        // public async Task<ActionResult> UpdatePower(PowerFormDto powerDto)
        // {
        //     var power = _mapper.Map<Power>(powerDto);
        //     _powerRepository.Update(power);
        //     await _powerRepository.SaveChanges();
        //     return Ok(_mapper.Map<PowerFormDto>(power));
        // }

    }
}