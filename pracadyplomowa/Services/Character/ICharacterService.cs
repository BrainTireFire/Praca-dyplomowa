using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Services
{
    public interface ICharacterService
    {
        public bool CheckExistenceAndReadEditAccess(int characterId, int userId, List<Character.AccessLevels> requiredAccessLevels, out ActionResult errorResult, out List<Character.AccessLevels> grantedAccessLevels);
    }
}