using System.ComponentModel.DataAnnotations;
using pracadyplomowa.Models.Enums;

namespace pracadyplomowa.Models.DTOs.Map.Field;

public class FieldUpdateDto
{
    [Required (ErrorMessage = "Id is required")]
    public int Id { get; set; }
    
    public int? PositionX { get; set; }
    
    public int? PositionY { get; set; }
    
    public int? PositionZ { get; set; }
    
    public string? Color { get; set; } = null!;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    //[EnumDataType(typeof(FieldCoverType), ErrorMessage = "Invalid cover level specified.")]
    public string? FieldCoverLevel { get; set; }
    
    //[EnumDataType(typeof(FieldMovementCostType), ErrorMessage = "Invalid movement cost specified.")]
    public string? FieldMovementCost { get; set; }

    [Required]
    public ICollection<PowerCompactDto> Powers { get; set; } = null!;
}