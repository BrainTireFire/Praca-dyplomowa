using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;
using pracadyplomowa.Models.Entities.Powers.EffectBlueprints;

namespace pracadyplomowa.Models.DTOs
{
    public class ChoiceGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int NumberToChoose { get; set; }

        public List<Effect> Effects { get; set; } = new List<Effect>();
        public List<Power> PowersAlwaysAvailable { get; set; } = new List<Power>();
        public List<Power> PowersToPrepare { get; set; } = new List<Power>();
        public List<Resource> Resources { get; set; } = new List<Resource>();

        public class Effect
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public NotAllowedReason NotAllowed {get; set;}

            public enum NotAllowedReason {
                None = 0,
                ExpertiseWithoutProficiency = 1,
            }

            public Effect(int id, string name, string description, NotAllowedReason notAllowed){
                Id = id;
                Name = name;
                Description = description;
                NotAllowed = notAllowed;
            }
        }
        public class Power
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
        }
        public class Resource
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public int Amount { get; set; }
            public int Level { get; set; }
        }

        public static List<ChoiceGroupDto> Get(Character character)
        {
            return character.R_CharacterBelongsToRace.R_RaceLevels.SelectMany(raceLevel => raceLevel.R_ChoiceGroups)
                .Union(character.R_CharacterHasLevelsInClass.SelectMany(raceLevel => raceLevel.R_ChoiceGroups))
                .Where(choiceGroup => !character.R_UsedChoiceGroups
                    .Select(used => used.R_ChoiceGroupId)
                    .Contains(choiceGroup.Id)
                    )
                    .Select(cg => new ChoiceGroupDto(cg, character)).ToList();
        }

        public ChoiceGroupDto(ChoiceGroup cg, Character ch){
            Id = cg.Id;
            Name = cg.Name;
            NumberToChoose = cg.NumberToChoose;
            Effects = cg.R_Effects.Select(e => new Effect(
                e.Id,
                e.Name,
                e.Description,
                e is SkillEffectBlueprint eskill && eskill.SkillEffectType.SkillEffect == Enums.EffectOptions.SkillEffect.UpgradeToExpertise && !ch.SkillProficiencyNative(eskill.SkillEffectType.SkillEffect_Skill) ? Effect.NotAllowedReason.ExpertiseWithoutProficiency : Effect.NotAllowedReason.None
            )).ToList();
            PowersAlwaysAvailable = cg.R_PowersAlwaysAvailable.Select(e => new Power()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            }).ToList();
            PowersToPrepare = cg.R_PowersToPrepare.Select(e => new Power()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            }).ToList();
            Resources = cg.R_Resources.Select(e => new Resource()
            {
                Id = e.Id,
                Name = e.R_Blueprint.Name,
                Level = e.Level,
                Amount = e.Count,
            }).ToList();
        }
    }
}