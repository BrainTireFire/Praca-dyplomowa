using System.ComponentModel.DataAnnotations;

namespace pracadyplomowa.Models.DTOs.Encounter;

public record CreateEncounterDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; init; }
    
    [Required]
    public int BoardId { get; init; }
    
    [Required]
    public int CampaignId { get; init; }
    
    [Required]
    public ICollection<int> CharactersIds { get; init; }
}