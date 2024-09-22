using System.Diagnostics;
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

            public int ReturnValue(Character roller){
                if(additionalValueType == AdditionalValueType.LevelsInClass){
                    return roller.R_CharacterHasLevelsInClass.Where(cl => cl.R_Class == R_LevelsInClass).Count();
                }
                else if(additionalValueType == AdditionalValueType.TotalLevel){
                    return roller.R_CharacterHasLevelsInClass.Count;
                }
                else if(additionalValueType == AdditionalValueType.ProficiencyModifier){
                    return roller.ProficiencyBonus;
                }
                else if(additionalValueType == AdditionalValueType.AbilityScoreModifier){
                    return Character.AbilityModifier(roller.AbilityValue(Ability));
                }
                else if(additionalValueType == AdditionalValueType.SkillBonus){
                    return roller.SkillValue(Skill);
                }
                else throw new UnreachableException();
            }
        }

        public int Roll(Character roller){
            Random rnd = new();
            int result = d20 * rnd.Next(1, 20);
            result += d12 * rnd.Next(1, 12);
            result += d10 * rnd.Next(1, 10);
            result += d8 * rnd.Next(1, 8);
            result += d6 * rnd.Next(1, 6);
            result += d4 * rnd.Next(1, 4);
            result += d100 * rnd.Next(1, 100);
            result += flat;
            foreach(AdditionalValue adVal in additionalValues){
                result += adVal.ReturnValue(roller);
            }
            return result;
        }

    }

}