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
    public class SizeEffectType(SizeEffect sizeEffect, Size sizeEffect_SizeToSet, int sizeBonus)
    {
        private readonly SizeEffect? _SizeEffect = sizeEffect;
        private readonly Size? _SizeEffect_SizeToSet = sizeEffect_SizeToSet;
        private readonly int? _SizeBonus = sizeBonus;

        public SizeEffect? SizeEffect { get => _SizeEffect == null ? _SizeEffect : throw new ArgumentNullException(); }
        public Size? SizeEffect_SizeToSet  { get => _SizeEffect_SizeToSet == null ? _SizeEffect_SizeToSet : throw new ArgumentNullException(); }
        public int? SizeBonus  { get => _SizeBonus == null ? _SizeBonus : throw new ArgumentNullException(); }

    }
}