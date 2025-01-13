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
    
    public bool IsActive { get; set; }
    
    [Required]
    public EncounterCampaignDto Campaign { get; init; }
    
    [Required]
    public BoardSummaryDto Board { get; init; }

    [Required] public ICollection<ParticipanceDataDto> Participances { get; init; }
}