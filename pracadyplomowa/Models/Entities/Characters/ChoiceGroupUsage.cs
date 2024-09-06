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
        public ChoiceGroup? R_ChoiceGroup {get; set;}
        public List<Power> R_PowersGranted  {get; set;} = [];
        public List<EffectGroup> R_EffectGroupsGranted {get; set;} = [];
    }
}