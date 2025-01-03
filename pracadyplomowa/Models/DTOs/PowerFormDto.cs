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
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public ActionType RequiredActionType { get; set; }
    public bool IsImplemented { get; set; }
    public bool IsMagic { get; set; }
    public CastableBy CastableBy { get; set; }
    public PowerType PowerType { get; set; }
    public TargetType TargetType { get; set; }
    public bool IsRanged { get; set; }
    public int? Range { get; set; }
    public int MaxTargets { get; set; }
    public int MaxTargetsToExclude { get; set; }
    public int? AreaSize { get; set; }
    public AreaShape AreaShape { get; set; }
    public int? AuraSize { get; set; }
    public bool OverrideCastersDC { get; set; } = false;
    public int? DifficultyClass { get; set; }
    public Ability? SavingThrow { get; set; }
    public bool RequiresConcentration { get; set; }
    public SavingThrowBehaviour? SavingThrowBehaviour { get; set; }
    public SavingThrowRoll? SavingThrowRoll { get; set; }
    public bool VerbalComponent { get; set; }
    public bool SomaticComponent { get; set; }
    public int Duration { get; set; }
    public UpcastBy UpcastBy { get; set; }
    public int? ClassForUpcasting { get; set; }
    public ImmaterialResourceBlueprintDto? ImmaterialResourceUsed { get; set; }
    public List<ItemCostRequirementDto> MaterialResourcesUsed { get; set; } = [];
    public List<EffectBlueprintDto> EffectBlueprints { get; set; } = [];

    public class EffectBlueprintDto {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int ResourceLevel { get; set; }
        public bool SavingThrowSuccess { get; set; }
    }

}

}