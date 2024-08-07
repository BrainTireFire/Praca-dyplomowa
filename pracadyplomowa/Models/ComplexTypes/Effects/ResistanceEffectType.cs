using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class ResistanceEffectType(ResistanceEffect resistanceEffect, DamageType resistanceEffect_DamageType)
    {
        private readonly ResistanceEffect? _ResistanceEffect = resistanceEffect;
        private readonly DamageType? _ResistanceEffect_DamageType = resistanceEffect_DamageType;
        public ResistanceEffect? ResistanceEffect { get => _ResistanceEffect == null ? _ResistanceEffect : throw new ArgumentNullException(); }
        public DamageType? ResistanceEffect_DamageType { get => _ResistanceEffect_DamageType == null ? _ResistanceEffect_DamageType : throw new ArgumentNullException(); }
    }
}