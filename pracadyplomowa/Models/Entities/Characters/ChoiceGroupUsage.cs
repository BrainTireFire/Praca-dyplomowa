using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ChoiceGroupUsage: ObjectWithId
    {
        public int R_CharacterId {get; set;}
        public Character R_Character {get; set;} = null!;
        public int R_ChoiceGroupId {get; set;}
        public ChoiceGroup R_ChoiceGroup {get; set;} = null!;
        public List<Power> R_PowersAlwaysAvailableGranted  {get; set;} = [];
        public List<Power> R_PowersToPrepareGranted  {get; set;} = [];
        public List<EffectInstance> R_EffectsGranted {get; set;} = [];
        public virtual List<ImmaterialResourceInstance> R_ResourcesGranted { get; set; } = [];

        //methods
        // public void AssignEffectInstance(EffectInstance instance){
        //     this.R_EffectsGranted
        // }
    }
}