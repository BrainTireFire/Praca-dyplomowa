using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.Entities.Powers.EffectBlueprints
{
    public class LanguageEffectInstance : EffectInstance
    {
        public LanguageEffectType LanguageEffectType{ get; set;} = new LanguageEffectType();

        
        public virtual required Language R_Language { get; set; }
        public int R_LanguageId { get; set; }
        private LanguageEffectInstance() : base("EF"){}
        public LanguageEffectInstance(string name) : base(name){}
        public LanguageEffectInstance(LanguageEffectBlueprint languageEffectBlueprint) : base(languageEffectBlueprint){
            LanguageEffectType = languageEffectBlueprint.LanguageEffectType;
            R_Language = languageEffectBlueprint.R_Language;
            R_LanguageId = languageEffectBlueprint.R_LanguageId;
        }
    }
}