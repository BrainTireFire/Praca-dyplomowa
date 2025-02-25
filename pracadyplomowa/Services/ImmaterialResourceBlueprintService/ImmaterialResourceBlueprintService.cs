using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Services
{
    public class ImmaterialResourceBlueprintService(IUnitOfWork unitOfWork): IImmaterialResourceBlueprintService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndEditAccess(int resourceId, int userId, out ActionResult errorResult){
            if(!_unitOfWork.ImmaterialResourceBlueprintRepository.GetItemFamiliesForEditabilityAnalysis([resourceId]).TryGetValue(resourceId, out var itemToAnalyze)){ 
                errorResult = new NotFoundObjectResult("Resource with specified Id was not found");
                return false;
            }
            if(!itemToAnalyze.HasEditAccess(userId)){
                errorResult = new UnauthorizedObjectResult("Cannot edit Resource with this Id");
                return false;
            }
            errorResult = new OkObjectResult("Resource family exists and is editable");
            return true;
        }
    }
}