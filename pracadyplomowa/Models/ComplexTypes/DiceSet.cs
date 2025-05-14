using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Characters
{
    public class DiceSet : ObjectWithId
    {
        public DiceSet()
        {

        }
        public DiceSet(DiceRollDto diceRollDto)
        {
            this.d20 = diceRollDto.d20;
            this.d12 = diceRollDto.d12;
            this.d10 = diceRollDto.d10;
            this.d8 = diceRollDto.d8;
            this.d6 = diceRollDto.d6;
            this.d4 = diceRollDto.d4;
            this.d100 = diceRollDto.d100;
            this.flat = diceRollDto.flat;
        }

        public DiceSet(DiceSet diceSet)
        {
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
        public virtual ValueEffectBlueprint? R_ValueEffectBlueprint { get; set; }
        public int? R_ValueEffectBlueprintId { get; set; }
        public virtual ValueEffectInstance? R_ValueEffectInstance { get; set; }
        public int? R_ValueEffectInstanceId { get; set; }
        public virtual Weapon? R_Weapon_Damage { get; set; }
        public int? R_Weapon_DamageId { get; set; }
        public virtual MeleeWeapon? R_MeleeWeapon_VersatileDamage { get; set; }
        public int? R_MeleeWeapon_VersatileDamageId { get; set; }
        public virtual Class? R_Class_SpellFormula { get; set; }
        public int? R_Class_SpellFormulaId { get; set; }
        public virtual Character? R_Character_UsedHitDice { get; set; }
        public int? R_Character_UsedHitDiceId { get; set; }
        public virtual ClassLevel? R_ClassLevel_HitDice { get; set; }
        public int? R_ClassLevel_HitDiceId { get; set; }
        public int d20 { get; set; }
        public int d12 { get; set; }
        public int d10 { get; set; }
        public int d8 { get; set; }
        public int d6 { get; set; }
        public int d4 { get; set; }
        public int d100 { get; set; }
        public int flat { get; set; }
        public virtual List<AdditionalValue> additionalValues { get; set; } = [];

        public static implicit operator int(DiceSet d) => d.flat;
        public static implicit operator DiceSet(int d) => new() { flat = d };

        public static DiceSet operator +(DiceSet x, DiceSet y) => new DiceSet()
        {
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
        public static DiceSet operator -(DiceSet x, DiceSet y) => new DiceSet()
        {
            d20 = x.d20 - y.d20,
            d12 = x.d12 - y.d12,
            d10 = x.d10 - y.d10,
            d8 = x.d8 - y.d8,
            d6 = x.d6 - y.d6,
            d4 = x.d4 - y.d4,
            d100 = x.d100 - y.d100,
            flat = x.flat - y.flat,
            additionalValues = []
        };

        public DiceSet getPersonalizedSet(Character? roller)
        {
            var personalizedSet = new DiceSet()
            {
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


        public class AdditionalValue : ObjectWithId
        {
            public AdditionalValue()
            {

            }
            public AdditionalValue(AdditionalValue value)
            {
                this.additionalValueType = value.additionalValueType;
                this.R_LevelsInClass = value.R_LevelsInClass;
                this.R_LevelsInClassId = value.R_LevelsInClassId;
                this.Ability = value.Ability;
                this.Skill = value.Skill;
            }
            public enum AdditionalValueType
            {
                LevelsInClass = 0,
                TotalLevel = 1,
                AbilityScoreModifier = 2,
                SkillBonus = 3,
                ProficiencyBonus = 4,
            }
            public AdditionalValueType additionalValueType { get; set; }
            public virtual Class? R_LevelsInClass { get; set; }
            public int? R_LevelsInClassId { get; set; }
            public Ability? Ability {get; set;}
            public Skill? Skill {get; set;}
            public DiceSet DiceSet {get; set;}
            public int DiceSetId {get; set;}

            public int ReturnValue(Character roller)
            {
                if (additionalValueType == AdditionalValueType.LevelsInClass)
                {
                    return roller.R_CharacterHasLevelsInClass.Where(cl => cl.R_Class == R_LevelsInClass).Count();
                }
                else if (additionalValueType == AdditionalValueType.TotalLevel)
                {
                    return roller.R_CharacterHasLevelsInClass.Count;
                }
                else if(additionalValueType == AdditionalValueType.AbilityScoreModifier)
                {
                    return Character.AbilityModifier(roller.AbilityValue((Ability)Ability));
                }
                else if(additionalValueType == AdditionalValueType.SkillBonus)
                {
                    return roller.SkillValue((Skill)Skill);
                }
                else if(additionalValueType == AdditionalValueType.ProficiencyBonus)
                {
                    return roller.ProficiencyBonus;
                }
                else throw new UnreachableException();
            }

            public override bool Equals(object? obj)
            {
                if (obj is not AdditionalValue)
                    return false;
                return this == (AdditionalValue)obj;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(AdditionalValue x, AdditionalValue y)
            {
                return x.additionalValueType == y.additionalValueType && x.R_LevelsInClassId == y.R_LevelsInClassId && x.Ability == y.Ability && x.Skill == y.Skill;
            }
            public static bool operator !=(AdditionalValue x, AdditionalValue y)
            {
                return !(x == y);
            }
        }

        public int Roll(Character? roller)
        {
            int[] arr = RollSeparate();
            int result = 0;
            foreach (var val in arr)
            {
                result += val;
            }
            if(roller != null){
                result += ResolveAdditionalValues(roller);
            }
            return result;
        }

        public class DiceRollResult {
            public int size;
            public int result;
        }

        public class Dice {
            private readonly Random rnd = new();
            public int size;
            public int result;
            public int Roll(){
                if(size != 0){
                    result = rnd.Next(1, size);
                }
                return result;
            }
        }

        public List<Dice> RollPrototype(bool advantage, bool disadvantage, int? rerollLowerThan){ //returns map where keys are dice size
            List<Dice> diceSet = [];
            for(int i = 0; i < this.d100; i++){
                diceSet.Add(new Dice(){size = 100});
            }
            for(int i = 0; i < this.d20; i++){
                diceSet.Add(new Dice(){size = 20});
            }
            for(int i = 0; i < this.d12; i++){
                diceSet.Add(new Dice(){size = 12});
            }
            for(int i = 0; i < this.d10; i++){
                diceSet.Add(new Dice(){size = 10});
            }
            for(int i = 0; i < this.d8; i++){
                diceSet.Add(new Dice(){size = 8});
            }
            for(int i = 0; i < this.d6; i++){
                diceSet.Add(new Dice(){size = 6});
            }
            for(int i = 0; i < this.d4; i++){
                diceSet.Add(new Dice(){size = 4});
            }
            diceSet.Add(new Dice(){size = 0, result = this.flat});
            foreach(Dice dice in diceSet){
                var result1 = dice.Roll();
                if(rerollLowerThan != null && result1 < rerollLowerThan){
                    dice.Roll();
                }
                if(advantage || disadvantage){
                    var result2 = dice.Roll();
                    if(advantage && !disadvantage && result1 > result2){
                        dice.result = result1;
                    }
                    if(disadvantage && !advantage && result1 < result2){
                        dice.result = result1;
                    }
                }
            }
            return diceSet;
        }

        public List<Dice> RollPrototype(Character roller, bool advantage, bool disadvantage, int? rerollLowerThan){ //returns map where keys are dice size
            List<Dice> diceRollResults = RollPrototype(advantage, disadvantage, rerollLowerThan);
            diceRollResults.Add(new Dice() {size = 0, result = this.ResolveAdditionalValues(roller)});
            return diceRollResults;
        }

        // public int Roll()
        // {
        //     int[] arr = RollSeparate();
        //     int result = 0;
        //     foreach (var val in arr)
        //     {
        //         result += val;
        //     }
        //     return result;
        // }

        public int[] RollSeparate()
        {
            Random rnd = new();
            int[] result = new int[8];
            result[0] = d20 * rnd.Next(1, 20);
            result[1] = d12 * rnd.Next(1, 12);
            result[2] = d10 * rnd.Next(1, 10);
            result[3] = d8 * rnd.Next(1, 8);
            result[4] = d6 * rnd.Next(1, 6);
            result[5] = d4 * rnd.Next(1, 4);
            result[6] = d100 * rnd.Next(1, 100);
            result[7] = flat;
            return result;
        }

        public int ResolveAdditionalValues(Character roller)
        {
            int result = 0;
            foreach (AdditionalValue adVal in additionalValues)
            {
                result += adVal.ReturnValue(roller);
            }
            return result;
        }

    }

}