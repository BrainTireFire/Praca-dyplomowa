using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SizeEffectBlueprint(string name) : EffectBlueprint(name)
    {
        private SizeEffectBlueprint() : this("EF"){}
        public SizeEffectType SizeEffectType{ get; set; } = new SizeEffectType();
    }
}