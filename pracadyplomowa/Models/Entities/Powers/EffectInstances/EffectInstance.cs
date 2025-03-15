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
        protected EffectInstance() : this("EF"){}
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
        [NotMapped]
        public string TargetName {
            get {
                if(R_TargetedCharacter != null) return R_TargetedCharacter.Name;
                else if(R_TargetedItem != null) return R_TargetedItem.Name;
                else return "No target";
            }
        }

        [NotMapped]
        public bool DeleteOnSave {get; set;}

        public abstract EffectInstance Clone();

        public virtual void Resolve(List<string> messages){

        }

        protected void ResolutionMessage(List<string> messages){
            if(this.R_TargetedCharacter != null) {
                messages.Add($"Resolving {this.Name} on {this.R_TargetedCharacter.Name}");
            }
            else if(this.R_TargetedItem != null){
                messages.Add($"Resolving {this.Name} on {this.R_TargetedItem.Name}");
            }
        }

        public virtual void Unlink(){ //unlink only from targeted objects, not sources!
            R_TargetedCharacter?.R_AffectedBy.Remove(this);
            R_TargetedItem?.R_AffectedBy.Remove(this);
            this.R_TargetedCharacter = null;
            this.R_TargetedCharacterId = null;
            this.R_TargetedItem = null;
            this.R_TargetedItemId = null;
            if(this.R_GrantedByEquippingItemId == null && this.R_GrantedThroughId == null){
                this.DeleteOnSave = true;
            }
        }

        public virtual void Link(Character character){
            R_TargetedCharacter = character;
            character.R_AffectedBy.Add(this);
        }
    }
}