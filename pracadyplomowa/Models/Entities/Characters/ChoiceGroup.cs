using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class ChoiceGroup : ObjectWithId
    {
        public int NumberToChoose { get; set; }
        
        public virtual ICollection<Power> Powers { get; set; } = [];

        public virtual ICollection<EffectBlueprint> R_EffectsToPick { get; set; } = [];

        public virtual RaceLevel? R_GrantedByRaceLevel { get; set; }
        public virtual int? GrantedByRaceLevelId { get; set; }
        public virtual ClassLevel? R_GrantedByClassLevel { get; set; }
        public virtual int? GrantedByClassLevelId { get; set; }
    }
}