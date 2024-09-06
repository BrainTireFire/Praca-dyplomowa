using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Language: ObjectWithOwner
    {
        public required string Name {get; set;}
        public ICollection<EffectInstance> R_EffectInstances {get; set;} = [];
        public ICollection<EffectBlueprint> R_EffectBlueprints {get; set;} = [];

    }
}