using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Aura : ObjectWithId
    {
        //Ids and keys
        public int CharacterAuraId { get; set; }
        
        public int Size { get; set; }
        
        // Relationships
        public virtual Character R_CharacterAura { get; set; } = null!;

        public virtual ICollection<EffectBlueprint> R_EffectsOnCharactersInRange { get; set; } = [];
        public virtual ICollection<EffectBlueprint> R_EffectsOnTilesInRange { get; set; } = []; //only for movement cost modifiers!
        public virtual ICollection<EffectGroup>  R_OwnedEffectGroups { get; set; } = [];
        public virtual EffectGroup R_GeneratedBy { get; set; } = null!;
        public virtual int? GeneratedBy_Id { get; set; }
    }
}