using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Repository.UnitOfWork;
using pracadyplomowa.Services.ItemFamily;

namespace pracadyplomowa.Services
{
    public class PowerService(IUnitOfWork unitOfWork) : IPowerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndEditAccess(int powerId, int userId, out ActionResult errorResult, out Power? power)
        {
            if (!_unitOfWork.PowerRepository.GetPowersForEditabilityAnalysis([powerId]).TryGetValue(powerId, out power))
            {
                errorResult = new NotFoundObjectResult("Power with specified Id was not found");
                return false;
            }
            if (!power.HasEditAccess(userId))
            {
                errorResult = new UnauthorizedObjectResult("Cannot edit Power with this Id");
                return false;
            }
            errorResult = new OkObjectResult("Power exists and is editable");
            return true;
        }
        public bool CheckExistenceAndEditAccess(int powerId, int userId, out ActionResult errorResult)
        {
            return CheckExistenceAndEditAccess(powerId, userId, out errorResult, out var power);
        }
    }
}