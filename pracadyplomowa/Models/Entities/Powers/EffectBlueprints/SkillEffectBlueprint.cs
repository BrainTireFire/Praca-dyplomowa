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
        protected SkillEffectBlueprint() : this("EF", 0, 0, 0, 0){}
        public SkillEffectBlueprint(string name, DiceSet value, RollMoment rollMoment, SkillEffect skillEffect, Skill skill) : base(name, value, rollMoment){
            SkillEffectType.SkillEffect = skillEffect;
            SkillEffectType.SkillEffect_Skill = skill;
        }
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new SkillEffectInstance(this, roller, target);
        }
    }
}