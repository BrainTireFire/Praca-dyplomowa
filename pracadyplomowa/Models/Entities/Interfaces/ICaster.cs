using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Interfaces
{
    public interface ICaster
    {
        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets);
        public bool ApplyPowerEffects(Power power, Dictionary<Character, HitType> targetsToHitSuccessMap); // returns true if power was succesfully used
    }
}