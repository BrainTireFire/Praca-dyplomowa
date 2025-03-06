using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SizeEffectBlueprint(string name) : ValueEffectBlueprint(name, 0, RollMoment.OnCast)
    {
        protected SizeEffectBlueprint() : this("EF"){}
        public SizeEffectType SizeEffectType{ get; set; } = new SizeEffectType();
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new SizeEffectInstance(this, roller, target);
        }
    }
}