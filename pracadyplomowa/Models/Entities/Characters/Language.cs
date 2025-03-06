using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Language: ObjectWithOwner
    {
        public required string Name {get; set;}
        public virtual ICollection<LanguageEffectInstance> R_EffectInstances {get; set;} = [];
        public virtual ICollection<LanguageEffectBlueprint> R_EffectBlueprints {get; set;} = [];

    }
}