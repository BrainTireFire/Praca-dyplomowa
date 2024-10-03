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
        public virtual List<Power> R_Powers { get; set; } = [];
        public virtual List<EffectBlueprint> R_Effects { get; set; } = [];

        public virtual RaceLevel? R_GrantedByRaceLevel { get; set; }
        public virtual int? R_GrantedByRaceLevelId { get; set; }
        public virtual ClassLevel? R_GrantedByClassLevel { get; set; }
        public virtual int? R_GrantedByClassLevelId { get; set; }
        public virtual ICollection<ChoiceGroupUsage> R_UsageInstances { get; set; } = [];

        public ChoiceGroupUsage Generate(Character character, List<EffectBlueprint> effects, List<Power> powers){
            ChoiceGroupUsage usage = new();
            if(effects.Intersect(this.R_Effects).Count() == effects.Count && (powers.Intersect(this.R_Powers).Count() == powers.Count) && (this.NumberToChoose == 0 || this.NumberToChoose == effects.Count + powers.Count)){
                foreach(EffectBlueprint blueprint in effects){
                    usage.R_EffectsGranted.Add(blueprint.Generate(character, character));
                } 
                foreach(Power power in powers){
                    usage.R_PowersGranted.Add(power);
                } 
            }
            else{
                throw new InvalidChoiceGroupSelectionException("Provided selection was invalid");
            }
            usage.R_ChoiceGroup = this;
            this.R_UsageInstances.Add(usage);
            character.R_UsedChoiceGroups.Add(usage);
            usage.R_Character = character;
            return usage;
        }

        public ChoiceGroupUsage Generate(Character character){
            return Generate(character, this.R_Effects, this.R_Powers);
        }

        public class InvalidChoiceGroupSelectionException(string message) : Exception(message){};
    }
}