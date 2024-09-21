using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class DiceSet : ObjectWithId
    {
        public int d20 { get; set; }
        public int d12 { get; set; }
        public int d10 { get; set; }
        public int d8 { get; set; }
        public int d6 { get; set; }
        public int d4 { get; set; }
        public int d100 { get; set; }
        public int flat {get; set;}
        public List<AdditionalValue> additionalValues { get; set; } = [];

        public static implicit operator int(DiceSet d) => d.flat;
        public static implicit operator DiceSet(int d) => new(){flat = d};


        public class AdditionalValue : ObjectWithId{
            public enum AdditionalValueType{
                LevelsInClass = 0,
                ProficiencyModifier = 1,
                TotalLevel = 2,
                AbilityScoreModifier = 3,
                SkillBonus = 4
            }
            public AdditionalValueType additionalValueType;
            public Class? R_LevelsInClass { get; set; }
            public int? R_LevelsInClassId { get; set; }
            public Ability Ability {get; set;}
            public Skill Skill {get; set;}
        }

    }

}