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
    public class ResistanceEffectBlueprint : EffectBlueprint
    {
        protected ResistanceEffectBlueprint() : this("EF", 0, 0){}
        public ResistanceEffectType ResistanceEffectType{ get; set;} = new ResistanceEffectType();

        public ResistanceEffectBlueprint(string name, ResistanceEffect resistanceEffect, DamageType damageType) : base(name){
            ResistanceEffectType.ResistanceEffect = resistanceEffect;
            ResistanceEffectType.ResistanceEffect_DamageType = damageType;
        }
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new ResistanceEffectInstance(this, target);
        }
    }
}