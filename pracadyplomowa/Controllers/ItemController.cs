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
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.Item;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class ItemController
    (
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IItemService itemService
    ) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IItemService _itemService = itemService;

        [HttpGet]
        public async Task<ActionResult<List<ItemListElementDto>>> GetItems([FromQuery] ItemParams itemParams)
        {
            var items = await _unitOfWork.ItemRepository.GetOwnedItems(User.GetUserId(), itemParams);


            List<ItemListElementDto> itemDtos = _mapper.Map<List<ItemListElementDto>>(items);


            return Ok(itemDtos);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemFormDto>> GetItem(int itemId){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound(itemId);
            }
            _unitOfWork.ItemRepository.GetItemsForEditabilityAnalysis([itemId]);
            var itemDto = _mapper.Map<Item, ItemFormDto>(item, opt => opt.AfterMap((src, dest) => dest.Editable = src.HasEditAccess(User.GetUserId())));
            return Ok(itemDto);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteItem(int itemId){
            var item = _unitOfWork.ItemRepository.GetById(itemId);
            if(item == null){
                return NotFound(itemId);
            }
            _unitOfWork.ItemRepository.GetItemsForEditabilityAnalysis([itemId]);
            if(!item.HasEditAccess(User.GetUserId())){
                return BadRequest("You cannot delete this item");
            }
            _unitOfWork.ItemRepository.Delete(itemId);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("meleeWeapon")]
        public async Task<ActionResult<int>> PostMeleeWeapon(MeleeWeaponFormDto meleeWeaponDto){
            var item = _mapper.Map<MeleeWeapon>(meleeWeaponDto);
            item.R_OwnerId = User.GetUserId();
            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpPatch("meleeWeapon")]
        public async Task<ActionResult<int>> UpdateMeleeWeapon(MeleeWeaponFormDto meleeWeaponDto){
            if(meleeWeaponDto.Id == null){
                return BadRequest("Id is required for update");
            }

            var itemId = (int)meleeWeaponDto.Id;
            if(!_itemService.CheckExistenceAndEditAccess(itemId, User.GetUserId(), out var actionResult)){
                return actionResult;
            }

            var itemLoaded = (MeleeWeapon)await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources((int)meleeWeaponDto.Id);
            var item = _mapper.Map(meleeWeaponDto, itemLoaded);
            _unitOfWork.ItemRepository.Update(itemLoaded);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpPost("rangedWeapon")]
        public async Task<ActionResult<int>> PostRangedWeapon(RangedWeaponFormDto rangedWeaponDto){
            var item = _mapper.Map<RangedWeapon>(rangedWeaponDto);
            item.R_OwnerId = User.GetUserId();
            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpPatch("rangedWeapon")]
        public async Task<ActionResult<int>> UpdateRangedWeapon(RangedWeaponFormDto rangedWeaponDto){
            if(rangedWeaponDto.Id == null){
                return BadRequest("Id is required for update");
            }

            var itemId = (int)rangedWeaponDto.Id;
            if(!_itemService.CheckExistenceAndEditAccess(itemId, User.GetUserId(), out var actionResult)){
                return actionResult;
            }

            var itemLoaded = (RangedWeapon)await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources((int)rangedWeaponDto.Id);
            var item = _mapper.Map(rangedWeaponDto, itemLoaded);
            _unitOfWork.ItemRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpPost("apparel")]
        public async Task<ActionResult<int>> PostApparel(ApparelFormDto apparelDto){
            var item = _mapper.Map<Apparel>(apparelDto);
            item.R_OwnerId = User.GetUserId();
            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpPatch("apparel")]
        public async Task<ActionResult<int>> UpdateApparel(ApparelFormDto apparelDto){
            if(apparelDto.Id == null){
                return BadRequest("Id is required for update");
            }

            var itemId = (int)apparelDto.Id;
            if(!_itemService.CheckExistenceAndEditAccess(itemId, User.GetUserId(), out var actionResult)){
                return actionResult;
            }

            var itemLoaded = (Apparel)await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources((int)apparelDto.Id);
            var item = _mapper.Map(apparelDto, itemLoaded);
            _unitOfWork.ItemRepository.Update(item);
            await _unitOfWork.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpGet("{itemId}/slots")]
        public async Task<ActionResult<List<SlotDto>>> GetItemSlots(int itemId){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlots(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _mapper.Map<List<SlotDto>>(item.R_ItemIsEquippableInSlots);
            return Ok(slots);
        }

        [HttpPatch("{itemId}/slots")]
        public async Task<ActionResult<List<SlotDto>>> SetItemSlots(int itemId, [FromBody] List<SlotDto> slotDtos){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlots(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _unitOfWork.EquipmentSlotRepository.GetAllWithIds(slotDtos.Select(x => x.Id).ToList());
            item.R_ItemIsEquippableInSlots.Clear();
            item.R_ItemIsEquippableInSlots.AddRange(await slots);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{itemId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> GetItemPowers(int itemId){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var powers = _mapper.Map<List<PowerCompactDto>>(item.R_EquipItemGrantsAccessToPower);
            return Ok(powers);
        }

        [HttpPatch("{itemId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> SetItemPowers(int itemId, [FromBody] List<PowerCompactDto> powerDtos){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _unitOfWork.PowerRepository.GetAllByIds(powerDtos.Select(x => x.Id).ToList());
            item.R_EquipItemGrantsAccessToPower.Clear();
            item.R_EquipItemGrantsAccessToPower.AddRange(await slots);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{itemId}/resources")]
        public async Task<ActionResult<List<SlotDto>>> GetItemResources(int itemId){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var resources = item.R_ItemGrantsResources.GroupBy(r => new {r.R_BlueprintId, r.R_Blueprint.Name, r.Level}).Select(g => new ImmaterialResourceAmountDto(){
                BlueprintId = g.Key.R_BlueprintId,
                Name = g.Key.Name,
                Level = g.Key.Level,
                Count = g.Count()
            }).ToList();
            return Ok(resources);
        }

        [HttpPatch("{itemId}/resources")]
        public async Task<ActionResult<List<SlotDto>>> SetItemResources(int itemId, [FromBody] List<ImmaterialResourceAmountDto> resourceDtos){
            var item = await _unitOfWork.ItemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            // var resourceBlueprints = await _unitOfWork.ImmaterialResourceBlueprintRepository.GetAllByIds(resourceDtos.Select(x => x.BlueprintId).ToList());
            item.R_ItemGrantsResources.Clear();
            var resourceInstances = resourceDtos
                .SelectMany(dto => 
                    Enumerable.Range(0, dto.Count).Select(_ => new ImmaterialResourceInstance 
                    { 
                        R_BlueprintId = dto.BlueprintId, 
                        Level = dto.Level 
                    })
                ).ToList();
            item.R_ItemGrantsResources.AddRange(resourceInstances);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{itemId}/effectsOnWearer")]
        public async Task<ActionResult> AddNewEffectInstanceOnWearer([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int itemId)
        {
            var effectInstance = _mapper.Map<EffectInstance>(effectDto);
            effectInstance.R_GrantedByEquippingItemId = itemId;

            _unitOfWork.EffectInstanceRepository.Add(effectInstance);
            await _unitOfWork.SaveChangesAsync();
            return Ok(effectInstance.Id);
        }

        [HttpPost("{itemId}/effectsOnItem")]
        public async Task<ActionResult> AddNewEffectInstanceOnItem([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int itemId)
        {
            var effectInstance = _mapper.Map<EffectInstance>(effectDto);
            effectInstance.R_TargetedItemId = itemId;

            _unitOfWork.EffectInstanceRepository.Add(effectInstance);
            await _unitOfWork.SaveChangesAsync();
            return Ok(effectInstance.Id);
        }

        [HttpGet("itemFamilies")]
        public async Task<ActionResult> GetItemFamilies([FromQuery] int? itemId, [FromQuery] List<ItemType> itemType){
            var itemFamilies = await _unitOfWork.ItemFamilyRepository.GetOwnedAndDefaultAndCurrent(itemId, User.GetUserId());


            List<ItemFamilyDto> itemFamiliesDto = _mapper.Map<List<ItemFamilyDto>>(itemFamilies.Where(x => itemType.Contains(x.ItemType)));


            return Ok(itemFamiliesDto);
        }

    }
}