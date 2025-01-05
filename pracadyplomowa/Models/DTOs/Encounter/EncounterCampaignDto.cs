using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Encounter;

public class EncounterCampaignDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    public string Description { get; set; } = null!;
    public List<ParticipanceCharacterSummaryDto> Members { get; set; } = null!;
}