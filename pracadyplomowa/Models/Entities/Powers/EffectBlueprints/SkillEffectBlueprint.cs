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
        //constructors
        private SkillEffectBlueprint() : this("EF"){}
        public SkillEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, SkillEffect skillEffect, Skill skill) : base(name, value, rollMoment){
            SkillEffectType.SkillEffect = skillEffect;
            SkillEffectType.SkillEffect_Skill = skill;
        }
        public SkillEffectBlueprint(string name) : base(name, 0, 0){ // only meant to be used for skill expertise upgrade
            SkillEffectType.SkillEffect = SkillEffect.UpgradeToExpertise;
            SkillEffectType.SkillEffect_Skill = 0; // 0 provided but it wont be used, i have no better idea how to do it
        }
        //methods
        public override EffectInstance Generate(Character roller, Character target){
            return new SkillEffectInstance(this, roller, target);
        }
    }
}