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
    public class ItemController: BaseApiController
    {
        private readonly IImmaterialResourceBlueprintRepository _immaterialResourceBlueprintRepository;
        private readonly IEffectBlueprintRepository _effectBlueprintRepository;
        private readonly IEffectInstanceRepository _effectInstanceRepository;
        private readonly IPowerRepository _powerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IEquipmentSlotRepository _equipmentSlotRepository;
        private readonly IMapper _mapper;

        public ItemController(IImmaterialResourceBlueprintRepository immaterialResourceBlueprintRepository, IEffectBlueprintRepository effectBlueprintRepository, IEffectInstanceRepository effectInstanceRepository, IPowerRepository powerRepository, IItemRepository itemRepository, IEquipmentSlotRepository equipmentSlotRepository, IMapper mapper)
        {
            _immaterialResourceBlueprintRepository = immaterialResourceBlueprintRepository;
            _powerRepository = powerRepository;
            _effectBlueprintRepository = effectBlueprintRepository;
            _effectInstanceRepository = effectInstanceRepository;
            _itemRepository = itemRepository;
            _equipmentSlotRepository = equipmentSlotRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ItemListElementDto>>> GetItems()
        {
            var items = await _itemRepository.GetAll();


            List<ItemListElementDto> itemDtos = _mapper.Map<List<ItemListElementDto>>(items);


            return Ok(itemDtos);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemFormDto>> GetItem(int itemId){
            var item = await _itemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound(itemId);
            }
            var itemDto = _mapper.Map<ItemFormDto>(item);
            return Ok(itemDto);
        }

        [HttpPost("meleeWeapon")]
        public async Task<ActionResult<int>> PostMeleeWeapon(MeleeWeaponFormDto meleeWeaponDto){
            var item = _mapper.Map<MeleeWeapon>(meleeWeaponDto);
            _itemRepository.Add(item);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpPatch("meleeWeapon")]
        public async Task<ActionResult<int>> UpdateMeleeWeapon(MeleeWeaponFormDto meleeWeaponDto){
            if(meleeWeaponDto.Id == null){
                return BadRequest("Id is required for update");
            }
            var itemLoaded = (MeleeWeapon)await _itemRepository.GetByIdWithSlotsPowersEffectsResources((int)meleeWeaponDto.Id);
            var item = _mapper.Map(meleeWeaponDto, itemLoaded);
            _itemRepository.Update(itemLoaded);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpPost("rangedWeapon")]
        public async Task<ActionResult<int>> PostRangedWeapon(RangedWeaponFormDto rangedWeaponDto){
            var item = _mapper.Map<RangedWeapon>(rangedWeaponDto);
            _itemRepository.Add(item);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpPatch("rangedWeapon")]
        public async Task<ActionResult<int>> UpdateRangedWeapon(RangedWeaponFormDto rangedWeaponDto){
            var item = _mapper.Map<RangedWeapon>(rangedWeaponDto);
            _itemRepository.Update(item);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpPost("apparel")]
        public async Task<ActionResult<int>> PostApparel(ApparelFormDto apparelDto){
            var item = _mapper.Map<Apparel>(apparelDto);
            _itemRepository.Add(item);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpPatch("apparel")]
        public async Task<ActionResult<int>> UpdateApparel(ApparelFormDto apparelDto){
            var item = _mapper.Map<Apparel>(apparelDto);
            _itemRepository.Update(item);
            await _itemRepository.SaveChanges();
            return Ok(item.Id);
        }

        [HttpGet("{itemId}/slots")]
        public async Task<ActionResult<List<SlotDto>>> GetItemSlots(int itemId){
            var item = await _itemRepository.GetByIdWithSlots(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _mapper.Map<List<SlotDto>>(item.R_ItemIsEquippableInSlots);
            return Ok(slots);
        }

        [HttpPatch("{itemId}/slots")]
        public async Task<ActionResult<List<SlotDto>>> SetItemSlots(int itemId, [FromBody] List<SlotDto> slotDtos){
            var item = await _itemRepository.GetByIdWithSlots(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _equipmentSlotRepository.GetAllWithIds(slotDtos.Select(x => x.Id).ToList());
            item.R_ItemIsEquippableInSlots.Clear();
            item.R_ItemIsEquippableInSlots.AddRange(await slots);
            await _itemRepository.SaveChanges();
            return Ok();
        }

        [HttpGet("{itemId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> GetItemPowers(int itemId){
            var item = await _itemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var powers = _mapper.Map<List<PowerCompactDto>>(item.R_EquipItemGrantsAccessToPower);
            return Ok(powers);
        }

        [HttpPatch("{itemId}/powers")]
        public async Task<ActionResult<List<SlotDto>>> SetItemPowers(int itemId, [FromBody] List<PowerCompactDto> powerDtos){
            var item = await _itemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            var slots = _powerRepository.GetAllByIds(powerDtos.Select(x => x.Id).ToList());
            item.R_EquipItemGrantsAccessToPower.Clear();
            item.R_EquipItemGrantsAccessToPower.AddRange(await slots);
            await _itemRepository.SaveChanges();
            return Ok();
        }

        [HttpGet("{itemId}/resources")]
        public async Task<ActionResult<List<SlotDto>>> GetItemResources(int itemId){
            var item = await _itemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
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
            var item = await _itemRepository.GetByIdWithSlotsPowersEffectsResources(itemId);
            if(item == null){
                return NotFound("Item with this Id does not exist");
            }
            // var resourceBlueprints = await _immaterialResourceBlueprintRepository.GetAllByIds(resourceDtos.Select(x => x.BlueprintId).ToList());
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
            await _itemRepository.SaveChanges();
            return Ok();
        }

        [HttpPost("{itemId}/effects")]
        public async Task<ActionResult> AddNewEffectBlueprint([FromBody] EffectBlueprintFormDto effectDto, [FromRoute] int itemId)
        {
            var effectBlueprint = _mapper.Map<EffectBlueprint>(effectDto);
            var effectInstance = _mapper.Map<EffectInstance>(effectBlueprint);
            effectInstance.R_GrantedByEquippingItemId = itemId;

            _effectInstanceRepository.Add(effectInstance);
            await _effectInstanceRepository.SaveChanges();
            return Ok();
        }

    }
}