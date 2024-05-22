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
        public virtual User R_CharacterBelongsToUser { get; set; } = null!;
        public virtual Race R_CharacterBelongsToRace { get; set; } = null!;
        public virtual EffectGroup R_ConcentratesOn { get; set; }
        public virtual ParticipanceData R_CharactesParticipateInEncounter { get; set; }
        
        public virtual ICollection<EquipData> R_Backup { get; set; } = [];
        public virtual ICollection<Campaign.Campaign> R_CharactersInCampaign { get; set; } = [];
        public virtual ICollection<ClassLevel> R_CharacterHasLevelsInClass { get; set; } = [];
        public virtual ICollection<Aura> R_AuraCenteredAtCharacter { get; set; } = [];
        public virtual ICollection<EffectInstance> R_AffectedBy { get; set; } = [];
        public virtual ICollection<Power> R_PowerPrepared { get; set; } = [];
        public virtual ICollection<Power> R_PowerKnowns { get; set; } = [];
    }
}