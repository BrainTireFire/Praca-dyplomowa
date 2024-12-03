using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class DiceSet : ObjectWithId
    {
        public DiceSet(){

        }

        public DiceSet(DiceSet diceSet){
            this.d20 = diceSet.d20;
            this.d12 = diceSet.d12;
            this.d10 = diceSet.d10;
            this.d8 = diceSet.d8;
            this.d6 = diceSet.d6;
            this.d4 = diceSet.d4;
            this.d100 = diceSet.d100;
            this.flat = diceSet.flat;
            this.additionalValues = diceSet.additionalValues.Select(x => new AdditionalValue(x)).ToList();
        }
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

        public static DiceSet operator +(DiceSet x, DiceSet y) => new DiceSet(){
            d20 = x.d20 + y.d20,
            d12 = x.d12 + y.d12,
            d10 = x.d10 + y.d10,
            d8 = x.d8 + y.d8,
            d6 = x.d6 + y.d6,
            d4 = x.d4 + y.d4,
            d100 = x.d100 + y.d100,
            flat = x.flat + y.flat,
            additionalValues = x.additionalValues.Union(y.additionalValues).ToList()
        };

        public DiceSet getPersonalizedSet(Character? roller){
            var personalizedSet = new DiceSet(){
                d20 = this.d20,
                d12 = this.d12,
                d10 = this.d10,
                d8 = this.d8,
                d6 = this.d6,
                d4 = this.d4,
                d100 = this.d100,
                flat = this.flat + (roller != null ? ResolveAdditionalValues(roller) : 0)
            };
            return personalizedSet;
        }


        public class AdditionalValue : ObjectWithId{
            public AdditionalValue(){
                
            }
            public AdditionalValue(AdditionalValue value){
                this.additionalValueType = value.additionalValueType;
                this.R_LevelsInClass = value.R_LevelsInClass;
                this.R_LevelsInClassId = value.R_LevelsInClassId;
                this.Ability = value.Ability;
                this.Skill = value.Skill;
            }
            public enum AdditionalValueType{
                LevelsInClass = 0,
                TotalLevel = 1,
                AbilityScoreModifier = 2,
                SkillBonus = 3,
            }
            public AdditionalValueType additionalValueType { get; set; }
            public Class? R_LevelsInClass { get; set; }
            public int? R_LevelsInClassId { get; set; }
            public Ability? Ability {get; set;}
            public Skill? Skill {get; set;}

            public int ReturnValue(Character roller){
                if(additionalValueType == AdditionalValueType.LevelsInClass){
                    return roller.R_CharacterHasLevelsInClass.Where(cl => cl.R_Class == R_LevelsInClass).Count();
                }
                else if(additionalValueType == AdditionalValueType.TotalLevel){
                    return roller.R_CharacterHasLevelsInClass.Count;
                }
                else if(additionalValueType == AdditionalValueType.AbilityScoreModifier){
                    return Character.AbilityModifier(roller.AbilityValue((Ability)Ability));
                }
                else if(additionalValueType == AdditionalValueType.SkillBonus){
                    return roller.SkillValue((Skill)Skill);
                }
                else throw new UnreachableException();
            }

            public override bool Equals(object? obj)
            {
                if(obj is not AdditionalValue)
                    return false;
                return this ==(AdditionalValue)obj;
            }

            public override int GetHashCode(){
                return base.GetHashCode();
            }

            public static bool operator ==(AdditionalValue x, AdditionalValue y) {
                return x.additionalValueType == y.additionalValueType && x.R_LevelsInClassId == y.R_LevelsInClassId && x.Ability == y.Ability && x.Skill == y.Skill;
            }
            public static bool operator !=(AdditionalValue x, AdditionalValue y) {
                return !(x==y);
            }
        }

        public int Roll(Character roller){
            int result = Roll();
            result += ResolveAdditionalValues(roller);
            return result;
        }

        public int Roll(){
            Random rnd = new();
            int result = d20 * rnd.Next(1, 20);
            result += d12 * rnd.Next(1, 12);
            result += d10 * rnd.Next(1, 10);
            result += d8 * rnd.Next(1, 8);
            result += d6 * rnd.Next(1, 6);
            result += d4 * rnd.Next(1, 4);
            result += d100 * rnd.Next(1, 100);
            result += flat;
            return result;
        }

        public int ResolveAdditionalValues(Character roller){
            int result = 0;
            foreach(AdditionalValue adVal in additionalValues){
                result += adVal.ReturnValue(roller);
            }
            return result;
        }

    }

}