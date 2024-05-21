using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class Character : ObjectWithOwner
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DiceSet UsedHitDice { get; set; } = new DiceSet();

        //Relationship
        public virtual User User { get; set; } = null!;
        public virtual Race Race { get; set; } = null!;
        public virtual EffectGroup EffectGroup { get; set; }
        public virtual ParticipanceData ParticipanceData { get; set; }
        public virtual ICollection<EquipData> EquipDatas { get; set; } = [];
        public virtual ICollection<Campaign.Campaign> Campaigns { get; set; } = []; //WHAT IS GOING ON HERE????
        public virtual ICollection<ClassLevel> ClassLevels { get; set; } = [];
        public virtual ICollection<Aura> Auras { get; set; } = [];
        public virtual ICollection<EffectInstance> EffectInstances { get; set; } = [];
        public virtual ICollection<Power> PowerPrepared { get; set; } = [];
        public virtual ICollection<Power> PowerKnowns { get; set; } = [];
    }
}