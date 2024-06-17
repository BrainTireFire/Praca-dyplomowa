using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class EffectBlueprint : ObjectWithId
    {

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ResourceLevel { get; set; }
        public bool Saved { get; set; }
        public EffectType EffectType { get; set; }


        //Relationship
        public virtual Item? R_CreatedByEquipping { get; set; }
        public int R_CreatedByEquippingId { get; set; }

        public virtual Power? R_Power { get; set; }
        public int? R_PowerId { get; set; }

        public virtual Aura? R_CastedOnCharactersByAura { get; set; } = null;
        public int? R_CastedOnCharactersByAuraId { get; set; }
        public virtual Aura? R_CastedOnTilesByAura { get; set; } = null;
        public int? R_CastedOnTilesByAuraId { get; set; }

        public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        public int? GrantsProficiencyInItemFamilyId { get; set; }

        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set; } = [];
    }
}