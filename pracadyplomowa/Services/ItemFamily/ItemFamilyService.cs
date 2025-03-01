using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Services
{
    public class ItemFamilyService(IUnitOfWork unitOfWork): IItemFamilyService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndEditAccess(int itemFamilyId, int userId, out ActionResult errorResult){
            if(!_unitOfWork.ItemFamilyRepository.GetItemFamiliesForEditabilityAnalysis([itemFamilyId]).TryGetValue(itemFamilyId, out var itemToAnalyze)){ 
                errorResult = new NotFoundObjectResult("Item with specified Id was not found");
                return false;
            }
            if(!itemToAnalyze.HasEditAccess(userId)){
                errorResult = new UnauthorizedObjectResult("Cannot edit Item with this Id");
                return false;
            }
            errorResult = new OkObjectResult("Item family exists and is editable");
            return true;
        }
    }
}