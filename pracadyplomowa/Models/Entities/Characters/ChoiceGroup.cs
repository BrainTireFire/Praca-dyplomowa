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
        public virtual List<Power> R_PowersAlwaysAvailable { get; set; } = [];
        public virtual List<Power> R_PowersToPrepare { get; set; } = [];
        public virtual List<EffectBlueprint> R_Effects { get; set; } = [];
        public virtual List<ImmaterialResourceAmount> R_Resources { get; set; } = [];

        public virtual RaceLevel? R_GrantedByRaceLevel { get; set; }
        public virtual int? R_GrantedByRaceLevelId { get; set; }
        public virtual ClassLevel? R_GrantedByClassLevel { get; set; }
        public virtual int? R_GrantedByClassLevelId { get; set; }
        public virtual ICollection<ChoiceGroupUsage> R_UsageInstances { get; set; } = [];

        public ChoiceGroupUsage Generate(Character character, List<EffectBlueprint> effects, List<Power> powersAlwaysAvailable, List<Power> powersToPrepare, List<ImmaterialResourceAmount> resources){
            ChoiceGroupUsage usage = new();
            if(effects.Intersect(this.R_Effects).Count() == effects.Count 
            && powersAlwaysAvailable.Intersect(this.R_PowersAlwaysAvailable).Count() == powersAlwaysAvailable.Count 
            && powersToPrepare.Intersect(this.R_PowersToPrepare).Count() == powersToPrepare.Count 
            && resources.Intersect(this.R_Resources).Count() == resources.Count 
            && (this.NumberToChoose == 0 || this.NumberToChoose == effects.Count + powersAlwaysAvailable.Count + powersToPrepare.Count + resources.Count)){
                foreach(EffectBlueprint blueprint in effects){
                    usage.R_EffectsGranted.Add(blueprint.Generate(character, character));
                } 
                foreach(Power power in powersAlwaysAvailable){
                    usage.R_PowersAlwaysAvailableGranted.Add(power);
                } 
                foreach(Power power in powersToPrepare){
                    usage.R_PowersToPrepareGranted.Add(power);
                } 
                foreach(ImmaterialResourceAmount resource in resources){
                    usage.R_ResourcesGranted.AddRange(resource.Generate());
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
            return Generate(character, this.R_Effects, this.R_PowersAlwaysAvailable, this.R_PowersToPrepare, this.R_Resources);
        }

        public class InvalidChoiceGroupSelectionException(string message) : Exception(message){};
    }
}