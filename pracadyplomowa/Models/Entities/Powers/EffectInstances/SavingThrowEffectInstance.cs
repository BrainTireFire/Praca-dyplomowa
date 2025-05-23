﻿using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class SavingThrowEffectInstance : ValueEffectInstance
{
    public SavingThrowEffectType EffectType { get; set; } = new SavingThrowEffectType();
    protected SavingThrowEffectInstance() : base("EF", 0){}
    public SavingThrowEffectInstance(string name) : base(name, 0){}
    public SavingThrowEffectInstance(SavingThrowEffectBlueprint savingThrowEffectBlueprint, Character? roller, Character target) : base(savingThrowEffectBlueprint, roller, target){
        EffectType = savingThrowEffectBlueprint.SavingThrowEffectType.Clone();
    }
    public SavingThrowEffectInstance(SavingThrowEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new SavingThrowEffectInstance(this);
    }
}