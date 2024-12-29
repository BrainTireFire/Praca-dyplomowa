using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository.UnitOfWork;

namespace pracadyplomowa.Services.Item
{
    public class ItemService(IUnitOfWork unitOfWork) : IItemService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public bool CheckExistenceAndEditAccess(int itemId, int userId, out ActionResult errorResult){
            if(!_unitOfWork.ItemRepository.GetItemsForEditabilityAnalysis([itemId]).TryGetValue(itemId, out var itemToAnalyze)){ 
                errorResult = new NotFoundObjectResult("Item with specified Id was not found");
                return false;
            }
            if(!itemToAnalyze.HasEditAccess(userId)){
                errorResult = new UnauthorizedObjectResult("Cannot edit Item with this Id");
                return false;
            }
            errorResult = new OkObjectResult("Item exists and is editable");
            return true;
        }
    }
}