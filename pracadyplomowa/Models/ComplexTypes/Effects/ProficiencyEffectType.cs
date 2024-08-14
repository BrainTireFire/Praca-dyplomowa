#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class ProficiencyEffectType(ProficiencyEffect proficiencyEffect)
    {
        private readonly ProficiencyEffect? _ProficiencyEffect = proficiencyEffect;
        public ProficiencyEffect ProficiencyEffect => (ProficiencyEffect)_ProficiencyEffect;
    }
}