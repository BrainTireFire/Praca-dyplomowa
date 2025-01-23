using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
        public class AttackRollAndDamageResultDto{
            public HitType HitType { get; set; }
            public Character.WeaponHitResult WeaponHitResult {get; set;} = new();
        }
}