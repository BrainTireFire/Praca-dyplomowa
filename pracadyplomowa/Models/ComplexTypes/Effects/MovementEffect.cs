using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class MovementEffectType
    {
        public MovementEffect MovementEffect { get; set; }
        public MovementEffectType(MovementEffectType cloned){
            this.MovementEffect = cloned.MovementEffect;
        }
        public MovementEffectType(){
        }

        public MovementEffectType Clone(){
            return new MovementEffectType(this);
        }
    }
}