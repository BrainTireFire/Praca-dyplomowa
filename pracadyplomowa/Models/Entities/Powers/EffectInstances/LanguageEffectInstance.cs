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
        public LanguageEffectType EffectType{ get; set;} = new LanguageEffectType();

        
        public virtual required Language R_Language { get; set; }
        public int R_LanguageId { get; set; }
        private LanguageEffectInstance() : base("EF"){}
        public LanguageEffectInstance(string name) : base(name){}
        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public LanguageEffectInstance(LanguageEffectBlueprint languageEffectBlueprint, Character target) : base(languageEffectBlueprint, target){
            EffectType = languageEffectBlueprint.LanguageEffectType.Clone();
            R_Language = languageEffectBlueprint.R_Language;
            R_LanguageId = languageEffectBlueprint.R_LanguageId;
        }
        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public LanguageEffectInstance(LanguageEffectInstance effectInstance) : base(effectInstance){
            EffectType  = effectInstance.EffectType.Clone();
            R_Language = effectInstance.R_Language;
            R_LanguageId = R_LanguageId;
        }
        public override EffectInstance Clone(){
            return new LanguageEffectInstance(this);
        }
    }
}