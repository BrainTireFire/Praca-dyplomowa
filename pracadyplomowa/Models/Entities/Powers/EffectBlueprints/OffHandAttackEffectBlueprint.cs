using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class OffHandAttackEffectBlueprint : EffectBlueprint
    {
        private OffHandAttackEffectBlueprint(): this("EF"){}
        
        // meant to represent fact that for this character off hand attacks should have ability modifier added to damage. This is a "one-trick pony" to hardcode this specific functionality
        public OffHandAttackEffectBlueprint(string name) : base(name){
            Description = @"When you engage in two-weapon fighting, you can add 
                                your ability modifier to the damage of the second attack.";
        }
    }
}