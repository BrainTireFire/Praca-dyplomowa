using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Powers
{
    public class Power : ObjectWithOwner
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ActionType RequiredActionType { get; set; }
        public bool IsImplemented { get; set; }
        public CastableBy CastableBy { get; set; }
        public PowerType PowerType { get; set; }
        public TargetType TargetType { get; set; }

        public int Range { get; set; }
        public int MaxTargets { get; set; }
        public int MaxTargetsToExclude { get; set; }
        public int AreaSize { get; set; }
        public AreaShape AreaShape { get; set; }
        public int AuraSize { get; set; }
        public int DifficultyClass { get; set; }
        public Ability SavingThrow { get; set; }
        public bool RequiresConcentration { get; set; }
        public SavingThrowBehaviour SavingThrowBehaviour { get; set; }
        public SavingThrowRoll SavingThrowRoll { get; set; }
        public bool VerbalComponent { get; set; }
        public bool SomaticComponent { get; set; }

        public virtual ICollection<Character> CharacterPreparedPowers { get; set; } = [];
        public virtual ICollection<Character> CharacterKnownsPowers { get; set; } = [];
        public virtual ICollection<Item> Items { get; set; } = [];
    }
}