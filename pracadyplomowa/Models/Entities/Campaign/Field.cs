using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public FieldCoverType FieldCoverLevel { get; set; }
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
        
        [NotMapped]
        public FieldMovementCostType ActualMovementCost {
            get {
                return this.FieldMovementCost;
            }
        }

        public void AssignToBoard(Board board)
        {
            R_Board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public void UpdateParticipanceData(ParticipanceData newParticipanceData)
        {
            if (newParticipanceData == null)
            {
                throw new ArgumentNullException(nameof(newParticipanceData), "ParticipanceData cannot be null.");
            }
            
            R_OccupiedBy = newParticipanceData;
            R_OccupiedById = newParticipanceData.Id;
            
            newParticipanceData.R_OccupiedField = this;
        }


        public bool IsAdjacentToField(Field field)
        {
            return Math.Abs(field.PositionX - this.PositionX) <= 1 || Math.Abs(field.PositionY - this.PositionY) <= 1;
        }

        public Dictionary<int, HitType> CheckIfPowerHitSuccessfull(Encounter encounter, Power power, List<Character> targets)
        {
            //retrieve data
            Dictionary<int, HitType> hitMap = [];

            foreach (var targetedCharacter in targets)
            {
                if (power.PowerType == PowerType.Attack)
                {
                    int roll = new DiceSet() { d20 = 1 }.RollPrototype(false, false, null).First().result;
                    HitType outcome = HitType.Miss;
                    if (roll == 1)
                    {
                        outcome = HitType.CriticalMiss;
                    }
                    if (roll == 20)
                    {
                        outcome = HitType.CriticalHit;
                    }
                    if (roll >= targetedCharacter.ArmorClass)
                    {
                        outcome = HitType.Hit;
                    }
                    hitMap.Add(
                        targetedCharacter.Id,
                        outcome
                    );
                }
                else if (power.PowerType == PowerType.Saveable && power.OverrideCastersDC)
                {
                    int roll = targetedCharacter.SavingThrowRoll((Ability)power.SavingThrow);
                    HitType outcome = HitType.Miss;
                    if (roll <= power.DifficultyClass && roll != 20)
                    {
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
            foreach (Character target in targetsToHitSuccessMap.Keys)
            {
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
                            if (outcome == HitType.CriticalHit && effectInstance is DamageEffectInstance damageEffectInstance)
                            {
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
            if (power.PowerType == PowerType.Saveable && power.SavingThrowRoll == Enums.SavingThrowRoll.RetakenEveryTurn)
            {
                effectGroup.DifficultyClassToBreak = power.DifficultyClass;
                effectGroup.SavingThrow = (Ability)power.SavingThrow;
            }
            effectGroup.Name = power.Name;
            return Outcome.Success;
        }
        
        // public bool IsOccupied(Character? characterExcluded = null){
        //     return this.R_OccupiedBy != null && this.R_OccupiedBy.R_Character != characterExcluded; //version if occupying multiple fields indeed generates multiple relationships
        // }

        public bool IsOccupiedAlternative(Character? characterExcluded = null){
            var enc = this.R_Board?.R_Encounter;
            if(enc == null){
                return false;
            }
            List<Tuple<int, int>> occupiedCoordinates = [];
            var occupiedDirectly = enc.R_Participances.Where(par => par.R_Character != characterExcluded).Select(p => p.R_OccupiedField).ToList();
            foreach(var occupiedField in occupiedDirectly){
                var size = occupiedField.R_OccupiedBy.R_Character.Size;
                occupiedCoordinates = occupiedField.GetOccupiedCoordinates(size);
            }
            return occupiedCoordinates.Contains(new Tuple<int, int>(PositionX, PositionY));
        }

        public bool CanBeEnteredBy(Character character){
            var willOccupyCoordinatesIfEnters = GetOccupiedCoordinates(character.Size);
            foreach(var field in this.R_Board.R_ConsistsOfFields){
                if(willOccupyCoordinatesIfEnters.Contains(new Tuple<int, int>(field.PositionX, field.PositionY))){
                    if(field.IsOccupiedAlternative(character) || field.FieldMovementCost == FieldMovementCostType.Impassable){
                        return false;
                    }
                }
            }
            return true;
        }


        public List<Tuple<int, int>> GetOccupiedCoordinates(Size size){
            List<Tuple<int, int>> occupiedCoordinates = [];
            if(size <= Size.Medium ){
                occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX, this.PositionY));
            }
            else{
                if(size >= Size.Large){
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX, this.PositionY));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 1, this.PositionY));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX, this.PositionY + 1));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 1, this.PositionY + 1));
                }
                if(size >= Size.Huge){
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 2, this.PositionY));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 2, this.PositionY + 1));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX, this.PositionY + 2));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 1, this.PositionY + 2));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 2, this.PositionY + 2));
                }
                if(size == Size.Gargantuan){
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 3, this.PositionY));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 3, this.PositionY + 1));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 3, this.PositionY + 2));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 3, this.PositionY + 3));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX, this.PositionY + 3));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 1, this.PositionY + 3));
                    occupiedCoordinates.Add(new Tuple<int, int>(this.PositionX + 2, this.PositionY + 3));
                }
            }
            return occupiedCoordinates;
        }
    }
}