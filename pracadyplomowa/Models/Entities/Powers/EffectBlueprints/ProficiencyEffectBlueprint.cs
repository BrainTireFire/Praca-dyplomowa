using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ProficiencyEffectBlueprint(ItemFamily itemFamily) : EffectBlueprint(itemFamily.Name)
    {
        private ProficiencyEffectBlueprint() : this(new ItemFamily(){Name = "EF"}){}
        //public ProficiencyEffectType ProficiencyEffectType{ get; set;} = new ProficiencyEffectType();


        public virtual ItemFamily R_GrantsProficiencyInItemFamily { get; set; } = itemFamily;
        public int R_GrantsProficiencyInItemFamilyId { get; set; } = itemFamily.Id;
    }
}