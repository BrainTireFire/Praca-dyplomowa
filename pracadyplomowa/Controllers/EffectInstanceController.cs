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
    public class EffectInstanceController: BaseApiController
    {
        private const int MAX_CLASS_LEVEL = 20;
        private readonly IItemFamilyRepository _itemFamilyRepository;
        private readonly IEffectInstanceRepository _effectInstanceRepository;
        private readonly IMapper _mapper;

        public EffectInstanceController(IItemFamilyRepository itemFamilyRepository, IEffectInstanceRepository effectInstanceRepository, IMapper mapper)
        {
            _itemFamilyRepository = itemFamilyRepository;
            _effectInstanceRepository = effectInstanceRepository;
            _mapper = mapper;
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies()
        {
            var itemFamilies = await _itemFamilyRepository.GetAll();


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

        [HttpGet("{effectId}")]
        public async Task<ActionResult<EffectBlueprintFormDto>> GetEffectInstance(int effectId)
        {
            var effectInstance = _effectInstanceRepository.GetById(effectId);
            var effectBlueprint = _mapper.Map<EffectBlueprint>(effectInstance);

            var effectBlueprintDtos = _mapper.Map<EffectBlueprintFormDto>(effectBlueprint);


            return Ok(effectBlueprintDtos);
        }



        [HttpPatch]
        public async Task<ActionResult> UpdateEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto)
        {
            var id = effectDto.Id;
            if(id != null){
                var effectInstanceOriginal = _effectInstanceRepository.GetById((int)id);
                if(effectInstanceOriginal != null){
                    var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDto);
                    var effectInstance = _mapper.Map<EffectInstance>(effectBlueprint);

                    effectInstance.R_GrantedByEquippingItemId = effectInstanceOriginal.R_GrantedByEquippingItemId;
                    effectInstance.R_GrantedThroughId = effectInstanceOriginal.R_GrantedThroughId;
                    effectInstance.R_TargetedCharacterId = effectInstanceOriginal.R_TargetedCharacterId;
                    effectInstance.R_OwnedByGroupId = effectInstanceOriginal.R_OwnedByGroupId;
                    effectInstance.R_TargetedItemId = effectInstanceOriginal.R_TargetedItemId;

                    // _effectBlueprintRepository.DetachEntity(effectBlueprintOriginal);
                    _effectInstanceRepository.Delete(effectInstanceOriginal.Id);
                    await _effectInstanceRepository.SaveChanges();
                    _effectInstanceRepository.ClearTracker();

                    _effectInstanceRepository.Add(effectInstance);
                    await _effectInstanceRepository.SaveChanges();

                    return Ok();
                }
                else{
                    return NotFound("Id not found");
                }
            }
            else{
                return BadRequest("Missing Id");
            }
        }

        [HttpDelete("{effectId}")]
        public async Task<ActionResult> DeleteEffectInstance([FromRoute] int effectId)
        {
            _effectInstanceRepository.Delete(effectId);
            await _effectInstanceRepository.SaveChanges();
            return Ok("Resource deleted");
        }

    }
}