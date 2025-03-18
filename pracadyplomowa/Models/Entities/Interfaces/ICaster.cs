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
        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets, List<string> messages);
        public Outcome ApplyPowerEffects(Power power, Dictionary<Character, HitType> targetsToHitSuccessMap, int? immaterialResourceLevel, int? powerLevel, out List<EffectInstance> generatedEffects, List<string> messages); // returns true if power was succesfully used
    }

    public enum Outcome {
        Success,
        ImmaterialResourceUnavailable,
        InsufficientMaterialComponents,
        ResourceLevelLowerThanPowerLevelSelected
    }
}