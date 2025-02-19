using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class InitiativeEffectType
    {
        public InitiativeEffectType(InitiativeEffectType cloned){
        }
        public InitiativeEffectType(){
        }

        public InitiativeEffectType Clone(){
            return new InitiativeEffectType(this);
        }
    }
}