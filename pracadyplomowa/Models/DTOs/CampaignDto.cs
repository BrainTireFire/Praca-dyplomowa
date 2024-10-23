using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Entities.Campaign;
using pracadyplomowa.Models.Entities.Characters;

namespace pracadyplomowa.Models.DTOs
{
    public class CampaignDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; } = null!;
        public List<CharacterSummaryDto> Members { get; set; } = null!;
        public CampaignDto(Campaign campaign)
        {
            Id = campaign.Id;
            Name = campaign.Name;
            Description = campaign.Description;

            var characterSummaries = campaign.R_CampaignHasCharacters.Select(c => new CharacterSummaryDto(c)).ToList();
            Members = new List<CharacterSummaryDto>(characterSummaries);
        }
    }
}