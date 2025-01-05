using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Interfaces;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.Entities.Campaign
{
    public class Field : ObjectWithId, ICaster
    {
        //Properties
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public string Color { get; set; } = null!;
        public string? Description { get; set; }
        public FieldCoverType FieldCoverLevel{ get; set; }
        public FieldMovementCostType FieldMovementCost { get; set; }


        //Relationships
        public virtual ICollection<Power> R_CasterPowers { get; set; } = [];
        public virtual Board R_Board { get; set; } = null!;
        public int R_BoardId { get; set; }
        public virtual ParticipanceData? R_OccupiedBy { get; set; }
        public int? R_OccupiedById { get; set; }
        public virtual ICollection<EffectGroup> R_EffectGroupOnField { get; set; } = [];
        
        public Field() { }
        
        public Field(int positionX, int positionY, int positionZ, string color, string fieldCoverLevelStr, string fieldMovementCostStr, string? description = null)
        {
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            Color = color ?? throw new ArgumentNullException(nameof(color));
            
            if (!Enum.TryParse(fieldCoverLevelStr, true, out FieldCoverType fieldCoverLevel))
            {
                throw new ArgumentException($"Invalid field cover level: {fieldCoverLevelStr}");
            }
            FieldCoverLevel = fieldCoverLevel;
            if (!Enum.TryParse(fieldMovementCostStr, true, out FieldMovementCostType fieldMovementCost))
            {
                throw new ArgumentException($"Invalid field cover level: {fieldCoverLevelStr}");
            }
            FieldMovementCost = fieldMovementCost;
            Description = description;
        }
        public Field(int positionX, int positionY, int positionZ, string color, string fieldCoverLevelStr, string fieldMovementCostStr, List<Power> powers, string? description = null) : this(positionX, positionY, positionZ, color, fieldCoverLevelStr, fieldMovementCostStr, description)
        {
            powers.ForEach(power => this.R_CasterPowers.Add(power));
        }
        
        public void AssignToBoard(Board board)
        {
            R_Board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public bool IsAdjacentToField(Field field){
            return Math.Abs(field.PositionX - this.PositionX) <= 1 || Math.Abs(field.PositionY - this.PositionY) <= 1;
        }

        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets){
            //retrieve data
            Dictionary<int, HitType> hitMap = [];

            foreach(var targetedCharacter in targets){
                if(power.PowerType == PowerType.Attack){
                    int roll = new DiceSet(){d20 = 1}.RollPrototype(false, false, null).First().result;
                    HitType outcome = HitType.Miss;
                    if(roll == 1){
                        outcome = HitType.CriticalMiss;
                    }
                    if(roll == 20){
                        outcome = HitType.CriticalHit;
                    }
                    if(roll >= targetedCharacter.ArmorClass){
                        outcome = HitType.Hit;
                    }
                    hitMap.Add(
                        targetedCharacter.Id,
                        outcome
                    );
                }
                else if(power.PowerType == PowerType.Saveable && power.OverrideCastersDC){
                    int roll = targetedCharacter.SavingThrowRoll((Ability)power.SavingThrow);
                    HitType outcome = HitType.Miss;
                    if(roll <= power.DifficultyClass && roll != 20){
                        outcome = HitType.Hit;
                    }
                    hitMap.Add(
                        targetedCharacter.Id,
                        outcome
                    );
                }
                else
                {
                    hitMap.Add(
                        targetedCharacter.Id,
                        HitType.Hit
                    );
                }
            }
            return hitMap;
        }

        public Outcome ApplyPowerEffects(Power power, Dictionary<Character, HitType> targetsToHitSuccessMap, int? immaterialResourceLevel)
        {
            EffectGroup effectGroup = new();
            
            //generate effects
            foreach(Character target in targetsToHitSuccessMap.Keys){
                if (targetsToHitSuccessMap.TryGetValue(target, out var outcome))
                {
                    foreach (EffectBlueprint effectBlueprint in power.R_EffectBlueprints)
                    {
                        bool shouldAdd = false;

                        if (power.PowerType == PowerType.Attack && outcome == HitType.Hit || outcome == HitType.CriticalHit)
                        {
                            shouldAdd = true;
                        }
                        else if (power.PowerType == PowerType.Saveable)
                        {
                            if ((outcome == HitType.Hit || outcome == HitType.CriticalHit) && !effectBlueprint.Saved)
                            {
                                shouldAdd = true;
                            }
                            else if ((outcome == HitType.Hit || outcome == HitType.CriticalHit) && effectBlueprint.Saved && power.SavingThrowBehaviour == SavingThrowBehaviour.Modifies)
                            {
                                shouldAdd = true;
                            }
                        }

                        if (shouldAdd)
                        {
                            var effectInstance = effectBlueprint.Generate(null, target);
                            if(outcome == HitType.CriticalHit && effectInstance is DamageEffectInstance damageEffectInstance){
                                damageEffectInstance.CriticalHit = true;
                            }
                            effectGroup.AddEffectOnCharacter(effectInstance);
                        }
                    }
                }
            }
            //configure effect group
            effectGroup.DurationLeft = power.Duration;
            effectGroup.IsConstant = false;
            if(power.PowerType == PowerType.Saveable && power.SavingThrowRoll == Enums.SavingThrowRoll.RetakenEveryTurn){
                effectGroup.DifficultyClassToBreak = power.DifficultyClass;
                effectGroup.SavingThrow = (Ability)power.SavingThrow;
            }
            effectGroup.Name = power.Name;
            return Outcome.Success;
        }
    }
}