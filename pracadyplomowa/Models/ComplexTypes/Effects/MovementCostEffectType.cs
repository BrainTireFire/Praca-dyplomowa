using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class MovementCostEffectType
    {
        public MovementCost MovementCostEffect { get; set; }
        public MovementCostEffectType(MovementCostEffectType cloned){
            this.MovementCostEffect = cloned.MovementCostEffect;
        }
        public MovementCostEffectType(){
        }

        public MovementCostEffectType Clone(){
            return new MovementCostEffectType(this);
        }
    }
}