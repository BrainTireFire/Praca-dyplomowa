using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers
{
    public abstract class EffectInstance : ObjectWithId
    {
        private EffectInstance() : this("EF"){}
        public EffectInstance(string name){
            Name = name;
        }
        public EffectInstance(EffectBlueprint blueprint, Character target) : this(blueprint.Name){
            Description = blueprint.Description;
            IsImplemented = blueprint.IsImplemented;
            Conditional = blueprint.Conditional;
            HasNoEffectInCombat = blueprint.HasNoEffectInCombat;
            R_TargetedCharacter = target;
            R_TargetedCharacterId = target.Id;
            target.R_AffectedBy.Add(this);
        }
        public EffectInstance(EffectInstance effectInstance){
            this.Name = effectInstance.Name;
            this.Description = effectInstance.Description;
            this.Conditional = effectInstance.Conditional;
            this.IsImplemented = effectInstance.IsImplemented;
            this.HasNoEffectInCombat = effectInstance.HasNoEffectInCombat;
        }

        public string Name { get; set; }
        public string Description { get; set; } = "";
        public bool Conditional { get; set; }
        [NotMapped]
        public bool ConditionalApproved { get; set; } = false;
        public bool IsImplemented { get; set; }
        public bool HasNoEffectInCombat { get; set; } = false;
        
        //Relationship
        public virtual EffectGroup? R_OwnedByGroup { get; set; }
        public virtual int? R_OwnedByGroupId { get; set; }
        public virtual ChoiceGroupUsage? R_GrantedThrough { get; set; } // means actual usage of a choice group
        public virtual int? R_GrantedThroughId { get; set;}
        public virtual Character? R_TargetedCharacter { get; set; }
        public virtual int? R_TargetedCharacterId { get; set; }
        public virtual Item? R_TargetedItem { get; set; }
        public virtual int? R_TargetedItemId { get; set; }
        public virtual Item? R_GrantedByEquippingItem { get; set; }
        public virtual int? R_GrantedByEquippingItemId {get; set;}

        [NotMapped]
        public string Source {
            get {
                if(R_OwnedByGroup != null) return R_OwnedByGroup.Name;
                else if(R_GrantedThrough != null) return R_GrantedThrough.R_ChoiceGroup.Name;
                else if(R_GrantedByEquippingItem != null) return R_GrantedByEquippingItem.Name;
                else return "Custom";
            }
        }

        public abstract EffectInstance Clone();

        public virtual void Resolve(){
            
        }
    }
}