using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Repository.Item;

namespace pracadyplomowa.Services.Item
{
    public class ItemService(IItemRepository itemRepository) : IItemService
    {
        private readonly IItemRepository _itemRepository = itemRepository;

        public bool CheckExistenceAndEditAccess(int itemId, int userId, out ActionResult errorResult){
            if(!_itemRepository.GetItemsForEditabilityAnalysis([itemId]).TryGetValue(itemId, out var itemToAnalyze)){ 
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