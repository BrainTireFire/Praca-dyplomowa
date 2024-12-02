using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using pracadyplomowa.Models.DTOs;
using pracadyplomowa.Models.Entities.Powers;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.RequestHelpers
{
    public class EffectBlueprintConverter : ITypeConverter<EffectBlueprintFormDto, EffectBlueprint>
    {
        public EffectBlueprint Convert(EffectBlueprintFormDto source, EffectBlueprint destination, ResolutionContext context)
        {
            EffectBlueprint result;
            switch(source.EffectType){
                case "actions":
                    result = context.Mapper.Map<ActionEffectBlueprint>(source);
                    break;
                default:
                    throw new UnreachableException();
            }
            return result;
        }
    }
}