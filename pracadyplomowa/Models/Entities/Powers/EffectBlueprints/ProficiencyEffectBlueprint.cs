using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class ProficiencyEffectBlueprint : EffectBlueprint
    {
        private ProficiencyEffectBlueprint() : this(new ItemFamily(){Name = "EF", ItemType = ItemType.Item}){}
        public ProficiencyEffectBlueprint(ItemFamily itemFamily) : this(itemFamily.ItemType){
            Name = itemFamily.Name + " proficiency";
            R_GrantsProficiencyInItemFamily = itemFamily;
            R_GrantsProficiencyInItemFamilyId = itemFamily.Id;
        }
#pragma warning disable CS8604 // Possible null reference argument.
        public ProficiencyEffectBlueprint(ItemType itemType) : base(Enum.GetName(itemType) + " proficiency"){
#pragma warning restore CS8604 // Possible null reference argument.
            ProficiencyEffectType.ProficiencyEffect = ProficiencyEffect.ItemType;
            ProficiencyEffectType.ItemType = itemType;
        }

        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public int? R_GrantsProficiencyInItemFamilyId { get; set; }
        public ProficiencyEffectType ProficiencyEffectType { get; set; } = new ProficiencyEffectType();
        //methods
        public override EffectInstance Generate(Character roller, Character target){
            return new ProficiencyEffectInstance(this, target);
        }
    }
}