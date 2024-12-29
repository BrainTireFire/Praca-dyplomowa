using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.DTOs.Board;

namespace pracadyplomowa.Models.DTOs.Encounter;

public record EncounterShortDto
{
    [Required]
    public int Id { get; init; }
        
    [Required]
    [MaxLength(50)]
    public string Name { get; init; }
    
    [Required]
    public CampaignDto Campaign { get; init; }
    
    [Required]
    public BoardSummaryDto Board { get; init; }
}