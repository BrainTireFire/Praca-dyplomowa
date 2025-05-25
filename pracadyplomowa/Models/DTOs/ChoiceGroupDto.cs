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
            // Map of granted power ids per "role" (known or to prepare) and source race/class
            var usedChoiceGroups = character.R_UsedChoiceGroups
                .Where(ucg => ucg.R_ChoiceGroup != null)
                .Select(ucg => new
                {
                    SourceRaceId = ucg.R_ChoiceGroup.R_GrantedByRaceLevel?.R_RaceId,
                    SourceClassId = ucg.R_ChoiceGroup.R_GrantedByClassLevel?.R_ClassId,
                    PowersAlwaysAvailableIds = ucg.R_PowersAlwaysAvailableGranted.Select(p => p.Id).ToHashSet(),
                    PowersToPrepareIds = ucg.R_PowersToPrepareGranted.Select(p => p.Id).ToHashSet()
                }).ToList();

            // Get all unused choice groups
            var allChoiceGroups = character.R_CharacterBelongsToRace.R_RaceLevels
                .SelectMany(rl => rl.R_ChoiceGroups)
                .Concat(character.R_CharacterHasLevelsInClass.SelectMany(cl => cl.R_ChoiceGroups))
                .Where(cg => !character.R_UsedChoiceGroups.Any(used => used.R_ChoiceGroupId == cg.Id))
                .ToList();

            var result = new List<ChoiceGroupDto>();

            foreach (var cg in allChoiceGroups)
            {
                // Get the race or class the choice group originates from
                var originRaceId = cg.R_GrantedByRaceLevel?.R_RaceId;
                var originClassId = cg.R_GrantedByClassLevel?.R_ClassId;

                // Block powers granted before from same class or same race (in the same "role")
                var alreadyGrantedAsAlwaysAvailable = usedChoiceGroups
                    .Where(ucg =>
                        (originRaceId != null && originRaceId == ucg.SourceRaceId) ||
                        (originClassId != null && originClassId == ucg.SourceClassId))
                    .SelectMany(ucg => ucg.PowersAlwaysAvailableIds)
                    .ToHashSet();

                var alreadyGrantedToPrepare = usedChoiceGroups
                    .Where(ucg =>
                        (originRaceId != null && originRaceId == ucg.SourceRaceId) ||
                        (originClassId != null && originClassId == ucg.SourceClassId))
                    .SelectMany(ucg => ucg.PowersToPrepareIds)
                    .ToHashSet();

                cg.R_PowersAlwaysAvailable = cg.R_PowersAlwaysAvailable
                    .Where(p => !alreadyGrantedAsAlwaysAvailable.Contains(p.Id))
                    .ToList();

                cg.R_PowersToPrepare = cg.R_PowersToPrepare
                    .Where(p => !alreadyGrantedToPrepare.Contains(p.Id))
                    .ToList();

                result.Add(new ChoiceGroupDto(cg, character));
            }

            return result;
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