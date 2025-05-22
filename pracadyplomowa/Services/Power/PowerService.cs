using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Services
{
    public class PowerService(IUnitOfWork unitOfWork): IPowerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndEditAccess(int powerId, int userId, out ActionResult errorResult){
            if(!_unitOfWork.PowerRepository.GetPowersForEditabilityAnalysis([powerId]).TryGetValue(powerId, out var itemToAnalyze)){ 
                errorResult = new NotFoundObjectResult("Power with specified Id was not found");
                return false;
            }
            if(!itemToAnalyze.HasEditAccess(userId)){
                errorResult = new UnauthorizedObjectResult("Cannot edit Power with this Id");
                return false;
            }
            errorResult = new OkObjectResult("Power exists and is editable");
            return true;
        }
    }
}