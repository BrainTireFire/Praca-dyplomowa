using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pracadyplomowa.Services.Item
{
    public interface IItemService
    {
        public bool CheckExistenceAndEditAccess(int itemId, int userId, out ActionResult errorResult);
    }
}