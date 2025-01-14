using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pracadyplomowa.Models.DTOs.Session
{
    public class ModifyInitiativeQueueOrderItem
    {
        [Required]
        public int  CharacterId { get; set; }
        [Required]
        public int PlaceInQueue { get; set; }
    }
}