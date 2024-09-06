using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class LanguageEffectBlueprint : EffectBlueprint
    {
        public LanguageEffectType LanguageEffectType{ get; set;} = new LanguageEffectType();

        
        public virtual required Language R_Language { get; set; }
        public int R_LanguageId { get; set; }
    }
}