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
    public class AttackPerAttackActionEffectType
    {
        public AttackPerActionEffect AttackPerActionEffect { get; set; }
        public AttackPerAttackActionEffectType(AttackPerAttackActionEffectType cloned){
            this.AttackPerActionEffect = cloned.AttackPerActionEffect;
        }
        public AttackPerAttackActionEffectType(){
        }

        public AttackPerAttackActionEffectType Clone(){
            return new AttackPerAttackActionEffectType(this);
        }
    }
}