using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Repository.Item;
using pracadyplomowa.Repository.Race;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Controllers
{
    [Authorize]
    public class ImmaterialResourceBlueprintController: BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImmaterialResourceBlueprintService _immaterialResourceBlueprintService;

        public ImmaterialResourceBlueprintController(IUnitOfWork unitOfWork, IMapper mapper, IImmaterialResourceBlueprintService immaterialResourceBlueprintService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _immaterialResourceBlueprintService = immaterialResourceBlueprintService;
        }

        [HttpGet]
        public async Task<ActionResult<ImmaterialResourceBlueprintWithOwnerDto>> GetAll()
        {
            var resources = await _unitOfWork.ImmaterialResourceBlueprintRepository.GetOwnedAndDefault(User.GetUserId());
            List<ImmaterialResourceBlueprintWithOwnerDto> resourceDtos = [];
            foreach (var resource in resources){
                resourceDtos.Add(new ImmaterialResourceBlueprintWithOwnerDto(){
                    Id = resource.Id,
                    Name = resource.Name,
                    OwnerName = resource.R_OwnerId == User.GetUserId() ? User.GetUsername() : "",
                    Editable = resource.HasEditAccess(User.GetUserId())
                });
            }
            
            return Ok(resourceDtos);
        }

        [HttpGet("{resourceId}")]
        public async Task<ActionResult<ImmaterialResourceBlueprintWithOwnerDto>> GetImmaterialResource(int resourceId)
        {
            var resource = _unitOfWork.ImmaterialResourceBlueprintRepository.GetById(resourceId);
            if(resource == null){
                return NotFound();
            }

            ImmaterialResourceBlueprintWithOwnerDto resourceDto = new ImmaterialResourceBlueprintWithOwnerDto(){
                    Id = resource.Id,
                    Name = resource.Name,
                    OwnerName = resource.R_OwnerId == User.GetUserId() ? User.GetUsername() : "",
                    Editable = resource.HasEditAccess(User.GetUserId()),
                    RefreshesOn = resource.RefreshesOn
                };


            return Ok(resourceDto);
        }

        [HttpDelete("{resourceId}")]
        public async Task<ActionResult> DeleteResource(int resourceId){
            if(!_immaterialResourceBlueprintService.CheckExistenceAndEditAccess(resourceId, User.GetUserId(), out var actionResult)){
                return actionResult;
            }
            _unitOfWork.ImmaterialResourceBlueprintRepository.Delete(resourceId);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateResource([FromBody] ImmaterialResourceBlueprintDto_ForInsert resourceDto){
            ImmaterialResourceBlueprint resource = new ImmaterialResourceBlueprint(){
                Name = resourceDto.Name,
                RefreshesOn = resourceDto.RefreshesOn,
                R_OwnerId = User.GetUserId(),
            };
            _unitOfWork.ImmaterialResourceBlueprintRepository.Add(resource);
            await _unitOfWork.SaveChangesAsync();
            return Ok(resource.Id);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateResource([FromBody] ImmaterialResourceBlueprintDto resourceDto){
            if(!_immaterialResourceBlueprintService.CheckExistenceAndEditAccess(resourceDto.Id, User.GetUserId(), out var actionResult)){
                return actionResult;
            }
            var resource = _unitOfWork.ImmaterialResourceBlueprintRepository.GetById(resourceDto.Id);
            resource!.Name = resourceDto.Name;
            resource!.RefreshesOn = resourceDto.RefreshesOn;
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}