using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.RequestHelpers
{
    public class DiceSetToDtoConverter : ITypeConverter<DiceSet, ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto>
    {
        public ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto Convert(DiceSet source, ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto destination, ResolutionContext context)
        {
            return new ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto
            {
                d100 = source.d100,
                d20 = source.d20,
                d12 = source.d12,
                d10 = source.d10,
                d8 = source.d8,
                d6 = source.d6,
                d4 = source.d4,
                flat = source.flat,
                additionalValues = source.additionalValues.Select(av => new ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto.AdditionalValueFormDto
                {
                    LevelsInClassId = av.R_LevelsInClassId,
                    ClassName = av.R_LevelsInClass?.Name,
                    Ability = av.Ability,
                    Skill = av.Skill
                }).ToList()
            };
        }
    }

    public class DtoToDiceSetConverter : ITypeConverter<ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto, DiceSet>
    {
        public DiceSet Convert(ValueEffectBlueprintFormDto.ValueSubeffectBlueprintFormDto.DiceSetFormDto source, DiceSet destination, ResolutionContext context)
        {
            return new DiceSet
            {
                d100 = source.d100,
                d20 = source.d20,
                d12 = source.d12,
                d10 = source.d10,
                d8 = source.d8,
                d6 = source.d6,
                d4 = source.d4,
                flat = source.flat,
                additionalValues = source.additionalValues.Select(av => new DiceSet.AdditionalValue
                {
                    R_LevelsInClassId = av.LevelsInClassId,
                    Ability = av.Ability,
                    Skill = av.Skill
                }).ToList()
            };
        }
    }
}