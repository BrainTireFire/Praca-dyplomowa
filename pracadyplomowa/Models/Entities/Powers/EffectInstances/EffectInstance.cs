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
    public class EffectInstance : ObjectWithId
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string SourceName { get; set; } = null!;
        public EffectType EffectType { get; set; }
        
        //Relationship
        public virtual EffectGroup R_OwnedByGroup { get; set; } = null!;
        public virtual int OwnedByGroupId { get; set; }
        // public virtual ItemFamily? R_GrantsProficiencyInItemFamily { get; set; }
        // public virtual int? R_GrantsProficiencyInItemFamilyId { get; set; }
        
    }
}