using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs.Encounter;

public record ParticipanceCharacterSummaryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public bool IsNpc { get; set; }
        
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string Class { get; set; }

    [Required]
    [MaxLength(50)]
    public string Race { get; set; }

    public SizeItem Size { get; set; }
}

public class SizeItem {
    public int Order { get; set; }
    public Size Name { get; set; }
}