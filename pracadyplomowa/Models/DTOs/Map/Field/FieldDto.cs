using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.DTOs.Encounter;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs.Map.Field;

public class FieldDto
{
    public int Id { get; set; }
    
    [Required (ErrorMessage = "PositionX is required")]
    public int PositionX { get; set; }
    
    [Required (ErrorMessage = "PositionY is required")]
    public int PositionY { get; set; }
    
    [Required (ErrorMessage = "PositionZ is required")]
    public int PositionZ { get; set; }
    
    [Required (ErrorMessage = "Color is required")]
    public string Color { get; set; } = null!;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Required (ErrorMessage = "FieldCoverLevel is required")]
    public string FieldCoverLevel { get; set; }
    
    [Required (ErrorMessage = "FieldMovementCost is required")]
    public string FieldMovementCost { get; set; }
    
    [Required]
    public ICollection<PowerCompactDto> Powers { get; set; } = [];
}