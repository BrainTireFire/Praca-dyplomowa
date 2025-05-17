using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class PowerController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PowerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("immaterialResourceBlueprints")]
        public async Task<ActionResult<List<ImmaterialResourceBlueprintDto>>> GetImmaterialResourceBlueprints([FromQuery] int? powerId)
        {
            var immaterialResourceBlueprints = await _unitOfWork.ImmaterialResourceBlueprintRepository.GetOwnedAndDefaultAndCurrent(powerId, User.GetUserId());


            List<ImmaterialResourceBlueprintDto> immaterialResourceBlueprintDtos = _mapper.Map<List<ImmaterialResourceBlueprintDto>>(immaterialResourceBlueprints);


            return Ok(immaterialResourceBlueprintDtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<PowerCompactDto>>> GetPowers([FromQuery] PowerParams powerParams)
        {
            var powers = await _unitOfWork.PowerRepository.GetAllPowersWithParams(powerParams);
            List<PowerCompactDto> powerDtos = _mapper.Map<List<PowerCompactDto>>(powers);
            Response.AddPaginationHeader(powers);
            return Ok(powerDtos);
        }

        [HttpGet("{powerId}")]
        public async Task<ActionResult<PowerFormDto>> GetPower(int powerId)
        {
            var power = await _unitOfWork.PowerRepository.GetByIdWithEffectBlueprintsAndMaterialResources(powerId);


            var powerDto = _mapper.Map<PowerFormDto>(power);


            return Ok(powerDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewPower(PowerFormDto powerDto)
        {
            var power = _mapper.Map<Power>(powerDto);
            power.R_OwnerId = User.GetUserId();

            _unitOfWork.PowerRepository.Add(power);
            try{
                await _unitOfWork.SaveChangesAsync();
            }
            catch(ValidationException valEx){
                return BadRequest(valEx.Message);
            }
            return Ok(power.Id);
        }

        
        [HttpPatch]
        public async Task<ActionResult> UpdatePower(PowerFormDto powerDto)
        {
            var power = _mapper.Map<Power>(powerDto);
            power.Range = power.IsRanged ? power.Range : 5;
            _unitOfWork.PowerRepository.Update(power);
            try{
                await _unitOfWork.SaveChangesAsync();
            }
            catch(ValidationException valEx){
                return BadRequest(valEx.Message);
            }
            return Ok(_mapper.Map<PowerFormDto>(power));
        }
        [HttpDelete("{powerId}")]
        public async Task<ActionResult> DeletePower(int powerId)
        {
            _unitOfWork.PowerRepository.Delete(powerId);
            try{
                await _unitOfWork.SaveChangesAsync();
            }
            catch(ValidationException valEx){
                return BadRequest(valEx.Message);
            }
            return Ok();
        }

        
        [HttpPost("{powerId}/effects")]
        public async Task<ActionResult> AddNewEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int powerId)
        {
            var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDto);
            effectBlueprint.R_PowerId = powerId;

            _unitOfWork.EffectBlueprintRepository.Add(effectBlueprint);
            await _unitOfWork.SaveChangesAsync();
            return Ok(effectBlueprint.Id);
        }

        
        [HttpPost("{powerId}/materialComponents")]
        public async Task<ActionResult> AddMaterialComponent([FromBody] ItemCostRequirementDto materialComponentDto, [FromRoute] int powerId)
        {
            var materialComponent = new ItemCostRequirement
            {
                R_ItemFamilyId = materialComponentDto.ItemFamilyId,
                PowerId = powerId,
                Worth = new CoinSack(){
                    GoldPieces = materialComponentDto.Worth.GoldPieces,
                    SilverPieces = materialComponentDto.Worth.SilverPieces,
                    CopperPieces = materialComponentDto.Worth.CopperPieces
                }
            };

            _unitOfWork.ItemCostRequirementRepository.Add(materialComponent);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("{powerId}/materialComponents")]
        public async Task<ActionResult> UpdateMaterialComponent([FromBody] ItemCostRequirementDto materialComponentDto, [FromRoute] int powerId)
        {
            var materialComponent = new ItemCostRequirement
            {
                Id = materialComponentDto.Id,
                R_ItemFamilyId = materialComponentDto.ItemFamilyId,
                PowerId = powerId,
                Worth = new CoinSack(){
                    GoldPieces = materialComponentDto.Worth.GoldPieces,
                    SilverPieces = materialComponentDto.Worth.SilverPieces,
                    CopperPieces = materialComponentDto.Worth.CopperPieces
                }
            };

            _unitOfWork.ItemCostRequirementRepository.Update(materialComponent);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("materialComponent/{componentId}")]
        public async Task<ActionResult> DeleteEffectBlueprint([FromRoute] int componentId)
        {
            _unitOfWork.ItemCostRequirementRepository.Delete(componentId);
            await _unitOfWork.SaveChangesAsync();
            return Ok("Resource deleted");
        }


        [HttpGet("itemFamilies")]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies([FromQuery] int? powerId)
        {
            var itemFamilies = await _unitOfWork.ItemFamilyRepository.GetOwnedAndDefault(User.GetUserId());


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies);


            return Ok(itemFamiliesDto);
        }
    }
}