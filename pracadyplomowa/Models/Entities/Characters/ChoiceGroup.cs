using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ChoiceGroup(string name) : ObjectWithId
    {
        public string Name {get; set;} = name;
        public int NumberToChoose { get; set; } = 0; // get all if set to 0
        
        //Relationship
        public virtual ICollection<Power> R_Powers { get; set; } = [];
        public virtual ICollection<EffectBlueprint> R_Effects { get; set; } = [];

        public virtual RaceLevel? R_GrantedByRaceLevel { get; set; }
        public virtual int? GrantedByRaceLevelId { get; set; }
        public virtual ClassLevel? R_GrantedByClassLevel { get; set; }
        public virtual int? GrantedByClassLevelId { get; set; }
        public virtual ICollection<ChoiceGroupUsage> R_UsageInstances { get; set; } = [];

        public ChoiceGroupUsage Generate(Character character, List<EffectBlueprint> effects, List<Power> powers){
            ChoiceGroupUsage usage = new ChoiceGroupUsage();
            if(effects.Intersect(this.R_Effects).Count() == effects.Count && (this.NumberToChoose == 0 || this.NumberToChoose == effects.Count)){
                EffectInstance instance = 
            }
            return usage;
        }
    }
}