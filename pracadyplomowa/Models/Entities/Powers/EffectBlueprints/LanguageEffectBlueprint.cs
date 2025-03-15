using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class LanguageEffectBlueprint : EffectBlueprint
    {
        public LanguageEffectType LanguageEffectType{ get; set;} = new LanguageEffectType();

        
        public virtual required Language R_Language { get; set; }
        public int R_LanguageId { get; set; }
        //constructors
        protected LanguageEffectBlueprint(): this("EF"){}
        public LanguageEffectBlueprint(string name) : base(name){
            Description = "Knowledge of spoken and written language";
        }
        public LanguageEffectBlueprint(string name, string description) : this(name){
            Description = description;
        }
        //methods
        public override EffectInstance Generate(Character? roller, Character target){
            return new LanguageEffectInstance(this, target);
        }
    }
}