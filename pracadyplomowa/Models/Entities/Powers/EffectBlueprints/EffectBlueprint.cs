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
    public abstract class EffectBlueprint(string name) : ObjectWithId
    {
        public string Name { get; set; } = name;
        public string  Description { get; set; } = "";
        public int Level { get; set; } // use this effect if Level value matches value selected by 
        public int ResourceAmount { get; set;}
        public bool Saved { get; set; }
        public bool Conditional { get; set; } = false;
        public bool IsImplemented { get; set; } = true;
        public bool HasNoEffectInCombat { get; set; } = false;


        //Relationship
        // public virtual Item? R_CreatedByEquipping { get; set; } // not used
        public int? R_CreatedByEquippingId { get; set; }

        public virtual Power? R_Power { get; set; }
        public int? R_PowerId { get; set; }

        public virtual Aura? R_CastedOnCharactersByAura { get; set; } = null;
        public int? R_CastedOnCharactersByAuraId { get; set; }
        public virtual Aura? R_CastedOnTilesByAura { get; set; } = null;
        public int? R_CastedOnTilesByAuraId { get; set; }


        public virtual ICollection<ChoiceGroup> R_ChoiceGroups { get; set; } = [];
        //constructors
        protected EffectBlueprint() : this("EF"){}
        //methods
        public abstract EffectInstance Generate(Character? roller, Character target); //roller added to parameter list as it will be used by overriding methods
        //     return new EffectInstance(this, target);
        // }
    }
}