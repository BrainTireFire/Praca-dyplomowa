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
        private readonly IPowerRepository _powerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IEquipmentSlotRepository _equipmentSlotRepository;
        private readonly IMapper _mapper;

        public ItemController(IImmaterialResourceBlueprintRepository immaterialResourceBlueprintRepository, IEffectBlueprintRepository effectBlueprintRepository, IPowerRepository powerRepository, IItemRepository itemRepository, IEquipmentSlotRepository equipmentSlotRepository, IMapper mapper)
        {
            _immaterialResourceBlueprintRepository = immaterialResourceBlueprintRepository;
            _powerRepository = powerRepository;
            _effectBlueprintRepository = effectBlueprintRepository;
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

    }
}