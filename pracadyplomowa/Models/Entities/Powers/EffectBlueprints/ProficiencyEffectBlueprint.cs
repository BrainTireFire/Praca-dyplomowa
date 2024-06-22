using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Items;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ProficiencyEffectBlueprint : EffectBlueprint
    {
        public ProficiencyEffectType ProficiencyEffectType{ get; set;} = null!;

        
        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public int? R_GrantsProficiencyInItemFamilyId { get; set; }
    }
}