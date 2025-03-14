﻿using pracadyplomowa.Models.ComplexTypes.Effects;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.Entities.Powers;

public class AttackRollEffectInstance : ValueEffectInstance
{
    public AttackRollEffectType EffectType { get; set; } = new AttackRollEffectType();
    protected AttackRollEffectInstance() : base("EF", 0){}
    public AttackRollEffectInstance(string name) : base(name, 0){}
    public AttackRollEffectInstance(AttackRollEffectBlueprint attackRollEffectBlueprint, Character? roller, Character target) : base(attackRollEffectBlueprint, roller, target){
        EffectType = attackRollEffectBlueprint.AttackRollEffectType.Clone();
    }
    public AttackRollEffectInstance(AttackRollEffectInstance effectInstance) : base(effectInstance){
        EffectType  = effectInstance.EffectType.Clone();
    }
    public override EffectInstance Clone(){
        return new AttackRollEffectInstance(this);
    }
}