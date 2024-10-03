using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class ChoiceGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int NumberToChoose { get; set; }

        public List<Effect> Effects { get; set; } = new List<Effect>();
        public List<Power> Powers { get; set; } = new List<Power>();

        public class Effect
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
        public class Power
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
        }

        public static List<ChoiceGroupDto> Get(Character character)
        {
            return character.R_CharacterBelongsToRace.R_RaceLevels.SelectMany(raceLevel => raceLevel.R_ChoiceGroups)
                .Union(character.R_CharacterHasLevelsInClass.SelectMany(raceLevel => raceLevel.R_ChoiceGroups))
                .Where(choiceGroup => !character.R_UsedChoiceGroups
                    .Select(used => used.R_ChoiceGroupId)
                    .Contains(choiceGroup.Id)
                    )
                    .Select(cg => new ChoiceGroupDto()
                    {
                        Id = cg.Id,
                        Name = cg.Name,
                        NumberToChoose = cg.NumberToChoose,
                        Effects = cg.R_Effects.Select(e => new Effect()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Description = e.Description,
                        }).ToList(),
                        Powers = cg.R_Powers.Select(e => new Power()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Description = e.Description,
                        }).ToList()
                    }).ToList();
        }
    }
}