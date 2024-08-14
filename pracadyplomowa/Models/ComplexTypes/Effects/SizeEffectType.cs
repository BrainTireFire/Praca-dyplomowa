#nullable disable

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

        public SizeEffect SizeEffect => (SizeEffect) _SizeEffect;
        public Size SizeEffect_SizeToSet  => (Size) _SizeEffect_SizeToSet;
        public int SizeBonus  => (int) _SizeBonus;

    }
}