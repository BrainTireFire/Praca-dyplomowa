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
    public class SkillEffectType(SkillEffect skillEffect, Skill skillEffect_Skill)
    {
        private readonly SkillEffect? _SkillEffect = skillEffect;
        private readonly Skill? _SkillEffect_Skill = skillEffect_Skill;

        public SkillEffect? SkillEffect { get => _SkillEffect == null ? _SkillEffect : throw new ArgumentNullException(); }
        public Skill? SkillEffect_Skill { get => _SkillEffect_Skill == null ? _SkillEffect_Skill : throw new ArgumentNullException(); }
    }
}