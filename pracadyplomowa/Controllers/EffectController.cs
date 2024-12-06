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



        [HttpPatch("blueprint")]
        public async Task<ActionResult> UpdateEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto)
        {
            var id = effectDto.Id;
            if(id != null){
                var effectBlueprintOriginal = _effectBlueprintRepository.GetById((int)id);
                if(effectBlueprintOriginal != null){
                    var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDto);

                    effectBlueprint.R_CreatedByEquippingId = effectBlueprintOriginal.R_CreatedByEquippingId;
                    effectBlueprint.R_CastedOnCharactersByAuraId = effectBlueprintOriginal.R_CastedOnCharactersByAuraId;
                    effectBlueprint.R_CastedOnTilesByAuraId = effectBlueprintOriginal.R_CastedOnTilesByAuraId;
                    effectBlueprint.R_PowerId = effectBlueprintOriginal.R_PowerId;

                    // _effectBlueprintRepository.DetachEntity(effectBlueprintOriginal);
                    _effectBlueprintRepository.Delete(effectBlueprintOriginal.Id);
                    await _effectBlueprintRepository.SaveChanges();
                    _effectBlueprintRepository.ClearTracker();

                    _effectBlueprintRepository.Add(effectBlueprint);
                    await _effectBlueprintRepository.SaveChanges();

                    return Ok(_mapper.Map<EffectBlueprintFormDto>(effectBlueprint));
                }
                else{
                    return NotFound("Id not found");
                }
            }
            else{
                return BadRequest("Missing Id");
            }
        }

        [HttpDelete("blueprint/{effectId}")]
        public async Task<ActionResult> DeleteEffectBlueprint([FromRoute] int effectId)
        {
            _effectBlueprintRepository.Delete(effectId);
            await _effectBlueprintRepository.SaveChanges();
            return Ok("Resource deleted");
        }

    }
}