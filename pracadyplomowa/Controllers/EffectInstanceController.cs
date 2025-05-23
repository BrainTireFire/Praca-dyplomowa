using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Errors;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class EffectInstanceController: BaseApiController
    {
        private const int MAX_CLASS_LEVEL = 20;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EffectInstanceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies([FromQuery] int? effectId)
        {
            var itemFamilies = await _unitOfWork.ItemFamilyRepository.GetOwnedAndDefaultAndCurrentForEffectInstance(effectId, User.GetUserId());


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }

        [HttpGet("languages")]
        public async Task<ActionResult<List<LanguageDto>>> GetLanguages([FromQuery] int? effectId)
        {
            var languages = await _unitOfWork.LanguageRepository.GetAll();


            List<LanguageDto> languagesDto = _mapper.Map<List<LanguageDto>>(languages);


            return Ok(languagesDto);
        }

        [HttpGet("{effectId}")]
        public async Task<ActionResult<EffectBlueprintFormDto>> GetEffectInstance(int effectId)
        {
            var effectInstance = await _unitOfWork.EffectInstanceRepository.GetByIdWithGroup(effectId);

            var effectBlueprintDtos = _mapper.Map<EffectBlueprintFormDto>(effectInstance);


            return Ok(effectBlueprintDtos);
        }



        [HttpPatch]
        public async Task<ActionResult> UpdateEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto)
        {
            var id = effectDto.Id;
            if(id != null){
                var effectInstanceOriginal = _unitOfWork.EffectInstanceRepository.GetById((int)id);
                if(effectInstanceOriginal != null){
                    _unitOfWork.EffectInstanceRepository.Delete(effectInstanceOriginal.Id);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.EffectInstanceRepository.ClearTracker();

                    var effectInstance = _mapper.Map<EffectInstance>(effectDto);

                    effectInstance.R_GrantedByEquippingItemId = effectInstanceOriginal.R_GrantedByEquippingItemId;
                    effectInstance.R_GrantedThroughId = effectInstanceOriginal.R_GrantedThroughId;
                    effectInstance.R_TargetedCharacterId = effectInstanceOriginal.R_TargetedCharacterId;
                    effectInstance.R_OwnedByGroupId = effectInstanceOriginal.R_OwnedByGroupId;
                    if(effectInstance.R_OwnedByGroupId != null){
                        effectInstance.R_OwnedByGroup = _unitOfWork.EffectGroupRepository.GetById((int)effectInstance.R_OwnedByGroupId);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        effectInstance.R_OwnedByGroup.DurationLeft = effectDto.DurationLeft;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    effectInstance.R_TargetedItemId = effectInstanceOriginal.R_TargetedItemId;

                    // _effectBlueprintRepository.DetachEntity(effectBlueprintOriginal);

                    _unitOfWork.EffectInstanceRepository.Add(effectInstance);
                    await _unitOfWork.SaveChangesAsync();

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
            var effect = _unitOfWork.EffectInstanceRepository.GetById(effectId);
            if(effect == null){
                return NotFound();
            }
            effect.DeleteOnSave = true;
            await _unitOfWork.SaveChangesAsync();
            return Ok("Resource deleted");
        }

        [HttpPatch("{effectId}/unlink")]
        public async Task<ActionResult> UnlinkEffectInstance([FromRoute] int effectId)
        {
            var effect = await _unitOfWork.EffectInstanceRepository.GetByIdWithTargets(effectId);
            if(effect == null){
                return NotFound(new ApiResponse(404, effectId.ToString()));
            }
            effect.Unlink();
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ApiResponse(200, effectId.ToString()));
        }

    }
}