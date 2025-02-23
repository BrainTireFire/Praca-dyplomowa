using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pracadyplomowa.Services.ItemFamily
{
    public interface IItemFamilyService
    {
        bool CheckExistenceAndEditAccess(int itemFamilyId, int userId, out ActionResult errorResult);
    }
}