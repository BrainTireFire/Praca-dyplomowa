using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs.Session
{
    public class ParticipanceDataDto
    {
        public string CharacterName { get; set; } = null!;
        public int ActionsTaken { get; set; }
        public int BonusActionsTaken { get; set; }
        public int ReactionsTaken { get; set; }
        public int AttacksMade { get; set; }
        public int MovementUsed { get; set; }
        public int TotalActions { get; set; }
        public int TotalBonusActions { get; set; }
        public int TotalAttacksPerAction { get; set; }
        public int TotalMovement { get; set; }
        public int Hitpoints { get; set; }
        public int MaxHitpoints { get; set; }
        public int TemporaryHitpoints { get; set; }
        public int SucceededDeathSaves { get; set; }
        public int FailedDeathSaves { get; set; }
        public Size Size { get; set; }
    }
}