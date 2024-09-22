using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SizeEffectBlueprint(string name) : EffectBlueprint(name)
    {
        private SizeEffectBlueprint() : this("EF"){}
        public SizeEffectType SizeEffectType{ get; set; } = new SizeEffectType();
        //methods
        public override EffectInstance Generate(Character roller, Character target){
            return new SizeEffectInstance(this, target);
        }
    }
}