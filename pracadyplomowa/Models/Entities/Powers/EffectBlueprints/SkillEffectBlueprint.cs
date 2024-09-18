using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class SkillEffectBlueprint : ValueEffectBlueprint
    {
        public SkillEffectType SkillEffectType{ get; set; } = new SkillEffectType();

        public SkillEffectBlueprint(string name, DiceSet value, SkillEffect skillEffect, Skill skill) : base(name, value){
            SkillEffectType.SkillEffect = skillEffect;
            SkillEffectType.SkillEffect_Skill = skill;
        }
        public SkillEffectBlueprint(string name) : base(name, 0){ // only meant to be used for skill expertise upgrade
            SkillEffectType.SkillEffect = SkillEffect.UpgradeToExpertise;
            SkillEffectType.SkillEffect_Skill = 0; // 0 provided but it wont be used, i have no better idea how to do it
        }
    }
}