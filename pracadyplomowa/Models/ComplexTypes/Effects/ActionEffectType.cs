#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class ActionEffectType(ActionEffect actionEffect)
    {
        private readonly ActionEffect? _ActionEffect = actionEffect;
        // private ActionEffect? ActionEffect_prop {get {return _ActionEffect;}}
        // [NotMapped]
        public ActionEffect ActionEffect => (ActionEffect)_ActionEffect;
    }
}