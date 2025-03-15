using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs.Session
{
    public class InitiativeQueueItemDto
    {
        public int CharacterId { get; set; }
        public bool IsNpc { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public int PlaceInQueue { get; set; }
        public int InitiativeRollResult {get; set;}
        public bool ActiveTurn {get; set;}
        public int SucceededDeathSaves {get; set;}
        public int FailedDeathSaves {get; set;}

    }
}