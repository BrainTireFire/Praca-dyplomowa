using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ProficiencyEffectBlueprint : EffectBlueprint
    {
        public ProficiencyEffectBlueprint(ItemFamily itemFamily) : base(itemFamily.Name){
            //ProficiencyEffectType.ProficiencyEffect = proficiencyTargetType;
            R_GrantsProficiencyInItemFamily = itemFamily;
            R_GrantsProficiencyInItemFamilyId = itemFamily.Id;
        }

        //public ProficiencyEffectType ProficiencyEffectType{ get; set;} = new ProficiencyEffectType();

        
        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public int? R_GrantsProficiencyInItemFamilyId { get; set; }
    }
}