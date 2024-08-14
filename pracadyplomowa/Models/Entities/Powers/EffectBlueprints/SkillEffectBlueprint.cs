using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SkillEffectBlueprint(Enums.EffectOptions.SkillEffect skillEffect, Enums.Skill skillEffect_Skill) : ValueEffectBlueprint
    {
        public SkillEffectType SkillEffectType{ get; set; } = new SkillEffectType(skillEffect, skillEffect_Skill);
    }
}