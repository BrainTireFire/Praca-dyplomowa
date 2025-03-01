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
    public class ActionEffectType
    {
        public ActionEffect ActionEffect { get; set; }
        public ActionEffectType(ActionEffectType cloned){
            this.ActionEffect = cloned.ActionEffect;
        }
        public ActionEffectType(){
        }

        public ActionEffectType Clone(){
            return new ActionEffectType(this);
        }
    }
}