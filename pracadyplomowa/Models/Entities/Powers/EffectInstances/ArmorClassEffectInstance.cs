using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers.EffectInstances
{
    public class ArmorClassEffectInstance : ValueEffectInstance
    {
        private ArmorClassEffectInstance() : base("EF", 0){}
        public ArmorClassEffectInstance(string name) : base(name, 0){}
        public ArmorClassEffectInstance(ArmorClassEffectBlueprint actionEffectBlueprint, Character roller) : base(actionEffectBlueprint, roller){
            
        }
    }
}