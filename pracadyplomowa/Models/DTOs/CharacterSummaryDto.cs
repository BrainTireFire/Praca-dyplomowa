using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class CharacterSummaryDto(int id, bool isNpc, string name, string description, string characterClass, string race, int? campaignId, int experiencePoints)
    {
        [Required]
        public int Id { get; set; } = id;

        [Required]
        public bool IsNpc { get; set; } = isNpc;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = name;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = description;

        [Required]
        [MaxLength(50)]
        public string Class { get; set; } = characterClass;

        [Required]
        [MaxLength(50)]
        public string Race { get; set; } = race;

        public int? CampaignId { get; set; } = campaignId;
        [JsonPropertyName("xp")]
        public int ExperiencePoints { get; set; } = experiencePoints;
        public List<int> itemIds { get; set; } = new List<int>();

        public CharacterSummaryDto(Character character) :
            this(character.Id, character.IsNpc, character.Name, character.Description, character.R_CharacterBelongsToRace.Name, character.R_CharacterHasLevelsInClass.First().R_Class.Name, character.R_CampaignId, character.ExperiencePoints)
        {
            itemIds = character.R_CharacterHasBackpack.R_BackpackHasItems.Select(i => i.Id).ToList();
        }
    }
}
