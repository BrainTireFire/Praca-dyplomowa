using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Items;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs
{
    public class PowerForEncounterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ResourceName { get; set; }
        // public int? MinimumResourceLevel { get; set; }
        public List<ImmaterialResourceSelection> AvailableLevels { get; set; } = [];
        public ActionType? ActionTypeRequired { get; set; }
        public bool RequiredResourceAvailable { get; set;}
        public List<MaterialComponentDto> MaterialComponents { get; set; } = null!;
        public bool RequiredMaterialComponentsAvailable { get; set; }
        public bool SomaticComponentRequirementSatisfied { get; set; }
        public bool VocalComponentRequirementSatisfied { get; set; }
        public int? Range { get; set;}
        public int? MaxTargets {get; set;}
        public AreaShape? AreaShape { get; set; }
        public int? AreaSize { get; set; }
        public CastableBy CastableBy { get; set; }
        public PowerType PowerType { get; set; }
        public TargetType TargetType { get; set; }


        public class MaterialComponentDto {
            public int Id {get; set;}
            public string Name {get; set;} = null!;
            public CoinSack Cost {get; set;} = null!;
        }

        public class ImmaterialResourceSelection {
            public int PowerLevel { get; set; }
            public int ResourceLevel { get; set; }
        }
    }

}