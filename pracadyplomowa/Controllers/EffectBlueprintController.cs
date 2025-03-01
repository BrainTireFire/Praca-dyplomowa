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
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class EffectBlueprintController: BaseApiController
    {
        private const int MAX_CLASS_LEVEL = 20;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EffectBlueprintController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies([FromQuery] int? effectId)
        {
            var itemFamilies = await _unitOfWork.ItemFamilyRepository.GetOwnedAndDefaultAndCurrentForEffectBlueprint(effectId, User.GetUserId());


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

        [HttpGet("{effectId}")]
        public async Task<ActionResult<EffectBlueprintFormDto>> GetEffectBlueprint(int effectId)
        {
            var effectBlueprint = _unitOfWork.EffectBlueprintRepository.GetById(effectId);


            var effectBlueprintDtos = _mapper.Map<EffectBlueprintFormDto>(effectBlueprint);


            return Ok(effectBlueprintDtos);
        }



        [HttpPatch]
        public async Task<ActionResult> UpdateEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto)
        {
            var id = effectDto.Id;
            if(id != null){
                var effectBlueprintOriginal = _unitOfWork.EffectBlueprintRepository.GetById((int)id);
                if(effectBlueprintOriginal != null){
                    var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDto);

                    effectBlueprint.R_CreatedByEquippingId = effectBlueprintOriginal.R_CreatedByEquippingId;
                    effectBlueprint.R_CastedOnCharactersByAuraId = effectBlueprintOriginal.R_CastedOnCharactersByAuraId;
                    effectBlueprint.R_CastedOnTilesByAuraId = effectBlueprintOriginal.R_CastedOnTilesByAuraId;
                    effectBlueprint.R_PowerId = effectBlueprintOriginal.R_PowerId;

                    // _unitOfWork.EffectBlueprintRepository.DetachEntity(effectBlueprintOriginal);
                    _unitOfWork.EffectBlueprintRepository.Delete(effectBlueprintOriginal.Id);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.EffectBlueprintRepository.ClearTracker();

                    _unitOfWork.EffectBlueprintRepository.Add(effectBlueprint);
                    await _unitOfWork.SaveChangesAsync();

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

        [HttpDelete("{effectId}")]
        public async Task<ActionResult> DeleteEffectBlueprint([FromRoute] int effectId)
        {
            _unitOfWork.EffectBlueprintRepository.Delete(effectId);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Resource deleted");
        }

    }
}