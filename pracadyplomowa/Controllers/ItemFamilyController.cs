using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class ItemFamilyController
    (
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IItemFamilyService itemFamilyService
    ) : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IItemFamilyService _itemFamilyService = itemFamilyService;
        
        [HttpGet]
        public async Task<ActionResult<List<ItemFamilyDto>>> GetItemFamilies()
        {
            var itemFamilies = await _unitOfWork.ItemFamilyRepository.GetOwnedAndDefault(User.GetUserId());


            List<ItemFamilyDto> itemFamiliesDto = [];
            foreach (var itemFamily in itemFamilies){
                itemFamiliesDto.Add(new ItemFamilyDto(){
                    Id = itemFamily.Id,
                    Name = itemFamily.Name,
                    ItemType = itemFamily.ItemType,
                    OwnerName = itemFamily.R_OwnerId == User.GetUserId() ? User.GetUsername() : "",
                    Editable = itemFamily.HasEditAccess(User.GetUserId())
                });
            }


            return Ok(itemFamiliesDto);
        }

        [HttpGet("{itemFamilyId}")]
        public async Task<ActionResult<ItemFamilyDto>> GetItemFamily(int itemFamilyId)
        {
            var itemFamily = _unitOfWork.ItemFamilyRepository.GetById(itemFamilyId);
            if(itemFamily == null){
                return NotFound();
            }

            ItemFamilyDto itemFamiliesDto = new ItemFamilyDto(){
                    Id = itemFamily.Id,
                    Name = itemFamily.Name,
                    ItemType = itemFamily.ItemType,
                    OwnerName = itemFamily.R_OwnerId == User.GetUserId() ? User.GetUsername() : "",
                    Editable = itemFamily.HasEditAccess(User.GetUserId())
                };


            return Ok(itemFamiliesDto);
        }

        [HttpDelete("{itemFamilyId}")]
        public async Task<ActionResult> DeleteItemFamily(int itemFamilyId){
            if(!_itemFamilyService.CheckExistenceAndEditAccess(itemFamilyId, User.GetUserId(), out var actionResult)){
                return actionResult;
            }
            _unitOfWork.ItemFamilyRepository.Delete(itemFamilyId);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateItemFamily([FromBody] ItemFamilyDtoInsert itemFamilyDto){
            ItemFamily newItemFamily = new ItemFamily(){
                Name = itemFamilyDto.Name,
                ItemType = itemFamilyDto.ItemType,
                R_OwnerId = User.GetUserId(),
            };
            _unitOfWork.ItemFamilyRepository.Add(newItemFamily);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateItemFamily([FromBody] ItemFamilyDtoUpdate itemFamilyDto){
            if(!_itemFamilyService.CheckExistenceAndEditAccess(itemFamilyDto.Id, User.GetUserId(), out var actionResult)){
                return actionResult;
            }
            var itemFamily = _unitOfWork.ItemFamilyRepository.GetById(itemFamilyDto.Id);
            itemFamily!.Name = itemFamilyDto.Name;
            itemFamily.ItemType = itemFamilyDto.ItemType;
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}