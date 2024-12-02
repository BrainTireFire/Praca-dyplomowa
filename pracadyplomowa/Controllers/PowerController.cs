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
    public class PowerController: BaseApiController
    {
        private readonly IImmaterialResourceBlueprintRepository _immaterialResourceBlueprintRepository;
        private readonly IEffectBlueprintRepository _effectBlueprintRepository;
        private readonly IPowerRepository _powerRepository;
        private readonly IItemCostRequirementRepository _itemCostRequirementRepository;
        private readonly IMapper _mapper;

        public PowerController(IImmaterialResourceBlueprintRepository immaterialResourceBlueprintRepository, IEffectBlueprintRepository effectBlueprintRepository, IPowerRepository powerRepository, IItemCostRequirementRepository itemCostRequirementRepository, IMapper mapper)
        {
            _immaterialResourceBlueprintRepository = immaterialResourceBlueprintRepository;
            _powerRepository = powerRepository;
            _effectBlueprintRepository = effectBlueprintRepository;
            _itemCostRequirementRepository = itemCostRequirementRepository;
            _mapper = mapper;
        }

        [HttpGet("immaterialResourceBlueprints")]
        public async Task<ActionResult<List<ImmaterialResourceBlueprintDto>>> GetImmaterialResourceBlueprints()
        {
            var immaterialResourceBlueprints = await _immaterialResourceBlueprintRepository.GetAll();


            List<ImmaterialResourceBlueprintDto> immaterialResourceBlueprintDtos = _mapper.Map<List<ImmaterialResourceBlueprintDto>>(immaterialResourceBlueprints);


            return Ok(immaterialResourceBlueprintDtos);
        }

        [HttpGet]
        public async Task<ActionResult<List<PowerCompactDto>>> GetPowers()
        {
            var powers = await _powerRepository.GetAll();


            List<PowerCompactDto> powerDtos = _mapper.Map<List<PowerCompactDto>>(powers);


            return Ok(powerDtos);
        }

        [HttpGet("{powerId}")]
        public async Task<ActionResult<PowerFormDto>> GetPower(int powerId)
        {
            var power = await _powerRepository.GetByIdWithEffectBlueprintsAndMaterialResources(powerId);


            var powerDto = _mapper.Map<PowerFormDto>(power);


            return Ok(powerDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewPower(PowerFormDto powerDto)
        {
            var power = _mapper.Map<Power>(powerDto);
            power.R_OwnerId = User.GetUserId();

            _powerRepository.Add(power);
            await _powerRepository.SaveChanges();
            return Ok(_mapper.Map<PowerFormDto>(power));
        }

        
        [HttpPatch]
        public async Task<ActionResult> UpdatePower(PowerFormDto powerDto)
        {
            var power = _mapper.Map<Power>(powerDto);
            _powerRepository.Update(power);
            await _powerRepository.SaveChanges();
            return Ok(_mapper.Map<PowerFormDto>(power));
        }

        
        [HttpPost("{powerId}/effects")]
        public async Task<ActionResult> AddNewEffectBlueprint([FromBody] EffectBlueprintFormDtoWrapper effectDtoWrapper, [FromRoute] int powerId)
        {
            var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDtoWrapper.formData);
            effectBlueprint.R_PowerId = powerId;

            _effectBlueprintRepository.Add(effectBlueprint);
            await _effectBlueprintRepository.SaveChanges();
            return Ok(_mapper.Map<EffectBlueprintFormDto>(effectBlueprint));
        }

        
        [HttpPost("{powerId}/materialComponents")]
        public async Task<ActionResult> AddMaterialComponent([FromBody] ItemFamilyWithWorthDto materialComponentDto, [FromRoute] int powerId)
        {
            var materialComponent = new ItemCostRequirement
            {
                R_ItemFamilyId = materialComponentDto.Id,
                PowerId = powerId,
                GoldPieces = materialComponentDto.Worth.GoldPieces,
                SilverPieces = materialComponentDto.Worth.SilverPieces,
                CopperPieces = materialComponentDto.Worth.CopperPieces
            };

            _itemCostRequirementRepository.Add(materialComponent);
            await _itemCostRequirementRepository.SaveChanges();
            return Ok();
        }
        [HttpPatch("{powerId}/materialComponents/{componentId}")]
        public async Task<ActionResult> AddMaterialComponent([FromBody] ItemFamilyWithWorthDto materialComponentDto, [FromRoute] int powerId, [FromRoute] int componentId)
        {
            var materialComponent = new ItemCostRequirement
            {
                R_ItemFamilyId = materialComponentDto.Id,
                PowerId = powerId,
                GoldPieces = materialComponentDto.Worth.GoldPieces,
                SilverPieces = materialComponentDto.Worth.SilverPieces,
                CopperPieces = materialComponentDto.Worth.CopperPieces
            };

            _itemCostRequirementRepository.Update(materialComponent);
            await _itemCostRequirementRepository.SaveChanges();
            return Ok();
        }

    }
}