using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Services.ItemFamily
{
    public interface IPowerService
    {
        public bool CheckExistenceAndEditAccess(int powerId, int userId, out ActionResult errorResult, out Power? power);
        public bool CheckExistenceAndEditAccess(int powerId, int userId, out ActionResult errorResult);
    }
}