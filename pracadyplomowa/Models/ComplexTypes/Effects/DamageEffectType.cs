using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [ComplexType]
    public class DamageEffectType(DamageEffect damageEffect, DamageType damageEffect_DamageType)
    {
        private readonly DamageEffect? _DamageEffect = damageEffect;
        private readonly DamageType? _DamageEffect_DamageType = damageEffect_DamageType;
        public DamageEffect? DamageEffect  { get => _DamageEffect == null ? _DamageEffect : throw new ArgumentNullException(); }
        public DamageType? DamageEffect_DamageType { get => _DamageEffect_DamageType == null ? _DamageEffect_DamageType : throw new ArgumentNullException(); }
    }
}