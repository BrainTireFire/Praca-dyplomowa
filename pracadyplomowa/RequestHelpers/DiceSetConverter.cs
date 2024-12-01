using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.RequestHelpers
{
    public class DiceSetConverter : ITypeConverter<ValueEffectBlueprint, EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>
    {
        public EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto Convert(ValueEffectBlueprint source, EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto destination, ResolutionContext context)
        {
            return new EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto
            {
                d100 = source.DiceSet.d100,
                d20 = source.DiceSet.d20,
                d12 = source.DiceSet.d12,
                d10 = source.DiceSet.d10,
                d8 = source.DiceSet.d8,
                d6 = source.DiceSet.d6,
                d4 = source.DiceSet.d4,
                flat = source.DiceSet.flat,
                additionalValues = source.DiceSet.additionalValues.Select(av => new EffectBlueprintFormDto.ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto.AdditionalValueFormDto
                {
                    LevelsInClassId = av.R_LevelsInClassId,
                    ClassName = av.R_LevelsInClass?.Name,
                    Ability = av.Ability.ToString(),
                    Skill = av.Skill.ToString()
                }).ToList()
            };
        }
    }
}