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
    public class EffectInstance(string name) : ObjectWithId
    {
        private EffectInstance() : this("EF"){}
        public EffectInstance(EffectBlueprint blueprint, Character target) : this(blueprint.Name){
            Description = blueprint.Description;
            IsImplemented = blueprint.IsImplemented;
            Conditional = blueprint.Conditional;
            HasNoEffectInCombat = blueprint.HasNoEffectInCombat;
            R_TargetedCharacter = target;
            R_TargetedCharacterId = target.Id;
            target.R_AffectedBy.Add(this);
        }

        public string Name { get; set; } = name;
        public string Description { get; set; } = "";
        public bool Conditional { get; set; }
        public bool IsImplemented { get; set; }
        public bool HasNoEffectInCombat { get; set; } = false;
        
        //Relationship
        public virtual EffectGroup R_OwnedByGroup { get; set; } = null!;
        public virtual int OwnedByGroupId { get; set; }
        public virtual ChoiceGroupUsage? R_GrantedThrough { get; set; } // means actual usage of a choice group
        public virtual int? R_GrantedThroughId { get; set;}
        public virtual Character? R_TargetedCharacter { get; set; }
        public virtual int? R_TargetedCharacterId { get; set; }
    }
}