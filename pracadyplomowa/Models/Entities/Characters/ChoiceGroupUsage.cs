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
        public Character? R_Character {get; set;}
        public int R_ChoiceGroupId {get; set;}
        public ChoiceGroup R_ChoiceGroup {get; set;} = null!;
        public List<Power> R_PowersGranted  {get; set;} = [];
        public List<EffectInstance> R_EffectsGranted {get; set;} = [];

        //methods
        // public void AssignEffectInstance(EffectInstance instance){
        //     this.R_EffectsGranted
        // }
    }
}