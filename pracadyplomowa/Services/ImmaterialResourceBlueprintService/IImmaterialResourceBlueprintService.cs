using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pracadyplomowa.Services.ItemFamily
{
    public interface IImmaterialResourceBlueprintService
    {
        bool CheckExistenceAndEditAccess(int resourceId, int userId, out ActionResult errorResult);
    }
}