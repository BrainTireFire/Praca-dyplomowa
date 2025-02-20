using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Enums;
using pracadyplomowa.Models.Enums.EffectOptions;

namespace pracadyplomowa.Models.ComplexTypes.Effects
{
    [Owned]
    public class SkillEffectType
    {
        public SkillEffect SkillEffect { get; set; }
        public Skill SkillEffect_Skill { get; set; }
        public SkillEffectType(SkillEffectType cloned){
            this.SkillEffect = cloned.SkillEffect;
            this.SkillEffect_Skill = cloned.SkillEffect_Skill;
        }
        public SkillEffectType(){
        }

        public SkillEffectType Clone(){
            return new SkillEffectType(this);
        }
    }
}