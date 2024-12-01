using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
public class PowerFormDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionType { get; set; }
    public bool IsImplemented { get; set; }
    public string CastableBy { get; set; }
    public string PowerType { get; set; }
    public string TargetType { get; set; }
    public int? Range { get; set; }
    public int MaxTargets { get; set; }
    public int MaxTargetsToExclude { get; set; }
    public int? AreaSize { get; set; }
    public string? AreaShape { get; set; }
    public int? AuraSize { get; set; }
    public int? DifficultyClass { get; set; }
    public string? SavingThrow { get; set; }
    public bool RequiresConcentration { get; set; }
    public string? SavingThrowBehaviour { get; set; }
    public string? SavingThrowRoll { get; set; }
    public bool VerbalComponent { get; set; }
    public bool SomaticComponent { get; set; }
    public int Duration { get; set; }
    public string UpcastBy { get; set; }
    public ClassDTO? ClassForUpcasting { get; set; }
    public ImmaterialResourceBlueprintDto ImmaterialResourceUsed { get; set; }
    public List<ItemFamilyWithWorthDto> MaterialResourcesUsed { get; set; } = [];
    public List<EffectBlueprintDto> EffectBlueprints { get; set; } = [];

    public class EffectBlueprintDto {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int ResourceLevel { get; set; }
        public bool SavingThrowSuccess { get; set; }
    }

}

}